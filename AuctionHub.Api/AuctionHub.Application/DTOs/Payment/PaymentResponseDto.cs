using AuctionHub.Domain.Enums;

namespace AuctionHub.Application.DTOs.Payment
{
    public class PaymentResponseDto
    {
        public string PaymentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string InvoiceId { get; set; }
        public string PaymentUrl { get; set; }
        public int PaymentAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
