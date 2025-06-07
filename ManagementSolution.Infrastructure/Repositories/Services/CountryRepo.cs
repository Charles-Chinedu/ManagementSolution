using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.TertiaryEntities;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class CountryRepo(AppDbContext appDbContext) : IGenericRepository<Country>
    {
        public async Task<GeneralResponse> DeleteById(string id)
        {
            var country = await appDbContext.Countries.FindAsync(id);
            if (country == null) return NotFound();
            appDbContext.Countries.Remove(country);
            await Commit();
            return Success();
        }

        public async Task<List<Country>> GetAllAsync() => await appDbContext.Countries.ToListAsync();

        public async Task<Country> GetById(string id) => await appDbContext.Countries.FindAsync(id);

        public async Task<GeneralResponse> Insert(Country item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Branch already added.");
            item.Id = Guid.NewGuid().ToString();
            appDbContext.Countries.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Country item)
        {
            var country = await appDbContext.Countries.FindAsync(item.Id);
            if (country is null) return NotFound();
            country.Name = item.Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Branch not found.");

        private static GeneralResponse Success() => new(true, "Success.");

        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            return !await appDbContext.Countries.AnyAsync(_ => EF.Functions.Like(_.Name, name));
        }

        public async Task<bool> Exists(string id)
        {
            var dept = await appDbContext.Countries.FindAsync(id);
            if (dept is null) return false;
            return true;
        }
    }
}
