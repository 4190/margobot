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
        [Command("hello"), Summary("bot odpowiada na hello")]
        public async Task HelloCommand()
        {
            await Context.Channel.SendMessageAsync($"Hello {Context.User.Mention}");
        }

    }
}



