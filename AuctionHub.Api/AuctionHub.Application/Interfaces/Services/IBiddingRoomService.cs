using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Domain;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IBiddingRoomService
    {
        Task<ApiResponse<string>> StartAuctionAsync(BiddingRoomRequestDto BiddingRoomRequestDto);
        Task<ApiResponse<BiddingRoomResponseDto>> CreateBiddingRoomAsync(BiddingRoomRequestDto biddingRoomRequestDto);
        Task<ApiResponse<List<BiddingRoomResponseDto>>> GetAllBiddingRoomsAsync();
        Task<ApiResponse<bool>> DeleteBiddingRoomByIdAsync(string biddingRoomId);
    }
}
