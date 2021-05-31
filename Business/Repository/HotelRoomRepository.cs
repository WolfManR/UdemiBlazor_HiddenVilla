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

        #region Implementation of IHotelRoomRepository

        /// <inheritdoc />
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = _mapper.Map<HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoomDTO>(addedHotelRoom.Entity);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<HotelRoomDTO> Get(int roomId)
        {
            try
            {
                var hotelRoom = await _db.HotelRooms.Include(r => r.HotelRoomImages).FirstOrDefaultAsync(x=>x.Id == roomId);
                return _mapper.Map<HotelRoomDTO>(hotelRoom);
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<int> Delete(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails is null) return 0;

            var images = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            
            _db.HotelRoomImages.RemoveRange(images);

            _db.HotelRooms.Remove(roomDetails);
            return await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<HotelRoomDTO>> Get()
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _db.HotelRooms.Include(r => r.HotelRoomImages).ProjectTo<HotelRoomDTO>(_mapper.ConfigurationProvider);
                return hotelRoomDTOs;
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc />
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

        #endregion
    }
}