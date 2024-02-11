using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API.Modules.Commands;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace CSSharpTemplates;

// [MinimumApiVersion(159)]
public class CSSharpTemplates : BasePlugin
{
    public override string ModuleName => "MyPlugin";
    public override string ModuleDescription => "";
    public override string ModuleAuthor => "AuthorName";
    public override string ModuleVersion => "0.0.1";

    public override void Load(bool hotReload)
    {
        Console.WriteLine($"{ModuleName} loaded successfully!");
    }

    [ConsoleCommand("css_hello", "Say hello in the player language")]
    public void OnCommandHello(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            var PlayerName = player.PlayerName;
            var PlayerLanguage = player.GetLanguage();

            command.ReplyToCommand($"{Localizer["hello.player", PlayerName, PlayerLanguage]}");
        }
    }
}