using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;
using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Application.Service
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<EventoDto>> GetAllAsync()
        {
            var eventos = await _eventoRepository.GetAllAsync();

            return eventos.Select(e => new EventoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Fecha = e.Fecha,
                Lugar = e.Lugar,
                Precio = e.Precio,
                CuposDisponibles = e.CuposDisponibles
            });
        }

        public async Task<EventoDto?> GetByIdAsync(int id)
        {
            var evento = await _eventoRepository.GetByIdAsync(id);

            if (evento == null)
                return null;

            return new EventoDto
            {
                Id = evento.Id,
                Nombre = evento.Nombre,
                Fecha = evento.Fecha,
                Lugar = evento.Lugar,
                Precio = evento.Precio,
                CuposDisponibles = evento.CuposDisponibles
            };
        }

        public async Task<ServiceResult<EventoDto>> CreateAsync(EventoDto dto)
        {
            var errores = ValidarEvento(dto);
            if (errores.Any())
                return ServiceResult<EventoDto>.Fail(errores);

            var evento = new Evento
            {
                Nombre = dto.Nombre.Trim(),
                Fecha = dto.Fecha,
                Lugar = dto.Lugar.Trim(),
                Precio = dto.Precio,
                CuposDisponibles = dto.CuposDisponibles
            };

            var creado = await _eventoRepository.AddAsync(evento);

            dto.Id = creado.Id;
            return ServiceResult<EventoDto>.Ok(dto);
        }

        public async Task<ServiceResult<EventoDto>> UpdateAsync(int id, EventoDto dto)
        {
            var errores = ValidarEvento(dto);
            if (errores.Any())
                return ServiceResult<EventoDto>.Fail(errores);

            var evento = await _eventoRepository.GetByIdAsync(id);
            if (evento == null)
                return ServiceResult<EventoDto>.Fail("El evento no existe.");

            evento.Nombre = dto.Nombre.Trim();
            evento.Fecha = dto.Fecha;
            evento.Lugar = dto.Lugar.Trim();
            evento.Precio = dto.Precio;
            evento.CuposDisponibles = dto.CuposDisponibles;

            await _eventoRepository.UpdateAsync(evento);

            return ServiceResult<EventoDto>.Ok(dto);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var eliminado = await _eventoRepository.DeleteAsync(id);

            if (!eliminado)
                return ServiceResult<bool>.Fail("El evento no existe.");

            return ServiceResult<bool>.Ok(true);
        }

        private List<string> ValidarEvento(EventoDto dto)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errores.Add("El nombre del evento es obligatorio.");
            else if (dto.Nombre.Length > 100)
                errores.Add("El nombre no puede exceder 100 caracteres.");

            if (dto.Fecha == default)
                errores.Add("La fecha del evento es obligatoria.");
            else if (dto.Fecha.Date < DateTime.Now.Date)
                errores.Add("La fecha del evento no puede ser en el pasado.");

            if (string.IsNullOrWhiteSpace(dto.Lugar))
                errores.Add("El lugar es obligatorio.");
            else if (dto.Lugar.Length > 150)
                errores.Add("El lugar no puede exceder 150 caracteres.");

            if (dto.Precio <= 0)
                errores.Add("El precio debe ser mayor a cero.");

            if (dto.CuposDisponibles < 0)
                errores.Add("Los cupos disponibles no pueden ser negativos.");

            return errores;
        }
    }
}