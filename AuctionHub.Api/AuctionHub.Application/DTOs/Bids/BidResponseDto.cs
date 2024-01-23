namespace AuctionHub.Application.DTOs.Bids
{
    public class BidResponseDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public int Amount { get; set; }
        public DateTime BidTime { get; set; }
        public string BiddingRoomId { get; set; }
    }
}
