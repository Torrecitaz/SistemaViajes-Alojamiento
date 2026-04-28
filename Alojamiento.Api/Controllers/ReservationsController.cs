using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;

namespace Alojamiento.Api.Controllers
{
    [Authorize(Roles = "Cliente,Admin")]
    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public ReservationsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reserva = await _bookingService.CreateBookingAsync(bookingDto);
                return CreatedAtAction(nameof(GetBooking), new { id = reserva.ReservaId }, reserva);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            // Placeholder: En un entorno real se llamaría al servicio
            return Ok(new { Id = id, Status = "Details pending implementation" });
        }
    }
}
