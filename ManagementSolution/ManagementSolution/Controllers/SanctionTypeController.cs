using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionTypeController(IGenericRepository<SanctionType> genericRepository) : GenericController<SanctionType>(genericRepository)
    {
    }
}
