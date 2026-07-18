using SistemaVentaBoletas.Domain.Core;

namespace SistemaVentaBoletas.Domain.Entities
{
    public class Boleta : BaseEntity
    {
        public string Codigo { get; set; }
        public string Estado { get; set; }

        public int EventoId { get; set; }
        public Evento? Evento { get; set; }
    }
}