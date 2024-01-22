using AuctionHub.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.ServiceExtensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AuctionHubDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
    }
}
