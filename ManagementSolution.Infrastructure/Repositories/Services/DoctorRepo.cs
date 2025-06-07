using ManagementSolution.Application.Responses;
using ManagementSolution.Domain.EmployeeDivisions.Doctors;
using ManagementSolution.Infrastructure.Data;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementSolution.Infrastructure.Repositories.Services
{
    public class DoctorRepo(AppDbContext appDbContext) : IGenericRepository<Doctor>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<GeneralResponse> DeleteById(string id)
        {
            var item = await _appDbContext.Doctors.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
            if (item == null) return NotFound();

            appDbContext.Doctors.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _appDbContext.Doctors.FindAsync(id);
            if (exists is not null) return true;
            return false;
        }

        public async Task<List<Doctor>> GetAllAsync() => await _appDbContext.Doctors
            .AsNoTracking()
            .ToListAsync();

        public async Task<Doctor> GetById(string id) => await _appDbContext.Doctors.FirstOrDefaultAsync(_ => _.EmployeeId == id);

        public async Task<GeneralResponse> Insert(Doctor item)
        {
            item.Id = Guid.NewGuid().ToString();
            _appDbContext.Doctors.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Doctor item)
        {
            var obj = await _appDbContext.Doctors
                .FirstOrDefaultAsync(_ => _.EmployeeId == item.EmployeeId);
            if (obj == null) return NotFound();
            obj.Id = item.Id;
            obj.MedicalRecommendation = item.MedicalRecommendation;
            obj.MedicalDiagnose = item.MedicalDiagnose;
            obj.Date = item.Date;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Data not found.");

        private static GeneralResponse Success() => new(true, "Successful");
        
        private async Task Commit() => await _appDbContext.SaveChangesAsync();
    }
}
