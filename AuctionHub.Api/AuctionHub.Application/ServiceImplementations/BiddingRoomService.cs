using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace AuctionHub.Application.ServiceImplementations
{
    public class BiddingRoomService : IBiddingRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BiddingRoomService> _logger;
        public BiddingRoomService(IUnitOfWork unitOfWork, ILogger<BiddingRoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> StartAuctionAsync(BiddingRoomRequestDto biddingRoomRequestDto)
        {
            try
            {
                var biddingRoom = new BiddingRoom
                {
                    RoomName = biddingRoomRequestDto.RoomName,
                    IsAuctionActive = biddingRoomRequestDto.IsAuctionActive,
                    EndTime = biddingRoomRequestDto.EndTime,
                    ItemName = biddingRoomRequestDto.ItemName
                };

                if (!biddingRoom.IsAuctionActive)
                {
                    biddingRoom.IsAuctionActive = true;
                    biddingRoom.EndTime = DateTime.UtcNow.AddHours(1);

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
    }
}
