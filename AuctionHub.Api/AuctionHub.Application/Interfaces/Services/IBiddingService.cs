using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using AuctionHub.Application.DTOs.Bids;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IBiddingService
    {
        Task<ApiResponse<BidResponseDto>> SubmitBidAsync(BidRequestDto bidRequestDto);
    }
}
