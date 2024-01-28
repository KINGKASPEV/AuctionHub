using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.DTOs.Invoice
{
    public class InvoiceRequestDto
    {
        public string InvoiceId { get; set; }
        public string BuyerEmail { get; set; }
        public Bid WinningBid { get; set; }
    }
}
