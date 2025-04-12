using ManagementSolution.Application.ConstantsRole;
using ManagementSolution.Application.Contracts.Identity;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities.Role;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ManagementSolution.Infrastructure.Services
{
    public class UserAccount(IOptions<JwtSettings> config, AppDbContext appDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null)
                return new GeneralResponse(false, "User registered already");
            var checkUser = await FindUserByEmail(user.Email!);
            if (checkUser != null) return new GeneralResponse(false, "User registered already");

            // Save User
            var applicationUser = await AddToDatabase(new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = user.FullName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });

            // Check, Create and Assign Role
            var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole() { Id = Guid.NewGuid().ToString(), Name = Constants.Admin });
                await AddToDatabase(new UserRole() { Id = Guid.NewGuid().ToString(), RoleId = createAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account Created.");
            }

            var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
            SystemRole response = new();
            if (checkUserRole is null)
            {
                response = await AddToDatabase(new SystemRole() { Id = Guid.NewGuid().ToString(), Name = Constants.User });
                await AddToDatabase(new UserRole() { Id = Guid.NewGuid().ToString(), RoleId = response.Id, UserId = applicationUser.Id });
            }
            else
            {
                await AddToDatabase(new UserRole() { Id = Guid.NewGuid().ToString(), RoleId = checkUserRole.Id, UserId = applicationUser.Id });
            }
            return new GeneralResponse(true, "Account Created.");
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user is null)
                return new LoginResponse(false, "User not found");
            var checkUser = await FindUserByEmail(user.Email!);
            if (checkUser is null)
                return new LoginResponse(false, "User not found");
            /// Verify Password
            if (!BCrypt.Net.BCrypt.Verify(user.Password, checkUser.Password!))
                return new LoginResponse(false, "Email/Password is incorrect");
            var getUserRole = await appDbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == checkUser.Id);
            if (getUserRole is null)
                return new LoginResponse(false, "User Role not found");
            var getRoleName = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Id == getUserRole.RoleId);
            if (getRoleName is null)
                return new LoginResponse(false, "Role not found.");

            string jwtToken = GenerateToken(checkUser, getRoleName.Name!);
            string refreshToken = GenerateRefreshToken();
            return new LoginResponse(true, "Login Successfully.", jwtToken, refreshToken);
        }

        private string GenerateToken(ApplicationUser user, string roleName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var userClaims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, roleName!)
            };
            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await appDbContext.AppUsers.FirstOrDefaultAsync(x => x.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}
