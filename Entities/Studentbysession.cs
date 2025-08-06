using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("studentbysession")]
    public partial class Studentbysession : PrimaryKey
    {
        [Column("student_id")]
        public long StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Student.Studentbysessions))]
        public Student Student { get; set; }

        [Required]
        [Column("class")]
        public string Class { get; set; }

        [Column("session_id")]
        public long SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        [InverseProperty(nameof(Session.Studentbysessions))]
        public Session Session { get; set; }
    }
}
