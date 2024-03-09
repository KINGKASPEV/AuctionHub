using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.DTOs.Notifications;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("notify-participants")]
        public async Task<IActionResult> NotifyParticipantsAsync([FromBody] BidRequestDto BidRequestDto)
        {
            var response = await _notificationService.NotifyParticipantsAsync(BidRequestDto);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("notify-auction-conclusion")]
        public async Task<IActionResult> NotifyAuctionConclusionAsync([FromBody] BiddingRoomRequest BiddingRoomRequest)
        {
            var response = await _notificationService.NotifyAuctionConclusionAsync(BiddingRoomRequest);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
