using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.Interfaces.Repositories
{
    public interface IBiddingRoomRepository : IGenericRepository<BiddingRoom>
    {
        Task CreateBiddingRoomAsync(BiddingRoom biddingRoom);
        Task UpdateBiddingRoomAsync(BiddingRoom biddingRoom);
        Task DeleteBiddingRoomAsync(string roomId);
        Task<List<BiddingRoom>> GetActiveAuctionsAsync();
        Task<BiddingRoom> GetBiddingRoomWithBidsAsync(string roomId);
        Task<BiddingRoom> GetBiddingRoomWithWinningBidAsync(string roomId);
    }
}
