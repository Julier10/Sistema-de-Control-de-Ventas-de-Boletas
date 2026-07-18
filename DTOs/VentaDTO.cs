using System;

namespace SistemaVentaBoletasAPI.DTOs
{
    public class VentaDTO
    {
        public DateTime FechaVenta { get; set; }
        public int CantidadBoletas { get; set; }
        public decimal Total { get; set; }
        public int EventoId { get; set; }
        public int ClienteId { get; set; }
    }
}