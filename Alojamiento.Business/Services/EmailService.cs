using Alojamiento.Business.Interfaces;

namespace Alojamiento.Business.Services
{
    public class EmailService : IEmailService
    {
        public async Task EnviarCorreoConfirmacionAsync(string destino, string asunto, string cuerpo)
        {
            // Aquí iría la integración con SendGrid, MailKit, AWS SES, etc.
            // Por ahora solo simularemos el envío:
            Console.WriteLine($"Simulando envío de correo a {destino} - Asunto: {asunto}");
            await Task.Delay(500); // Simulación de tiempo de red
        }
    }
}
