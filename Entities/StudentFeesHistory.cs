using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("student_fees_history")]
    public partial class StudentFeesHistory : IdWithCreatedDate
    {
        [Column("student_id")]
        public long StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Student.StudentFeesHistories))]
        public Student Student { get; set; }

        [Column("paid_mode")]
        public string PaidMode { get; set; } 

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("paid_date", TypeName = "date")]
        public DateTime PaidDate { get; set; }

        [Column("student_by_session_id")]
        public long StudentbysessionId { get; set; }

        [ForeignKey(nameof(StudentbysessionId))]
        [InverseProperty(nameof(Studentbysession.StudentFeesHistories))]
        public Studentbysession Studentbysession { get; set; }
    }
}
