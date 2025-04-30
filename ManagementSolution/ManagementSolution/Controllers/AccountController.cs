using ManagementSolution.Application.Contracts.Identity;
using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("register")]
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
        public async Task<ActionResult> SignInAsync(Login user)
        {
            var result = await userAccount.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshTokenAsync(RefreshToken token)
        {
            var result = await userAccount.RefreshTokenAsync(token);
            return Ok(result);
        }
    }
}
