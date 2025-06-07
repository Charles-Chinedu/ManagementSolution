
using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IGenericRepository<Department> genericRepository) : GenericController<Department>(genericRepository)
    {
    }
}
