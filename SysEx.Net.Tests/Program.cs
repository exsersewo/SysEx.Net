using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SysEx.Net.Tests
{
    class Program
    {
        static void Main(string[] args) => MainTask().GetAwaiter().GetResult();

        static async Task MainTask()
        {
            try
            {
                var client = new SysExClient();

                var resp = await client.GetMemeImageAsync("22million", "https://cdn.discordapp.com/avatars/270047199184945152/e6a23a1c72703edb5e178411bba9190c.png");

                if(resp is Stream)
                {
                    Console.Write("Got Successfully");
                }

                var re = resp as MemoryStream;

                Image img = Image.FromStream(re);
                img.Save(AppContext.BaseDirectory + "image.jpg");

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(-1);
        }
    }
}
