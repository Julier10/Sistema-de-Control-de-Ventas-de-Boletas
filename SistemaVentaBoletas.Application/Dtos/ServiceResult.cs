using System.Collections.Generic;

namespace SistemaVentaBoletas.Application.Dtos
{
   
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T> { Success = true, Data = data };
        }

        public static ServiceResult<T> Fail(List<string> errors)
        {
            return new ServiceResult<T> { Success = false, Errors = errors };
        }

        public static ServiceResult<T> Fail(string error)
        {
            return new ServiceResult<T> { Success = false, Errors = new List<string> { error } };
        }
    }
}