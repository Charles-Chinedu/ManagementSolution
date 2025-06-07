using ManagementSolution.Domain.TertiaryEntities;
using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(IGenericRepository<City> genericRepository) : GenericController<City>(genericRepository)
    {
    }
}
