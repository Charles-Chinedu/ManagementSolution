using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class SanctionTypeRepo(AppDbContext appDbContext) : IGenericRepository<SanctionType>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.SanctionTypes.FindAsync(id);
            if (item == null) return NotFound();

            _appDbContext.SanctionTypes.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.SanctionTypes.FindAsync(id);
            if (exists == null) return false;
            return true;
        }

        public async Task<List<SanctionType>> GetAllAsync() => await _appDbContext.SanctionTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<SanctionType> GetById(string id) => await _appDbContext.SanctionTypes.FindAsync(id);

        public async Task<GeneralResponse> Insert(SanctionType item)
        {
            if (!await CheckName(item.Name)) return new GeneralResponse(false, "Overtime type already added");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.SanctionTypes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(SanctionType item)
        {
            var obj = await _appDbContext.SanctionTypes.FindAsync(item.Id);
            if (obj is null) return NotFound();
            obj.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Data not found.");

        private static GeneralResponse Success() => new(true, "Successful");

        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await _appDbContext.OvertimeTypes.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
