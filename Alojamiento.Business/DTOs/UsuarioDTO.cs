namespace Alojamiento.Business.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string FotoPerfilUrl { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public double Calificacion { get; set; }
        public bool EsColaborador { get; set; }
    }
}
