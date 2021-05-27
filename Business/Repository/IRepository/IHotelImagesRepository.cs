using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelImagesRepository
    {
        Task<int> Create(HotelRoomImageDTO image);
        Task<int> Delete(int imageId);
        Task<int> DeleteByRoomId(int roomId);
        Task<IEnumerable<HotelRoomImageDTO>> GetRoomImages(int roomId);
    }
}