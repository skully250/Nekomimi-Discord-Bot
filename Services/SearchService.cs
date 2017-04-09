using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;

using Discord;
using Discord.Commands;

namespace Nekomaid_Club_Bot.Services
{
    public class SearchService
    {
        public async Task sendPic(IUserMessage umsg, string tag, string website)
        {
            var channel = umsg.Channel;

            tag = tag ?.Trim() ?? "";

            var url = await pageSearch(tag, website).ConfigureAwait(false);

            if (url == null)
            {
                await channel.SendMessageAsync("There were no results; Make sure you're searching either - safebooru, gelbooru, rule34, konachan, yandere");
            } else
            {
                await channel.SendMessageAsync(url);
            }
        }

        public static async Task<String> pageSearch(string tag, string website)
        {
            tag = tag?.Replace(" ", "_");
            var lewdweb = "";
            switch (website)
            {
                case "safebooru":
                    lewdweb = $"https://safebooru.org/index.php?page=dapi&s=post&q=index&limit=100&tags={tag}";
                    break;
                case "gelbooru":
                    lewdweb = $"http://gelbooru.com/index.php?page=dapi&s=post&q=index&limit=100&tags={tag}";
                    break;
                case "rule34":
                    lewdweb = $"https://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=100&tags={tag}";
                    break;
                case "konachan":
                    lewdweb = $"https://konachan.com/post.xml?s=post&q=index&limit=100&tags={tag}";
                    break;
                case "yandere":
                    lewdweb = $"https://yande.re/post.xml?limit=100&tags={tag}";
                    break;
            }
            try
            {
                var toReturn = await Task.Run(async () =>
                {
                    using (var http = new HttpClient())
                    {
                        var data = await http.GetStreamAsync(lewdweb).ConfigureAwait(false);
                        var doc = new XmlDocument();
                        doc.Load(data);

                        var node = doc.LastChild.ChildNodes[new Random().Next(0, doc.LastChild.ChildNodes.Count)];

                        var url = node.Attributes["file_url"].Value;
                        if (!url.StartsWith("http"))
                            url = "http:" + url;
                        return url;
                    }
                }).ConfigureAwait(false);
                return toReturn;
            } catch
            {
                return null;
            }
        }
    }
}
