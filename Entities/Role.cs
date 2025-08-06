using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("role")]
    public class Role : PrimaryKey
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Column("name")]
        public string Name { get; set; }

        [InverseProperty(nameof(UserRole.Role))]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
