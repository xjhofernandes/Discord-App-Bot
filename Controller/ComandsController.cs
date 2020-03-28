using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_App_Bot.Controller
{
    public class ComandsController : ModuleBase<SocketCommandContext>
    {

        [Command("Ola")]
        public async Task OlaMundo()
        {
            await ReplyAsync("Cala a boca, viado");
        }

        [Command("join")]
        public async Task JoinChannel()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync($" {user.Username}, você precisa primeira estar em um Channel.");
                return;
            }
            else
            {
                //await _musicService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync($"Galerinha, cheguei aqui no Channel: {user.VoiceChannel.Name}");
            }
        }

        [Command("google")]
        public async Task GoogleSearch([Remainder]string text)
        {
            var busca = "https://www.google.com/search?q=";
            await ReplyAsync(busca + text.Replace(" ", "+"));
        }
    }
}
