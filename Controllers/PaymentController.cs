using Car_Rental_System_New.Model;
using Car_Rental_System_New.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_System_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpPost("make")]
        public ActionResult<Payment> MakePayment(int reservationId, decimal amount)
        {
            var payment = _paymentService.MakePayment(reservationId, amount);
            return Ok(payment);
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpPost("confirm")]
        public ActionResult<Payment> ConfirmPayment(int paymentId)
        {
            try
            {
                var payment = _paymentService.ConfirmPayment(paymentId);
                return Ok(payment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpPost("refund")]
        public ActionResult<Payment> IssueRefund(int paymentId, decimal refundAmount)
        {
            try
            {
                var payment = _paymentService.IssueRefund(paymentId, refundAmount);
                return Ok(payment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
