using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Domain.EmployeeDivisions.Vacations;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class VacationTypeRepo(AppDbContext appDbContext) : IGenericRepository<VacationType>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.VacationTypes.FindAsync(id);
            if (item == null) return NotFound();

            _appDbContext.VacationTypes.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.SanctionTypes.FindAsync(id);
            if (exists == null) return false;
            return true;
        }

        public async Task<List<VacationType>> GetAllAsync() => await _appDbContext.VacationTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<VacationType> GetById(string id) => await _appDbContext.VacationTypes.FindAsync(id);

        public async Task<GeneralResponse> Insert(VacationType item)
        {
            if (!await CheckName(item.Name)) return new GeneralResponse(false, "Overtime type already added");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.VacationTypes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(VacationType item)
        {
            var obj = await _appDbContext.VacationTypes.FindAsync(item.Id);
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
            var item = await _appDbContext.VacationTypes.FirstOrDefaultAsync(_ => _.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
