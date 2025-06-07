using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.Entities;
using ManagementSolution.Domain.TertiaryEntities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class CityRepo(AppDbContext appDbContext) : IGenericRepository<City>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var city = await appDbContext.Cities.FindAsync(id);
            if (city == null) return NotFound();
            appDbContext.Cities.Remove(city);
            await Commit();
            return Success();
        }

        public async Task<List<City>> GetAllAsync() => await appDbContext.Cities
            .AsNoTracking()
            .Include(_ => _.Country)
            .ToListAsync();

        public async Task<City> GetById(string id) => await appDbContext.Cities.FindAsync(id);

        public async Task<GeneralResponse> Insert(City item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Branch already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Cities.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(City item)
        {
            var city = await appDbContext.Cities.FindAsync(item.Id);
            if (city is null) return NotFound();
            city.Name = item.Name;
            city.CountryId = item.CountryId;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Branch not found.");

        private static GeneralResponse Success() => new(true, "Success.");

        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            return !await appDbContext.Cities.AnyAsync(_ => EF.Functions.Like(_.Name, name));
        }

        public async Task<bool> Exists(string id)
        {
            var dept = await appDbContext.Cities.FindAsync(id);
            if (dept is null) return false;
            return true;
        }
    }
}
