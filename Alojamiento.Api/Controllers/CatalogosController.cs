using Microsoft.AspNetCore.Mvc;
using Alojamiento.DataManagement.Context;
using Microsoft.EntityFrameworkCore;

namespace Alojamiento.Api.Controllers
{
    /// <summary>
    /// Catálogos: ciudades, tipos de alojamiento, servicios (para poblar selects del frontend)
    /// </summary>
    [ApiController]
    [Route("api/v1/catalogos")]
    public class CatalogosController : ControllerBase
    {
        private readonly AlojamientoDbContext _context;

        public CatalogosController(AlojamientoDbContext context)
        {
            _context = context;
        }

        /// <summary>Listar todas las ciudades con su país</summary>
        [HttpGet("ciudades")]
        public async Task<IActionResult> GetCiudades()
        {
            var ciudades = await _context.Ciudades
                .Include(c => c.Pais)
                .Where(c => !c.EliminadoLogico)
                .OrderBy(c => c.Nombre)
                .Select(c => new
                {
                    c.CiudadId,
                    c.Nombre,
                    pais = c.Pais != null ? c.Pais.Nombre : "",
                    c.EsCapital
                })
                .ToListAsync();

            return Ok(ciudades);
        }

        /// <summary>Listar tipos de alojamiento</summary>
        [HttpGet("tipos-alojamiento")]
        public async Task<IActionResult> GetTiposAlojamiento()
        {
            var tipos = await _context.TiposAlojamiento
                .Where(t => !t.EliminadoLogico)
                .OrderBy(t => t.Nombre)
                .Select(t => new { t.TipoAlojamientoId, t.Nombre, t.Descripcion })
                .ToListAsync();

            return Ok(tipos);
        }

        /// <summary>Listar servicios/amenities disponibles</summary>
        [HttpGet("servicios")]
        public async Task<IActionResult> GetServicios()
        {
            var servicios = await _context.Servicios
                .Where(s => !s.EliminadoLogico)
                .OrderBy(s => s.Nombre)
                .Select(s => new { s.ServicioId, s.Nombre, s.Icono })
                .ToListAsync();

            return Ok(servicios);
        }

        /// <summary>Listar monedas disponibles</summary>
        [HttpGet("monedas")]
        public async Task<IActionResult> GetMonedas()
        {
            var monedas = await _context.Monedas
                .Where(m => !m.EliminadoLogico)
                .Select(m => new { m.MonedaId, m.Nombre, m.Codigo, m.Simbolo })
                .ToListAsync();

            return Ok(monedas);
        }

        /// <summary>Listar países disponibles</summary>
        [HttpGet("paises")]
        public async Task<IActionResult> GetPaises()
        {
            var paises = await _context.Paises
                .Where(p => !p.EliminadoLogico)
                .OrderBy(p => p.Nombre)
                .Select(p => new { p.PaisId, p.Nombre, p.CodigoISO2, p.CodigoISO3 })
                .ToListAsync();

            return Ok(paises);
        }
    }
}
