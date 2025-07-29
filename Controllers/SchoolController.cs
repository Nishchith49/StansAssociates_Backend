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
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolServices _schoolServices;

        public SchoolController(ISchoolServices schoolServices)
        {
            _schoolServices = schoolServices;
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddSchool(AddSchoolModel model)
        {
            return await _schoolServices.AddSchool(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateStaff(UpdateSchoolModel model)
        {
            return await _schoolServices.UpdateSchool(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetSchoolModel>>> GetSchools(PagedResponseInput model)
        {
            return await _schoolServices.GetSchools(model);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<GetSchoolModel>> GetSchool(long id)
        {
            return await _schoolServices.GetSchool(id);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteSchool(long id)
        {
            return await _schoolServices.DeleteSchool(id);
        }
    }
}
