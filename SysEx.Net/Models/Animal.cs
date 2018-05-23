using Newtonsoft.Json;

namespace SysEx.Net
{
    public class Animal
	{
		[JsonProperty(PropertyName = "file")]
		public string URL { get; set; }
	}
}
