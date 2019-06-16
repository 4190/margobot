using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disc
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;


        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        


        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(),null);
            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            string Token = "NTMzNjg4MjcwNzE5Mjg3Mjk2.XQY7OA.DrFAb0oT2HD43l7Mw3ynA621bSo";
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Task.Delay(-1); 
        }
        //komentaaaa
        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Piece of shit", "https://www.google.com/");
        }

        private Task Client_MessageReceived(SocketMessage arg)
        {
            throw new NotImplementedException();
        }


    }
}
