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
        private readonly IBiddingService _biddingService;

        public InvoiceService(IUnitOfWork unitOfWork, ILogger<InvoiceService> logger, IBiddingService biddingService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _biddingService = biddingService;
        }

        public async Task<ApiResponse<InvoiceResponseDto>> GenerateInvoiceAsync(string biddingRoomId, BiddingRoomRequestDto biddingRoomRequestDto)
        {
            try
            {
                var winningBidId = await _biddingService.GetWinningBidIdAsync(biddingRoomId);

                if (winningBidId == null)
                {
                    return ApiResponse<InvoiceResponseDto>.Failed(false, "No winning bid found.", 400, new List<string> { "No winning bid found." });
                }

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
