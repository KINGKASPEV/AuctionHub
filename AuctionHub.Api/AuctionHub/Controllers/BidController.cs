using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/bidding")]
    public class BidController : ControllerBase
    {
        private readonly IBiddingService _biddingService;

        public BidController(IBiddingService biddingService)
        {
            _biddingService = biddingService;
        }

        [HttpPost("submit-bid")]
        public async Task<IActionResult> SubmitBidAsync(string biddingRoomId, [FromBody] BidRequestDto bidRequestDto)
        {
            var response = await _biddingService.SubmitBidAsync(biddingRoomId, bidRequestDto);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("get-all-bids")]
        public async Task<IActionResult> GetAllBidsAsync()
        {
            var response = await _biddingService.GetAllBidsAsync();

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
