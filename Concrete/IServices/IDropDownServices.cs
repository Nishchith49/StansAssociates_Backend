using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IDropDownServices
    {
        Task<List<RouteDropDownModel>> GetRouteDropDown();
        Task<List<DropDownModel>> GetModuleDropDown();
        Task<List<DropDownModel>> GetSchoolDropDown();
    }
}
