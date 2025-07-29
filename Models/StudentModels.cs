using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class AddStudentModel
    {
        [JsonProperty("affiliation")]
        public string Affiliation { get; set; }

        [JsonProperty("admissionNo")]
        public string AdmissionNo { get; set; }

        [JsonProperty("rollNo")]
        public string? RollNo { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("fName")]
        public string FName { get; set; }

        [JsonProperty("lName")]
        public string? LName { get; set; }

        [JsonProperty("fatherName")]
        public string FatherName { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("dob")]
        public DateTime? DOB { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("routeId")]
        public long RouteId { get; set; }

        //[JsonProperty("totalPaid")]
        //public decimal TotalPaid { get; set; }

        [JsonProperty("street")]
        public string? Street { get; set; }

        [JsonProperty("pincode")]
        public string? Pincode { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("studentImg")]
        public string? StudentImg { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("remark")]
        public string? Remark { get; set; }

        [JsonProperty("doa")]
        public DateTime DOA { get; set; }
    }


    public class UpdateStudentModel : AddStudentModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }


    public class GetStudentModel : UpdateStudentModel
    {
        [JsonProperty("schoolName")]
        public string SchoolName { get; set; }

        [JsonProperty("busNo")]
        public string BusNo { get; set; }

        [JsonProperty("boardingPoint")]
        public string BoardingPoint { get; set; }

        [JsonProperty("routeCost")]
        public decimal RouteCost { get; set; }

        [JsonProperty("totalPaid")]
        public decimal TotalPaid { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }


    public class GetStudentsFilterModel : PagedResponseInput
    {
        [JsonProperty("session")]
        public string? Session { get; set; } 
    }


    public class AddStudentFeeModel
    {
        [JsonProperty("studentId")]
        public long StudentId { get; set; }

        [JsonProperty("paidMode")]
        public string PaidMode { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("paidDate")]
        public DateTime PaidDate { get; set; }
    }


    public class GetStudentFeesFilterModel : PagedResponseInput 
    {
        [JsonProperty("studentId")]
        public long? StudentId { get; set; }
    }


    public class GetStudentFeeModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("paidMode")]
        public string PaidMode { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("paidDate")]
        public DateTime PaidDate { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }


    public class GetStudentFeeDetailsModel
    {
        [JsonProperty("studentId")]
        public long StudentId { get; set; }

        [JsonProperty("fName")]
        public string FName { get; set; }

        [JsonProperty("lName")]
        public string? LName { get; set; }

        [JsonProperty("fatherName")]
        public string FatherName { get; set; }

        [JsonProperty("admissionNo")]
        public string AdmissionNo { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("paid")]
        public decimal Paid { get; set; }

        [JsonProperty("due")]
        public decimal Due { get; set; }

        [JsonProperty("fees")]
        public List<GetStudentFeeModel> Fees { get; set; }
    }


    public class AddSessionModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }


    public class GetSessionModel : AddSessionModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
