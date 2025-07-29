using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("team_permissions")]
    public class TeamPermission : PrimaryKey
    {
        [Column("team_id")]
        public long TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        [InverseProperty(nameof(User.TeamPermissions))]
        public User Teams { get; set; }

        [Column("module_id")]
        public long ModuleId { get; set; }

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty(nameof(Module.TeamPermissions))]
        public Module Module { get; set; }

        [Column("can_view")]
        public bool CanView { get; set; }

        [Column("can_add")]
        public bool CanAdd { get; set; }

        [Column("can_edit")]
        public bool CanEdit { get; set; }

        [Column("can_delete")]
        public bool CanDelete { get; set; }
    }
}
