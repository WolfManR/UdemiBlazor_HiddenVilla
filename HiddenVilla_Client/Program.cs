using Blazored.LocalStorage;

using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.IService;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl")) });
            builder.Services.AddScoped<IHotelRoomsService, HotelRoomsService>();
            builder.Services.AddScoped<IHotelAmenitiesService, HotelAmenitiesService>();

            builder.Services.AddBlazoredLocalStorage();
            await builder.Build().RunAsync();
            
        }
    }
}
