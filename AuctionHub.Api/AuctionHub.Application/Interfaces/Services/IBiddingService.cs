using AuctionHub.Domain.Entities;
using AuctionHub.Domain;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IBiddingService
    {
        Task<ApiResponse<string>> StartAuctionAsync(BiddingRoom biddingRoom);
        Task<ApiResponse<string>> SubmitBidAsync(Bid bid);
    }
}
