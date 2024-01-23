using AuctionHub.Domain.Entities;
using AuctionHub.Domain;
using AuctionHub.Application.DTOs.Invoice;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<ApiResponse<InvoiceResponseDto>> GenerateInvoiceAsync(BiddingRoom biddingRoom);
    }
}
