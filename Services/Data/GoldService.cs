using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Discord.WebSocket;
using Discord.Commands;

namespace Nekomimi_Rewrite.Services
{
    public class UserGold
    {
        public ulong id { get; set; }
        public int gold { get; set; }
    }

    public class GoldService
    {
        string gold;
        List<UserGold> json;

        public GoldService()
        {
            loadGold();
            gold = File.ReadAllText("gold.json");
            json = JsonConvert.DeserializeObject<List<UserGold>>(gold);
        }

        public void loadGold()
        {
            var path = "gold.json";
            if (!File.Exists(path))
            {
                List<UserGold> gold = new List<UserGold>();
                var _json = JsonConvert.SerializeObject(gold);
                using (var file = new FileStream(path, FileMode.Create)) { }
                File.WriteAllText(path, _json);
            }
            else
                return;
        }

        public int checkGold(SocketGuildUser user)
        {
            int goldamount = 0;
            if (json.Any(x => x.id == user.Id))
            {
                goldamount = json.First(x => x.id == user.Id).gold;
            }
            return goldamount;
        }

        public async Task saveData(CommandContext context, SocketGuildUser user, List<UserGold> json)
        {
            var goldam = json.First(x => x.id == user.Id).gold;
            await context.Channel.SendMessageAsync($":heavy_dollar_sign: **{user.Username}'s** gold: {goldam}");
            var _json = JsonConvert.SerializeObject(json);
            File.WriteAllText("gold.json", _json);
        }

        public async Task checkGoldAsync(CommandContext context, SocketGuildUser user)
        {
            try
            {
                if (!json.Any(x => x.id == user.Id))
                {
                    json.Add(new UserGold() { id = user.Id, gold = 0 });
                }
                await saveData(context, user, json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task removeGoldAsync(CommandContext context, SocketGuildUser user)
        {
            try
            {
                if (!json.Any(x => x.id == user.Id))
                {
                    json.Add(new UserGold() { id = user.Id, gold = 0 });
                }
                else
                {
                    json.First(x => x.id == user.Id).gold--;
                }
                await saveData(context, user, json);
                await context.Message.DeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task AddGoldAsync(CommandContext context, SocketGuildUser user)
        {
            try
            {
                if (!json.Any(x => x.id == user.Id))
                {
                    json.Add(new UserGold() { id = user.Id, gold = 1 });
                }
                else
                {
                    json.First(x => x.id == user.Id).gold++;
                }
                await saveData(context, user, json);
                await context.Message.DeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
