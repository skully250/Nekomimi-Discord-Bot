using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nekomaid_Club_Bot.Util
{
    public class UserGold
    {
        public ulong id { get; set; }
        public int gold { get; set; }
    }

    public class Gold
    {
        public static void goldCheck()
        {
            var path = "gold.json";
            if (!File.Exists(path))
            {
                List<UserGold> gold = new List<UserGold>();
                var json = JsonConvert.SerializeObject(gold);
                using (var file = new FileStream(path, FileMode.Create)) { }
                File.WriteAllText(path, json);
            }
            else
                return;
        }

        public static void serialiseJSon()
        {

        }

        public static async Task checkGoldAsync(CommandContext context, SocketGuildUser user)
        {
            goldCheck();
            var gold = File.ReadAllText("gold.json");
            var json = JsonConvert.DeserializeObject<List<UserGold>>(gold);
            try
            {
                if (!json.Any(x => x.id == user.Id))
                {
                    json.Add(new UserGold() { id = user.Id, gold = 0 });
                    var _json = JsonConvert.SerializeObject(json);
                    File.WriteAllText("gold.json", _json);
                }
                var goldam = json.First(x => x.id == user.Id).gold;
                await context.Channel.SendMessageAsync($":heavy_dollar_sign: **{user.Username}'s** gold: {goldam}");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task AddGoldAsync(CommandContext context, SocketGuildUser user)
        {
            goldCheck();
            var gold = File.ReadAllText("gold.json");
            var json = JsonConvert.DeserializeObject<List<UserGold>>(gold);
            try
            {
                if (!json.Any(x => x.id == user.Id))
                {
                    json.Add(new UserGold() { id = user.Id, gold = 1 });
                    await context.Channel.SendMessageAsync($":heavy_dollar_sign: **{user.Username}'s** gold: 1");
                } else
                {
                    json.First(x => x.id == user.Id).gold++;
                    var goldam = json.First(x => x.id == user.Id).gold;
                    await context.Channel.SendMessageAsync($":heavy_dollar_sign: **{user.Username}'s** gold: {goldam}");
                    var _json = JsonConvert.SerializeObject(json);
                    File.WriteAllText("gold.json", _json);
                }
                await context.Message.DeleteAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
