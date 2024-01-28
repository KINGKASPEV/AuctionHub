namespace AuctionHub.Application.DTOs.Paystack
{
    public class PaystackInitializeRequest
    {
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
    }
}
