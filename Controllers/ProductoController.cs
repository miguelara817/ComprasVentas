using ComprasVentas.Dto;
using ComprasVentas.Dto.common;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<PageResultDto<ProductoDto>>> GetAllProductosPagination([FromQuery] ProductoFilterDto filter)
        {
            var productos = await _productoService.GetProductosAsync(filter);
            return Ok(productos);
        }

        [HttpPost]
        //[FromForm] para utilizar datos tipop multimedia con strings , ints, etc
        public async Task<ActionResult<ProductoDto>> CreateProducto([FromForm] CreateProductoDto createProductoDto)
        {
            var producto = _productoService.CreateAsync(createProductoDto);
            return Ok(producto);
        }
    }
}
