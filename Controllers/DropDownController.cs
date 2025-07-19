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


        [HttpGet("[action]")]
        public async Task<List<DropDownModel>> GetRouteDropDown()
        {
            return await _dropDownServices.GetRouteDropDown();
        }
    }
}
