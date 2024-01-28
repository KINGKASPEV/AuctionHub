using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(AuctionHubDbContext dbContext) : base(dbContext) { }

        public async Task<AppUser> GetUserByIdAsync(string userId) => await _dbContext.Users.FindAsync(userId);
        public async Task<AppUser> GetUserByEmailAsync(string email) => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
