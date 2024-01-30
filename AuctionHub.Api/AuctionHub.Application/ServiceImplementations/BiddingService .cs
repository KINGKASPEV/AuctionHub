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

        public async Task<ApiResponse<BidResponseDto>> SubmitBidAsync(string biddingRoomId, BidRequestDto bidRequestDto)
        {
            try
            {
                var bid = new Bid
                {
                    Amount = bidRequestDto.Amount,
                    BiddingRoomId = biddingRoomId,
                    CreatedBy = bidRequestDto.CreatedBy,
                };

                var biddingRoom = await _unitOfWork.BiddingRooms.GetBiddingRoomWithWinningBidAsync(biddingRoomId);

                if (biddingRoom == null)
                {
                    return ApiResponse<BidResponseDto>.Failed(false, "Bidding room not found.", 404, new List<string> { "Bidding room not found." });
                }

                if (biddingRoom.IsAuctionActive && biddingRoom.EndTime > DateTime.UtcNow)
                {
                    biddingRoom.Bids.Add(bid);

                    var winningBid = biddingRoom.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

                    if (winningBid != null)
                    {
                        biddingRoom.WinningBidId = winningBid.Id;

                        await _unitOfWork.Bids.CreateBidAsync(bid);
                        await _unitOfWork.BiddingRooms.UpdateBiddingRoomAsync(biddingRoom);
                        _unitOfWork.SaveChanges();

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
                        return ApiResponse<BidResponseDto>.Failed(false, "No bids submitted yet.", 400, new List<string> { "No bids submitted yet." });
                    }
                }
                else
                {
                    return ApiResponse<BidResponseDto>.Failed(false, "Auction is not active or has ended.", 400, new List<string> { "Auction is not active or has ended." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting a bid.");
                return ApiResponse<BidResponseDto>.Failed(false, "Error occurred while submitting a bid.", 500, new List<string> { ex.Message });
            }
        }
        public async Task<string> GetWinningBidIdAsync(string biddingRoomId)
        {
            var biddingRoom = await _unitOfWork.BiddingRooms.GetBiddingRoomWithWinningBidAsync(biddingRoomId);

            // Check if biddingRoom is not null and if it has a winning bid
            if (biddingRoom != null && !string.IsNullOrEmpty(biddingRoom.WinningBidId))
            {
                return biddingRoom.WinningBidId;
            }

            return null; // or throw an exception or handle it based on your requirements
        }
    }
}
