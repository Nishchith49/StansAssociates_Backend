using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IStudentServices
    {
        Task<APIResponse> AddStudent(AddStudentModel model);
        Task<APIResponse> UpdateStudent(UpdateStudentModel model);
        Task<PagedResponse<List<GetStudentModel>>> GetStudents(GetStudentsFilterModel model);
        Task<ServiceResponse<GetStudentModel>> GetStudent(long id);
        Task<APIResponse> DeleteStudent(long id);
        Task<APIResponse> AddStudentFee(AddStudentFeeModel model);
        Task<PagedResponse<List<GetStudentFeeDetailsModel>>> GetStudentFees(GetStudentFeesFilterModel model);
        Task<APIResponse> AddSession(AddSessionModel model);
        Task<PagedResponse<List<GetSessionModel>>> GetSessions(PagedResponseInput model);
    }
}
