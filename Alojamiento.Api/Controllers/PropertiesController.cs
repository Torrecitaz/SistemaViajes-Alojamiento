using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.Domain.Enums;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alojamiento.Api.Controllers
{
    [Authorize(Roles = "Anfitrion,Admin")]
    [ApiController]
    [Route("api/v1/properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IWebHostEnvironment _env;

        public PropertiesController(IPropertyService propertyService, IWebHostEnvironment env)
        {
            _propertyService = propertyService;
            _env = env;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperties(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10,
            [FromQuery] TipoAlojamiento? tipo = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] bool? admitenMascotas = null)
        {
            var pagedProperties = await _propertyService.GetPropertiesAsync(pageNumber, pageSize, tipo, maxPrice, admitenMascotas);

            // Exponer metadatos de paginación en el header
            var metadata = new
            {
                pagedProperties.TotalCount,
                pagedProperties.PageSize,
                pagedProperties.CurrentPage,
                pagedProperties.TotalPages,
                pagedProperties.HasNext,
                pagedProperties.HasPrevious
            };
            
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(pagedProperties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProperty([FromForm] PropertyCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized(new { message = "Usuario no válido o sesión expirada." });
            }

            try
            {
                var response = await _propertyService.CreatePropertyAsync(dto, usuarioId, _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
                return CreatedAtAction(nameof(GetProperty), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al crear propiedad.", detail = ex.Message });
            }
        }
    }
}
