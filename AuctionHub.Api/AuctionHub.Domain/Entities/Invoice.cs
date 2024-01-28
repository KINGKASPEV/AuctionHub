namespace AuctionHub.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string BiddingRoomId { get; set; }
        public string BuyerEmail { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>(); 
        public BiddingRoom BiddingRoom { get; set; }
        public string WinningBidId { get; set; }
        public Bid WinningBid { get; set; }
    }
}
