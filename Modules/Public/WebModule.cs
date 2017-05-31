using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Discord.Commands;

using Nekomimi_Rewrite.Services;

namespace Nekomimi_Rewrite.Modules.Public
{
    public class WebModule : ModuleBase<CommandContext>
    {
        CatService catserv;
        LewdService lewdserv;

        public WebModule(CatService cat, LewdService lewd)
        {
            catserv = cat;
            lewdserv = lewd;
        }

        [Command("cat", RunMode = RunMode.Async)]
        public async Task cat()
        {
            await ReplyAsync(catserv.getRandomNeko());
        }

        [Command("search", RunMode = RunMode.Async)]
        public async Task lewd(string tag, string website)
        {
            if (Context.Channel.Name == "nsfw" || (Context.Channel.Name != "nsfw" && website == "safebooru"))
                await lewdserv.sendPic(Context.Message, tag, website);
        }

        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        [Command("hentaibomb", RunMode = RunMode.Async)]
        public async Task bomb(string tag)
        {
            if (Context.Channel.Name == "nsfw")
            {
                await lewdserv.sendPic(Context.Message, tag, "safebooru");
                await lewdserv.sendPic(Context.Message, tag, "gelbooru");
                await lewdserv.sendPic(Context.Message, tag, "konachan");
                await lewdserv.sendPic(Context.Message, tag, "rule34");
                await lewdserv.sendPic(Context.Message, tag, "yandere");
            }
        }

        [Command("dog", RunMode = RunMode.Async), ]
        public async Task dog()
        {
            using (var http = new HttpClient())
                await ReplyAsync("http://random.dog/" + await http.GetStringAsync("http://random.dog/woof").ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}