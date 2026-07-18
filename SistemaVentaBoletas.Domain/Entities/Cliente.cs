using System.Collections.Generic;
using SistemaVentaBoletas.Domain.Core;

namespace SistemaVentaBoletas.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public ICollection<Venta>? Ventas { get; set; }
    }
}