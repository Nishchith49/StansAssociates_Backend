using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IDropDownServices
    {
        Task<List<DropDownModel>> GetRouteDropDown();
    }
}
