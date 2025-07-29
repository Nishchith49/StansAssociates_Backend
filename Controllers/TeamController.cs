using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "School")]
    [ApiExplorerSettings(GroupName = "StansAssociates Backend Admin")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamServices _teamServices;

        public TeamController(ITeamServices teamServices)
        {
            _teamServices = teamServices;
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> AddTeam(AddTeam model)
        {
            return await _teamServices.AddTeam(model);
        }


        [HttpPost("[action]")]
        public async Task<APIResponse> UpdateTeam(UpdateTeam model)
        {
            return await _teamServices.UpdateTeam(model);
        }
    }
}
