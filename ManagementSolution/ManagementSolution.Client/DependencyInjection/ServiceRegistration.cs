using ManagementSolution.Client.Helpers;
using ManagementSolution.Client.Services.Contracts;
using ManagementSolution.Client.Services.Implementation;
using Microsoft.AspNetCore.Components.Authorization;

namespace ManagementSolution.Client.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<GetHttpClient>();
            services.AddScoped<LocalStorageService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            return services;
        }

    }
}
