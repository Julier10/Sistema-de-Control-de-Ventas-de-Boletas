namespace SistemaVentaBoletasAPI.DTOs
{
    public class EventoDTO
    {
        public string Nombre { get; set; }

        public DateTime Fecha { get; set; }

        public string Lugar { get; set; }

        public decimal Precio { get; set; }

        public int CuposDisponibles { get; set; }
    }
}
