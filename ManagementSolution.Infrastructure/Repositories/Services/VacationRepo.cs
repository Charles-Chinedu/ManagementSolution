using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Vacations;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class VacationRepo(AppDbContext appDbContext) : IGenericRepository<Vacation>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.Vacations.FirstOrDefaultAsync(_ => _.EmployeeId == id);
            if (item == null) return NotFound();
            _appDbContext.Vacations.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.Vacations.FindAsync(id);
            if (exists == null) return false;
            return true;
        }

        public async Task<List<Vacation>> GetAllAsync() => await _appDbContext.Vacations
            .AsNoTracking()
            .Include(_ => _.VacationType)
            .ToListAsync();

        public async Task<Vacation> GetById(string id) => await _appDbContext.Vacations.FirstOrDefaultAsync(_ => _.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Vacation item)
        {
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Vacations.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Vacation item)
        {
            var obj = await _appDbContext.Vacations.FirstOrDefaultAsync(_ => _.EmployeeId == item.EmployeeId);
            if (obj == null) return NotFound();
            obj.StartDate = item.StartDate;
            obj.NumberOfDays = item.NumberOfDays;
            obj.VacationTypeId = item.VacationTypeId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Data not found.");

        private static GeneralResponse Success() => new(true, "Successful");

        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
