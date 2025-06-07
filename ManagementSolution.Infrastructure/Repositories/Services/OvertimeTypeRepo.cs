using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Overtimes;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class OvertimeTypeRepo(AppDbContext appDbContext) : IGenericRepository<OvertimeType>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.OvertimeTypes.FindAsync(id);
            if (item == null) return NotFound();
            _appDbContext.OvertimeTypes.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.OvertimeTypes.FindAsync(id);
            if (exists == null) return false;
            return true;
        }

        public async Task<List<OvertimeType>> GetAllAsync() => await _appDbContext.OvertimeTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<OvertimeType> GetById(string id) => await _appDbContext.OvertimeTypes.FindAsync(id);

        public async Task<GeneralResponse> Insert(OvertimeType item)
        {
            if (!await CheckName(item.Name)) return new GeneralResponse(false, "Overtime type already added");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.OvertimeTypes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(OvertimeType item)
        {
            var obj = await _appDbContext.OvertimeTypes.FindAsync(item.Id);
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
