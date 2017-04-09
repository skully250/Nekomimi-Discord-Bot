using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nekomaid_Club_Bot.Util;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nekomaid_Club_Bot.Modules.Public
{
    class GoldModule : ModuleBase
    {
        [Command("gold")]
        public async Task myGold() => await Gold.checkGoldAsync(Context, Context.User as SocketGuildUser);

        [Command("addGold")]
        public async Task addGold(SocketGuildUser user) => await Gold.AddGoldAsync(Context, user);
    }
}
