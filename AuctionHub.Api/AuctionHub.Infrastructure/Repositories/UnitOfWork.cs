using AuctionHub.Application.Interfaces.Repositories;

namespace AuctionHub.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionHubDbContext _dbContext;

        public UnitOfWork(AuctionHubDbContext dbContext)
        {
            _dbContext = dbContext;
            BiddingRooms = new BiddingRoomRepository(_dbContext);
            Bids = new BidRepository(_dbContext);
            Invoices = new InvoiceRepository(_dbContext);
            Payments = new PaymentRepository(_dbContext);
            Notifications = new NotificationRepository(_dbContext);
            User = new UserRepository(_dbContext);
        }

        public IBiddingRoomRepository BiddingRooms { get; private set; }
        public IBidRepository Bids { get; private set; }
        public IInvoiceRepository Invoices { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserRepository User { get; private set; }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
