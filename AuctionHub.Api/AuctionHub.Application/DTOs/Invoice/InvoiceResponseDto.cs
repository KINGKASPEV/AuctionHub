using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.DTOs.Invoice
{
    public class InvoiceResponseDto
    {
        public string InvoiceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string BiddingRoomId { get; set; }
        //public List<Payment> Payments { get; set; } = new List<Payment>();
        public string WinningBidId { get; set; }
    }
}
