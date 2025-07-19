using Newtonsoft.Json;
using System;

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
        public DateTime DOB { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("pincode")]
        public string Pincode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

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
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }
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
