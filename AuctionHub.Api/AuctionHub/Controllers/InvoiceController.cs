using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("generate-invoice/{biddingRoomId}/{winningBidId}")]
        public async Task<IActionResult> GenerateInvoiceAsync(string biddingRoomId, string winningBidId, [FromBody] BiddingRoomRequestDto biddingRoomRequestDto)
        {
            var response = await _invoiceService.GenerateInvoiceAsync(biddingRoomId, winningBidId, biddingRoomRequestDto);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
