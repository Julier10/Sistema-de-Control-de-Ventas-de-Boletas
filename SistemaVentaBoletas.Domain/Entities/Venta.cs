using System;
using SistemaVentaBoletas.Domain.Core;

namespace SistemaVentaBoletas.Domain.Entities
{
    public class Venta : BaseEntity
    {
        public DateTime FechaVenta { get; set; }
        public int CantidadBoletas { get; set; }
        public decimal Total { get; set; }

        public int EventoId { get; set; }
        public Evento? Evento { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}