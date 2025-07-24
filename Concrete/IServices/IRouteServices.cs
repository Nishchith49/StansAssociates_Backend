using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IRouteServices
    {
        Task<APIResponse> AddRoute(AddRouteModel model);
        Task<APIResponse> UpdateRoute(UpdateRouteModel model);
        Task<PagedResponse<List<GetRouteModel>>> GetRoutes(PagedResponseInput model);
        Task<ServiceResponse<GetRouteModel>> GetRoute(long id);
        Task<APIResponse> DeleteRoute(long id);
    }
}
