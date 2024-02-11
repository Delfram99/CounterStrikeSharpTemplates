using System.Globalization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;

namespace CSSharpTemplates;

// [MinimumApiVersion(160)]
public class CSSharpTemplates : BasePlugin, IPluginConfig<CSSharpTemplatesConfig>
{
    public override string ModuleName => "MyPlugin";
    public override string ModuleDescription => "";
    public override string ModuleAuthor => "AuthorName";
    public override string ModuleVersion => "0.0.1";

    public CSSharpTemplatesConfig Config { get; set; } = new();

    public void OnConfigParsed(CSSharpTemplatesConfig config)
	{
        Console.WriteLine($"{config.StringValue} {config.IntValue}!");
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        Logger.LogInformation("[MyPlugin] {Message} {Message}!", Config.StringValue, Config.IntValue);
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