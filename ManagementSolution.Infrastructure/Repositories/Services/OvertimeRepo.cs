using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Overtimes;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class OvertimeRepo(AppDbContext appDbContext) : IGenericRepository<Overtime>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.Overtimes.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
            if (item == null) return NotFound();

            appDbContext.Overtimes.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.Overtimes.FindAsync(id);
            if (exists == null) return false;
            return true;
        }

        public async Task<List<Overtime>> GetAllAsync() => await _appDbContext.Overtimes
            .AsNoTracking()
            .ToListAsync();

        public async Task<Overtime> GetById(string id) => await _appDbContext.Overtimes.FirstOrDefaultAsync(_ => _.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Overtime item)
        {
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Overtimes.Add(item);
            await Commit();
            return Success();
        }


        public async Task<GeneralResponse> Update(Overtime item)
        {
            var obj = await _appDbContext.Overtimes.FirstOrDefaultAsync(_ => _.EmployeeId == item.EmployeeId);
            if (obj == null) return NotFound();
            obj.StartDate = item.StartDate;
            obj.EndDate = item.EndDate;
            obj.OvertimeTypeId = item.OvertimeTypeId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Data not found.");

        private static GeneralResponse Success() => new(true, "Successful");

        private async Task Commit() => await _appDbContext.SaveChangesAsync();

    }
}
