using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AuctionHubDbContext dbContext) : base(dbContext) { }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            return await _dbContext.Notifications
                .OrderByDescending(notification => notification.NotificationTime)
                .ToListAsync();
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            await AddAsync(notification);
            await SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            Update(notification);
            await SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(string notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification != null)
            {
                Delete(notification);
                await SaveChangesAsync();
            }
        }
    }

}
