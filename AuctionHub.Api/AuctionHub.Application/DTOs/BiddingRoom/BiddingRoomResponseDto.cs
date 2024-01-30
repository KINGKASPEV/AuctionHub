using AuctionHub.Application.DTOs.Bids;

namespace AuctionHub.Application.DTOs.BiddingRoom
{
    public class BiddingRoomResponseDto
    {
        public string BiddingRoomId { get; set; }
        public string RoomName { get; set; }
        public bool IsAuctionActive { get; set; }
        public DateTime? EndTime { get; set; }
        public string ItemName { get; set; }
    }
}
