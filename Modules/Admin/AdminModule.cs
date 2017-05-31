using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Nekomimi_Rewrite.Modules.Admin
{
    public class AdminModule : ModuleBase<CommandContext>
    {
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        [Command("mute")]
        public async Task mute(IGuildUser user)
        {
            var role = Context.Guild.GetRole(310741004305170433);
            await user.AddRoleAsync(role);
            await ReplyAsync($"{user.Username} Has been muted");
        }

        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        [Command("unmute")]
        public async Task unmute(IGuildUser user)
        {
            var role = Context.Guild.GetRole(310741004305170433);
            await user.RemoveRoleAsync(role);
            await ReplyAsync($"{user.Username} Has been unmuted");
        }
    }
}
