using ManagementSolution.Application.DTOs;
using ManagementSolution.Client.Services.Contracts;
using System.Net;
using System.Net.Http.Headers;

namespace ManagementSolution.Client.Helpers
{
    public class CustomHttpHandler(GetHttpClient getHttpClient, LocalStorageService localStorageService, IUserAccountService accountService) : DelegatingHandler
    {
        private readonly GetHttpClient _getHttpClient = getHttpClient;
        private readonly LocalStorageService _localStorageService = localStorageService;
        private readonly IUserAccountService _accountService = accountService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri.AbsoluteUri.Contains("refresh-token");

            if (loginUrl || registerUrl || refreshTokenUrl) return await base.SendAsync(request, cancellationToken);

            var result = await base.SendAsync(request, cancellationToken);
            if(result.StatusCode == HttpStatusCode.Unauthorized)
            {
                /// Get token from localStorage
                var stringToken = await _localStorageService.GetToken();
                if (stringToken == null) return result;

                /// Check if the header contains token
                string token = string.Empty;
                try { token = request.Headers.Authorization!.Parameter!; } 
                catch { }

                var deserializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if (deserializedToken == null) return result;

                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", deserializedToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }

                var newJwtToken = await GetRefreshToken(deserializedToken.RefreshToken!);
                if (string.IsNullOrEmpty(newJwtToken)) return result;

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newJwtToken);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken)
        {
            var result = await _accountService.RefreshTokenAsync(new RefreshToken() 
            { 
                Token = refreshToken 
            });
            string serializedToken = Serializations.SerializeObj(new UserSession()
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
            await _localStorageService.SetToken(serializedToken);
            return result.Token;
        }
    }
}
