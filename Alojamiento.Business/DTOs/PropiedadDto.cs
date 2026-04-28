namespace Alojamiento.Business.DTOs
{
    public class PropiedadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string TipoAlojamiento { get; set; } = string.Empty;
        public decimal PrecioPorNoche { get; set; }
        public string Moneda { get; set; } = "USD";
        public bool TieneWifi { get; set; }
        public bool AdmiteMascotas { get; set; }
        public bool TienePiscina { get; set; }
        public double Calificacion { get; set; }
        public List<string> ImagenesUrls { get; set; } = new();
    }
}
