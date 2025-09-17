using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
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
        public async Task<List<RouteDropDownModel>> GetRouteDropDown(long? schoolId)
        {
            return await _dropDownServices.GetRouteDropDown(schoolId);
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


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetCityDropDown(long stateId)
        {
            return await _dropDownServices.GetCityDropDown(stateId);
        }


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetStateDropDown(long countryId)
        {
            return await _dropDownServices.GetStateDropDown(countryId);
        }


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetCountryDropDown()
        {
            return await _dropDownServices.GetCountryDropDown();
        }


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetEmployeeDropDown()
        {
            return await _dropDownServices.GetEmployeeDropDown();
        }


        [HttpGet("[action]")]
        public async Task<APIResponse> InsertIndiaData()
        {
            return await _dropDownServices.InsertIndiaData();
        }
    }
}
