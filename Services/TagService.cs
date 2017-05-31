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

    class TagService
    {
        CommandContext _context;
        string tags;
        List<UserTags> json;
        
        public TagService(CommandContext context)
        {
            _context = context;
            loadTags();
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

        public async Task FindTag(SocketGuildUser user, string tagName)
        {
            try
            {
                string tagContent;
                if (json.First(x => x.id == user.Id).tags.TryGetValue(tagName, out tagContent))
                    await _context.Channel.SendMessageAsync(tagContent);
                else
                    await _context.Channel.SendMessageAsync($"The content for {tagName} could not be found or doesnt exist");
            } catch (Exception e)
            {
                await _context.Channel.SendMessageAsync(e.Message);
            }
        }
        
        public async Task CreateTag(SocketGuildUser user, string tagName, string tagContent)
        {
            try
            {
                if (json.First(x => x.id == user.Id).tags.TryAdd(tagName, tagContent))
                    await _context.Channel.SendMessageAsync($"Tag {tagName} has been added");
                else
                    await _context.Channel.SendMessageAsync($"Tag {tagName} was not successfully added");
            } catch (Exception e)
            {
                await _context.Channel.SendMessageAsync(e.Message);
            }
        }

        public async Task ModifyTag(SocketGuildUser user)
        {
            try
            {
                //TODO: Work out an efficient way to modify tags
            } catch (Exception e)
            {
                await _context.Channel.SendMessageAsync(e.Message);
            }
        }

        public async Task RemoveTag(SocketGuildUser user, string tagName)
        {
            try
            {
                string tagContent;
                if (json.First(x => x.id == user.Id).tags.TryRemove(tagName, out tagContent))
                {
                    await _context.Channel.SendMessageAsync($"Tag {tagName} was successfully removed");
                } else
                {
                    await _context.Channel.SendMessageAsync($"Tag {tagName} was not removed or doesnt exist");
                }
            } catch (Exception e)
            {
                await _context.Channel.SendMessageAsync(e.Message);
            }
        }

    }
}