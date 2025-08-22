using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Staff")]
    [ApiExplorerSettings(GroupName = "StansAssociates Backend Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardServices _dashboardServices;

        public DashboardController(IDashboardServices dashboardServices)
        {
            _dashboardServices = dashboardServices;
        }


        [HttpGet("[action]")]
        public async Task<ServiceResponse<GetDashboardModel>> GetDashboard()
        {
            return await _dashboardServices.GetDashboard();
        }
    }
}
