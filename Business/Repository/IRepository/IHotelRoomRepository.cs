using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO);
        Task<HotelRoomDTO> Update(int roomId, HotelRoomDTO hotelRoomDTO);
        Task<HotelRoomDTO> Get(int roomId);
        Task<int> Delete(int roomId);
        Task<IEnumerable<HotelRoomDTO>> Get();
        Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);
    }
}