using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Disc.Core.Commands
{
    public class Helloworld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Summary("bot odpowiada na hello")]     // linijka moze wygladac tez tak:   [Command("hello"), Alias("jakis tekst", "jakis kolejny tekst"), Summary("bot odpowiada na hello")]    
        //Command to bazowa komenda do wywołania działania bota przypisanego do komendy, Alias to inne slowa/frazy ktore wywolaja to samo.
        //przydatne w wypadku np Case Sensitive jesli chcemy by dzialalo zarowno hello jak i Hello 
        public async Task HelloCommand()
        {
            await Context.Channel.SendMessageAsync($"Hello {Context.User.Mention}");
        }

    }
}



