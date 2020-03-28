using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord_Console_Core.Services;

namespace Discord_Console_Core.Controllers
{
    public class MusicCommandController : ModuleBase<SocketCommandContext>
    {
        private MusicServices _musicService;

        public MusicCommandController(MusicServices musicService)
        {
            _musicService = musicService;
        }

        [Command("Join")]
        public async Task Join()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("You need to connect to a voice channel.");
                return;
            }
            else
            {
                await _musicService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync($"now connected to {user.VoiceChannel.Name}");
            }
        }

        [Command("Leave")]
        public async Task Leave()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("Please join the channel the bot is in to make it leave.");
            }
            else
            {
                await _musicService.LeaveAsync(user.VoiceChannel);
                await ReplyAsync($"Bot has now left {user.VoiceChannel.Name}");
            }
        }

        [Command("Play")]
        public async Task Play([Remainder]string query)
            => await ReplyAsync(await _musicService.PlayAsync(query, Context.Guild));


        [Command("Stop")]
        public async Task Stop()
            => await ReplyAsync(await _musicService.StopAsync(Context.Guild));

        [Command("Skip")]
        public async Task Skip()
            => await ReplyAsync(await _musicService.SkipAsync(Context.Guild));

        [Command("Volume")]
        public async Task Volume(ushort vol)
            => await ReplyAsync(await _musicService.SetVolumeAsync(vol, Context.Guild));

        [Command("Pause")]
        public async Task Pause()
            => await ReplyAsync(await _musicService.PauseOrResumeAsync(Context.Guild));

        [Command("Resume")]
        public async Task Resume()
            => await ReplyAsync(await _musicService.ResumeAsync(Context.Guild));
    }
}
