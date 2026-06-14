namespace SistemaVentaBoletasAPI.Models
{
    public class Evento
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime Fecha { get; set; }

        public string Lugar { get; set; }

        public decimal Precio { get; set; }

        public int CuposDisponibles { get; set; }

        public ICollection<Boleta>? Boletas { get; set; }

        public ICollection<Venta>? Ventas { get; set; }
    }
}
