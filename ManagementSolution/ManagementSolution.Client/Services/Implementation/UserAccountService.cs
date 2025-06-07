using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Application.Responses;
using ManagementSolution.Client.Helpers;
using ManagementSolution.Client.Services.Contracts;
using ManagementSolution.Domain.Entities.Role;
using System.Net.Http.Json;

namespace ManagementSolution.Client.Services.Implementation
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        private readonly GetHttpClient _getHttpClient = getHttpClient;
        public const string AuthUrl = "api/account";

        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!result.IsSuccessStatusCode)
            {
                var error = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Error: {result.StatusCode} - {error}");
               
                return new GeneralResponse(false, "Error occurred.");
            }
            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<LoginResponse> SignInAsync(LogIn user)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if (!result.IsSuccessStatusCode)
            {
                var error = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Error: {result.StatusCode} - {error}");

                return new LoginResponse(false, "Error occurred.", null!, null!);
            }
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", token);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occurred.");
            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }

        public async Task<List<ManageUser>> GetUsers()
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<ManageUser>>($"{AuthUrl}/users");
            return result!;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PutAsJsonAsync($"{AuthUrl}/update-user", user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }

        public async Task<List<SystemRole>> GetRoles()
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<SystemRole>>($"{AuthUrl}/roles");
            return result!;
        }

        public async Task<GeneralResponse> DeleteUser(string id)
        {
            var httpClient = _getHttpClient.GetPublicHttpClient();
            var result = await httpClient.DeleteAsync($"{AuthUrl}/delete-user/{id}");
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");
            
            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }
    }
}
