using Microsoft.AspNetCore.Identity;

namespace AuctionHub.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = new DateTime();
        public DateTime? VerifiedAt { get; set; }
    }
}
