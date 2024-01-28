using System.ComponentModel.DataAnnotations;

namespace AuctionHub.Application.DTOs.AppUser
{
    public class AppUserRequestDto
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
