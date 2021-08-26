using Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
	public interface IRoomOrderDetailsRepository
	{
		Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details);
		Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int id);
        Task<RoomOrderDetailsDTO> Get(int id);
        Task<IEnumerable<RoomOrderDetailsDTO>> Get();
        Task<bool> UpdateOrderStatus(int roomOrderId, string status);
        Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate);
	}
}
