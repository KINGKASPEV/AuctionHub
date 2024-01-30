using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Utilities;
using AuctionHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure.Repositories
{
    public class BidRepository : GenericRepository<Bid>, IBidRepository
    {
        public BidRepository(AuctionHubDbContext dbContext) : base(dbContext) { }

        public async Task<List<Bid>> GetBidsForRoomAsync(string biddingRoomId)
        {
            return await _dbContext.Bids
                .Where(bid => bid.BiddingRoomId == biddingRoomId)
                .OrderByDescending(bid => bid.BidTime)
                .ToListAsync();
        }

        public async Task CreateBidAsync(Bid bid)
        {
            await AddAsync(bid);
            await SaveChangesAsync();
        }

        public async Task UpdateBidAsync(Bid bid)
        {
            Update(bid);
            await SaveChangesAsync();
        }

        public async Task DeleteBidAsync(string bidId)
        {
            var bid = await GetByIdAsync(bidId);
            if (bid != null)
            {
                Delete(bid);
                await SaveChangesAsync();
            }
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _dbContext.Bids.ToListAsync();
        }
    }
}
