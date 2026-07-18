using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Interfaces;
using SistemaVentaBoletasAPI.DTOs;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;

        public EventosController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventos = await _eventoRepository.GetAllAsync();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO dto)
        {
            Evento evento = new Evento()
            {
                Nombre = dto.Nombre,
                Fecha = dto.Fecha,
                Lugar = dto.Lugar,
                Precio = dto.Precio,
                CuposDisponibles = dto.CuposDisponibles
            };

            var creado = await _eventoRepository.AddAsync(evento);

            return Ok(creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDTO dto)
        {
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento == null)
                return NotFound();

            evento.Nombre = dto.Nombre;
            evento.Fecha = dto.Fecha;
            evento.Lugar = dto.Lugar;
            evento.Precio = dto.Precio;
            evento.CuposDisponibles = dto.CuposDisponibles;

            var actualizado = await _eventoRepository.UpdateAsync(evento);

            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _eventoRepository.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return Ok();
        }
    }
}