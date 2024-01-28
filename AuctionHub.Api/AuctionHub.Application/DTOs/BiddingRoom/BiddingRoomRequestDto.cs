using AuctionHub.Application.DTOs.Bids;
using AuctionHub.Domain.Entities;

namespace AuctionHub.Application.DTOs.BiddingRoom
{
    public class BiddingRoomRequestDto
    {
        public string BiddingRoomId { get; set; }
        public string WinningBidId { get; set; }
        public BidRequestDto WinningBid { get; set; }
        public string RoomName { get; set; }
        public bool IsAuctionActive { get; set; }
        public DateTime? EndTime { get; set; }
        public string ItemName { get; set; }
    }
}
