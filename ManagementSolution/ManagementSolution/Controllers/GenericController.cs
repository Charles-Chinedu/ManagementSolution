using ManagementSolution.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T>(IGenericRepository<T> genericRepository) : ControllerBase where T : class
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() => Ok(await genericRepository.GetAllAsync());

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid request sent.");
            return Ok(await genericRepository.DeleteById(id));
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid request sent.");
            return Ok(await genericRepository.GetById(id));
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid request sent.");
            var exists = await genericRepository.Exists(id);
            if (!exists) return NotFound(false);
            return Ok(true);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            if (model is null) return BadRequest("Invalid request sent.");
            return Ok(await genericRepository.Insert(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if (model is null) return BadRequest("Invalid request sent.");
            return Ok(await genericRepository.Update(model));
        }
    }
}
