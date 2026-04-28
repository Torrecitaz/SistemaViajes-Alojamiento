using System.Threading.Tasks;
using Alojamiento.Business.DTOs;

namespace Alojamiento.Business.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
    }
}
