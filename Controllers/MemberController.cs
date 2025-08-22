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
    public class MemberController : ControllerBase
    {
        private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddStaff(AddStaffModel model)
        {
            return await _memberServices.AddStaff(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateStaff(UpdateStaffModel model)
        {
            return await _memberServices.UpdateStaff(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetStaffModel>>> GetStaffs(PagedResponseInput model)
        {
            return await _memberServices.GetStaffs(model);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<GetStaffModel>> GetStaff(long id)
        {
            return await _memberServices.GetStaff(id);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteStaff(long id)
        {
            return await _memberServices.DeleteStaff(id);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddTeacher(AddTeacherModel model)
        {
            return await _memberServices.AddTeacher(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateTeacher(UpdateTeacherModel model)
        {
            return await _memberServices.UpdateTeacher(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetTeacherModel>>> GetTeachers(PagedResponseInput model)
        {
            return await _memberServices.GetTeachers(model);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<GetTeacherModel>> GetTeacher(long id)
        {
            return await _memberServices.GetTeacher(id);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteTeacher(long id)
        {
            return await _memberServices.DeleteTeacher(id);
        }
    }
}
