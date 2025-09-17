using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IDropDownServices
    {
        Task<List<RouteDropDownModel>> GetRouteDropDown(long? schoolId);
        Task<List<DropDownModel>> GetModuleDropDown();
        Task<List<DropDownModel>> GetSchoolDropDown();
        Task<List<DropDownModel>> GetCityDropDown(long stateId);
        Task<List<DropDownModel>> GetStateDropDown(long countryId);
        Task<List<DropDownModel>> GetCountryDropDown();
        Task<List<DropDownModel>> GetEmployeeDropDown();
        Task<APIResponse> InsertIndiaData();
    }
}
