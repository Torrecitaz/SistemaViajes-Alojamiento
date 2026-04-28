namespace Alojamiento.Business.DTOs
{
    public class PropiedadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string TipoAlojamientoNombre { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public string Moneda { get; set; } = "USD";
        public double Calificacion { get; set; }
        public int TotalResenas { get; set; }
        public string EstadoPropiedad { get; set; } = string.Empty;
        public bool AdmiteMascotas { get; set; }
        public int CapacidadMaxima { get; set; }
        public List<string> Instalaciones { get; set; } = new();
        public List<string> Fotos { get; set; } = new();
    }
}
