using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletas.Application.Contract
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync(int id);
        Task<ServiceResult<ClienteDto>> CreateAsync(ClienteDto dto);
        Task<ServiceResult<ClienteDto>> UpdateAsync(int id, ClienteDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}