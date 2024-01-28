using AuctionHub.Application.DTOs.Invoice;
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
        private readonly PaystackService _paystackService;

        public PaymentService(IUnitOfWork unitOfWork, ILogger<PaymentService> logger, PaystackService paystackService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _paystackService = paystackService;
        }

        public async Task<ApiResponse<PaymentResponseDto>> ProcessPaymentAsync(InvoiceRequestDto InvoiceRequestDto)
        {
            try
            {
                // Call Paystack to initialize the payment
                var initializeResponse = await _paystackService.InitializePaymentAsync(InvoiceRequestDto.WinningBid.Amount, InvoiceRequestDto.BuyerEmail);

                // Check if the payment initialization was successful
                if (!initializeResponse.Status)
                {
                    return ApiResponse<PaymentResponseDto>.Failed(false, initializeResponse.Message, 400, new List<string> { initializeResponse.Message });
                }

                // Save the payment information in your database
                var payment = new Payment
                {
                    InvoiceId = InvoiceRequestDto.InvoiceId,
                    PaymentAmount = InvoiceRequestDto.WinningBid.Amount,
                    PaymentStatus = PaymentStatus.Pending,
                    PaystackReference = initializeResponse.Data.Reference // Store this reference for verification
                };

                await _unitOfWork.Payments.CreatePaymentAsync(payment);
                _unitOfWork.SaveChanges();

                // Redirect the user to the Paystack payment page
                var paymentResponseDto = new PaymentResponseDto
                {
                    PaymentId = payment.Id,
                    InvoiceId = payment.InvoiceId,
                    PaymentAmount = payment.PaymentAmount,
                    PaymentStatus = payment.PaymentStatus,
                    PaymentUrl = initializeResponse.Data.AuthorizationUrl
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
