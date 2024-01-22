using AuctionHub.Application.DTOs.Bid;
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

        public async Task<ApiResponse<string>> StartAuctionAsync(BiddingRoom biddingRoom)
        {
            try
            {
                // Business logic to start the auction
                if (!biddingRoom.IsAuctionActive)
                {
                    biddingRoom.IsAuctionActive = true;
                    biddingRoom.EndTime = DateTime.UtcNow.AddHours(1); // Adjust the end time based on your requirements

                    await _unitOfWork.BiddingRooms.UpdateBiddingRoomAsync(biddingRoom);
                    _unitOfWork.SaveChanges();

                    return ApiResponse<string>.Success("Auction started successfully.", "Auction started", 200);
                }
                else
                {
                    return ApiResponse<string>.Failed(false, "Auction is already active.", 400, new List<string> { "Auction is already active." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while starting the auction.");
                return ApiResponse<string>.Failed(false, "Error occurred while starting the auction.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<BidResponseDto>> SubmitBidAsync(Bid bid)
        {
            try
            {
                // Business logic to submit a bid
                var biddingRoom = await _unitOfWork.BiddingRooms.GetBiddingRoomWithWinningBidAsync(bid.BiddingRoomId);

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
                            BiddingRoomId = winningBid.BiddingRoomId
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


        //    public async Task<ApiResponse<BidResponseDto>> SubmitBidAsync(Bid bid)
        //    {
        //        try
        //        {
        //            // Business logic to submit a bid
        //            var biddingRoom = await _unitOfWork.BiddingRooms.GetBiddingRoomWithWinningBidAsync(bid.BiddingRoomId);

        //            if (biddingRoom.IsAuctionActive && biddingRoom.EndTime > DateTime.UtcNow)
        //            {
        //                biddingRoom.Bids.Add(bid);

        //                // Determine the winning bid logic
        //                var winningBid = biddingRoom.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

        //                if (winningBid != null)
        //                {
        //                    biddingRoom.WinningBidId = winningBid.Id;

        //                    await _unitOfWork.Bids.CreateBidAsync(bid);
        //                    await _unitOfWork.BiddingRooms.UpdateBiddingRoomAsync(biddingRoom);
        //                    _unitOfWork.SaveChanges();

        //                    return ApiResponse<BidResponseDto>.Success(true, "Bid submitted successfully.", 200);
        //                }
        //                else
        //                {
        //                    // Handle scenario where there are no bids yet
        //                    return ApiResponse<BidResponseDto>.Failed(false, "No bids submitted yet.", 400, new List<string> { "No bids submitted yet." });
        //                }
        //            }
        //            else
        //            {
        //                // Handle bid submission outside of active auction time
        //                return ApiResponse<BidResponseDto>.Failed(false, "Auction is not active or has ended.", 400, new List<string> { "Auction is not active or has ended." });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error occurred while submitting a bid.");
        //            return ApiResponse<BidResponseDto>.Failed(false, "Error occurred while submitting a bid.", 500, new List<string> { ex.Message });
        //        }
        //    }
    }
}
