using Microsoft.AspNetCore.Mvc;
using SistemaVentaBoletasAPI.Context;
using SistemaVentaBoletasAPI.DTOs;
using SistemaVentaBoletasAPI.Models;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Clientes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Post(ClienteDTO dto)
        {
            Cliente cliente = new Cliente()
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono
            };

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ClienteDTO dto)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
                return NotFound();

            cliente.Nombre = dto.Nombre;
            cliente.Apellido = dto.Apellido;
            cliente.Email = dto.Email;
            cliente.Telefono = dto.Telefono;

            _context.SaveChanges();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok();
        }
    }
}