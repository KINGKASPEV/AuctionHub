using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.Interfaces.Repositories
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<List<Invoice>> GetInvoicesForRoomAsync(string biddingRoomId);
        Task CreateInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(string invoiceId);
    }
}
