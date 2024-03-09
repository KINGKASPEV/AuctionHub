using AuctionHub.Application.DTOs.Bids;

namespace AuctionHub.Application.DTOs.BiddingRoom
{
    public class BiddingRoomRequestDto
    {
       // public BidRequestDto WinningBid { get; set; }
        public string RoomName { get; set; }
        public bool IsAuctionActive { get; set; } = true;
        public DateTime? EndTime { get; set; }
        public string ItemName { get; set; }
    }
}
