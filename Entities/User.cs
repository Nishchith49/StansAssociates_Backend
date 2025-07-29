using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("user")]
    public class User : IdAndDatesWithIsActiveAndIsDeleted
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            RefreshTokens = new HashSet<RefreshToken>();
            Schools = new HashSet<User>();
            TeamPermissions = new HashSet<TeamPermission>();
            Students = new HashSet<Student>();
            Routes = new HashSet<Route>();
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("email_id")]
        public string EmailId { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("dob", TypeName = "date")]
        public DateTime? DOB { get; set; }

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
        public string? ProfilePicture { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("school_id")]
        public long? SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        [InverseProperty(nameof(User.Schools))]
        public User School { get; set; }

        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [InverseProperty(nameof(RefreshToken.User))]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        [InverseProperty(nameof(User.School))]
        public virtual ICollection<User> Schools { get; set; }

        [InverseProperty(nameof(TeamPermission.Teams))]
        public virtual ICollection<TeamPermission> TeamPermissions { get; set; }

        [InverseProperty(nameof(Student.School))]
        public virtual ICollection<Student> Students { get; set; }

        [InverseProperty(nameof(Route.School))]
        public virtual ICollection<Route> Routes { get; set; }
    }
}
