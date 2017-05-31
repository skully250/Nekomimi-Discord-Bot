using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

using Newtonsoft.Json;
using Discord.Commands;
using Discord.WebSocket;

namespace Nekomimi_Rewrite.Services
{
    public class UserTags
    {
        public ulong id { get; set; }
        public ConcurrentDictionary<string, string> tags { get; set; }
    }

    public class TagService
    {
        string tags;
        List<UserTags> json;
        
        public TagService()
        {
            loadTags();
            tags = File.ReadAllText("tags.json");
            json = JsonConvert.DeserializeObject<List<UserTags>>(tags);
        }

        public void loadTags()
        {
            var path = "tags.json";
            if (!File.Exists(path))
            {
                List<UserTags> tags = new List<UserTags>();
                var json = JsonConvert.SerializeObject(tags);
                using (var file = new FileStream(path, FileMode.Create)) { }
                File.WriteAllText(path, json);
            }
            else
                return;
        }

        public void saveTags()
        {
            var _json = JsonConvert.SerializeObject(json);
            File.WriteAllText("tags.json", _json);
        }

        public async Task FindTag(CommandContext context, SocketGuildUser user, string tagName)
        {
            try
            {
                string tagContent;
                if (json.First(x => x.id == user.Id).tags.TryGetValue(tagName, out tagContent))
                    await context.Channel.SendMessageAsync(tagContent);
                else
                    await context.Channel.SendMessageAsync($"The content for {tagName} could not be found or doesnt exist");
                saveTags();
            } catch (Exception e)
            {
                await context.Channel.SendMessageAsync(e.Message);
            }
        }
        
        public async Task CreateTag(CommandContext context, SocketGuildUser user, string tagName, string tagContent)
        {
            try
            {
                if (!json.Any(x => x.id == user.Id))
                {
                    var tagAdd = new ConcurrentDictionary<string, string>();
                    tagAdd.TryAdd(tagName, tagContent);
                    json.Add(new UserTags() { id = user.Id, tags = tagAdd });
                }
                else
                {
                    json.First(x => x.id == user.Id).tags.TryAdd(tagName, tagContent);
                }
                await context.Channel.SendMessageAsync($"Tag {tagName} has been added");
                saveTags();
            } catch (Exception e)
            {
                await context.Channel.SendMessageAsync(e.Message);
            }
        }

        public async Task ModifyTag(CommandContext context, SocketGuildUser user)
        {
            try
            {
                //TODO: Work out an efficient way to modify tags
            } catch (Exception e)
            {
                await context.Channel.SendMessageAsync(e.Message);
            }
        }

        public async Task RemoveTag(CommandContext context, SocketGuildUser user, string tagName)
        {
            try
            {
                string tagContent;
                if (json.First(x => x.id == user.Id).tags.TryRemove(tagName, out tagContent))
                {
                    await context.Channel.SendMessageAsync($"Tag {tagName} was successfully removed");
                } else
                {
                    await context.Channel.SendMessageAsync($"Tag {tagName} was not removed or doesnt exist");
                }
                saveTags();
            } catch (Exception e)
            {
                await context.Channel.SendMessageAsync(e.Message);
            }
        }

    }
}