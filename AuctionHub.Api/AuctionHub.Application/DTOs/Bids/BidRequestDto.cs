namespace AuctionHub.Application.DTOs.Bids
{
    public class BidRequestDto
    {
        public int Amount { get; set; }
        public string BiddingRoomId { get; set; }
        public string CreatedBy { get; set; }
    }
}
