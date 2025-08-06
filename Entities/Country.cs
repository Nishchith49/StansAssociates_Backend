using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("country")]
    public class Country : PrimaryKey
    {
        public Country()
        {
            States = new HashSet<State>();
            Users = new HashSet<User>();
            Students = new HashSet<Student>();
        }

        [Column("name")]
        public string Name { get; set; }

        [Column("iso2")]
        public string Iso2 { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("latitude", TypeName = "decimal(18,8)")]
        public decimal Latitude { get; set; }

        [Column("longitude", TypeName = "decimal(18,8)")]
        public decimal Longitude { get; set; }

        [InverseProperty(nameof(State.Country))]
        public virtual ICollection<State> States { get; set; }

        [InverseProperty(nameof(User.Country))]
        public virtual ICollection<User> Users { get; set; }

        [InverseProperty(nameof(Student.Country))]
        public virtual ICollection<Student> Students { get; set; }
    }
}
