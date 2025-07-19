using JobPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("session")]
    public partial class Session : IdWithCreatedDate
    {
        public Session()
        {
            Studentbysessions = new HashSet<Studentbysession>();
        }

        [Column("name")]
        public string Name { get; set; }

        [InverseProperty(nameof(Studentbysession.Session))]
        public virtual ICollection<Studentbysession> Studentbysessions { get; set; }
    }
}
