using Newtonsoft.Json;
using System.Text.Json;

namespace StansAssociates_Backend.Models
{
    public class GetDashboardModel
    {
        [JsonProperty("totalStudents")]
        public long TotalStudents { get; set; }

        [JsonProperty("totalStaffs")]
        public long TotalStaffs { get; set; }

        [JsonProperty("totalRoutes")]
        public long TotalRoutes { get; set; }

        [JsonProperty("totalEarnings")]
        public decimal TotalEarnings { get; set; }

        [JsonProperty("totalEarningsThisYear")]
        public decimal TotalEarningsThisYear { get; set; }
    }
}
