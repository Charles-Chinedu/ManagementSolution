using ManagementSolution.Application.Contracts.Identity;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementSolution.Infrastructure.DependecyInjection
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")?? 
                throw new InvalidOperationException("No connection found.")));
            services.AddScoped<IUserAccount, UserAccount>();

            return services;
        }
    }
}
