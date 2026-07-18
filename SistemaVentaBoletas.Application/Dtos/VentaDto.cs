using System;

namespace SistemaVentaBoletas.Application.Dtos
{
    public class VentaDto
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public int CantidadBoletas { get; set; }
        public decimal Total { get; set; }
        public int EventoId { get; set; }
        public int ClienteId { get; set; }
    }
}