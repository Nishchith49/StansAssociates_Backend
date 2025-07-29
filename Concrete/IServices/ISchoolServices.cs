using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface ISchoolServices
    {
        Task<APIResponse> AddSchool(AddSchoolModel model);
        Task<APIResponse> UpdateSchool(UpdateSchoolModel model);
        Task<PagedResponse<List<GetSchoolModel>>> GetSchools(PagedResponseInput model);
        Task<ServiceResponse<GetSchoolModel>> GetSchool(long id);
        Task<APIResponse> DeleteSchool(long id);
    }
}
