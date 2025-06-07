using ManagementSolution.Domain.EmployeeDivisions.Doctors;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(IGenericRepository<Doctor> genericRepository) : GenericController<Doctor>(genericRepository)
    {
    }
}
