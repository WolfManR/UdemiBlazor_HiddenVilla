using Business.Repository.IRepository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allRooms = await hotelRoomRepository.Get().ConfigureAwait(false);
            return Ok(allRooms);
        }
    }
}
