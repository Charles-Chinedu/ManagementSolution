using ManagementSolution.Application.DTOs;
using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Application.Responses;
using ManagementSolution.Client.Helpers;
using ManagementSolution.Client.Services.Contracts;
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

        public async Task<LoginResponse> SignInAsync(Login user)
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
    }
}
