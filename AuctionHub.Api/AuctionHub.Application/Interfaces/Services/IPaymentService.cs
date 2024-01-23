using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using AuctionHub.Application.DTOs.Payment;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<ApiResponse<PaymentResponseDto>> ProcessPaymentAsync(Invoice invoice);
    }
}
