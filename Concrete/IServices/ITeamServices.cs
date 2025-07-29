using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface ITeamServices
    {
        Task<APIResponse> AddTeam(AddTeam model);
        Task<APIResponse> UpdateTeam(UpdateTeam model);
    }
}
