using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IMemberServices
    {
        Task<APIResponse> AddStaff(AddStaffModel model);
        Task<APIResponse> UpdateStaff(UpdateStaffModel model);
        Task<PagedResponse<List<GetStaffModel>>> GetStaffs(PagedResponseInput model);
        Task<ServiceResponse<List<GetStaffModel>>> GetStaff(long id);
        Task<APIResponse> DeleteStaff(long id);

        Task<APIResponse> AddTeacher(AddTeacherModel model);
        Task<APIResponse> UpdateTeacher(UpdateTeacherModel model);
        Task<PagedResponse<List<GetTeacherModel>>> GetTeachers(PagedResponseInput model);
        Task<ServiceResponse<List<GetTeacherModel>>> GetTeacher(long id);
        Task<APIResponse> DeleteTeacher(long id);
    }
}
