using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Discord_Console_Core.Controllers
{
    public class ComandsController : ModuleBase<SocketCommandContext>
    {
        private readonly IServiceProvider _services;

        public ComandsController(IServiceProvider services)
        {
            _services = services;
        }

        [Command("Ola")]
        public async Task OlaMundo()
        {
            await ReplyAsync("Cala a boca, viado");
        }

        [Command("nao")]
        public async Task JoinChannel()
        {
            var user = Context.User as SocketGuildUser;

            try
            {
                if (user.VoiceChannel is null)
                {
                    await ReplyAsync($" {user.Username}, primeiro esteja em um Channel. ;)");
                    return;
                }
                else
                {
                    //await _musicService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
                    await ReplyAsync($"Galerinha, cheguei aqui no Channel: {user.VoiceChannel.Name}");
                }
            }
            catch
            {
                await ReplyAsync($"Por favor, execute esse comando no CHAT do Channel.");
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
