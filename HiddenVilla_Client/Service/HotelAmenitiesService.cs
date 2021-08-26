using HiddenVilla_Client.Service.IService;

using Models;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelAmenitiesService : IHotelAmenitiesService
    {
        private readonly HttpClient client;

        public HotelAmenitiesService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities()
        {
            var response = await client.GetAsync($"api/hotelamenity");
            var content = await response.Content.ReadAsStringAsync();
            var amenities = JsonSerializer.Deserialize<IEnumerable<HotelAmenityDTO>>(content);
            return amenities;
        }
    }
}
