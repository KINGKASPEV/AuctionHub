namespace AuctionHub.Application.DTOs.Paystack
{
    public class PaystackTransactionResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public PaystackTransactionData Data { get; set; }
    }
}
