using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Alojamiento.Business.Interfaces;
using Alojamiento.Business.DTOs;
using Alojamiento.DataManagement.Context;
using Alojamiento.Domain.Entities.Seguridad;

namespace Alojamiento.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly AlojamientoDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AlojamientoDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            var exists = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (exists) throw new InvalidOperationException("El correo ya está en uso.");

            // Crear Usuario base
            var usuario = new Usuario
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                NombreCompleto = dto.NombreCompleto,
                Telefono = dto.Telefono,
                EmailVerificado = false
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(); // Para obtener el ID

            // Asignar rol Cliente o Anfitrion en base a dto.EsAnfitrion
            var rolName = dto.EsAnfitrion ? "Anfitrion" : "Cliente";
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == rolName);
            if (rol != null)
            {
                _context.UsuarioRoles.Add(new UsuarioRol { UsuarioId = usuario.UsuarioId, RolId = rol.RolId });
            }

            // Crear perfil específico
            if (dto.EsAnfitrion)
            {
                _context.Anfitriones.Add(new Anfitrion { UsuarioId = usuario.UsuarioId, Verificado = false });
            }
            else
            {
                _context.Clientes.Add(new Cliente { UsuarioId = usuario.UsuarioId });
            }

            await _context.SaveChangesAsync();

            return await GenerateAuthResponse(usuario, rolName);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuariosRoles)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null) throw new InvalidOperationException("Credenciales inválidas.");

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!isValid) throw new InvalidOperationException("Credenciales inválidas.");

            var rolName = usuario.UsuariosRoles.FirstOrDefault()?.Rol?.Nombre ?? "Cliente";

            return await GenerateAuthResponse(usuario, rolName);
        }

        private Task<AuthResponseDTO> GenerateAuthResponse(Usuario usuario, string rolName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyStr = _configuration["Jwt:Key"] ?? "ClaveSuperSecretaParaDesarrolloQueDeberiaEstarEnVariablesDeEntorno123";
            var key = Encoding.ASCII.GetBytes(keyStr);
            var issuer = _configuration["Jwt:Issuer"] ?? "AlojamientoApi";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                    new Claim(ClaimTypes.Role, rolName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new AuthResponseDTO
            {
                Token = tokenHandler.WriteToken(token),
                User = new UserDetailsDTO
                {
                    Id = usuario.UsuarioId,
                    Correo = usuario.Email,
                    Nombre = usuario.NombreCompleto,
                    Rol = rolName
                }
            };

            return Task.FromResult(response);
        }
    }
}
