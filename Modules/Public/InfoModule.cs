using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nekomaid_Club_Bot.Modules.Public
{
    class InfoModule : ModuleBase
    {
        [Command("whois")]
        public async Task whois([Remainder] IUser user = null)
        {
            var userinfo = user ?? Context.Client.CurrentUser;

            var eb = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                ImageUrl = userinfo.GetAvatarUrl()
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

            var footer = new EmbedFooterBuilder();

            footer.IconUrl = Context.User.GetAvatarUrl();
            footer.Text = $"Thank you for requesting this info {Context.User.Username}";

            var header = new EmbedAuthorBuilder();
            header.Name = "Nekomimi";
            header.IconUrl = Context.Client.CurrentUser.GetAvatarUrl();

            eb.WithAuthor(header);
            eb.WithFooter(footer);

            await Context.Channel.SendMessageAsync("", false, eb);
        }

        private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
