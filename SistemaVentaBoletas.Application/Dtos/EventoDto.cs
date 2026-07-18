using System;

namespace SistemaVentaBoletas.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int CuposDisponibles { get; set; }
    }
}