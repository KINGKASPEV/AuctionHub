using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.Interfaces.Repositories
{
    public interface IBidRepository : IGenericRepository<Bid>
    {
        Task<List<Bid>> GetBidsForRoomAsync(string biddingRoomId);
        Task CreateBidAsync(Bid bid);
        Task UpdateBidAsync(Bid bid);
        Task<List<Bid>> GetAllBidsAsync();
        Task DeleteBidAsync(string bidId);
    }
}
