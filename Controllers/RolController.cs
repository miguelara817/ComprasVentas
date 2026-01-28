using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rolService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Rol rol)
        {
            await _rolService.CreateAsync(rol);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
