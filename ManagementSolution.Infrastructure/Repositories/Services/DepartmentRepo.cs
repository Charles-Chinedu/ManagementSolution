using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class DepartmentRepo(AppDbContext appDbContext) : IGenericRepository<Department>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var dept = await appDbContext.Departments.FindAsync(id);
            if (dept == null)
                return NotFound();
            appDbContext.Remove(dept);
            await Commit();
            return Success();
        }

        public async Task<List<Department>> GetAllAsync() => await appDbContext
            .Departments
            .AsNoTracking()
            .Include(_ => _.GeneralDepartment)
            .ToListAsync();

        public async Task<Department> GetById(string id) => await appDbContext.Departments.FindAsync(id)!;

        public async Task<GeneralResponse> Insert(Department item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Department already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Departments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department item)
        {
            var dept = await appDbContext.Departments.FirstOrDefaultAsync(_ => _.Id == item.Id);
            if (dept is null) return NotFound();
            dept.Name = item.Name;
            dept.GeneralDepartmentId = item.GeneralDepartmentId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Department not found.");

        private static GeneralResponse Success() => new(true, "Process completed.");

        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            return !await appDbContext.Departments
                .AnyAsync(d => EF.Functions.Like(d.Name, name));
        }

        public async Task<bool> Exists(string id)
        {
            var dept = await appDbContext.Departments.FindAsync(id);
            if (dept is null) return false;
            return true;
        }
    }
}
