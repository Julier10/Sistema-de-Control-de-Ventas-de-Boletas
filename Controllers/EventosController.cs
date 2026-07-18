using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventos = await _eventoService.GetAllAsync();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var evento = await _eventoService.GetByIdAsync(id);

            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto dto)
        {
            var resultado = await _eventoService.CreateAsync(dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto dto)
        {
            var resultado = await _eventoService.UpdateAsync(id, dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _eventoService.DeleteAsync(id);

            if (!resultado.Success)
                return NotFound(resultado.Errors);

            return Ok();
        }
    }
}