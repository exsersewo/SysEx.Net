using Newtonsoft.Json;
using System.Collections.Generic;

namespace SysEx.Net.Models
{
    public struct MemeResponse
    {
        public bool Successful;
        public string Example;

        [JsonProperty(PropertyName = "availabletemplates")]
        public List<MemeEndpoints> Endpoints;
    }
}
