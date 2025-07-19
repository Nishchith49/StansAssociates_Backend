using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("route")]
    public partial class Route : IdAndDatesWithIsActiveAndIsDeleted
    {
        public Route()
        {
            Students = new List<Student>();
        }

        [Column("bus_no")]
        public string BusNo { get; set; }

        [Column("boarding_point")]
        public string BoardingPoint { get; set; }

        [Column("route_cost")]
        public decimal RouteCost { get; set; }

        [InverseProperty(nameof(Student.Route))]
        public virtual ICollection<Student> Students { get; set; }
    }
}
