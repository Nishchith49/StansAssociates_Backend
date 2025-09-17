using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("employee_attendance")]
    public class EmployeeAttendance : IdWithDateColumns
    {
        [Column("employee_id")]
        public long EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Employee.EmployeeAttendances))]
        public Employee Employee { get; set; }

        [Column("attendance_date")]
        public DateOnly AttendanceDate { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("remarks")]
        public string? Remarks { get; set; }
    }
}
