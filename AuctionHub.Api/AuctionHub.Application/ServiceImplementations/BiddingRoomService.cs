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

        public async Task<ApiResponse<string>> CreateBiddingRoomAsync(BiddingRoomRequestDto biddingRoomRequestDto)
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

                await _unitOfWork.BiddingRooms.AddAsync(biddingRoom);
                _unitOfWork.SaveChanges();

                return ApiResponse<string>.Success("Bidding room created successfully.", "Bidding room created", 201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a bidding room.");
                return ApiResponse<string>.Failed(false, "Error occurred while creating a bidding room.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<List<BiddingRoomResponseDto>>> GetAllBiddingRoomsAsync()
        {
            try
            {
                var biddingRooms = await _unitOfWork.BiddingRooms.GetAllAsync();

                // Map BiddingRoom entities to BiddingRoomResponseDto
                var responseDtoList = biddingRooms.Select(br => new BiddingRoomResponseDto
                {
                    BiddingRoomId = br.Id,
                    RoomName = br.RoomName,
                    IsAuctionActive = br.IsAuctionActive,
                    EndTime = br.EndTime,
                    ItemName = br.ItemName
                }).ToList();

                return ApiResponse<List<BiddingRoomResponseDto>>.Success(responseDtoList, "Bidding rooms retrieved successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all bidding rooms.");
                return ApiResponse<List<BiddingRoomResponseDto>>.Failed(false, "Error occurred while getting all bidding rooms.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<bool>> DeleteBiddingRoomByIdAsync(string biddingRoomId)
        {
            try
            {
                var biddingRoom = await _unitOfWork.BiddingRooms.GetByIdAsync(biddingRoomId);

                if (biddingRoom == null)
                {
                    return ApiResponse<bool>.Failed(false, "Bidding room not found.", 404, new List<string> { "Bidding room not found." });
                }

                _unitOfWork.BiddingRooms.Delete(biddingRoom);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "Bidding room deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a bidding room.");
                return ApiResponse<bool>.Failed(false, "Error occurred while deleting a bidding room.", 500, new List<string> { ex.Message });
            }
        }
    }
}

