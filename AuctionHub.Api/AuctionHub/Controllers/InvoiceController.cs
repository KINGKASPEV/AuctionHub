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
        private readonly IBiddingService _biddingService; 

        public InvoiceController(IInvoiceService invoiceService, IBiddingService biddingService)
        {
            _invoiceService = invoiceService;
            _biddingService = biddingService;
        }

        [HttpPost("generate-invoice/{biddingRoomId}")]
        public async Task<IActionResult> GenerateInvoiceAsync(string biddingRoomId, [FromBody] BiddingRoomRequestDto biddingRoomRequestDto)
        {
            try
            {
                var winningBidId = await _biddingService.GetWinningBidIdAsync(biddingRoomId);

                if (winningBidId == null)
                {
                    return BadRequest("No winning bid found for the specified bidding room.");
                }

                var response = await _invoiceService.GenerateInvoiceAsync(biddingRoomId, biddingRoomRequestDto);

                if (response.Succeeded)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-winning-bid/{biddingRoomId}")]
        public async Task<IActionResult> GetWinningBidIdAsync(string biddingRoomId)
        {
            try
            {
                var winningBidId = await _biddingService.GetWinningBidIdAsync(biddingRoomId);

                if (winningBidId == null)
                {
                    return NotFound("No winning bid found for the specified bidding room.");
                }

                return Ok(new { WinningBidId = winningBidId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
