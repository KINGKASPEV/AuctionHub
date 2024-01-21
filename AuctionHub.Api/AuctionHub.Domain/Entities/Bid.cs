namespace AuctionHub.Domain.Entities
{
    public class Bid : BaseEntity
    {
        public int Amount { get; set; }
        public DateTime BidTime { get; set; } 
        public string BiddingRoomId { get; set; }
        public BiddingRoom BiddingRoom { get; set; }
    }
}
