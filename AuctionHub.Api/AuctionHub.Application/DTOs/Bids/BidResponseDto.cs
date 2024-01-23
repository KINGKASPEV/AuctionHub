namespace AuctionHub.Application.DTOs.Bids
{
    public class BidResponseDto
    {
        public int Amount { get; set; }
        public DateTime BidTime { get; set; }
        public string BiddingRoomId { get; set; }
    }
}
