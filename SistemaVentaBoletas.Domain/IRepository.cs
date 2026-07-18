using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVentaBoletas.Domain.Core;

namespace SistemaVentaBoletas.Domain.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}