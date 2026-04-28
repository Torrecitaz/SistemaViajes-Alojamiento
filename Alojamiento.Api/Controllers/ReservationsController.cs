using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;

namespace Alojamiento.Api.Controllers
{
    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public ReservationsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>Crear una nueva reserva</summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO bookingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                return Unauthorized(new { message = "Sesión expirada. Inicia sesión de nuevo." });

            try
            {
                var reserva = await _bookingService.CreateBookingAsync(bookingDto, usuarioId);
                return Ok(new
                {
                    message = "Reserva creada exitosamente",
                    reservaId = reserva.ReservaId,
                    codigo = reserva.CodigoReserva,
                    total = reserva.Total,
                    estado = reserva.EstadoReserva,
                    checkIn = reserva.FechaCheckIn,
                    checkOut = reserva.FechaCheckOut
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear reserva.", detail = ex.Message });
            }
        }

        /// <summary>Obtener detalle de una reserva</summary>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetBooking(int id)
        {
            return Ok(new { Id = id, Status = "Pendiente de implementación completa" });
        }
    }
}
