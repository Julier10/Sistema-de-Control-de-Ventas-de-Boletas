using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletasController : ControllerBase
    {
        private readonly IBoletaService _boletaService;

        public BoletasController(IBoletaService boletaService)
        {
            _boletaService = boletaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var boletas = await _boletaService.GetAllAsync();
            return Ok(boletas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var boleta = await _boletaService.GetByIdAsync(id);

            if (boleta == null)
                return NotFound();

            return Ok(boleta);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BoletaDto dto)
        {
            var resultado = await _boletaService.CreateAsync(dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BoletaDto dto)
        {
            var resultado = await _boletaService.UpdateAsync(id, dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _boletaService.DeleteAsync(id);

            if (!resultado.Success)
                return NotFound(resultado.Errors);

            return Ok();
        }
    }
}