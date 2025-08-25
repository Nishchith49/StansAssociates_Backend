using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class DropDownModel
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }


    public class RouteDropDownModel
    {
        [JsonProperty("schoolName")]
        public string SchoolName { get; set; }

        [JsonProperty("busNo")]
        public string BusNo { get; set; }

        [JsonProperty("boardingPoint")]
        public string BoardingPoint { get; set; }

        [JsonProperty("routeCost")]
        public decimal RouteCost { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
