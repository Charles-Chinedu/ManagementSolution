using ManagementSolution.Domain.EmployeeDivisions.Overtimes;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeTypeController(IGenericRepository<OvertimeType> genericRepository) : GenericController<OvertimeType>(genericRepository)
    {
    }
}
