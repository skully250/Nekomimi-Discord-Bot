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

namespace Nekomaid_Club_Bot.Core
{
    public class CommandHandler
    {
        private CommandService comserv;
        private RandomCatService catserv;
        private SearchService searchserv;

        private DiscordSocketClient client;
        private CommandHandler handler => this;
        private IDependencyMap map;

        public static Dictionary<ulong, string> prefixDict = new Dictionary<ulong, string>();
        private JsonSerializer json = new JsonSerializer();

        public async Task Install(IDependencyMap _map)
        {
            client = _map.Get<DiscordSocketClient>();
            comserv = new CommandService();
            catserv = new RandomCatService();
            searchserv = new SearchService();

            _map.Add(handler);
            _map.Add(catserv);
            _map.Add(searchserv);
            //_map.Add(comserv);
            map = _map;

            await comserv.AddModulesAsync(Assembly.GetEntryAssembly());
            Console.WriteLine("All Command modules loaded");

            client.MessageReceived += handleCommand;
        }

        public async Task handleCommand(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null) return;

            var context = new CommandContext(client, message);

            string prefix;
            if (prefixDict.TryGetValue(context.Guild.Id, out prefix)) Console.WriteLine("Prefix is " + prefix);
            else prefix = "!";

            int argpos = 0;
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argpos) || message.HasStringPrefix(prefix, ref argpos))) return;

            var result = await comserv.ExecuteAsync(context, argpos, map);

            if (!result.IsSuccess)
                await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }
    }
}
