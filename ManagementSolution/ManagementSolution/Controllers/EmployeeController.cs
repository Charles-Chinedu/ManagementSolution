using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IGenericRepository<Employee> genericRepository) : GenericController<Employee>(genericRepository)
    {
    }
}
