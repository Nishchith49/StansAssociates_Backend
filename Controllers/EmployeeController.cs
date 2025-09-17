using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiExplorerSettings(GroupName = "StansAssociates Backend Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddEmployee(AddEmployeeModel model)
        {
            return await _employeeServices.AddEmployee(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateEmployee(UpdateEmployeeModel model)
        {
            return await _employeeServices.UpdateEmployee(model);
        }


        [HttpGet("[action]")]
        public async Task<APIResponse> EnableOrDisableEmployee(long id)
        {
            return await _employeeServices.EnableOrDisableEmployee(id);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetEmployeeModel>>> GetEmployees(GetEmployeesFilterModel model)
        {
            return await _employeeServices.GetEmployees(model);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<GetEmployeeModel>> GetEmployee(long id)
        {
            return await _employeeServices.GetEmployee(id);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteEmployee(long id)
        {
            return await _employeeServices.DeleteEmployee(id);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddEmployeeAttendance(AddEmployeeAttendanceModel model)
        {
            return await _employeeServices.AddEmployeeAttendance(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> BulkAddEmployeeAttendance(BulkAddEmployeeAttendanceModel model)
        {
            return await _employeeServices.BulkAddEmployeeAttendance(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateEmployeeAttendance(UpdateEmployeeAttendanceModel model)
        {
            return await _employeeServices.UpdateEmployeeAttendance(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetEmployeeAttendanceModel>>> GetEmployeeAttendances(GetEmployeeAttendancesFilterModel model)
        {
            return await _employeeServices.GetEmployeeAttendances(model);
        }
    }
}
