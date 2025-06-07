using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class EmployeeRepo(AppDbContext appDbContext) : IGenericRepository<Employee>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await appDbContext.Employees.FindAsync(id);
            if (item == null)
                return NotFound();
            appDbContext.Employees.Remove(item);
            await Commit();
            return Success();
        }

        public Task<bool> Exists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var employees = await appDbContext.Employees
                .AsNoTracking()
                .Include(_ => _.Town)
                .ThenInclude(_ => _.City)
                .ThenInclude(_ => _.Country)
                .Include(_ => _.Branch)
                .ThenInclude(_ => _.Department)
                .ThenInclude(_ => _.GeneralDepartment)
                .ToListAsync();
            return employees;
        }

        public async Task<Employee> GetById(string id)
        {
            var employee = await appDbContext.Employees
                .Include(_ => _.Town)
                .ThenInclude(_ => _.City)
                .ThenInclude(_ => _.Country)
                .Include(_ => _.Branch)
                .ThenInclude(_ => _.Department)
                .ThenInclude(_ => _.GeneralDepartment)
                .FirstOrDefaultAsync(_ => _.Id == id);
            return employee!;
        }

        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Employee already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Employees.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee employee)
        {
            var findUser = await appDbContext.Employees.FirstOrDefaultAsync(_ => _.Id == employee.Id);
            if (findUser is null) return new GeneralResponse(false, "Employee does not exist");

            findUser.Name = employee.Name;
            findUser.Other = employee.Other;
            findUser.Address = employee.Address;
            findUser.TelephoneNumber = employee.TelephoneNumber;
            findUser.BranchId = employee.BranchId;
            findUser.TownId = employee.TownId;
            findUser.CivilId = employee.CivilId;
            findUser.FileNumber = employee.FileNumber;
            findUser.JobName = employee.JobName;
            findUser.Photo = employee.Photo;
            
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Department not found.");

        private static GeneralResponse Success() => new(true, "Process completed.");

        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            return !await appDbContext.Towns
                .AnyAsync(d => EF.Functions.Like(d.Name, name));
        }
    }
}
