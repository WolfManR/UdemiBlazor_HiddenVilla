using AutoMapper;

using Business.Repository.IRepository;

using Common;

using DataAccess.Data;

using Microsoft.EntityFrameworkCore;

using Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
	public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public RoomOrderDetailsRepository(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
		{
			try
			{
				details.CheckInDate = details.CheckInDate.Date;
				details.CheckOutDate = details.CheckOutDate.Date;
				var roomOrder = mapper.Map<RoomOrderDetails>(details);
				roomOrder.Status = SD.Status_Pending;
				var result = await context.RoomOrderDetails.AddAsync(roomOrder);
				await context.SaveChangesAsync();
				return mapper.Map<RoomOrderDetailsDTO>(result);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<RoomOrderDetailsDTO> Get(int id)
		{
			try
			{
				var data = await context.RoomOrderDetails
					.Include(u => u.HotelRoom).ThenInclude(r=>r.HotelRoomImages)
					.FirstOrDefaultAsync(o=>o.Id == id);
				if (data is null) return null;
				var roomOrder = mapper.Map<RoomOrderDetailsDTO>(data);
				roomOrder.HotelRoomDTO.TotalDays = roomOrder.CheckOutDate.Subtract(roomOrder.CheckInDate).Days;

				return roomOrder;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<RoomOrderDetailsDTO>> Get()
		{
            try
            {
				var data = await context.RoomOrderDetails.Include(u => u.HotelRoom).ToListAsync();
				var roomOrders = mapper.Map<IEnumerable<RoomOrderDetailsDTO>>(data);
				return roomOrders;
            }
            catch (Exception)
            {
				return null;
            }
		}

		public async Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int id)
		{
			var data = await context.RoomOrderDetails.FindAsync(id);
			if(data is null) return null;
			if (!data.IsPaymentSuccessful)
			{
				data.IsPaymentSuccessful = true;
				data.Status = SD.Status_Booked;
				var markPaymentSuccessfull = context.RoomOrderDetails.Update(data);
				await context.SaveChangesAsync();
				return mapper.Map<RoomOrderDetailsDTO>(markPaymentSuccessfull.Entity);
			}

			return new RoomOrderDetailsDTO();
		}

		public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate)
		{
            var exisingBooking = await context.RoomOrderDetails
                .Where(x =>
            x.RoomId == roomId &&
            x.IsPaymentSuccessful &&
            (
            // check if checkin date that user wants does not fall in between ant dates for room that is booked
            (checkInDate < x.CheckOutDate && checkInDate.Date > x.CheckInDate) ||
            // check if checkout date that user wants does not fall in between ant dates for room that is booked
            (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date < x.CheckInDate.Date)
            ))
                .FirstOrDefaultAsync();

            return exisingBooking is not null;
		}
	}
}
