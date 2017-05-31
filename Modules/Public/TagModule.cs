using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

using Nekomimi_Rewrite.Services;

namespace Nekomimi_Rewrite.Modules.Public
{
    public class TagModule : ModuleBase<CommandContext>
    {

        TagService _tagserv;

        public TagModule(TagService service)
        {
            _tagserv = service;
        }

        [Command("addTag")]
        public async Task addTag(string tagName, [Remainder] string tagContent)
        {
            await _tagserv.CreateTag(Context, Context.User as SocketGuildUser, tagName, tagContent);
        }

        [Command("removeTag")]
        public async Task removeTag(string tagName)
        {
            await _tagserv.RemoveTag(Context, Context.User as SocketGuildUser, tagName);
        }

        [Command("tag")]
        public async Task tag(string tagName)
        {
            await _tagserv.FindTag(Context, Context.User as SocketGuildUser, tagName);
        }

    }
}
