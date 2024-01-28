using AuctionHub.Application.DTOs.AppUser;
using AuctionHub.Domain;

namespace AuctionHub.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApiResponse<AppUserResponseDto>> CreateUserAsync(AppUserRequestDto userRequest);
        Task<ApiResponse<AppUserResponseDto>> UpdateUserAsync(string userId, AppUserRequestDto userRequest);
        Task<ApiResponse<AppUserResponseDto>> GetUserByIdAsync(string userId);
        Task<ApiResponse<bool>> DeleteUserAsync(string userId);
    }
}
