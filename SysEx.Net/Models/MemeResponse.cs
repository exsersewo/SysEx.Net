using Newtonsoft.Json;
using System.Collections.Generic;

namespace SysEx.Net.Models
{
    public struct MemeResponse
    {
        [JsonProperty("success")]
        public bool Successful;

        [JsonProperty("data")]
        public List<MemeEndpoints> Endpoints;
    }
}
