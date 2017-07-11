using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nekomimi_Rewrite.Services
{
    class CatImage
    {
        public CatImage(string file)
        {
            Url = file;
        }

        public string Url { get; private set; }
    }

    public class CatService
    {
        private const string nekoURL = "http://random.cat/meow";

        public string getRandomNeko()
        {
            CatImage neko = null;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(nekoURL).Result;
                    var json = response.Content.ReadAsStringAsync().Result;
                    neko = JsonConvert.DeserializeObject<CatImage>(json);
                } catch(Exception ex)
                {
                    Console.WriteLine("Failed to get cat image - " + ex.Message);
                }
            }

            return neko.Url;
        }
    }
}
