using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Discord_Console_Core.Services
{
    //public class MusicServices
    //{
    //    private readonly LavaNode _lavaRestClient;
    //    //private readonly LavaSocket _lavaSocketClient; Descontinuado
    //    private readonly DiscordSocketClient _client;

    //    public MusicServices(LavaNode lavaRestClient, DiscordSocketClient client, LogMessage logService)
    //    {
    //        _client = client;
    //        _lavaRestClient = lavaRestClient;
    //    }

    //    public async Task ConnectAsync(SocketVoiceChannel voiceChannel, ITextChannel textChannel)
    //        => await _lavaRestClient.ConnectAsync();

    //    public async Task LeaveAsync(SocketVoiceChannel voiceChannel)
    //        => await _lavaRestClient.DisconnectAsync();

    //    public async Task<string> PlayAsync(string query, IGuild guildId)
    //    {
    //        var _player = _lavaRestClient.GetPlayer(guildId);
    //        var results = await _lavaRestClient.SearchYouTubeAsync(query);

    //        if (results.LoadStatus == LoadStatus.NoMatches || results.LoadStatus == LoadStatus.LoadFailed)
    //        {
    //            return "No matches found.";
    //        }

    //        var track = results.Tracks.FirstOrDefault();

    //        if (_player.PlayerState != PlayerState.Playing)
    //        {
    //            _player.Queue.Enqueue(track);
    //            return $"{track.Title} has been added to the queue.";
    //        }
    //        else
    //        {
    //            await _player.PlayAsync(track);
    //            return $"Now Playing: {track.Title}";
    //        }
    //    }

    //    public async Task<string> StopAsync(IGuild guildId)
    //    {
    //        var _player = _lavaRestClient.GetPlayer(guildId);
    //        if (_player is null)
    //            return "Error with Player";
    //        await _player.StopAsync();
    //        return "Music Playback Stopped.";
    //    }

    //    public async Task<string> SkipAsync(IGuild guildId)
    //    {
    //        var _player = _lavaRestClient.GetPlayer(guildId);
    //        if (_player is null || _player.Queue.Items.Count() is 0)
    //            return "Nothing in queue.";

    //        var oldTrack = _player.Track;
    //        await _player.SkipAsync();
    //        return $"Skiped: {oldTrack.Title} \nNow Playing: {_player.Track.Title}";
    //    }

    //    public async Task<string> SetVolumeAsync(ushort vol, IGuild guildId)
    //    {
    //        var _player = _lavaRestClient.GetPlayer(guildId);
    //        if (_player is null)
    //            return "Player isn't playing.";

    //        if (vol > 150 || vol <= 2)
    //        {
    //            return "Please use a number between 2 - 150";
    //        }

    //        await _player.UpdateVolumeAsync(vol);
    //        return $"Volume set to: {vol}";
    //    }

    //    public async Task<string> PauseOrResumeAsync(IGuild guildId)
    //    {
    //        var _player = _lavaRestClient.GetPlayer(guildId);
    //        if (_player is null)
    //            return "Player isn't playing.";

    //        if (_player.PlayerState != PlayerState.Paused)
    //        {
    //            await _player.PauseAsync();
    //            return "Player is Paused.";
    //        }
    //        else
    //        {
    //            await _player.ResumeAsync();
    //            return "Playback resumed.";
    //        }
    //    }

    //    public async Task<string> ResumeAsync(IGuild guildId)
    //    {
    //        var _player = _lavaRestClient.GetPlayer(guildId);
    //        if (_player is null)
    //            return "Player isn't playing.";

    //        if (_player.PlayerState == PlayerState.Paused)
    //        {
    //            await _player.ResumeAsync();
    //            return "Playback resumed.";
    //        }

    //        return "Player is not paused.";
    //    }


    //    //private async Task ClientReadyAsync()
    //    //{
    //    //    await _lavaRestClient.ConnectAsync();
    //    //}

    //    private async Task TrackFinished(LavaPlayer player, LavaTrack track, TrackEndReason reason)
    //    {
    //        if (!reason.ShouldPlayNext())
    //            return;

    //        if (!player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
    //        {
    //            await player.TextChannel.SendMessageAsync("There are no more tracks in the queue.");
    //            return;
    //        }

    //        await player.PlayAsync(nextTrack);
    //    }

 

    //}
}
