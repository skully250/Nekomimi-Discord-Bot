using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Discord;
using Discord.Commands;

namespace Nekomaid_Club_Bot
{
    class Nekomimi
    {

        public Nekomimi()
        {
            Start();
        }

        public static void Start()
        {
            DiscordClient _client = new DiscordClient(x =>
            {
                x.AppName = "Nekomimi";
                x.LogHandler = LogHandler;
            });

            _client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.HelpMode = HelpMode.Public;
            });

            CommandService comserv = _client.GetService<CommandService>();

            comserv.CreateCommand("Hello")
                .Description("Greets a person at their whim")
                .Do(async (e) =>
            {
                string username = e.Message.User.ToString().Split('#')[0];
                if (username == "enka")
                    await e.Channel.SendMessage("Hello Cute Squirrelmaid *pets*");
                else
                    await e.Channel.SendMessage("Hello " + username + "!");
            });

            comserv.CreateCommand("lolipolice").Do(async (e) =>
            {
                await e.Channel.SendMessage("https://a.disquscdn.com/uploads/mediaembed/images/3983/1706/original.gif?w=800&h");
            });

            comserv.CreateCommand("cat").Do(async (e) =>
            {
                RandomCat image = new RandomCat();
                await e.Channel.SendMessage(image.getRandomNeko());
            });

            comserv.CreateCommand("ohayou")
                .Alias("おはよう")
                .Do(async (e) =>
            {
                await e.Channel.SendMessage("http://i.imgur.com/Hbl7w2T.gif");
            });

            _client.MessageReceived += messageHandler;

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect("Mjk0ODA5OTA1NTQ5MDE3MDg4.C7drNg.2xTdAHHZQqbyB_IL48D0CT2qhCU", TokenType.Bot);
            });
        }

        public static void messageHandler(object sender, MessageEventArgs e)
        {
            string[] messageArray = e.Message.Text.Split(' ');
            if (messageArray[0] == "pet")
            {
                if (messageArray.Length >= 2)
                {
                    if (messageArray[1] != "me")
                    {
                        string message = messageArray[1];
                        for (int i = 2; i < messageArray.Length; i++)
                        {
                            message += " " + messageArray[i];
                        }
                        e.Channel.SendMessage("*pets " + message + "*");
                    }
                }
            }
            switch(e.Message.Text)
            {
                case "(╯°□°）╯︵ ┻━┻":
                    e.Channel.SendMessage("┬─┬﻿ ノ( ゜-゜ノ)");
                    break;
                case "pet me":
                    if (e.User.Name == "enka")
                        e.Channel.SendMessage("*pets " + e.User.Name + "'s fluffy tail*");
                    else
                        e.Channel.SendMessage("*pets " + e.User.Name + "*");
                    break;
            }
        }

        public static void LogHandler(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
