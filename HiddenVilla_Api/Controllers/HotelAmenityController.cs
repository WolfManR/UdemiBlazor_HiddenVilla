using Business.Repository.IRepository;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAmenityController : ControllerBase
    {
        private readonly IHotelAmenityRepository hotelAmenityRepository;

        public HotelAmenityController(IHotelAmenityRepository hotelAmenityRepository)
        {
            this.hotelAmenityRepository = hotelAmenityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allAmenities = await hotelAmenityRepository.Get().ConfigureAwait(false);
            return Ok(allAmenities);
        }
    }
}
