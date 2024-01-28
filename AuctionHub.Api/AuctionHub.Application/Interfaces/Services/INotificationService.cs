using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using AuctionHub.Application.DTOs.Notifications;
using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.DTOs.BiddingRoom;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<ApiResponse<NotificationResponseDto>> NotifyParticipantsAsync(BidRequestDto BidRequestDto);
        Task<ApiResponse<NotificationResponseDto>> NotifyAuctionConclusionAsync(BiddingRoomRequestDto BiddingRoomRequestDto);
    }
}
