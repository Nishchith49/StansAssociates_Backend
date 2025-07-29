using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class TeamServices : ITeamServices
    {
        private readonly StansassociatesAntonyContext _context;
        private readonly IStorageServices _storageServices;
        private readonly IAccountServices _accountServices;

        public TeamServices(StansassociatesAntonyContext context,
                            IStorageServices storageServices,
                            IAccountServices accountServices)
        {
            _context = context;
            _storageServices = storageServices;
            _accountServices = accountServices;
        }


        public async Task<APIResponse> AddTeam(AddTeam model)
        {
            var team = await _context.Users
                                     .Include(x => x.TeamPermissions)
                                     .FirstOrDefaultAsync(x => x.Id == model.TeamId);
            if (team is null)
                return new(ResponseConstants.InvalidId, 400);

            if (model.Permissions == null || model.Permissions.Count == 0)
                return new("At least one permission is required.", 400);

            if (team.TeamPermissions == null)
                team.TeamPermissions = new List<TeamPermission>();

            model.Permissions.ForEach(x =>
            {
                team.TeamPermissions.Add(new TeamPermission
                {
                    CanAdd = x.CanAdd,
                    CanDelete = x.CanDelete,
                    CanEdit = x.CanEdit,
                    CanView = x.CanView,
                    ModuleId = x.ModuleId,
                    TeamId = team.Id
                });
            });
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdateTeam(UpdateTeam model)
        {
            var team = await _context.Users
                                     .Include(x => x.TeamPermissions)
                                     .FirstOrDefaultAsync(x => x.Id == model.TeamId);
            if (team is null)
                return new(ResponseConstants.InvalidId, 400);

            if (model.Permissions == null || model.Permissions.Count == 0)
                return new("At least one permission is required.", 400);

            team.TeamPermissions.Clear();

            model.Permissions.ForEach(x =>
            {
                team.TeamPermissions.Add(new TeamPermission
                {
                    CanAdd = x.CanAdd,
                    CanDelete = x.CanDelete,
                    CanEdit = x.CanEdit,
                    CanView = x.CanView,
                    ModuleId = x.ModuleId,
                    TeamId = team.Id
                });
            });
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }
    }
}

