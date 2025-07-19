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
}
