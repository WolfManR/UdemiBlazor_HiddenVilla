using HiddenVilla_Client.Service.IService;

using Models;

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly HttpClient client;

        public StripePaymentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<SuccessModel> CheckOut(StripePaymentDTO model)
        {
            var content = JsonSerializer.Serialize(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/stripepayment/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SuccessModel>(contentTemp);
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
