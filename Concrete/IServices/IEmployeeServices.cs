using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IEmployeeServices
    {
        Task<APIResponse> AddEmployee(AddEmployeeModel model);
        Task<APIResponse> UpdateEmployee(UpdateEmployeeModel model);
        Task<APIResponse> EnableOrDisableEmployee(long id);
        Task<PagedResponse<List<GetEmployeeModel>>> GetEmployees(GetEmployeesFilterModel model);
        Task<ServiceResponse<GetEmployeeModel>> GetEmployee(long id);
        Task<APIResponse> DeleteEmployee(long id);
        Task<APIResponse> AddEmployeeAttendance(AddEmployeeAttendanceModel model);
        Task<APIResponse> BulkAddEmployeeAttendance(BulkAddEmployeeAttendanceModel model);
        Task<APIResponse> UpdateEmployeeAttendance(UpdateEmployeeAttendanceModel model);
        Task<PagedResponse<List<GetEmployeeAttendanceModel>>> GetEmployeeAttendances(GetEmployeeAttendancesFilterModel model);
    }
}
