namespace SistemaVentaBoletasAPI.Models
{
    public class Boleta
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Estado { get; set; }

        public int EventoId { get; set; }

        public Evento? Evento { get; set; }
    }
}