using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Application.ServiceImplementations;
using AuctionHub.Domain.Entities;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBiddingService, BiddingService>();
            services.AddScoped<IBiddingRoomService, BiddingRoomService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<PaystackService>();
            services.AddHttpClient();
            services.AddDbContext<AuctionHubDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.Configure<PaystackSettings>(config.GetSection("PaystackSettings"));
            services.AddSingleton<RabbitMQConfig>(config.GetSection("RabbitMQConfig").Get<RabbitMQConfig>());
        }
    }
}
