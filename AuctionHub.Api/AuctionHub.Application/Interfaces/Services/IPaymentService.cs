using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using AuctionHub.Application.DTOs.Payment;
using AuctionHub.Application.DTOs.Invoice;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<ApiResponse<PaymentResponseDto>> ProcessPaymentAsync(InvoiceRequestDto InvoiceRequestDto);
    }
}
