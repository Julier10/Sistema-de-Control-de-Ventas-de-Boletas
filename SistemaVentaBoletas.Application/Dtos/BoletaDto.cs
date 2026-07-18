namespace SistemaVentaBoletas.Application.Dtos
{
    public class BoletaDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int EventoId { get; set; }
    }
}