using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletas.Application.Contract
{
    public interface IVentaService
    {
        Task<IEnumerable<VentaDto>> GetAllAsync();
        Task<VentaDto?> GetByIdAsync(int id);
        Task<ServiceResult<VentaDto>> CreateAsync(VentaDto dto);
        Task<ServiceResult<VentaDto>> UpdateAsync(int id, VentaDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}