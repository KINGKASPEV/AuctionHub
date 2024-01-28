using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.DTOs.Invoice;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace AuctionHub.Application.ServiceImplementations
{

    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<InvoiceResponseDto>> GenerateInvoiceAsync(string biddingRoomId, string winningBidId, BiddingRoomRequestDto biddingRoomRequestDto)
        {
            try
            {
                var invoice = new Invoice
                {
                    BiddingRoomId = biddingRoomId,
                    WinningBidId = winningBidId
                };

                await _unitOfWork.Invoices.CreateInvoiceAsync(invoice);
                _unitOfWork.SaveChanges();

                var invoiceResponseDto = new InvoiceResponseDto
                {
                    InvoiceId = invoice.Id,
                    BiddingRoomId = invoice.BiddingRoomId,
                    WinningBidId = invoice.WinningBidId,
                    CreatedAt = invoice.CreatedAt
                };

                return ApiResponse<InvoiceResponseDto>.Success(invoiceResponseDto, "Invoice generated successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating an invoice.");
                return ApiResponse<InvoiceResponseDto>.Failed(false, "Error occurred while generating an invoice.", 500, new List<string> { ex.Message });
            }
        }
    }
}
