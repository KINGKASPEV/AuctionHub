namespace AuctionHub.Application.DTOs.AppUser
{
    public class AppUserResponseDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
