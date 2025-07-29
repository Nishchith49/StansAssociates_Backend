using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("module")]
    public class Module : PrimaryKey
    {
        public Module()
        {
            TeamPermissions = new List<TeamPermission>();
        }

        [Column("name")]
        public string Name { get; set; }

        [InverseProperty(nameof(TeamPermission.Module))]
        public virtual ICollection<TeamPermission> TeamPermissions { get; set; }
    }
}
