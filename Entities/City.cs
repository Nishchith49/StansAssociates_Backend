using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("city")]
    public class City : PrimaryKey
    {
        public City()
        {
            Users = new HashSet<User>();
            Students = new HashSet<Student>();
        }

        [Column("city_name")]
        public string CityName { get; set; }

        [Column("state_id")]
        public long StateId { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("latitude", TypeName = "decimal(18,8)")]
        public decimal Latitude { get; set; }

        [Column("longitude", TypeName = "decimal(18,8)")]
        public decimal Longitude { get; set; }

        [ForeignKey(nameof(StateId))]
        [InverseProperty(nameof(State.Cities))]
        public State State { get; set; }

        [InverseProperty(nameof(User.City))]
        public virtual ICollection<User> Users { get; set; }

        [InverseProperty(nameof(Student.City))]
        public virtual ICollection<Student> Students { get; set; }
    }
}
