using AuctionHub.Domain;
using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IBiddingRoomService
    {
        Task<ApiResponse<string>> StartAuctionAsync(BiddingRoom biddingRoom);
    }
}
