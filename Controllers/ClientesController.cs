using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Contract;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto dto)
        {
            var resultado = await _clienteService.CreateAsync(dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteDto dto)
        {
            var resultado = await _clienteService.UpdateAsync(id, dto);

            if (!resultado.Success)
                return BadRequest(resultado.Errors);

            return Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _clienteService.DeleteAsync(id);

            if (!resultado.Success)
                return NotFound(resultado.Errors);

            return Ok();
        }
    }
}