using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("employee")]
    public class Employee : IdAndDatesWithIsActiveAndIsDeleted
    {
        public Employee()
        {
            EmployeeAttendances = new HashSet<EmployeeAttendance>();
        }

        [Column("emp_code")]
        public string EmpCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("designation")]
        public string Designation { get; set; }

        [Column("tdp")]
        public int TDP { get; set; }

        [Column("tad")]
        public int TAD { get; set; }

        [Column("thl")]
        public int THL { get; set; }

        [InverseProperty(nameof(EmployeeAttendance.Employee))]
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}
