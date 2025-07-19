using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("user_role")]
    public class UserRole : PrimaryKey
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.UserRoles))]
        public User User { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; } 

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Role.UserRoles))]
        public Role Role { get; set; }
    }
}
