using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Application.Responses;

namespace ManagementSolution.Application.Contracts.Identity
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);

        Task<LoginResponse> SignInAsync(Login user);
    
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
    }

}
