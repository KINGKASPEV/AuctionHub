using AuctionHub.Application.DTOs.Payment;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using AuctionHub.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace AuctionHub.Application.ServiceImplementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IUnitOfWork unitOfWork, ILogger<PaymentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<PaymentResponseDto>> ProcessPaymentAsync(Invoice invoice)
        {
            try
            {
                // Business logic to process payment for the highest bidder
                var payment = new Payment
                {
                    InvoiceId = invoice.Id,
                    PaymentAmount = invoice.WinningBid.Amount,
                    PaymentStatus = PaymentStatus.Pending // Adjust payment status based on your payment processing logic
                };

                await _unitOfWork.Payments.CreatePaymentAsync(payment);
                _unitOfWork.SaveChanges();

                // Populate PaymentResponseDto with relevant data
                var paymentResponseDto = new PaymentResponseDto
                {
                    PaymentId = payment.Id,
                    InvoiceId = payment.InvoiceId,
                    PaymentAmount = payment.PaymentAmount,
                    PaymentStatus = payment.PaymentStatus
                    // Add more properties if needed
                };

                return ApiResponse<PaymentResponseDto>.Success(paymentResponseDto, "Payment processed successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing payment.");
                return ApiResponse<PaymentResponseDto>.Failed(false, "Error occurred while processing payment.", 500, new List<string> { ex.Message });
            }
        }
    }
}
