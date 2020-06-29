using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

using Discord;
using Discord.Commands;

namespace Disc.Core.Commands
{
    public static class GetFile
    {
        public static List<string> GetHero(string text)
        {
            var hero = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText($"heroes/{text}.txt"));
            return hero;
        }
        public static void SaveHero(List<string> hero, string text)
        {
            List<string> Hero = hero;
            string js = JsonConvert.SerializeObject(Hero);
            File.WriteAllText($"heroes/{text}.txt", js);
        }
        public static List<string> GetHeroCoords(string text)
        {
            var hero = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText($"coords/{text}Coords.txt"));
            return hero;
        }
        public static void SaveHeroCoords(List<string> hero, string text)
        {
            List<string> Hero = hero;
            string js = JsonConvert.SerializeObject(Hero);
            File.WriteAllText($"coords/{text}Coords.txt", js);
        }
    }

    public class Helloworld : ModuleBase<SocketCommandContext>
    {
        [Command("test"), Summary("testing")]          // Use for testing
        public async Task TestCommand()
        {
            var embed = new EmbedBuilder();
            // Or with methods
            embed.AddField("Minimal: ",
                "00")                                                       // "Field value. I also support [hyperlink markdown](https://example.com)!")
                .WithAuthor("AUTHOR")
                //  .WithFooter(footer => footer.Text = "I am a footer.")
                .WithColor(Color.Blue)
                .WithTitle("Wicked Patrick")
                .WithCurrentTimestamp();
            embed.AddField("Maximal", "00", false);

            //Your embed needs to be built before it is able to be sent
            await ReplyAsync(embed: embed.Build());

        }

        [Command("info"), Summary("info about heroes")]
        public async Task InfoCommand([Remainder]string text)
        {
            try
            {
                List<string> hero = GetFile.GetHero(text);

                var embed = new EmbedBuilder();
                embed.AddField("Minimal: ", hero[3])
                    .WithAuthor(hero[1])
                    .WithColor(Color.Blue)
                    .WithTitle("Last killed: " + hero[2] + "--" + hero[0])
                    .WithCurrentTimestamp()
                    .WithImageUrl(hero[5]);
                embed.AddField("Maximal", hero[4], false);
                await ReplyAsync(embed: embed.Build());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Unknown hero");
                await Context.Channel.SendMessageAsync("Unknown hero.", false, null);
            }
        }

        [Command("kill"), Summary("add kill timers")]
        public async Task KillCommand([Remainder]string text)
        {
            var minimal = "";
            var maximal = "";
            List<string> hero = GetFile.GetHero(text);
            var killTime = DateTime.Now.ToString("HH:mm");
            string killDay = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString();
            hero[0] = killDay;
            hero[2] = killTime;

            minimal = DateTime.Now.AddHours(Convert.ToInt32(hero[6])).AddMinutes(Convert.ToInt32(hero[7])).ToString("HH:mm");
            maximal = DateTime.Now.AddHours(Convert.ToInt32(hero[8])).AddMinutes(Convert.ToInt32(hero[9])).ToString("HH:mm");

            hero[3] = minimal;
            hero[4] = maximal;
            string log = $"\n {DateTime.Now}:: User {Context.Message.Author} added kill entry for {hero[1]}";
            File.AppendAllText("LogFile.txt", log + Environment.NewLine);
            GetFile.SaveHero(hero, text);
            await Context.Channel.SendMessageAsync("Timer updated.", false, null);
        }

        [Command("add"), Summary("add coord of hero")]
        public async Task AddCommand([Remainder]string text)
        {
            string heroName = text.Substring(0, text.IndexOf(" "));
            bool coordExist = false;
            List<string> heroCoords = GetFile.GetHeroCoords(heroName);

            int index = text.IndexOf(" ") + 1;
            Console.WriteLine(text.Substring(index, text.Length - index));
            for (int i = 0; i < heroCoords.Count; i++)
            {
                if(heroCoords[i] == text.Substring(index, text.Length - index))
                {
                    coordExist = true;
                }
            }

            if(coordExist) { await Context.Channel.SendMessageAsync("Coord already exists"); }
            else
            {
                heroCoords.Add(text.Substring(index, text.Length - index)); heroCoords.Sort();
                GetFile.SaveHeroCoords(heroCoords, heroName);
                await Context.Channel.SendMessageAsync("Added to list: " + text.Substring(index, text.Length - index));

                string log = $"\n {DateTime.Now}:: User {Context.Message.Author} added coord entry for {heroName}-- {text.Substring(9, text.Length - 9)}\n";
                File.AppendAllText("LogFile.txt", log + Environment.NewLine);
            }
        }

        [Command("list"), Summary("return list of coords from file")]
        public async Task ListCommand([Remainder]string text)
        {
            List<string> heroCoords = GetFile.GetHeroCoords(text);

            string list = "";
            for (int i = 0; i < heroCoords.Count; i++)
                list += heroCoords[i] + ", ";
            await Context.Channel.SendMessageAsync(list, false, null);
        }
    }
}
