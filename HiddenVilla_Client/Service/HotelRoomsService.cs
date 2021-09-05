using HiddenVilla_Client.Service.IService;

using Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelRoomsService : IHotelRoomsService
    {
        private readonly HttpClient client;

        public HotelRoomsService(HttpClient client)
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
            var response = await client.GetAsync($"api/hotelroom{roomId}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var room = JsonSerializer.Deserialize<HotelRoomDTO>(content);
                return room;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonSerializer.Deserialize<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
