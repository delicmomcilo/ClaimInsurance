using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ClaimAuditFunction
{
    public class ClaimAudit
    {
        [Key]
        [JsonProperty(PropertyName = "claimid")]
        public string ClaimId { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }
    }

}
