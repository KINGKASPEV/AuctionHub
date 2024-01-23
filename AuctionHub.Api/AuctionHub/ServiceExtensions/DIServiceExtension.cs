using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Application.ServiceImplementations;
using AuctionHub.Infrastructure;
using AuctionHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.ServiceExtensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBiddingRoomRepository, BiddingRoomRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBiddingService, BiddingService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddDbContext<AuctionHubDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
    }
}
