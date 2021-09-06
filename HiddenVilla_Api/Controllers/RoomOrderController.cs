using Business.Repository.IRepository;

using Microsoft.AspNetCore.Mvc;

using Models;

using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderDetailsRepository repository;

        public RoomOrderController(IRoomOrderDetailsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Create(details);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Error while creatin Room Details/Booking"
                });
            }
        }
    }
}
