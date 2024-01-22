using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AuctionHubDbContext dbContext) : base(dbContext) { }

        public async Task<List<Payment>> GetPaymentsForInvoiceAsync(string invoiceId)
        {
            return await _dbContext.Payments
                .Where(payment => payment.InvoiceId == invoiceId)
                .ToListAsync();
        }

        public async Task CreatePaymentAsync(Payment payment)
        {
            await AddAsync(payment);
            await SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            Update(payment);
            await SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(string paymentId)
        {
            var payment = await GetByIdAsync(paymentId);
            if (payment != null)
            {
                Delete(payment);
                await SaveChangesAsync();
            }
        }
    }
}
