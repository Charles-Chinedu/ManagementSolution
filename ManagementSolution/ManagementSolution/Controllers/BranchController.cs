using ManagementSolution.Domain.Entities;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IGenericRepository<Branch> genericRepository) : GenericController<Branch>(genericRepository)
    {
    }
}
