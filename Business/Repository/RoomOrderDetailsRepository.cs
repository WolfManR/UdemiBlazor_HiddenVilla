using AutoMapper;

using Business.Repository.IRepository;

using Common;

using DataAccess.Data;

using Models;

using System;
using System.Collections.Generic;
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

		public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int id)
		{
			throw new NotImplementedException();
		}

		public Task<RoomOrderDetailsDTO> Get(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<RoomOrderDetailsDTO>> Get()
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate)
		{
			throw new NotImplementedException();
		}
	}
}
