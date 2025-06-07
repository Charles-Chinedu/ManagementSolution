using ManagementSolution.Domain.Entities;
using ManagementSolution.Domain.TertiaryEntities;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController(IGenericRepository<Country> genericRepository) : GenericController<Country>(genericRepository)
    {
    }
}
