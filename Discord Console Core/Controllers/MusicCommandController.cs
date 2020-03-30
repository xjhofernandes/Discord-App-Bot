using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord_Console_Core.Services;
using Lavalink4NET;
using Lavalink4NET.Player;
using Lavalink4NET.Rest;
using Lavalink4NET.Decoding;
using SuperSocket.ClientEngine;

namespace Discord_Console_Core.Controllers
{
    public class MusicCommandController : ModuleBase<SocketCommandContext>
    {
        private readonly IAudioService _audioService;

        private LavalinkPlayer myPlayer;

        public MusicCommandController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        [Command("Join")]
        public async Task Join()
        {
            var user = Context.User as SocketGuildUser;
            var bot = Context.Client;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("You need to connect to a voice channel.");
                return;
            }
            else
            {
                var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                               ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);

                //var myTrack = await _audioService.GetTrackAsync("Skrillex", SearchMode.YouTube);

                //await myPlayer.PlayAsync(myTrack);
                await ReplyAsync($"now connected to {user.VoiceChannel.Name}");
            }
        }

        //    [Command("Leave")]
        //    public async Task Leave()
        //    {
        //        var user = Context.User as SocketGuildUser;
        //        if (user.VoiceChannel is null)
        //        {
        //            await ReplyAsync("Please join the channel the bot is in to make it leave.");
        //        }
        //        else
        //        {
        //            await _musicService.LeaveAsync(user.VoiceChannel);
        //            await ReplyAsync($"Bot has now left {user.VoiceChannel.Name}");
        //        }
        //    }

        [Command("Play")]
        public async Task Play([Remainder] string query)
        {
            var user = Context.User as SocketGuildUser;
            var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                           ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);

            var myTrack = await _audioService.GetTrackAsync(query, SearchMode.YouTube);
            await myPlayer.PlayAsync(myTrack);
        }

        [Command("PlaySoundCloud")]
        public async Task PlaySoundCloud([Remainder] string query)
        {
            var user = Context.User as SocketGuildUser;
            var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                           ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);

            var myTrack = await _audioService.GetTrackAsync(query, SearchMode.SoundCloud);
            await myPlayer.PlayAsync(myTrack);
        }

        [Command("Pause")]
        public async Task Pause()
        {
            var user = Context.User as SocketGuildUser;
            var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                           ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);

            await myPlayer.PauseAsync();
        }

        //    [Command("Skip")]
        //    public async Task Skip()
        //        => await ReplyAsync(await _musicService.SkipAsync(Context.Guild));

        [Command("Volume")]
        public async Task Volume(Single vol)
        {
            var user = Context.User as SocketGuildUser;
            var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                           ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);
            var volume = Convert.ToSingle(vol/100);

            await myPlayer.SetVolumeAsync(volume);
        }

        [Command("Stop")]
        public async Task Stop()
        {
            var user = Context.User as SocketGuildUser;
            var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                           ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);

            await myPlayer.StopAsync(disconnect: true);
        }

        [Command("Resume")]
        public async Task Resume()
        {
            var user = Context.User as SocketGuildUser;
            var myPlayer = _audioService.GetPlayer<LavalinkPlayer>(Context.Guild.Id)
                           ?? await _audioService.JoinAsync(Context.Guild.Id, user.VoiceChannel.Id);

            await myPlayer.ResumeAsync();
        }
    }
}
