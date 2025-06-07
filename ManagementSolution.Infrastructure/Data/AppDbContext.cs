using ManagementSolution.Application.Models.Identity;
using ManagementSolution.Domain.EmployeeDivisions.Doctors;
using ManagementSolution.Domain.EmployeeDivisions.Overtimes;
using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Domain.EmployeeDivisions.Vacations;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Domain.Entities.Role;
using ManagementSolution.Domain.TertiaryEntities;
using ManagementSolution.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        // General Departments / Department / Branch
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }

        // Country / City / Town
        public DbSet<Country> Countries{ get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }

        // Authentication / Role / System Roles
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        // Vacation / Sanction / Doctor / Overtime
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Overtime>  Overtimes { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
