using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVentaBoletas.Application.Dtos;

namespace SistemaVentaBoletas.Application.Contract
{
    public interface IEventoService
    {
        Task<IEnumerable<EventoDto>> GetAllAsync();
        Task<EventoDto?> GetByIdAsync(int id);
        Task<ServiceResult<EventoDto>> CreateAsync(EventoDto dto);
        Task<ServiceResult<EventoDto>> UpdateAsync(int id, EventoDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}