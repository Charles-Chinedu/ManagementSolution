using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class SanctionRepo(AppDbContext appDbContext) : IGenericRepository<Sanction>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.Sanctions.FirstOrDefaultAsync(_ => _.EmployeeId == id);
            if (item == null) return NotFound();
            _appDbContext.Sanctions.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.Sanctions.FindAsync(id);
            if (exists == null) return false;
            return true;
        }

        public async Task<List<Sanction>> GetAllAsync() => await _appDbContext.Sanctions
            .AsNoTracking()
            .Include(_ => _.SanctionType)
            .ToListAsync();

        public async Task<Sanction> GetById(string id) => await _appDbContext.Sanctions.FirstOrDefaultAsync(_ => _.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Sanction item)
        {
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Sanctions.Add(item);
            await Commit();
            return Success(); 
        }

        public async Task<GeneralResponse> Update(Sanction item)
        {
            var obj = await _appDbContext.Sanctions.FirstOrDefaultAsync(_ => _.EmployeeId == item.EmployeeId);
            if (obj == null) return NotFound();
            obj.PunishmentDate = item.PunishmentDate;
            obj.Punishment = item.Punishment;
            obj.Date = item.Date;
            obj.SanctionType = item.SanctionType;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Data not found.");

        private static GeneralResponse Success() => new(true, "Successful");

        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
