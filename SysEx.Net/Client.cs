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

		public async Task<string> GetLlamaAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/llama.json"));

		public async Task<string> GetSealAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/seal.json"));

		public async Task<string> GetDuckAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/duck.json"));

		public async Task<string> GetSquirrelAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/squirrel.json"));

		public async Task<string> GetLizardAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/lizard.json"));

		public async Task<string> GetMorphAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/morphs.json"));

		public async Task<string> GetSnakeAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/snake.json"));

		async Task<string> GetAnimalAsync(Uri url)
		{
			var resp = await WebRequest.ReturnStringAsync(url);
			var items = JsonConvert.DeserializeObject<List<Animal>>(resp);
			if (items == null) return null;
			var animal = items[random.Next(0, items.Count)].URL;
			return animal;
		}

		public async Task<string> GetRoastAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/roasts.json"));
			var items = JsonConvert.DeserializeObject<List<Roasts>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)].Roast;
		}

		public async Task<Joke> GetDadJokeAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/dadjokes.json"));
			var items = JsonConvert.DeserializeObject<List<Joke>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)];
		}

		public async Task<Joke> GetPickupLineAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/pickuplines.json"));
			var items = JsonConvert.DeserializeObject<List<Joke>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)];
		}

		public async Task<string> GetWeebActionGifAsync(GifType type)
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/actions/?action=" + type.ToString().ToLowerInvariant()));
			return resp;
		}

		public async Task<string> GetWeebReactionGifAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/reactions/"));
			return resp;
		}

		public async Task<string> GetLewdKitsuneAsync()
		{
			var rawresp = await WebRequest.ReturnStringAsync(new Uri("https://kitsu.systemexit.co.uk/lewd"));
			dynamic item = JObject.Parse(rawresp);
			var img = item["kitsune"];
			if (img == null) return null;
			return img;
		}
		public async Task<string> GetKitsuneAsync()
		{
			var rawresp = await WebRequest.ReturnStringAsync(new Uri("https://kitsu.systemexit.co.uk/kitsune"));
			dynamic item = JObject.Parse(rawresp);
			var img = item["kitsune"];
			if (img == null) return null;
			return img;
		}
	}
}
