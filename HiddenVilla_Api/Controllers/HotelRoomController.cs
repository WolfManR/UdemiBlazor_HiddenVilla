using Business.Repository.IRepository;

using Common;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Models;

using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomRepository hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            this.hotelRoomRepository = hotelRoomRepository;
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allRooms = await hotelRoomRepository.Get().ConfigureAwait(false);
            return Ok(allRooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get(int? roomId)
        {
            if (roomId is null)
            {
                return BadRequest(new ErrorModel { ErrorMessage = "Invalid Room Id", StatusCode = StatusCodes.Status400BadRequest });
            }

            var roomDetails = await hotelRoomRepository.Get(roomId.Value);

            if (roomDetails is null)
            {
                return BadRequest(new ErrorModel { ErrorMessage = "Invallid Room Id", StatusCode = StatusCodes.Status404NotFound });
            }

            return Ok(roomDetails);
        }
    }
}
