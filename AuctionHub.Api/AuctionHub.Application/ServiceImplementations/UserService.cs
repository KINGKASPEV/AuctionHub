using AuctionHub.Application.DTOs.AppUser;
using AuctionHub.Application.Interfaces.Repositories;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace AuctionHub.Application.ServiceImplementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<AppUserResponseDto>> CreateUserAsync(AppUserRequestDto userRequest)
        {
            try
            {
                var existingUser = await _unitOfWork.User.GetUserByEmailAsync(userRequest.Email);

                if (existingUser != null)
                {
                    return ApiResponse<AppUserResponseDto>.Failed(false, "User with this email already exists.", 400,
                        new List<string> { "User with this email already exists." });
                }

                var newUser = new AppUser
                {
                    Email = userRequest.Email,
                    CreatedAt = DateTime.UtcNow,
                };

                await _unitOfWork.User.AddAsync(newUser);
                  _unitOfWork.SaveChanges();

                var userResponseDto = new AppUserResponseDto
                {
                    UserId = newUser.Id,
                    Email = newUser.Email,
                    CreatedAt = newUser.CreatedAt
                };

                return ApiResponse<AppUserResponseDto>.Success(userResponseDto, "User created successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a user.");
                return ApiResponse<AppUserResponseDto>.Failed(false, "Error occurred while creating a user.", 500,
                    new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<AppUserResponseDto>> UpdateUserAsync(string userId, AppUserRequestDto userRequest)
        {
            try
            {
                var existingUser = await _unitOfWork.User.GetUserByIdAsync(userId);

                if (existingUser == null)
                {
                    return ApiResponse<AppUserResponseDto>.Failed(false, "User not found.", 404, new List<string> { "User not found." });
                }

                existingUser.Email = userRequest.Email;

                 _unitOfWork.User.Update(existingUser);
                _unitOfWork.SaveChanges();

                var responseDto = new AppUserResponseDto
                {
                    UserId = existingUser.Id,
                    Email = existingUser.Email,
                    CreatedAt = existingUser.CreatedAt
                };

                return ApiResponse<AppUserResponseDto>.Success(responseDto, "User updated successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a user.");
                return ApiResponse<AppUserResponseDto>.Failed(false, "Error occurred while updating a user.", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<AppUserResponseDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                var existingUser = await _unitOfWork.User.GetUserByIdAsync(userId);

                if (existingUser == null)
                {
                    return ApiResponse<AppUserResponseDto>.Failed(false, "User not found.", 404, new List<string> { "User not found." });
                }

                var responseDto = new AppUserResponseDto
                {
                    UserId = existingUser.Id,
                    Email = existingUser.Email,
                    CreatedAt = existingUser.CreatedAt
                };

                return ApiResponse<AppUserResponseDto>.Success(responseDto, "User retrieved successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a user by ID.");
                return ApiResponse<AppUserResponseDto>.Failed(false, "Error occurred while getting a user by ID.", 500, new List<string> { ex.Message });
            }
        }


        public async Task<ApiResponse<bool>> DeleteUserAsync(string userId)
        {
            try
            {
                var existingUser = await _unitOfWork.User.GetUserByIdAsync(userId);

                if (existingUser == null)
                {
                    return ApiResponse<bool>.Failed(false, "User not found.", 404, new List<string> { "User not found." });
                }

                _unitOfWork.User.Delete(existingUser);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "User deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a user.");
                return ApiResponse<bool>.Failed(false, "Error occurred while deleting a user.", 500, new List<string> { ex.Message });
            }
        }
    }
}
