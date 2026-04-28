namespace Alojamiento.Business.Interfaces
{
    public interface IEmailService
    {
        Task EnviarCorreoConfirmacionAsync(string destino, string asunto, string cuerpo);
    }
}
