using Microsoft.AspNetCore.Mvc;
using SistemaVentaBoletasAPI.Context;
using SistemaVentaBoletasAPI.DTOs;
using SistemaVentaBoletasAPI.Models;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Ventas.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var venta = _context.Ventas.Find(id);

            if (venta == null)
                return NotFound();

            return Ok(venta);
        }

        [HttpPost]
        public IActionResult Post(VentaDTO dto)
        {
            Venta venta = new Venta()
            {
                FechaVenta = dto.FechaVenta,
                CantidadBoletas = dto.CantidadBoletas,
                Total = dto.Total,
                EventoId = dto.EventoId,
                ClienteId = dto.ClienteId
            };

            _context.Ventas.Add(venta);
            _context.SaveChanges();

            return Ok(venta);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, VentaDTO dto)
        {
            var venta = _context.Ventas.Find(id);

            if (venta == null)
                return NotFound();

            venta.FechaVenta = dto.FechaVenta;
            venta.CantidadBoletas = dto.CantidadBoletas;
            venta.Total = dto.Total;
            venta.EventoId = dto.EventoId;
            venta.ClienteId = dto.ClienteId;

            _context.SaveChanges();

            return Ok(venta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var venta = _context.Ventas.Find(id);

            if (venta == null)
                return NotFound();

            _context.Ventas.Remove(venta);
            _context.SaveChanges();

            return Ok();
        }
    }
}