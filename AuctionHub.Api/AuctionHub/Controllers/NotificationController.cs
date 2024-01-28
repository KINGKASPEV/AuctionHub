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
        public async Task<IActionResult> NotifyParticipantsAsync([FromBody] Bid bid)
        {
            var response = await _notificationService.NotifyParticipantsAsync(bid);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("notify-auction-conclusion")]
        public async Task<IActionResult> NotifyAuctionConclusionAsync([FromBody] BiddingRoom biddingRoom)
        {
            var response = await _notificationService.NotifyAuctionConclusionAsync(biddingRoom);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
