using AuctionHub.Domain.Enums;

namespace AuctionHub.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int PaymentAmount { get; set; }
        public string PaystackReference { get; set; }
        public PaymentStatus PaymentStatus { get; set; } 
    }
}
