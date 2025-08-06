using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class AddStaffModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("dob")]
        public DateTime? DOB { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("pincode")]
        public string Pincode { get; set; }

        [JsonProperty("cityId")]
        public long CityId { get; set; }

        [JsonProperty("stateId")]
        public long StateId { get; set; }

        [JsonProperty("countryId")]
        public long CountryId { get; set; }

        [JsonProperty("profilePicture")]
        public string? ProfilePicture { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }


    public class UpdateStaffModel : AddStaffModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }


    public class UpdatePasswordModel
    {
        [JsonProperty("staffId")]
        public long StaffId { get; set; }

        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }


    public class GetStaffModel : UpdateStaffModel
    {
        [JsonProperty("schoolName")]
        public string SchoolName { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("stateName")]
        public string StateName { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [JsonProperty("permissions")]
        public new List<GetTeamPermissions> Permissions { get; set; }
    }


    public class AddTeacherModel : AddStaffModel
    {

    }


    public class UpdateTeacherModel : UpdateStaffModel
    {

    }


    public class GetTeacherModel : GetStaffModel
    {

    }
}
