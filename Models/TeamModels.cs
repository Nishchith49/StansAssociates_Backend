using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class AddTeam
    {
        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("permissions")]
        public List<TeamPermissions> Permissions { get; set; } = [];
    }

    public class TeamPermissions
    {
        [JsonProperty("moduleId")]
        public long ModuleId { get; set; }

        [JsonProperty("canView")]
        public bool CanView { get; set; }

        [JsonProperty("canAdd")]
        public bool CanAdd { get; set; }

        [JsonProperty("canEdit")]
        public bool CanEdit { get; set; }

        [JsonProperty("canDelete")]
        public bool CanDelete { get; set; }
    }

    public class GetTeamPermissions : TeamPermissions
    {
        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }
    }


    public class UpdateTeam : AddTeam
    {
        [JsonProperty("teamId")]
        public long TeamId { get; set; }
    }


    public class GetTeamsModel : UpdateTeam
    {
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [JsonProperty("orderSequence")]
        public int OrderSequence { get; set; }

        [JsonProperty("isVisible")]
        public bool IsVisible { get; set; }

        [JsonProperty("permissions")]
        public new List<GetTeamPermissions> Permissions { get; set; }
    }


    public class TeamIdModel
    {
        [JsonProperty("teamId")]
        public long TeamId { get; set; }
    }


    public class TeamOrderSequenceModel
    {
        [JsonProperty("teamOrderSequence")]
        public List<TeamOrderSequence> TeamOrderSequence { get; set; }
    }


    public class TeamOrderSequence : TeamIdModel
    {
        [JsonProperty("orderSequence")]
        public int OrderSequence { get; set; }
    }
}
