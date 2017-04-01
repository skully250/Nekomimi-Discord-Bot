using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Discord;
using Discord.Commands;
using Discord.Audio;

using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace Nekomaid_Club_Bot
{
    class Nekomimi
    {

        public Nekomimi()
        {
            Start();
        }

        public static void sendPet(MessageEventArgs e, string[] messageArray)
        {
            string message = messageArray[1];
            if (messageArray[1] == "lyna")
                message = "<:lyna:294645917909254144>";
            if (messageArray.Length > 2)
            {
                for (int i = 2; i < messageArray.Length; i++)
                {
                    message += " " + messageArray[i];
                }
            }
            try
            {
                e.Channel.SendMessage("*pets " + message + "*");
            } catch (Exception exception)
            {
                Console.Write(exception.Message);
                e.Channel.SendMessage("Why would you do that? you mean human being");
            }
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

            _client.UsingAudio(x =>
            {
                x.Mode = AudioMode.Outgoing;
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

            string[] foxes = {
                "http://i.imgur.com/1VkY1R4.jpg", "http://i.imgur.com/g77dAsa.gif", "http://i.imgur.com/6Xt3mQo.jpg", "http://i.imgur.com/Tm05gsh.jpg",
                "http://i.imgur.com/9FzD5TA.gif", "http://i.imgur.com/A1dAUP1.jpg", "http://i.imgur.com/qACO45t.jpg", "http://i.imgur.com/jqoH6tT.jpg",
                "http://i.imgur.com/JmAUuGe.jpg"};
            comserv.CreateCommand("fox").Do(async (e) =>
            {
                int index = new Random().Next(0, foxes.Length);
                await e.Channel.SendMessage(foxes[index]);
            });

            comserv.CreateCommand("ohayou")
                .Alias("おはよう")
                .Do(async (e) =>
            {
                await e.Channel.SendMessage("http://i.imgur.com/Hbl7w2T.gif");
            });

            comserv.CreateCommand("bakanano")
                .Alias("バカなの")
                .Do(async (e) =>
            {
                await e.Channel.SendMessage("http://i2.kym-cdn.com/photos/images/newsfeed/001/095/334/4f9.gif");
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
                if (messageArray.Length == 2)
                {
                    if (messageArray[1] == "me")
                    {

                    }
                    else
                    {
                        sendPet(e, messageArray);
                    }
                }
                else if (messageArray.Length > 2)
                {
                    sendPet(e, messageArray);
                }
            }

            switch(e.Message.Text)
            {
                case "world":
                    e.Channel.SendMessage("What are we gonna do DJeeta?");
                    break;
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
