using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Discord.Commands;
using Discord.WebSocket;

using Nekomaid_Club_Bot.Services;
using Nekomaid_Club_Bot.Modules.Public;

namespace Nekomaid_Club_Bot.Core
{
    public class CommandHandler
    {
        private CommandService comserv;
        private RandomCatService catserv;
        private SearchService searchserv;
        private UserService userserv;

        private DiscordSocketClient client;
        private CommandHandler handler => this;
        private IDependencyMap map;

        public static Dictionary<ulong, Dictionary<ulong, string>> userDict = new Dictionary<ulong, Dictionary<ulong, string>>();
        private JsonSerializer json = new JsonSerializer();

        public CommandHandler(IDependencyMap _map)
        {
            client = _map.Get<DiscordSocketClient>();
            comserv = new CommandService();
            catserv = new RandomCatService();
            searchserv = new SearchService();
            userserv = new UserService(userDict);

            _map.Add(handler);
            _map.Add(catserv);
            _map.Add(searchserv);
            //_map.Add(comserv);
            map = _map;

            client.MessageReceived += handleCommand;
        }

        public async Task Install()
        {
            await comserv.AddModuleAsync<InfoModule>();
            await comserv.AddModuleAsync<GoldModule>();
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
        }
    }
}
