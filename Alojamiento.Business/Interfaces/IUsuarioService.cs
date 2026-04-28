using System.Threading.Tasks;
using Alojamiento.Domain.Entities;
using Alojamiento.Business.DTOs;
using Alojamiento.Business.Utils;

namespace Alojamiento.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> RegistrarUsuarioAsync(UsuarioCreateDTO usuarioDto);
        Task<UsuarioDTO?> ObtenerPorIdAsync(int id);
        Task<PagedList<UsuarioDTO>> ListarUsuariosAsync(int pageNumber, int pageSize, string? nombre);
        Task<UsuarioDTO> ActualizarUsuarioAsync(int id, UsuarioCreateDTO usuarioDto);
        Task<UsuarioDTO> ActualizarParcialAsync(int id, string telefono); // Simplificación para PATCH
        Task EliminarUsuarioAsync(int id);
        
        // Requerimiento: Sistema de puntos
        Task SumarPuntosAsync(int usuarioId, int puntos);
    }
}