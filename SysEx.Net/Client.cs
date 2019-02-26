using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SysEx.Net.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SysEx.Net
{
	public class SysExClient
	{
		readonly Random random;

		public SysExClient()
		{
			random = new Random();
		}

        /// <summary>
        /// Gets a Llama image
        /// </summary>
		public async Task<string> GetLlamaAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/llama.json"));

        /// <summary>
        /// Gets a Seal image
        /// </summary>
		public async Task<string> GetSealAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/seal.json"));

        /// <summary>
        /// Gets a Duck image
        /// </summary>
		public async Task<string> GetDuckAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/duck.json"));

        /// <summary>
        /// Gets a Squirrel image
        /// </summary>
		public async Task<string> GetSquirrelAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/squirrel.json"));

        /// <summary>
        /// Gets a Lizard image
        /// </summary>
		public async Task<string> GetLizardAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/lizard.json"));

        /// <summary>
        /// Gets a morphed animal image
        /// </summary>
		public async Task<string> GetMorphAsync() =>
			await GetAnimalAsync(new Uri("https://api.systemexit.co.uk/v1/morphs.json"));

        /// <summary>
        /// Gets a Snake image
        /// </summary>
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

        /// <summary>
        /// Get a roast
        /// </summary>
		public async Task<string> GetRoastAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/roasts.json"));
			var items = JsonConvert.DeserializeObject<List<Roasts>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)].Roast;
		}

        /// <summary>
        /// Gets a terrible, terrible Dad joke
        /// </summary>
		public async Task<Joke> GetDadJokeAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/dadjokes.json"));
			var items = JsonConvert.DeserializeObject<List<Joke>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)];
		}

        /// <summary>
        /// Gets a terrible, terrible pickup line
        /// </summary>
		public async Task<Joke> GetPickupLineAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/v1/pickuplines.json"));
			var items = JsonConvert.DeserializeObject<List<Joke>>(resp);
			if (items == null) return null;
			return items[random.Next(0, items.Count)];
		}

        /// <summary>
        /// Gets A Weeb Action Gif
        /// </summary>
        /// <param name="type">Gif Action Type</param>
        /// <returns>Url of image</returns>
		public async Task<string> GetWeebActionGifAsync(GifType type)
		{
			var resp = await WebRequest.GetRedirectUriAsync(new Uri("https://api.systemexit.co.uk/actions/?action=" + type.ToString().ToLowerInvariant()));
			return resp.OriginalString;
		}

        /// <summary>
        /// Gets A Weeb Reaction Gif
        /// </summary>
        /// <returns>Url of image</returns>
		public async Task<string> GetWeebReactionGifAsync()
		{
			var resp = await WebRequest.ReturnStringAsync(new Uri("https://api.systemexit.co.uk/reactions/"));
			return resp;
		}

        /// <summary>
        /// Gets a meme image based on given input
        /// </summary>
        /// <param name="template">Template to use</param>
        /// <param name="images">Source images to use with template</param>
        /// <returns>Either Object. MemeResponse on empty input or failure, or MemoryStream on success</returns>
        public async Task<object> GetMemeImageAsync(string template = null, params string[] images)
        {
            var endpoints = JsonConvert.DeserializeObject<MemeResponse>(await WebRequest.ReturnStringAsync(new Uri("https://api.skuldbot.uk/fun/meme/?endpoints")));
            if (template == null && (images.Length <= 0 || images == null))
            {
                return endpoints;
            }
            if(endpoints.Endpoints.Exists(x=>x.Name.ToLowerInvariant() == template.ToLowerInvariant()))
            {
                var endpoint = endpoints.Endpoints.FirstOrDefault(z => z.Name.ToLowerInvariant() == template.ToLowerInvariant());
                string queryString = "";
                int x = 1;
                foreach(var image in images)
                {
                    if(image == images.Last())
                    {
                        queryString += $"source{x}={image}";
                    }
                    else
                    {
                        queryString += $"source{x}={image}&";
                    }
                    x++;
                }

                var resp = await WebRequest.GetStreamAsync(new Uri($"https://api.skuldbot.uk/fun/meme/{template}/?{queryString}"));
                if(resp != null)
                {
                    return resp;
                }
            }
            return new MemeResponse
            {
                Successful = false,
                Endpoints = endpoints.Endpoints
            };
        }

        /// <summary>
        /// Gets a NSFW image of a "Kitsune"
        /// </summary>
		public async Task<string> GetLewdKitsuneAsync()
		{
			var rawresp = await WebRequest.ReturnStringAsync(new Uri("https://kitsu.systemexit.co.uk/lewd"));
			dynamic item = JObject.Parse(rawresp);
			var img = item["kitsune"];
			if (img == null) return null;
			return img;
		}
        /// <summary>
        /// Gets a SFW* image of a "Kitsune" (*potentially)
        /// </summary>
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
