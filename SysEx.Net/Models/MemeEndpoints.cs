using Newtonsoft.Json;

namespace SysEx.Net.Models
{
    public struct MemeEndpoints
    {
        [JsonProperty("Name")]
        public string Name;
        [JsonProperty("RequiredSources")]
        public int RequiredSources;
    }
}
