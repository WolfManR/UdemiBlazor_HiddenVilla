using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO);
        Task<HotelRoomDTO> Update(int roomId, HotelRoomDTO hotelRoomDTO);
        Task<HotelRoomDTO> Get(int roomId, string checkInDate = null, string checkOutDate = null);
        Task<int> Delete(int roomId);
        Task<IEnumerable<HotelRoomDTO>> Get(string checkInDate = null, string checkOutDate = null);
        Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);
		Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr);
	}
}