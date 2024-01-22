using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionHub.Infrastructure.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AuctionHubDbContext dbContext) : base(dbContext) { }

        public async Task<List<Invoice>> GetInvoicesForRoomAsync(string biddingRoomId)
        {
            return await _dbContext.Invoices
                .Where(invoice => invoice.BiddingRoomId == biddingRoomId)
                .ToListAsync();
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            await AddAsync(invoice);
            await SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            Update(invoice);
            await SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(string invoiceId)
        {
            var invoice = await GetByIdAsync(invoiceId);
            if (invoice != null)
            {
                Delete(invoice);
                await SaveChangesAsync();
            }
        }
    }
}
