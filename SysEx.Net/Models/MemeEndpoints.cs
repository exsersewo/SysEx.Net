using Newtonsoft.Json;

namespace SysEx.Net.Models
{
    public struct MemeEndpoints
    {
        [JsonProperty("endpoint")]
        public string Name;
        [JsonProperty("sources")]
        public int RequiredSources;
    }
}
