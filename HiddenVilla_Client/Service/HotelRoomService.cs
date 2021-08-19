using HiddenVilla_Client.Service.IService;

using Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly HttpClient client;

        public HotelRoomService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
            var response = await client.GetAsync($"api/hotelroom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonSerializer.Deserialize<IEnumerable<HotelRoomDTO>>(content);
            return rooms;
        }

        public async Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate)
        {
            return default;
        }
    }
}
