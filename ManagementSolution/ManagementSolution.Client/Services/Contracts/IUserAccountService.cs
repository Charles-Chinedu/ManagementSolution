using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities.Role;

namespace ManagementSolution.Client.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAsync(Register user);

        Task<LoginResponse> SignInAsync(LogIn user);

        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);

        Task<List<ManageUser>> GetUsers();

        Task<GeneralResponse> UpdateUser(ManageUser user);

        Task<List<SystemRole>> GetRoles();

        Task<GeneralResponse> DeleteUser(string id);
    }
}
