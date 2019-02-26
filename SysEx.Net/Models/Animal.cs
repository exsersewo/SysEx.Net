using Newtonsoft.Json;

namespace SysEx.Net.Models
{
    public class Animal
	{
		[JsonProperty(PropertyName = "file")]
		public string URL { get; set; }
	}
}
