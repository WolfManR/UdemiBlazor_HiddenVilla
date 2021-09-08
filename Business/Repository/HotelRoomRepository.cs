using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = _mapper.Map<HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<HotelRoomDTO> Update(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId != hotelRoomDTO.Id) return null;


                var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                var room = _mapper.Map(hotelRoomDTO, roomDetails);
                room.UpdatedBy = "";
                room.UpdatedDate = DateTime.Now;
                var updatedRoom = _db.HotelRooms.Update(room);
                await _db.SaveChangesAsync();
                return _mapper.Map<HotelRoomDTO>(updatedRoom.Entity);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> Get(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                var hotelRoom = await _db.HotelRooms.Include(r => r.HotelRoomImages).FirstOrDefaultAsync(x=>x.Id == roomId);
                var dto = _mapper.Map<HotelRoomDTO>(hotelRoom);

                if(!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
				{
                    dto.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDateStr);
				}

                return dto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> Delete(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails is null) return 0;

            var images = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            
            _db.HotelRoomImages.RemoveRange(images);

            _db.HotelRooms.Remove(roomDetails);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomDTO>> Get(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _db.HotelRooms.Include(r => r.HotelRoomImages).ProjectTo<HotelRoomDTO>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
					foreach (var room in hotelRoomDTOs)
					{
                        room.IsBooked = await IsRoomBooked(room.Id, checkInDateStr, checkOutDateStr);
                    }
                }

                return hotelRoomDTOs;
            }
            catch
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    var hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => string.Equals(x.Name.ToLower(), name.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                    return _mapper.Map<HotelRoomDTO>(hotelRoom); 
                }
                else
                {
                    var hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => 
                        string.Equals(x.Name.ToLower(), name.ToLower(), StringComparison.CurrentCultureIgnoreCase) &&
                        x.Id != roomId);
                    return _mapper.Map<HotelRoomDTO>(hotelRoom);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr)
        {
			try
			{
                if(!string.IsNullOrEmpty(checkOutDateStr) && !string.IsNullOrEmpty(checkInDateStr))
				{
                    DateTime checkInDate = DateTime.ParseExact(checkInDateStr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateStr, "MM/dd/yyyy", null);

                    var exisingBooking = await _db.RoomOrderDetails
                        .Where(x =>
                            x.RoomId == roomId &&
                            x.IsPaymentSuccessful &&
                            (
                                // check if checkin date that user wants does not fall in between ant dates for room that is booked
                                (checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate) ||
                                // check if checkout date that user wants does not fall in between ant dates for room that is booked
                                (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                            ))
                        .FirstOrDefaultAsync();

                    return exisingBooking is not null;
                }

                return true;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}