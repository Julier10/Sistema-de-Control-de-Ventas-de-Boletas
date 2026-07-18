using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletas.Application.Contract
{
    public interface IBoletaService
    {
        Task<IEnumerable<BoletaDto>> GetAllAsync();
        Task<BoletaDto?> GetByIdAsync(int id);
        Task<ServiceResult<BoletaDto>> CreateAsync(BoletaDto dto);
        Task<ServiceResult<BoletaDto>> UpdateAsync(int id, BoletaDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}