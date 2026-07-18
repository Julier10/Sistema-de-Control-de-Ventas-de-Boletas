using System;
using System.Collections.Generic;
using SistemaVentaBoletas.Domain.Core;

namespace SistemaVentaBoletas.Domain.Entities
{
    public class Evento : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public decimal Precio { get; set; }
        public int CuposDisponibles { get; set; }

        public ICollection<Boleta>? Boletas { get; set; }
        public ICollection<Venta>? Ventas { get; set; }
    }
}
