using ManagementSolution.Application.Contracts.Identity;
using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Domain.Entities.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateAsync(Register user)
        {
            var result = await userAccount.CreateAsync(user);
            if (!result.Flag)
            {
                if (result.Message.Contains("already", StringComparison.OrdinalIgnoreCase))
                    return Conflict(result); // 409 Conflict

                return BadRequest(result); // 400 Bad Request
            }

            return Ok(result); // 200 OK
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> SignInAsync(LogIn user)
        {
            var result = await userAccount.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshTokenAsync(RefreshToken token)
        {
            var result = await userAccount.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await userAccount.GetUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await userAccount.GetRoles();
            return Ok(roles);
        }

        [HttpPut("update-user")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(ManageUser user)
        {
            var result = await userAccount.UpdateUser(user);
            return Ok(result);
        }

        [HttpDelete("delete-user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await userAccount.DeleteUser(id);
            return Ok(result);
        }

        [HttpGet("user-image/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserImage(string id)
        {
            var result = await userAccount.GetUserImage(id);
            return Ok(result);
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserProfile profile)
        {
            var result = await userAccount.UpdateProfile(profile);
            return Ok(result);
        }
    }
}
