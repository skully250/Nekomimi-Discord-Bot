using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Nekomaid_Club_Bot.Modules.Public
{
    class AdminModule : ModuleBase
    {

        [Command("cleanup")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task Cleanup()
        {
            var messages = Context.Channel.GetMessagesAsync(100);

        }
    }
}
