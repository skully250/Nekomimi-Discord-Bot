using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nekomaid_Club_Bot.Services
{
    class CatImage
    {
        public CatImage(string file)
        {
            Url = file;
        }

        public string Url { get; private set; }
    }

    public class RandomCatService
    {
        private const string NekoUrl = "http://random.cat/meow";

        public string getRandomNeko()
        {
            CatImage randomneko = null;

            using (WebClient webclient = new WebClient())
            {
                try
                {
                    string json = webclient.DownloadString(NekoUrl);
                    randomneko = JsonConvert.DeserializeObject<CatImage>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to get cat image - " + ex.Message);
                }
            }

            return randomneko.Url;
        }
    }
}