using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace AuctionHub.Application.ServiceImplementations
{
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BiddingService> _logger;

        public BiddingService(IUnitOfWork unitOfWork, ILogger<BiddingService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<BidResponseDto>> SubmitBidAsync(BidRequestDto bidRequestDto)
        {
            try
            {
                // Convert BidRequestDto to Bid entity
                var bid = new Bid
                {
                    // Map properties accordingly
                    Amount = bidRequestDto.Amount,
                    BiddingRoomId = bidRequestDto.BiddingRoomId,
                    CreatedBy = bidRequestDto.CreatedBy,
                    // Add other properties as needed
                };

                // Business logic to submit a bid
                var biddingRoom = await _unitOfWork.BiddingRooms.GetBiddingRoomWithWinningBidAsync(bidRequestDto.BiddingRoomId);

                if (biddingRoom.IsAuctionActive && biddingRoom.EndTime > DateTime.UtcNow)
                {
                    biddingRoom.Bids.Add(bid);

                    // Determine the winning bid logic
                    var winningBid = biddingRoom.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

                    if (winningBid != null)
                    {
                        biddingRoom.WinningBidId = winningBid.Id;

                        await _unitOfWork.Bids.CreateBidAsync(bid);
                        await _unitOfWork.BiddingRooms.UpdateBiddingRoomAsync(biddingRoom);
                        _unitOfWork.SaveChanges();

                        // Populate BidResponseDto with relevant data
                        var bidResponseDto = new BidResponseDto
                        {
                            Amount = winningBid.Amount,
                            BidTime = winningBid.BidTime,
                            BiddingRoomId = winningBid.BiddingRoomId,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = winningBid.CreatedBy
                        };

                        return ApiResponse<BidResponseDto>.Success(bidResponseDto, "Bid submitted successfully.", 200);
                    }
                    else
                    {
                        // Handle scenario where there are no bids yet
                        return ApiResponse<BidResponseDto>.Failed(false, "No bids submitted yet.", 400, new List<string> { "No bids submitted yet." });
                    }
                }
                else
                {
                    // Handle bid submission outside of active auction time
                    return ApiResponse<BidResponseDto>.Failed(false, "Auction is not active or has ended.", 400, new List<string> { "Auction is not active or has ended." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting a bid.");
                return ApiResponse<BidResponseDto>.Failed(false, "Error occurred while submitting a bid.", 500, new List<string> { ex.Message });
            }
        }
    }
}
