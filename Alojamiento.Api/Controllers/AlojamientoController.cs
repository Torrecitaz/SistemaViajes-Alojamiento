using Alojamiento.Business.Interfaces;
using Alojamiento.Domain.Entities;
using Alojamiento.Domain.Entities.Alojamientos;
using Alojamiento.Domain.Entities.Reservas;
using Microsoft.AspNetCore.Mvc;

namespace Alojamiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlojamientoController : ControllerBase
    {
        private readonly IAlojamientoService _alojamientoService;

        public AlojamientoController(IAlojamientoService alojamientoService)
        {
            _alojamientoService = alojamientoService;
        }

        // GET: api/Alojamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propiedad>>> GetPropiedades()
        {
            var propiedades = await _alojamientoService.ObtenerPropiedadesAsync();
            return Ok(propiedades);
        }

        // POST: api/Alojamiento/propiedad
        [HttpPost("propiedad")]
        public async Task<ActionResult<Propiedad>> PostPropiedad(Propiedad propiedad)
        {
            var nuevaPropiedad = await _alojamientoService.CrearPropiedadAsync(propiedad);
            return Ok(nuevaPropiedad);
        }

        // POST: api/Alojamiento/reserva
        [HttpPost("reserva")]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            try
            {
                var nuevaReserva = await _alojamientoService.ProcesarReservaAsync(reserva);
                return Ok(nuevaReserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}