using Alojamiento.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alojamiento.Api.Controllers
{
    [ApiController]
    [Route("api/v1/buscador")]
    public class BuscadorController : ControllerBase
    {
        private readonly IBuscadorService _buscadorService;

        public BuscadorController(IBuscadorService buscadorService)
        {
            _buscadorService = buscadorService;
        }

        /// <summary>Buscar propiedades con filtros</summary>
        [HttpGet]
        public async Task<IActionResult> Buscar(
            [FromQuery] string? ciudad,
            [FromQuery] bool? admiteMascotas,
            [FromQuery] bool? tienePiscina,
            [FromQuery] bool? tieneWifi,
            [FromQuery] decimal? precioMaximo)
        {
            var resultados = await _buscadorService.BuscarPropiedadesAsync(ciudad, admiteMascotas, tienePiscina, tieneWifi, precioMaximo);
            return Ok(resultados);
        }
    }
}
