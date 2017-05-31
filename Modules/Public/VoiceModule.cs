using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Nekomimi_Rewrite.Services;

using Discord.Audio;
using Discord.Commands;
using Discord;

namespace Nekomimi_Rewrite.Modules.Public
{
    class VoiceModule : ModuleBase<ICommandContext>
    {
        private readonly AudioService _service;

        public VoiceModule(AudioService service)
        {
            _service = service;
        }

        // You *MUST* mark these commands with 'RunMode.Async'
        // otherwise the bot will not respond until the Task times out.
        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinCmd()
        {
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
            await _service.SendAudioAsync(Context.Guild, Context.Channel, "1925.mp3");
        }

        // Remember to add preconditions to your commands,
        // this is merely the minimal amount necessary.
        // Adding more commands of your own is also encouraged.
        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveCmd()
        {
            await _service.LeaveAudio(Context.Guild);
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task PlayCmd(/*[Remainder] string song*/)
        {
            await _service.SendAudioAsync(Context.Guild, Context.Channel, "1925.mp3");
        }
    }
}
