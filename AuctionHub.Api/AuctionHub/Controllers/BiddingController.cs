using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/bidding")]
    public class BiddingController : ControllerBase
    {
        private readonly IBiddingService _biddingService;

        public BiddingController(IBiddingService biddingService)
        {
            _biddingService = biddingService;
        }

        [HttpPost("start-auction")]
        public async Task<IActionResult> StartAuctionAsync([FromBody] BiddingRoom biddingRoom)
        {
            var response = await _biddingService.StartAuctionAsync(biddingRoom);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("submit-bid")]
        public async Task<IActionResult> SubmitBidAsync([FromBody] Bid bid)
        {
            var response = await _biddingService.SubmitBidAsync(bid);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
