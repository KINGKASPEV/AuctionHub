using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.DTOs.Notifications;
using AuctionHub.Domain;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<ApiResponse<NotificationResponseDto>> NotifyParticipantsAsync(BidRequestDto BidRequestDto);
        Task<ApiResponse<NotificationResponseDto>> NotifyAuctionConclusionAsync(BiddingRoomRequest biddingRoomRequest);
    }
}
