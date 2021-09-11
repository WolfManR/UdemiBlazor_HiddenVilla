using HiddenVilla_Client.Service.IService;

using Models;

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
	public class RoomOrderDetailsService : IRoomOrderDetailsService
	{
		private readonly HttpClient client;

		public RoomOrderDetailsService(HttpClient client)
		{
			this.client = client;
		}

		public async Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details)
		{
			var content = JsonSerializer.Serialize(details);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("api/roomorder/create", bodyContent);

			if (response.IsSuccessStatusCode)
			{
				var contentTemp = await response.Content.ReadAsStringAsync();
				var result = JsonSerializer.Deserialize<RoomOrderDetailsDTO>(contentTemp);
				return result;
			}
			else
			{
				var contentTemp = await response.Content.ReadAsStringAsync();
				var errorModel = JsonSerializer.Deserialize<ErrorModel>(contentTemp);
				throw new Exception(errorModel.ErrorMessage);
			}
		}

		public async Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
		{
			var content = JsonSerializer.Serialize(details);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("api/roomorder/paymentsuccessfull", bodyContent);

			if (response.IsSuccessStatusCode)
			{
				var contentTemp = await response.Content.ReadAsStringAsync();
				var result = JsonSerializer.Deserialize<RoomOrderDetailsDTO>(contentTemp);
				return result;
			}
			else
			{
				var contentTemp = await response.Content.ReadAsStringAsync();
				var errorModel = JsonSerializer.Deserialize<ErrorModel>(contentTemp);
				throw new Exception(errorModel.ErrorMessage);
			}
		}
	}
}
