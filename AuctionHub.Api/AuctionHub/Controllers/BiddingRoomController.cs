using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/bidding-room")]
    public class BiddingRoomController : ControllerBase
    {
        private readonly IBiddingRoomService _biddingRoomService;

        public BiddingRoomController(IBiddingRoomService biddingRoomService)
        {
            _biddingRoomService = biddingRoomService;
        }

        [HttpPost("start-auction")]
        public async Task<IActionResult> StartAuctionAsync([FromBody] BiddingRoom biddingRoom)
        {
            var response = await _biddingRoomService.StartAuctionAsync(biddingRoom);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
