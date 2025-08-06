using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("student")]
    public partial class Student : IdAndDatesWithIsActiveAndIsDeleted
    {
        public Student()
        {
            StudentFeesHistories = new HashSet<StudentFeesHistory>();
            Studentbysessions = new HashSet<Studentbysession>();
        }

        [Column("affiliation")]
        public string Affiliation { get; set; }

        [Column("admission_no")]
        public string AdmissionNo { get; set; }

        [Column("doa", TypeName = "date")]
        public DateTime DOA { get; set; }

        [Column("roll_no")]
        public string? RollNo { get; set; }

        [Column("class")]
        public string Class { get; set; }

        [Column("section")]
        public string Section { get; set; }

        [Column("f_name")]
        public string FName { get; set; }

        [Column("l_name")]
        public string? LName { get; set; }

        [Column("father_name")]
        public string FatherName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("dob", TypeName = "date")]
        public DateTime? DOB { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("routeId")]
        public long RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        [InverseProperty(nameof(Route.Students))]
        public Route Route { get; set; }

        [Column("total_paid")]
        public decimal TotalPaid { get; set; }

        [Column("street")]
        public string? Street { get; set; }

        [Column("pincode")]
        public string? Pincode { get; set; }

        [Column("city_id")]
        public long? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty(nameof(City.Students))]
        public City City { get; set; }

        [Column("state_id")]
        public long? StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        [InverseProperty(nameof(State.Students))]
        public State State { get; set; }

        [Column("country_id")]
        public long? CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty(nameof(Country.Students))]
        public Country Country { get; set; }

        [Column("student_img")]
        public string? StudentImg { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("school_id")]
        public long SchoolId { get; set; }

        [ForeignKey(nameof(SchoolId))]
        [InverseProperty(nameof(User.Students))]
        public User School { get; set; }

        [InverseProperty(nameof(StudentFeesHistory.Student))]
        public virtual ICollection<StudentFeesHistory> StudentFeesHistories { get; set; }

        [InverseProperty(nameof(Studentbysession.Student))]
        public virtual ICollection<Studentbysession> Studentbysessions { get; set; }
    }
}
