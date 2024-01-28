using AuctionHub.Application.DTOs.Invoice;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process-payment")]
        public async Task<IActionResult> ProcessPaymentAsync([FromBody] InvoiceRequestDto InvoiceRequestDto)
        {
            var response = await _paymentService.ProcessPaymentAsync(InvoiceRequestDto);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
