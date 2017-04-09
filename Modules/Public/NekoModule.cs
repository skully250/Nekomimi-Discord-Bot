using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Nekomaid_Club_Bot.Services;

namespace Nekomaid_Club_Bot.Modules.Public
{
    public class NekoModule : ModuleBase
    {
        RandomCatService _catserv;
        SearchService _searchserv;

        public NekoModule(RandomCatService catserv, SearchService searchserv)
        {
            _catserv = catserv;
            _searchserv = searchserv;
        }

        [Command("how are you")]
        public async Task how()
        {
            await ReplyAsync("Im doing very good, thank you!");
        }

        [Command("search")]
        public async Task nsfw(string tag, string website)
        {
            await _searchserv.sendPic(Context.Message, tag, website);
        }

        [Command("pet"), Summary("Pet a user or yourself")]
        public async Task Pet([Remainder] string user)
        {
            if (user == "me" || user == "Me")
            {
                if (Context.Message.Author.Username == "enka")
                    await ReplyAsync($"*pets kiveis fluffy tail*");
                else 
                    await ReplyAsync($"*pets {Context.Message.Author.Username}*");
            }
            else if (user == "enka" || user == "kivei")
                await ReplyAsync($"*pets kiveis fluffy tail*");
            else
                await ReplyAsync($"*pets {user}*");
        }

        [Command("lolipolice"), Summary("Call down the lolipolice")]
        public async Task lolipolice()
        {
            await ReplyAsync("https://a.disquscdn.com/uploads/mediaembed/images/3983/1706/original.gif?w=800&h");
        }

        [Command("ohayou"), Alias("おはよう")]
        public async Task ohayou()
        {
            await ReplyAsync("http://i.imgur.com/Hbl7w2T.gif");
        }

        [Command("bakanano"), Alias("バカナの")]
        public async Task bakanano()
        {
            await ReplyAsync("http://i2.kym-cdn.com/photos/images/newsfeed/001/095/334/4f9.gif");
        }

        [Command("cat")]
        public async Task cat()
        {
            await ReplyAsync(_catserv.getRandomNeko());
        }

        [Command("Dog")]
        public async Task dog()
        {
            using (var http = new HttpClient())
            {
                await ReplyAsync("http://random.dog/" + await http.GetStringAsync("http://random.dog/woof").ConfigureAwait(false)).ConfigureAwait(false);
            }
        }
    }
}
