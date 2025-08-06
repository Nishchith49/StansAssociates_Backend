using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("state")]
    public class State : PrimaryKey
    {
        public State()
        {
            Cities = new HashSet<City>();
            Users = new HashSet<User>();
            Students = new HashSet<Student>();
        }

        [Column("state_name")]
        public string StateName { get; set; }

        [Column("iso2")]
        public string Iso2 { get; set; }

        [Column("country_id")]
        public long CountryId { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("latitude", TypeName = "decimal(18,8)")]
        public decimal Latitude { get; set; }

        [Column("longitude", TypeName = "decimal(18,8)")]
        public decimal Longitude { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty(nameof(Country.States))]
        public Country Country { get; set; }

        [InverseProperty(nameof(City.State))]
        public virtual ICollection<City> Cities { get; set; }

        [InverseProperty(nameof(User.State))]
        public virtual ICollection<User> Users { get; set; }

        [InverseProperty(nameof(Student.State))]
        public virtual ICollection<Student> Students { get; set; }
    }
}
