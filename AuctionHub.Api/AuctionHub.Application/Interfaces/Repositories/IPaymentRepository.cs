using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.Interfaces.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<List<Payment>> GetPaymentsForInvoiceAsync(string invoiceId);
        Task CreatePaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(string paymentId);
    }
}
