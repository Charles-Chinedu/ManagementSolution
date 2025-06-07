using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionController(IGenericRepository<Sanction> genericRepository) : GenericController<Sanction>(genericRepository)
    {
    }
}
