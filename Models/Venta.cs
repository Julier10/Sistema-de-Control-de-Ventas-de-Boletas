namespace SistemaVentaBoletasAPI.Models
{
    public class Venta
    {
        public int Id { get; set; }

        public DateTime FechaVenta { get; set; }

        public int CantidadBoletas { get; set; }

        public decimal Total { get; set; }

        public int EventoId { get; set; }

        public Evento? Evento { get; set; }

        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}