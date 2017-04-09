using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        [Command("search")]
        public async Task nsfw(string tag, string website)
        {
            await _searchserv.sendPic(Context.Message, tag, website);
        }

        [Command("ping"), Summary("Ping the bot")]
        public async Task Pong()
        {
            await ReplyAsync("pong");
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
                await ReplyAsync($"*pets kiveis fluffy tail");
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

        [Command("whois")]
        public async Task whois([Remainder] IUser user = null)
        {
            var userinfo = user ?? Context.Client.CurrentUser;

            var eb = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                ThumbnailUrl = userinfo.GetAvatarUrl()
            };

            eb.AddField((efb) =>
            {
                efb.Name = "User";
                efb.IsInline = true;
                efb.Value = $"Name + Discriminator: {userinfo.Username}#{userinfo.Discriminator} \n" +
                            $"Created at: {userinfo.CreatedAt} \n" +
                            $"Status: {userinfo.Status}";
            });

            await Context.Channel.SendMessageAsync("", false, eb);
        }

        [Command("info")]
        public async Task Info()
        {
            var eb = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                ThumbnailUrl = Context.Guild.IconUrl
            };
            eb.AddField((efb) =>
            {
                efb.Name = "Info";
                efb.IsInline = true;
                efb.Value = $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                            $"- Uptime: {GetUptime()}\n";
            });

            eb.AddField((efb) =>
            {
                efb.Name = "Stats";
                efb.IsInline = false;
                efb.Value = $"- Heap Size: {GetHeapSize()} MB\n" +
                            $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                            $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}" +
                            $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}";
            });

            await Context.Channel.SendMessageAsync("", false, eb);
        }

        private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
