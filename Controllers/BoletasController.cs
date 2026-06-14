using Microsoft.AspNetCore.Mvc;
using SistemaVentaBoletasAPI.Context;
using SistemaVentaBoletasAPI.DTOs;
using SistemaVentaBoletasAPI.Models;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoletasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Boletas.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var boleta = _context.Boletas.Find(id);

            if (boleta == null)
                return NotFound();

            return Ok(boleta);
        }

        [HttpPost]
        public IActionResult Post(BoletaDTO dto)
        {
            Boleta boleta = new Boleta()
            {
                Codigo = dto.Codigo,
                Estado = dto.Estado,
                EventoId = dto.EventoId
            };

            _context.Boletas.Add(boleta);
            _context.SaveChanges();

            return Ok(boleta);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, BoletaDTO dto)
        {
            var boleta = _context.Boletas.Find(id);

            if (boleta == null)
                return NotFound();

            boleta.Codigo = dto.Codigo;
            boleta.Estado = dto.Estado;
            boleta.EventoId = dto.EventoId;

            _context.SaveChanges();

            return Ok(boleta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var boleta = _context.Boletas.Find(id);

            if (boleta == null)
                return NotFound();

            _context.Boletas.Remove(boleta);
            _context.SaveChanges();

            return Ok();
        }
    }
}