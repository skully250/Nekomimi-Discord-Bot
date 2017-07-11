using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Net.Providers.WS4Net;

using Nekomimi_Rewrite.Services;
using System.Reflection;

namespace Nekomimi_Rewrite.Core
{
    class Program
    {
        private readonly DiscordSocketClient client;
        private readonly IServiceCollection _map = new ServiceCollection();
        private readonly CommandService _commands = new CommandService();
        private CommandHandler handler;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private Program()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                WebSocketProvider = WS4NetProvider.Instance,
                //UdpSocketProvider = UDPClientProvider.Instance,
            });
        }

        public async Task MainAsync()
        {
            client.Log += Logger;

            

            await InitCommands();

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider _services;
        public async Task InitCommands()
        {
            _map.AddSingleton(new TagService());
            _map.AddSingleton(new CatService());
            _map.AddSingleton(new LewdService());
            _map.AddSingleton(new GoldService());
            _map.AddSingleton(new TimeService());

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            _services = _map.BuildServiceProvider();

            client.MessageReceived += handleCommand;
        }

        public async Task handleCommand(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null) return;

            var context = new CommandContext(client, message);

            int argpos = 0;
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argpos) || message.HasCharPrefix('!', ref argpos))) return;

            var result = await _commands.ExecuteAsync(context, argpos, _services);

            if (!result.IsSuccess)
                await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }

        private Task Logger(LogMessage msg)
        {
            var cc = Console.ForegroundColor;
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{msg.Severity,8}] {msg.Source}: {msg.Message}");
            Console.ForegroundColor = cc;
            return Task.CompletedTask;
        }
    }
}