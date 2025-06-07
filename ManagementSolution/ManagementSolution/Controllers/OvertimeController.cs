using ManagementSolution.Domain.EmployeeDivisions.Overtimes;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController(IGenericRepository<Overtime> genericRepository) : GenericController<Overtime>(genericRepository)
    {
    }
}
