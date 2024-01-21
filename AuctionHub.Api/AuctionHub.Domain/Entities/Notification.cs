namespace AuctionHub.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public DateTime NotificationTime { get; set; } 
    }
}
