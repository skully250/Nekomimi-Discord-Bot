using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nekomimi_Rewrite.Modules.Public
{
    public class MainModule : ModuleBase<CommandContext>
    {
        [Command("pet")]
        public async Task pet([Remainder] string user)
        {
            if (user.ToLower() == "me")
            {
                if (Context.Message.Author.Username == "enka")
                    await ReplyAsync("*pets kivei's luxuriously fluffy tail*");
                else
                    await ReplyAsync($"*pets {Context.User.Username}*");
            } else
            {
                if (user == "enka" || user == "kivei")
                    await ReplyAsync("*pets kivei's luxuriously fluffy tail*");
                else
                    await ReplyAsync($"*pets {user}*");
            }
        }

        [Command("feed me")]
        public async Task feed()
        {
            if (Context.User.Username.ToLower() == "fae")
                await ReplyAsync($"*hands fae a bag of chips*");
            else if (Context.User.Username.ToLower() == "enka")
                await ReplyAsync($"*hands kivei a big bag of chips*");
            else
                await ReplyAsync($"*Throws a single potato chip at {Context.User.Username}*");
        }
    }
}
