using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IDashboardServices
    {
        Task<ServiceResponse<GetDashboardModel>> GetDashboard();
    }
}
