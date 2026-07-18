using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Interfaces;
using SistemaVentaBoletasAPI.DTOs;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletasController : ControllerBase
    {
        private readonly IBoletaRepository _boletaRepository;

        public BoletasController(IBoletaRepository boletaRepository)
        {
            _boletaRepository = boletaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var boletas = await _boletaRepository.GetAllAsync();
            return Ok(boletas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var boleta = await _boletaRepository.GetByIdAsync(id);

            if (boleta == null)
                return NotFound();

            return Ok(boleta);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BoletaDTO dto)
        {
            Boleta boleta = new Boleta()
            {
                Codigo = dto.Codigo,
                Estado = dto.Estado,
                EventoId = dto.EventoId
            };

            var creada = await _boletaRepository.AddAsync(boleta);

            return Ok(creada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BoletaDTO dto)
        {
            var boleta = await _boletaRepository.GetByIdAsync(id);

            if (boleta == null)
                return NotFound();

            boleta.Codigo = dto.Codigo;
            boleta.Estado = dto.Estado;
            boleta.EventoId = dto.EventoId;

            var actualizada = await _boletaRepository.UpdateAsync(boleta);

            return Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminada = await _boletaRepository.DeleteAsync(id);

            if (!eliminada)
                return NotFound();

            return Ok();
        }
    }
}