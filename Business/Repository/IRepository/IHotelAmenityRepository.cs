using Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelAmenityRepository
    {
        Task<HotelAmenityDTO> Create(HotelAmenityDTO amenityDTO);
        Task<int> Delete(int amenityId);
        Task<HotelAmenityDTO> Get(int amenityId);
        IEnumerable<HotelAmenityDTO> Get();
        Task<HotelAmenityDTO> Update(int amenityId, HotelAmenityDTO amenityDTO);
    }
}