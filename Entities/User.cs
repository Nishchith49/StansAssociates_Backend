using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("user")]
    public class User  : IdAndDatesWithIsActiveAndIsDeleted
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("email_id")]
        public string EmailId { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("dob", TypeName = "date")]
        public DateTime DOB { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("pincode")]
        public string Pincode { get; set; }

        [Column("Profile_picture")]
        public string ProfilePicture { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [InverseProperty(nameof(RefreshToken.User))]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
