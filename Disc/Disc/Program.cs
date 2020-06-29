using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disc
{
    class Program
    {


        List<IMessage> mes = new List<IMessage>();
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
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(),null);
            Client.Ready += Client_Ready;
            Client.Log += Client_Log;
            Client.LatencyUpdated += Client_LatencyUpdated;


            string Token = "Njg3NjU5NzY1NTYzNzg1MjU2.XqpiQw.Sff6OjItM-TFUA6Yyq9F8mw7DkM";      //token bota. NIE UPUBLICZNIAĆ NIGDZIE
            // string testToken = "";
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Task.Delay(-1);       //bot nie wylacza sie po za dlugim oczekiwaniu
        }

        private Task Client_LatencyUpdated(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if(Context.Channel.Name == "esther")
            {
                Context.Channel.SendMessageAsync("@Siru-Nyan#4015 buy premium");
            }


        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Piece of shit", "https://www.google.com/");       //wyswietla ze bot jest w grze "piece of shit".
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            

            int ArgPos = 0;
            if (!(Message.HasStringPrefix("a!", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos)))
            {

                return;
            }


            var Result = await Commands.ExecuteAsync(Context, ArgPos, null);    

            if(!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}"); 
            }
        }


    }
}
