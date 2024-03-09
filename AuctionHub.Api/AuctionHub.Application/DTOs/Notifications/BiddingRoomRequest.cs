using AuctionHub.Application.DTOs.Bids;

namespace AuctionHub.Application.DTOs.Notifications
{
    public class BiddingRoomRequest
    {
         public BidRequestDto WinningBid { get; set; }
        public string RoomName { get; set; }
        public bool IsAuctionActive { get; set; }
        //public DateTime? EndTime { get; set; }
        public string ItemName { get; set; }
    }
}
