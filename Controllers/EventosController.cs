using Microsoft.AspNetCore.Mvc;
using SistemaVentaBoletasAPI.Context;
using SistemaVentaBoletasAPI.DTOs;
using SistemaVentaBoletasAPI.Models;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Eventos.ToList());
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var evento = _context.Eventos.Find(id);

            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        // POST: api/Eventos
        [HttpPost]
        public IActionResult Post(EventoDTO dto)
        {
            Evento evento = new Evento()
            {
                Nombre = dto.Nombre,
                Fecha = dto.Fecha,
                Lugar = dto.Lugar,
                Precio = dto.Precio,
                CuposDisponibles = dto.CuposDisponibles
            };

            _context.Eventos.Add(evento);
            _context.SaveChanges();

            return Ok(evento);
        }

        // PUT: api/Eventos/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, EventoDTO dto)
        {
            var evento = _context.Eventos.Find(id);

            if (evento == null)
                return NotFound();

            evento.Nombre = dto.Nombre;
            evento.Fecha = dto.Fecha;
            evento.Lugar = dto.Lugar;
            evento.Precio = dto.Precio;
            evento.CuposDisponibles = dto.CuposDisponibles;

            _context.SaveChanges();

            return Ok(evento);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var evento = _context.Eventos.Find(id);

            if (evento == null)
                return NotFound();

            _context.Eventos.Remove(evento);
            _context.SaveChanges();

            return Ok();
        }
    }
}