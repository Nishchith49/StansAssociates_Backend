using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("refresh_token")]
    public class RefreshToken : IdWithCreatedDate
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.RefreshTokens))]
        public User User { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("expires")]
        public DateTime Expires { get; set; }
    }
}
