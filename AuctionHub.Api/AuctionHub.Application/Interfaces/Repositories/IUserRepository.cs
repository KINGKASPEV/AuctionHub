using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByEmailAsync(string email);
    }
}
