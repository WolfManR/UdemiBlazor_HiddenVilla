using Business.Repository.IRepository;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

using Models;

using Stripe.Checkout;

using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderDetailsRepository repository;
        private readonly IEmailSender emailSender;

        public RoomOrderController(IRoomOrderDetailsRepository repository, IEmailSender emailSender)
        {
            this.repository = repository;
            this.emailSender = emailSender;
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

        [HttpPost]
        public async Task<IActionResult> PaymentSuccessfull([FromBody] RoomOrderDetailsDTO details)
        {
            var service = new SessionService();
            var sessionDetails = await service.GetAsync(details.StripeSessionId);
            if(sessionDetails.PaymentStatus == "paid")
			{
                var result = await repository.MarkPaymentSuccessful(details.Id);
                if(result is null)
				{
                    return BadRequest(new ErrorModel
                    {
                        ErrorMessage = "Can not mark payment as successfull"
                    });
                }

                await emailSender.SendEmailAsync(
                    details.Email,
                    "Booking Confirmed - Hidden Villa",
                    "Your booking has been confirmed at Hidden Villa with order ID : " + details.Id);

                return Ok(result);
            }
            else
			{
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Can not mark payment as successfull"
                });
            }
        }
    }
}
