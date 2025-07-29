using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class AddSchoolModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

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


    public class UpdateSchoolModel : AddSchoolModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }


    public class GetSchoolModel : UpdateSchoolModel
    {
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}
