using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SysEx.Net
{
	public class SysExClient
	{
		readonly Random random;

		public SysExClient()
		{
			random = new Random();
		}

		public async Task<string> v1LlamaAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/llama.json"));

		public async Task<string> v1SealAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/seal.json"));

		public async Task<string> v1DuckAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/duck.json"));

		public async Task<string> v1SquirrelAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/squirrel.json"));

		public async Task<string> v1LizardAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/lizard.json"));

		public async Task<string> v1MorphAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/morphs.json"));

		public async Task<string> v1SnakeAsync() =>
			await v1AnimalAsync(new Uri("https://api.systemexit.co.uk/v1/snake.json"));

		async Task<string> v1AnimalAsync(Uri url)
		{
			var resp = await WebRequest.ReturnStringAsync(url);
			var items = JsonConvert.DeserializeObject<List<Animal>>(resp);
			if (items == null) return null;
			var animal = items[random.Next(0, items.Count)].URL;
			return animal;
		}

		public async Task<string> v1RoastAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/roasts.json"));
			var items = JsonConvert.DeserializeObject<List<Roasts>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)].Roast;
		}

		public async Task<Joke> v1DadJokeAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/dadjokes.json"));
			var items = JsonConvert.DeserializeObject<List<Joke>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)];
		}

		public async Task<Joke> v1PickupLineAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/pickuplines.json"));
			var items = JsonConvert.DeserializeObject<List<Joke>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)];
		}

		public async Task<string> v1WeebActionGifAsync(GifType type)
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/actions/?action=" + type.ToString().ToLowerInvariant()));
			return resp;
		}

		public async Task<string> v1WeebReactionGifAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/reactions/"));
			return resp;
		}

		public async Task<string> v1LewdKitsuneAsync()
		{
			var rawresp = await WebRequest.ReturnStringAsync(new Uri("https://kitsu.systemexit.co.uk/lewd"));
			dynamic item = JObject.Parse(rawresp);
			var img = item["kitsune"];
			if (img == null) return null;
			return img;
		}
		public async Task<string> v1KitsuneAsync()
		{
			var rawresp = await WebRequest.ReturnStringAsync(new Uri("https://kitsu.systemexit.co.uk/kitsune"));
			dynamic item = JObject.Parse(rawresp);
			var img = item["kitsune"];
			if (img == null) return null;
			return img;
		}
	}
}
