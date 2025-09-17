using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class AddRouteModel
    {
        [JsonProperty("schoolId")]
        public long SchoolId { get; set; }

        [JsonProperty("busNo")]
        public string BusNo { get; set; }

        [JsonProperty("boardingPoint")]
        public string BoardingPoint { get; set; }

        [JsonProperty("routeCost")]
        public decimal RouteCost { get; set; }
    }


    public class UpdateRouteModel : AddRouteModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }


    public class GetRoutesFilterModel : PagedResponseInput
    {
        [JsonProperty("schoolId")]
        public long? SchoolId { get; set; }
    }


    public class GetRouteModel: UpdateRouteModel
    {
        [JsonProperty("schoolName")]
        public string SchoolName { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}
