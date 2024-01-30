using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Domain;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IBiddingService
    {
        Task<ApiResponse<BidResponseDto>> SubmitBidAsync(string biddingRoomId, BidRequestDto bidRequestDto);
        Task<string> GetWinningBidIdAsync(string biddingRoomId);
        Task<ApiResponse<IEnumerable<BidResponseDto>>> GetAllBidsAsync();
    }
}
