using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Nekomimi_Rewrite.Services;

namespace Nekomimi_Rewrite.Modules.Public
{
    public class InfoModule : ModuleBase<CommandContext>
    {
        LewdService _lewdserv;

        public InfoModule(LewdService lewdserv)
        {
            _lewdserv = lewdserv;
        }

        [Command("whois")]
        public async Task whois([Remainder] IUser user = null)
        {
            var userinfo = user ?? Context.Client.CurrentUser;

            var eb = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                ImageUrl = userinfo.GetAvatarUrl(),
            };

            eb.AddField((efb) =>
            {
                efb.Name = "User";
                efb.IsInline = true;
                efb.Value = $"Name + Discriminator: {userinfo.Username}#{userinfo.Discriminator} \n" +
                            $"Created on: {userinfo.CreatedAt} \n" +
                            $"Status: {userinfo.Status}";
            });

            await Context.Channel.SendMessageAsync("", false, eb);
        }
    }
}
