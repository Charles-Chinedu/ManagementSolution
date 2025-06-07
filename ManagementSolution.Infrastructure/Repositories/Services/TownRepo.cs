using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class TownRepo(AppDbContext appDbContext) : IGenericRepository<Town>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var town = await appDbContext.Towns.FindAsync(id);
            if (town == null)
                return NotFound();
            appDbContext.Remove(town);
            await Commit();
            return Success();
        }

        public async Task<List<Town>> GetAllAsync() => await appDbContext.Towns
            .AsNoTracking()
            .Include(_ => _.City)
            .ToListAsync();

        public async Task<Town> GetById(string id) => await appDbContext.Towns.FindAsync(id)!;

        public async Task<GeneralResponse> Insert(Town item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, $"{item.Name} already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Towns.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Town item)
        {
            var town = await appDbContext.Towns.FirstOrDefaultAsync(_ => _.Id == item.Id);
            if (town is null) return NotFound();
            town.Name = item.Name;
            town.CityId = item.CityId;
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

        public async Task<bool> Exists(string id)
        {
            var dept = await appDbContext.Towns.FindAsync(id);
            if (dept is null) return false;
            return true;
        }
    }
}
