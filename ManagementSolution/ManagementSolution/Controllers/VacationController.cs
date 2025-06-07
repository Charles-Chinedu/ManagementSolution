using ManagementSolution.Domain.EmployeeDivisions.Sanctions;
using ManagementSolution.Domain.EmployeeDivisions.Vacations;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController(IGenericRepository<Vacation> genericRepository) : GenericController<Vacation>(genericRepository)
    {
    }
}
