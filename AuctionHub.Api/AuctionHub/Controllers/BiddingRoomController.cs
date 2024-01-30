using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.Interfaces.Services;
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
        public async Task<IActionResult> StartAuctionAsync([FromBody] BiddingRoomRequestDto BiddingRoomRequestDto)
        {
            var response = await _biddingRoomService.StartAuctionAsync(BiddingRoomRequestDto);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("create-bidding-room")]
        public async Task<IActionResult> CreateBiddingRoomAsync([FromBody] BiddingRoomRequestDto biddingRoomRequestDto)
        {
            var response = await _biddingRoomService.CreateBiddingRoomAsync(biddingRoomRequestDto);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpGet("get-all-bidding-rooms")]
        public async Task<IActionResult> GetAllBiddingRoomsAsync()
        {
            var response = await _biddingRoomService.GetAllBiddingRoomsAsync();

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("delete-bidding-room/{id}")]
        public async Task<IActionResult> DeleteBiddingRoomByIdAsync(string id)
        {
            var response = await _biddingRoomService.DeleteBiddingRoomByIdAsync(id);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
