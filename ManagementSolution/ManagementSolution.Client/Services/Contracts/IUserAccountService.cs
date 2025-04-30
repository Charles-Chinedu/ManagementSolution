using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Application.Responses;

namespace ManagementSolution.Client.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAsync(Register user);

        Task<LoginResponse> SignInAsync(Login user);

        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
    }
}
