using System.Threading.Tasks;
using Alojamiento.Business.Utils;
using Alojamiento.Business.DTOs;
using Alojamiento.Domain.Enums;

namespace Alojamiento.Business.Interfaces
{
    public interface IPropertyService
    {
        Task<PagedList<PropertyResponseDTO>> GetPropertiesAsync(int pageNumber, int pageSize, Alojamiento.Domain.Enums.TipoAlojamiento? tipo, decimal? maxPrice, bool? admitenMascotas);
        Task<PropertyResponseDTO?> GetPropertyByIdAsync(int id);
        Task<PropertyResponseDTO> CreatePropertyAsync(PropertyCreateDTO dto, int usuarioId, string webRootPath);
    }
}
