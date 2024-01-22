using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure.Repositories
{
    public class BiddingRoomRepository : GenericRepository<BiddingRoom>, IBiddingRoomRepository
    {
        public BiddingRoomRepository(AuctionHubDbContext dbContext) : base(dbContext) { }

        public async Task CreateBiddingRoomAsync(BiddingRoom biddingRoom)
        {
            await AddAsync(biddingRoom);
            await SaveChangesAsync();
        }

        public async Task UpdateBiddingRoomAsync(BiddingRoom biddingRoom)
        {
            Update(biddingRoom);
            await SaveChangesAsync();
        }

        public async Task<List<BiddingRoom>> GetActiveAuctionsAsync()
        {
            return await _dbContext.BiddingRooms
                .Where(room => room.IsAuctionActive && room.EndTime > DateTime.UtcNow)
            .ToListAsync();
        }

        public async Task<BiddingRoom> GetBiddingRoomWithBidsAsync(string roomId)
        {
            return await _dbContext.BiddingRooms
                .Include(room => room.Bids)
                .FirstOrDefaultAsync(room => room.Id == roomId);
        }

        public async Task<BiddingRoom> GetBiddingRoomWithWinningBidAsync(string roomId)
        {
            return await _dbContext.BiddingRooms
                .Include(room => room.WinningBid)
                .FirstOrDefaultAsync(room => room.Id == roomId);
        }

        public async Task<List<BiddingRoom>> GetAllBiddingRoomsAsync()
        {
            return await _dbContext.BiddingRooms.ToListAsync();
        }

        public async Task DeleteBiddingRoomAsync(string roomId)
        {
            var biddingRoom = await GetByIdAsync(roomId);
            if (biddingRoom != null)
            {
                Delete(biddingRoom);
                await SaveChangesAsync();
            }
        }
    }
}
