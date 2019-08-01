using Newtonsoft.Json;

namespace DemoWebsite.Models
{
    /// <summary>
    /// payment status
    /// </summary>
    public class Status
    {
        /// <summary>
        /// description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        /// <summary>
        /// severity
        /// </summary>
        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }
        /// <summary>
        /// code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}