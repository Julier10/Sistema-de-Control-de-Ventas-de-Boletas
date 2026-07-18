using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ventas = await _ventaService.GetAllAsync();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var venta = await _ventaService.GetByIdAsync(id);

            if (venta == null)
                return NotFound();

            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaDto dto)
        {
            var resultado = await _ventaService.CreateAsync(dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VentaDto dto)
        {
            var resultado = await _ventaService.UpdateAsync(id, dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _ventaService.DeleteAsync(id);

            if (!resultado.Success)
                return NotFound(resultado.Errors);

            return Ok();
        }
    }
}