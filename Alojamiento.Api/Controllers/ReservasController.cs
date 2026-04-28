using Alojamiento.Business.Interfaces;
using Alojamiento.Domain.Entities;
using Alojamiento.Domain.Entities.Reservas;
using Microsoft.AspNetCore.Mvc;

namespace Alojamiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] Reserva reserva)
        {
            var isAvailable = await _reservaService.VerificarDisponibilidadAsync(reserva.PropiedadId, reserva.FechaCheckIn, reserva.FechaCheckOut);
            
            if (!isAvailable)
            {
                return BadRequest("La propiedad no está disponible en las fechas seleccionadas.");
            }

            var nuevaReserva = await _reservaService.RegistrarReservaAsync(reserva);
            return Ok(nuevaReserva);
        }

        // Otros endpoints de consulta de reservas
    }
}
