using System.Threading.Tasks;

namespace Alojamiento.Business.Interfaces
{
    public interface ICollaboratorService
    {
        Task<object> GetDashboardMetricsAsync(int collaboratorId);
        Task ReportNoShowAsync(int reservaId);
        Task ApplyPromotionAsync(int propiedadId, int puntosCosto);
    }
}
