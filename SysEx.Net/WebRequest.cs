using SysEx.Net.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace SysEx.Net
{
    class WebRequest
    {
		public static HttpWebRequest CreateWebRequest(Uri uri)
		{
			var cli = (HttpWebRequest)System.Net.WebRequest.Create(uri);

			cli.AllowAutoRedirect = true;
			cli.KeepAlive = false;
			cli.Timeout = 20000;
			cli.ProtocolVersion = HttpVersion.Version10;

			return cli;
		}
		public static async Task<string> ReturnStringAsync(Uri url)
		{
			try
			{
				var client = CreateWebRequest(url);

				var resp = (HttpWebResponse)(await client.GetResponseAsync());
				if (resp.StatusCode == HttpStatusCode.OK)
				{
					var reader = new StreamReader(resp.GetResponseStream());
					var responce = await reader.ReadToEndAsync();
					resp.Dispose();
					client.Abort();
					return responce;
				}
				else
				{
					resp.Dispose();
					client.Abort();
					return null;
				}
			}
			catch
			{
				return null;
			}
		}
        public static async Task<Uri> GetRedirectUriAsync(Uri url)
        {
            try
            {
                var client = CreateWebRequest(url);

                var resp = (HttpWebResponse)(await client.GetResponseAsync());
                return resp.ResponseUri ?? null;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<MemoryStream> GetStreamAsync(Uri url)
        {
            try
            {
                var client = CreateWebRequest(url);

                var resp = (HttpWebResponse)(await client.GetResponseAsync());
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    var reader = resp.GetResponseStream();
                    long contLength = resp.ContentLength;

                    var stream = resp.GetResponseStream();

                    byte[] outData;
                    using (var tempStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[128];
                        while (true)
                        {
                            int read = stream.Read(buffer, 0, buffer.Length);
                            if (read <= 0)
                            {
                                outData = tempStream.ToArray();
                                break;
                            }
                            tempStream.Write(buffer, 0, read);
                        }
                    }

                    resp.Dispose();
                    client.Abort();

                    return new MemoryStream(outData);
                }
                else
                {
                    resp.Dispose();
                    client.Abort();
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
	}
}
