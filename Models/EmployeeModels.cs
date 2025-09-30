using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class AddEmployeeModel
    {
        [JsonProperty("empCode")]
        public string EmpCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("tdp")]
        public int Tdp { get; set; }

        [JsonProperty("tad")]
        public int Tad { get; set; }

        [JsonProperty("thl")]
        public int Thl { get; set; }
    }


    public class UpdateEmployeeModel : AddEmployeeModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }


    public class GetEmployeesFilterModel : PagedResponseInput
    {
        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("month")]
        public int? Month { get; set; }
    }


    public class GetEmployeeModel : UpdateEmployeeModel
    {
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [JsonProperty("employeeAttendance")]
        public IReadOnlyList<GetEmployeeAttendanceModel> EmployeeAttendance { get; set; }
    }


    public class AddEmployeeAttendanceModel
    {
        [JsonProperty("employeeId")]
        public long EmployeeId { get; set; }

        [JsonProperty("attendanceDate")]
        public DateOnly AttendanceDate { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("remarks")]
        public string? Remarks { get; set; }
    }


    public class BulkAddEmployeeAttendanceModel
    {
        [JsonProperty("attendanceDate")]
        public DateOnly AttendanceDate { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("remarks")]
        public string? Remarks { get; set; }
    }


    public class UpdateEmployeeAttendanceModel : AddEmployeeAttendanceModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }


    public class GetEmployeeAttendancesFilterModel : PagedResponseInput
    {
        [JsonProperty("employeeId")]
        public long? EmployeeId { get; set; }

        [JsonProperty("fromDate")]
        public DateOnly? FromDate { get; set; }

        [JsonProperty("toDate")]
        public DateOnly? ToDate { get; set; }
    }


    public class GetEmployeeAttendanceModel : UpdateEmployeeAttendanceModel
    {
        [JsonProperty("statusName")]
        public string StatusName { get; set; }

        [JsonProperty("employeeName")]
        public string EmployeeName { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}
