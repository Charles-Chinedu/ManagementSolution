using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class GeneralDepartmentRepo(AppDbContext appDbContext) : IGenericRepository<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var dept = await appDbContext.GeneralDepartments.FindAsync(id);
            if (dept == null)
                return NotFound();
            appDbContext.Remove(dept);
            await Commit();
            return Success();
        }

        public async Task<List<GeneralDepartment>> GetAllAsync() => await appDbContext.GeneralDepartments.ToListAsync();

        public async Task<GeneralDepartment> GetById(string id) => await appDbContext.GeneralDepartments.FindAsync(id);

        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            var checkIfNull = await CheckName(item.Name!);
            if (!checkIfNull) return new GeneralResponse(false, "Department already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.GeneralDepartments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var dept = await appDbContext.GeneralDepartments.FirstOrDefaultAsync(_ => _.Id == item.Id);
            if (dept is null) return NotFound();
            dept.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Department not found.");

        private static GeneralResponse Success() => new(true, "Process completed.");

        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            return !await appDbContext.GeneralDepartments
                .AnyAsync(d => EF.Functions.Like(d.Name, name));
        }

        public async Task<bool> Exists(string id)
        {
            var dept = await appDbContext.GeneralDepartments.FindAsync(id);
            if (dept is null) return false;
            return true;
        }
    }
}
