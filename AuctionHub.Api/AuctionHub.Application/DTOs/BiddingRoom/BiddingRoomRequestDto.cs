namespace AuctionHub.Application.DTOs.BiddingRoom
{
    public class BiddingRoomRequestDto
    {
        public string Id { get; set; }
        public string WinningBidId { get; set; }
        public string RoomName { get; set; }
        public bool IsAuctionActive { get; set; }
        public DateTime? EndTime { get; set; }
        public string ItemName { get; set; }
    }
}
