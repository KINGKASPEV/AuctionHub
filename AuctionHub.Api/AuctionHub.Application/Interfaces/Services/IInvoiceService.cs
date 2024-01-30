using AuctionHub.Application.DTOs.BiddingRoom;
using AuctionHub.Application.DTOs.Invoice;
using AuctionHub.Domain;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<ApiResponse<InvoiceResponseDto>> GenerateInvoiceAsync(string biddingRoomId, BiddingRoomRequestDto biddingRoomRequestDto);
    }
}
