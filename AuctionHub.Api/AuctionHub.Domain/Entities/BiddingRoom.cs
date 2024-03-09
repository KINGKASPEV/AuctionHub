namespace AuctionHub.Domain.Entities
{
    public class BiddingRoom : BaseEntity
    {
        public List<Bid> Bids { get; set; } = new List<Bid>();
        public string WinningBidId { get; set; }
        public Bid WinningBid { get; set; }
        public string RoomName { get; set; }
        public bool IsAuctionActive { get; set; } = true;
        public DateTime? EndTime { get; set; }
        public string ItemName { get; set; }

    }
}
