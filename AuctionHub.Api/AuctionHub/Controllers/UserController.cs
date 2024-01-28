using AuctionHub.Application.DTOs.AppUser;
using AuctionHub.Application.Interfaces.Services;
using AuctionHub.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHub.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AppUserRequestDto userRequest)
        {
            var response = await _userService.CreateUserAsync(userRequest);
            return BuildResponse(response);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] AppUserRequestDto userRequest)
        {
            var response = await _userService.UpdateUserAsync(userId, userRequest);
            return BuildResponse(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var response = await _userService.GetUserByIdAsync(userId);
            return BuildResponse(response);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var response = await _userService.DeleteUserAsync(userId);
            return BuildResponse(response);
        }

        private IActionResult BuildResponse<T>(ApiResponse<T> response)
        {
            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
