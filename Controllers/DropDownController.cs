using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Concrete.Services;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly IDropDownServices _dropDownServices;

        public DropDownController(IDropDownServices dropDownServices)
        {
            _dropDownServices = dropDownServices;
        }


        [Authorize]
        [HttpGet("[action]")]
        public async Task<List<RouteDropDownModel>> GetRouteDropDown()
        {
            return await _dropDownServices.GetRouteDropDown();
        }


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetModuleDropDown()
        {
            return await _dropDownServices.GetModuleDropDown();
        }


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetSchoolDropDown()
        {
            return await _dropDownServices.GetSchoolDropDown();
        }
    }
}
