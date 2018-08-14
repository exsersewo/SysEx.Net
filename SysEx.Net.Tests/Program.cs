using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysEx.Net;

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

                var resp = await client.GetWeebActionGifAsync(GifType.Slap);

                Console.WriteLine(resp);

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
