using ComprasVentas.Dto;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalService _sucursalService;

        public SucursalController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<List<SucursalDto>>> GetAll()
        {
            return Ok(await _sucursalService.FindAllSucursalesAsync());
        }

        [HttpGet("/{sucursalId}/almacenes")]
        [Authorize]
        public async Task<ActionResult<List<AlmacenDto>>> GetAlmacenes(int sucursalId)
        {
            return Ok(await _sucursalService.FindAllAlmacenesAsync(sucursalId));
        }
    }
}
