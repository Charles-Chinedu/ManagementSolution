using ManagementSolution.Application.Contracts.Identity;
using ManagementSolution.Domain.EmployeeDivisions.Doctors;
using ManagementSolution.Domain.EmployeeDivisions.Overtimes;
using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Domain.EmployeeDivisions.Vacations;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Domain.TertiaryEntities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using ManagementSolution.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementSolution.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")?? 
                throw new InvalidOperationException("No connection found.")));
            services.AddScoped<IUserAccount, UserAccount>();

            services.AddScoped<IGenericRepository<GeneralDepartment>, GeneralDepartmentRepo>()
                    .AddScoped<IGenericRepository<Department>, DepartmentRepo>()
                    .AddScoped<IGenericRepository<Branch>, BranchRepo>()
                    .AddScoped<IGenericRepository<Country>, CountryRepo>()
                    .AddScoped<IGenericRepository<City>, CityRepo>()
                    .AddScoped<IGenericRepository<Department>, DepartmentRepo>()
                    .AddScoped<IGenericRepository<Town>, TownRepo>()
                    .AddScoped<IGenericRepository<Employee>, EmployeeRepo>();

            services.AddScoped<IGenericRepository<Overtime>, OvertimeRepo>();
            services.AddScoped<IGenericRepository<OvertimeType>, OvertimeTypeRepo>();

            services.AddScoped<IGenericRepository<Sanction>, SanctionRepo>();
            services.AddScoped<IGenericRepository<SanctionType>, SanctionTypeRepo>();

            services.AddScoped<IGenericRepository<Vacation>, VacationRepo>();
            services.AddScoped<IGenericRepository<VacationType>, VacationTypeRepo>();

            services.AddScoped<IGenericRepository<Doctor>, DoctorRepo>();

            services.AddScoped<IGenericRepository<Employee>, EmployeeRepo>();



            return services;
        }
    }
}
