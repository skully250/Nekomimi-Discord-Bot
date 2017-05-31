using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.WebSocket;
using Discord.Commands;

using Nekomimi_Rewrite.Services;

namespace Nekomimi_Rewrite.Modules.Public
{
    public class GoldModule : ModuleBase<CommandContext>
    {
        GoldService _goldserv;

        public GoldModule(GoldService goldserv)
        {
            _goldserv = goldserv;
        }

        [Command("gold")]
        public async Task myGold() => await _goldserv.checkGoldAsync(Context, Context.User as SocketGuildUser);

        [Command("addGold", RunMode = RunMode.Async)]
        public async Task addGold(SocketGuildUser user) => await _goldserv.AddGoldAsync(Context, user);

        [Command("giveGold", RunMode = RunMode.Async)]
        public async Task giveGold(SocketGuildUser user)
        {
            if (_goldserv.checkGold(Context.User as SocketGuildUser) != 0)
            {
                await _goldserv.AddGoldAsync(Context, user);
                await _goldserv.removeGoldAsync(Context, Context.Message.Author as SocketGuildUser);
            }
        }
    }
}
