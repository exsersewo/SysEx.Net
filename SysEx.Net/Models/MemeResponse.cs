using Newtonsoft.Json;
using System.Collections.Generic;

namespace SysEx.Net.Models
{
    public struct MemeResponse
    {
        [JsonProperty("Successful")]
        public bool Successful;
        [JsonProperty("example")]
        public string Example;

        [JsonProperty("available-templates")]
        public List<MemeEndpoints> Endpoints;
    }
}
