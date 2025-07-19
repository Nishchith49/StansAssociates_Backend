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
    public class RouteController : ControllerBase
    {
        private readonly IRouteServices _routeServices;

        public RouteController(IRouteServices routeServices)
        {
            _routeServices = routeServices;
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddRoute(AddRouteModel model)
        {
            return await _routeServices.AddRoute(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateRoute(UpdateRouteModel model)
        {
            return await _routeServices.UpdateRoute(model);
        }


        [HttpPost("[action]")]
        public async Task<PagedResponse<List<GetRouteModel>>> GetRoutes(PagedResponseInput model)
        {
            return await _routeServices.GetRoutes(model);
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<List<GetRouteModel>>> GetRoute(long id)
        {
            return await _routeServices.GetRoute(id);
        }


        [HttpDelete("[action]")]
        public async Task<APIResponse> DeleteRoute(long id)
        {
            return await _routeServices.DeleteRoute(id);
        }
    }
}