using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class BranchRepo(AppDbContext appDbContext) : IGenericRepository<Branch>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var branch = await appDbContext.Branches.FindAsync(id);
            if (branch == null) return NotFound();
            appDbContext.Branches.Remove(branch);
            await Commit();
            return Success();
        }

        public async Task<List<Branch>> GetAllAsync() => await appDbContext.Branches
            .AsNoTracking()
            .Include(_ => _.Department)
            .ToListAsync();

        public async Task<Branch> GetById(string id) => await appDbContext.Branches.FindAsync(id)!;

        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Branch already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Branches.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var branch = await appDbContext.Branches.FindAsync(item.Id);
            if (branch is null) return NotFound();
            branch.Name = item.Name;
            branch.DepartmentId = item.DepartmentId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Branch not found.");

        private static GeneralResponse Success() => new(true, "Success.");

        private async Task Commit() => await appDbContext.SaveChangesAsync();
    
        private async Task<bool> CheckName(string name)
        {
            return !await appDbContext.Branches.AnyAsync(_ => EF.Functions.Like(_.Name, name));
        }

        public async Task<bool> Exists(string id)
        {
            var dept = await appDbContext.Branches.FindAsync(id);
            if (dept is null) return false;
            return true;
        }
    }
}
