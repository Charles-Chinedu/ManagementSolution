using ManagementSolution.Application.Contracts.Identity;
using ManagementSolution.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> CreateAsync(Register user)
        {
            var result = await userAccount.CreateAsync(user);
            return Ok(result);
        }

        
    }
}
