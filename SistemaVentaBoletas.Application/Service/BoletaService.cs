using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;
using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Application.Service
{
    public class BoletaService : IBoletaService
    {
        private static readonly string[] EstadosValidos = { "Disponible", "Vendido", "Cancelado" };

        private readonly IBoletaRepository _boletaRepository;
        private readonly IEventoRepository _eventoRepository;

        public BoletaService(IBoletaRepository boletaRepository, IEventoRepository eventoRepository)
        {
            _boletaRepository = boletaRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<BoletaDto>> GetAllAsync()
        {
            var boletas = await _boletaRepository.GetAllAsync();

            return boletas.Select(b => new BoletaDto
            {
                Id = b.Id,
                Codigo = b.Codigo,
                Estado = b.Estado,
                EventoId = b.EventoId
            });
        }

        public async Task<BoletaDto?> GetByIdAsync(int id)
        {
            var boleta = await _boletaRepository.GetByIdAsync(id);

            if (boleta == null)
                return null;

            return new BoletaDto
            {
                Id = boleta.Id,
                Codigo = boleta.Codigo,
                Estado = boleta.Estado,
                EventoId = boleta.EventoId
            };
        }

        public async Task<ServiceResult<BoletaDto>> CreateAsync(BoletaDto dto)
        {
            var errores = await ValidarBoletaAsync(dto);
            if (errores.Any())
                return ServiceResult<BoletaDto>.Fail(errores);

            var boleta = new Boleta
            {
                Codigo = dto.Codigo.Trim(),
                Estado = dto.Estado.Trim(),
                EventoId = dto.EventoId
            };

            var creada = await _boletaRepository.AddAsync(boleta);

            dto.Id = creada.Id;
            return ServiceResult<BoletaDto>.Ok(dto);
        }

        public async Task<ServiceResult<BoletaDto>> UpdateAsync(int id, BoletaDto dto)
        {
            var errores = await ValidarBoletaAsync(dto);
            if (errores.Any())
                return ServiceResult<BoletaDto>.Fail(errores);

            var boleta = await _boletaRepository.GetByIdAsync(id);
            if (boleta == null)
                return ServiceResult<BoletaDto>.Fail("La boleta no existe.");

            boleta.Codigo = dto.Codigo.Trim();
            boleta.Estado = dto.Estado.Trim();
            boleta.EventoId = dto.EventoId;

            await _boletaRepository.UpdateAsync(boleta);

            return ServiceResult<BoletaDto>.Ok(dto);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var eliminada = await _boletaRepository.DeleteAsync(id);

            if (!eliminada)
                return ServiceResult<bool>.Fail("La boleta no existe.");

            return ServiceResult<bool>.Ok(true);
        }

        private async Task<List<string>> ValidarBoletaAsync(BoletaDto dto)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Codigo))
                errores.Add("El código de la boleta es obligatorio.");
            else if (dto.Codigo.Length > 20)
                errores.Add("El código no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Estado))
                errores.Add("El estado es obligatorio.");
            else if (!EstadosValidos.Contains(dto.Estado))
                errores.Add("El estado debe ser: Disponible, Vendido o Cancelado.");

            if (dto.EventoId <= 0)
            {
                errores.Add("Debe indicar un evento válido.");
            }
            else
            {
                var evento = await _eventoRepository.GetByIdAsync(dto.EventoId);
                if (evento == null)
                    errores.Add("El evento indicado no existe.");
            }

            return errores;
        }
    }
}