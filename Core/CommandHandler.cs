using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

using Nekomimi_Rewrite.Services;

namespace Nekomimi_Rewrite.Core
{
    class CommandHandler
    {
        private CommandService comserv;
        private CatService catserv;
        private LewdService lewdserv;
        private AudioService audioserv;
        private GoldService goldserv;
        private TagService tagserv;

        private DiscordSocketClient client;
        /*private IDependencyMap map;

        private CommandHandler handler => this;

        public CommandHandler(IDependencyMap _map)
        {
            client = _map.Get<DiscordSocketClient>();

            comserv = new CommandService();

            catserv = new CatService();
            lewdserv = new LewdService();
            audioserv = new AudioService();
            goldserv = new GoldService();
            tagserv = new TagService();


            _map.Add(catserv);
            _map.Add(lewdserv);
            _map.Add(audioserv);
            _map.Add(goldserv);
            _map.Add(tagserv);

            _map.Add(handler);

            map = _map;

            client.MessageReceived += handleCommand;
        }

        public async Task Install()
        {
            //await comserv.AddModuleAsync<AdminModule>();
            await comserv.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task handleCommand(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null) return;

            var context = new CommandContext(client, message);

            int argpos = 0;
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argpos) || message.HasCharPrefix('!', ref argpos))) return;

            var result = await comserv.ExecuteAsync(context, argpos, map);

            if (!result.IsSuccess)
                await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }*/
    }
}
