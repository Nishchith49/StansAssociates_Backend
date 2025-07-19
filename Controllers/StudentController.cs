using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiExplorerSettings(GroupName = "StansAssociates Backend Admin")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddStudent(AddStudentModel model)
        {
            return await _studentServices.AddStudent(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateStudent(UpdateStudentModel model)
        {
            return await _studentServices.UpdateStudent(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetStudentModel>>> GetStudents(GetStudentsFilterModel model)
        {
            return await _studentServices.GetStudents(model);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<List<GetStudentModel>>> GetStudent(long id)
        {
            return await _studentServices.GetStudent(id);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteStudent(long id)
        {
            return await _studentServices.DeleteStudent(id);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddStudentFee(AddStudentFeeModel model)
        {
            return await _studentServices.AddStudentFee(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetStudentFeeDetailsModel>>> GetStudentFees(GetStudentFeesFilterModel model)
        {
            return await _studentServices.GetStudentFees(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddSession(AddSessionModel model)
        {
            return await _studentServices.AddSession(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetSessionModel>>> GetSessions(PagedResponseInput model)
        {
            return await _studentServices.GetSessions(model);
        }
    }
}
