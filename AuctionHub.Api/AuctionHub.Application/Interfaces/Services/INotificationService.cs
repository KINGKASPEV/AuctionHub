using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using AuctionHub.Application.DTOs.Notifications;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<ApiResponse<NotificationResponseDto>> NotifyParticipantsAsync(Bid bid);
        Task<ApiResponse<NotificationResponseDto>> NotifyAuctionConclusionAsync(BiddingRoom biddingRoom);
    }
}
