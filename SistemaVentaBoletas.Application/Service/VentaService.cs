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
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEventoRepository _eventoRepository;

        public VentaService(
            IVentaRepository ventaRepository,
            IClienteRepository clienteRepository,
            IEventoRepository eventoRepository)
        {
            _ventaRepository = ventaRepository;
            _clienteRepository = clienteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<VentaDto>> GetAllAsync()
        {
            var ventas = await _ventaRepository.GetAllAsync();

            return ventas.Select(v => new VentaDto
            {
                Id = v.Id,
                FechaVenta = v.FechaVenta,
                CantidadBoletas = v.CantidadBoletas,
                Total = v.Total,
                EventoId = v.EventoId,
                ClienteId = v.ClienteId
            });
        }

        public async Task<VentaDto?> GetByIdAsync(int id)
        {
            var venta = await _ventaRepository.GetByIdAsync(id);

            if (venta == null)
                return null;

            return new VentaDto
            {
                Id = venta.Id,
                FechaVenta = venta.FechaVenta,
                CantidadBoletas = venta.CantidadBoletas,
                Total = venta.Total,
                EventoId = venta.EventoId,
                ClienteId = venta.ClienteId
            };
        }

        public async Task<ServiceResult<VentaDto>> CreateAsync(VentaDto dto)
        {
            var errores = await ValidarVentaAsync(dto);
            if (errores.Any())
                return ServiceResult<VentaDto>.Fail(errores);

            var venta = new Venta
            {
                FechaVenta = dto.FechaVenta == default ? DateTime.Now : dto.FechaVenta,
                CantidadBoletas = dto.CantidadBoletas,
                Total = dto.Total,
                EventoId = dto.EventoId,
                ClienteId = dto.ClienteId
            };

            var creada = await _ventaRepository.AddAsync(venta);

            dto.Id = creada.Id;
            dto.FechaVenta = venta.FechaVenta;
            return ServiceResult<VentaDto>.Ok(dto);
        }

        public async Task<ServiceResult<VentaDto>> UpdateAsync(int id, VentaDto dto)
        {
            var errores = await ValidarVentaAsync(dto);
            if (errores.Any())
                return ServiceResult<VentaDto>.Fail(errores);

            var venta = await _ventaRepository.GetByIdAsync(id);
            if (venta == null)
                return ServiceResult<VentaDto>.Fail("La venta no existe.");

            venta.FechaVenta = dto.FechaVenta;
            venta.CantidadBoletas = dto.CantidadBoletas;
            venta.Total = dto.Total;
            venta.EventoId = dto.EventoId;
            venta.ClienteId = dto.ClienteId;

            await _ventaRepository.UpdateAsync(venta);

            return ServiceResult<VentaDto>.Ok(dto);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var eliminada = await _ventaRepository.DeleteAsync(id);

            if (!eliminada)
                return ServiceResult<bool>.Fail("La venta no existe.");

            return ServiceResult<bool>.Ok(true);
        }

        private async Task<List<string>> ValidarVentaAsync(VentaDto dto)
        {
            var errores = new List<string>();

            if (dto.CantidadBoletas <= 0)
                errores.Add("La cantidad de boletas debe ser mayor a cero.");

            if (dto.Total <= 0)
                errores.Add("El total de la venta debe ser mayor a cero.");

            if (dto.ClienteId <= 0)
            {
                errores.Add("Debe indicar un cliente válido.");
            }
            else
            {
                var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
                if (cliente == null)
                    errores.Add("El cliente indicado no existe.");
            }

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