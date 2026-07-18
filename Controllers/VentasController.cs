using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Interfaces;
using SistemaVentaBoletasAPI.DTOs;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaRepository _ventaRepository;

        public VentasController(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ventas = await _ventaRepository.GetAllAsync();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);

            if (venta == null)
                return NotFound();

            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaDTO dto)
        {
            Venta venta = new Venta()
            {
                FechaVenta = dto.FechaVenta,
                CantidadBoletas = dto.CantidadBoletas,
                Total = dto.Total,
                EventoId = dto.EventoId,
                ClienteId = dto.ClienteId
            };

            var creada = await _ventaRepository.AddAsync(venta);

            return Ok(creada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VentaDTO dto)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);

            if (venta == null)
                return NotFound();

            venta.FechaVenta = dto.FechaVenta;
            venta.CantidadBoletas = dto.CantidadBoletas;
            venta.Total = dto.Total;
            venta.EventoId = dto.EventoId;
            venta.ClienteId = dto.ClienteId;

            var actualizada = await _ventaRepository.UpdateAsync(venta);

            return Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminada = await _ventaRepository.DeleteAsync(id);

            if (!eliminada)
                return NotFound();

            return Ok();
        }
    }
}