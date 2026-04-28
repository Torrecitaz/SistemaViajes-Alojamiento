using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using System.Text.Json;

namespace Alojamiento.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // TC-F01, TC-F02, TC-F03, TC-S16, TC-S17, TC-S20
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] UsuarioCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // TC-F03, TC-S16, TC-S17
            }

            try
            {
                var usuario = await _usuarioService.RegistrarUsuarioAsync(dto);
                return CreatedAtAction(nameof(ObtenerCliente), new { id = usuario.Id }, usuario); // TC-F01
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message }); // TC-F02
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> ObtenerMiPerfil()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId)) return Unauthorized();

            var usuario = await _usuarioService.ObtenerPorIdAsync(userId);
            if (usuario == null) return NotFound(new { message = "Usuario no encontrado" });
            return Ok(usuario);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerCliente(int id)
        {
            var usuario = await _usuarioService.ObtenerPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Cliente no encontrado" }); // TC-F05
            }
            return Ok(usuario); // TC-F04
        }

        // TC-F06, TC-F07
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarClientes([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string? nombre = null)
        {
            var pagedResult = await _usuarioService.ListarUsuariosAsync(page, size, nombre);

            var metadata = new
            {
                pagedResult.TotalCount,
                pagedResult.PageSize,
                pagedResult.CurrentPage,
                pagedResult.TotalPages,
                pagedResult.HasNext,
                pagedResult.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata)); // TC-F06

            return Ok(pagedResult); // TC-F07
        }

        // TC-F08
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] UsuarioCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var usuario = await _usuarioService.ActualizarUsuarioAsync(id, dto);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // TC-F09
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> ActualizarTelefono(int id, [FromBody] string telefono)
        {
            try
            {
                var usuario = await _usuarioService.ActualizarParcialAsync(id, telefono);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // TC-F10
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            await _usuarioService.EliminarUsuarioAsync(id);
            return NoContent();
        }
    }
}