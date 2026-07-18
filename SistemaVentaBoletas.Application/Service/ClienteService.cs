using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;
using SistemaVentaBoletas.Domain.Entities;
using SistemaVentaBoletas.Infrastructure.Interfaces;

namespace SistemaVentaBoletas.Application.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();

            return clientes.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Email = c.Email,
                Telefono = c.Telefono
            });
        }

        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente == null)
                return null;

            return new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };
        }

        public async Task<ServiceResult<ClienteDto>> CreateAsync(ClienteDto dto)
        {
            var errores = ValidarCliente(dto);
            if (errores.Any())
                return ServiceResult<ClienteDto>.Fail(errores);

            var cliente = new Cliente
            {
                Nombre = dto.Nombre.Trim(),
                Apellido = dto.Apellido.Trim(),
                Email = dto.Email.Trim(),
                Telefono = dto.Telefono.Trim()
            };

            var creado = await _clienteRepository.AddAsync(cliente);

            dto.Id = creado.Id;
            return ServiceResult<ClienteDto>.Ok(dto);
        }

        public async Task<ServiceResult<ClienteDto>> UpdateAsync(int id, ClienteDto dto)
        {
            var errores = ValidarCliente(dto);
            if (errores.Any())
                return ServiceResult<ClienteDto>.Fail(errores);

            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                return ServiceResult<ClienteDto>.Fail("El cliente no existe.");

            cliente.Nombre = dto.Nombre.Trim();
            cliente.Apellido = dto.Apellido.Trim();
            cliente.Email = dto.Email.Trim();
            cliente.Telefono = dto.Telefono.Trim();

            await _clienteRepository.UpdateAsync(cliente);

            return ServiceResult<ClienteDto>.Ok(dto);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var eliminado = await _clienteRepository.DeleteAsync(id);

            if (!eliminado)
                return ServiceResult<bool>.Fail("El cliente no existe.");

            return ServiceResult<bool>.Ok(true);
        }

        private List<string> ValidarCliente(ClienteDto dto)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errores.Add("El nombre es obligatorio.");
            else if (dto.Nombre.Length > 50)
                errores.Add("El nombre no puede exceder 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                errores.Add("El apellido es obligatorio.");
            else if (dto.Apellido.Length > 50)
                errores.Add("El apellido no puede exceder 50 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                errores.Add("El email es obligatorio.");
            else if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.Add("El formato del email no es válido.");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                errores.Add("El teléfono es obligatorio.");
            else if (!Regex.IsMatch(dto.Telefono, @"^[0-9\-\+\s\(\)]{7,15}$"))
                errores.Add("El formato del teléfono no es válido.");

            return errores;
        }
    }
}