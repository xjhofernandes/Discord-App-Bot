using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Discord_Console_Core.Services;

namespace Discord_Console_Core
{

    public class Program : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            //var botToken = "NjkzMzEwMTYzNTExMTQ4NTY0.Xn9wrQ.BO73mRxkYe2MPjhqpicSV_7zp9I";
            var botToken = "NjkzMzEwMTYzNTExMTQ4NTY0.Xn9wrQ.BO73mRxkYe2MPjhqpicSV_7zp9I";

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 50,
                LogLevel = LogSeverity.Debug
            });
            _commands = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).AddSingleton<MusicServices>().BuildServiceProvider();

            _client.Ready += Client_Ready;
            _client.Log += ClientLog;
            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, botToken);
            await Client_Ready();
            await ComandosBot();

            await _client.StartAsync();
            await Task.Delay(-1);
        }
        private async Task ComandosBot()
        {
            _client.MessageReceived += iniciandoComandos;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task iniciandoComandos(SocketMessage arg)
        {
            var mensagem = arg as SocketUserMessage;
            if (mensagem is null || mensagem.Author.IsBot) return;

            var Context = new SocketCommandContext(_client, mensagem);
            int argPost = 0;
            if (mensagem.HasStringPrefix("-", ref argPost))
            {
                var result = await _commands.ExecuteAsync(Context, argPost, _services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }

        }

        private async Task Client_Ready()
        {
            await _client.SetGameAsync("O jogo do menino bot", "http://google.com.br", ActivityType.Listening);
        }

        private Task ClientLog(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "oi" || message.Content == "Oi" && !message.Author.IsBot)
                await message.Channel.SendMessageAsync("Cala boca viado");

            if (message.Content == "-comandos" || message.Content == "-cmd" && !message.Author.IsBot)
                await message.Channel.SendMessageAsync("Segue a lista de comandos: \n-join: Me coloca dentro do seu Channel ( ͡° ͜ʖ ͡°) \n-google: Faz aquela busca marota \n*Comandos em desenvolvimento! :) ");

            if (message.Content == "play" || message.Content == "play" && !message.Author.IsBot)
                await message.Channel.SendMessageAsync("Em implementação");

            if (message.Content != null && message.Content[0] != '-' && !message.Author.IsBot)
                await message.Channel.SendMessageAsync("Koé maluco, os comandos começam com \"-\". Tenta aí de novo: -comandos ou -cmd ");
        }

        private Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });
        }

        private async Task SendAsync(IAudioClient client, string path)
        {
            // Create FFmpeg using the previous example
            using (var ffmpeg = CreateStream(path))
            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }
        }
    }
}
