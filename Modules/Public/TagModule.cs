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

        [Command("tag")]
        public async Task tagHandle(string action, string tagName = null, [Remainder] string tagContent = null)
        {
            SocketGuildUser user = Context.User as SocketGuildUser;

            if (tagName != null)
            {
                switch (action)
                {
                    case "add":
                        if (tagContent != null)
                            await _tagserv.CreateTag(Context, user, tagName, tagContent);
                        break;
                    case "modify":
                        if (tagContent != null)
                            await _tagserv.ModifyTag(Context, user, tagName, tagContent);
                        break;
                    case "remove":
                        await _tagserv.RemoveTag(Context, user, tagName);
                        break;
                }
            } else
            {
                await _tagserv.FindTag(Context, user, action);
            }
        }
    }
}
