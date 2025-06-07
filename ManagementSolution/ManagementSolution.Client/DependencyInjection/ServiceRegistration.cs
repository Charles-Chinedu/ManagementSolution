using ManagementSolution.Client.Helpers;
using ManagementSolution.Client.Services.Contracts;
using ManagementSolution.Client.Services.Implementation;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Domain.TertiaryEntities;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor;

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



            // General Department / Department / Branch
            services.AddScoped<IGenericService<GeneralDepartment>, GenericService<GeneralDepartment>>();
            services.AddScoped<IGenericService<Department>, GenericService<Department>>();
            services.AddScoped<IGenericService<Branch>, GenericService<Branch>>();

            // Country / City / Town
            services.AddScoped<IGenericService<Country>, GenericService<Country>>();
            services.AddScoped<IGenericService<City>, GenericService<City>>();
            services.AddScoped<IGenericService<Town>, GenericService<Town>>();

            // Employee
            services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();
            
            return services;
        }

    }
}
