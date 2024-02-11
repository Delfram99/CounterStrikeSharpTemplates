using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;

namespace CSSharpTemplates;

// [MinimumApiVersion(160)]
public partial class CSSharpTemplates : BasePlugin, IPluginConfig<CSSharpTemplatesConfig>
{
    public CSSharpTemplatesConfig Config { get; set; } = new();
    internal static DataBaseService? _dataBaseService;

    public override string ModuleName => "MyPlugin";
    public override string ModuleDescription => "";
    public override string ModuleAuthor => "AuthorName";
    public override string ModuleVersion => "0.0.1";

    public void OnConfigParsed(CSSharpTemplatesConfig config)
	{
        _dataBaseService = new DataBaseService(config);
        _dataBaseService.TestAndCheckDataBaseTableAsync().GetAwaiter().GetResult();
        
        Config = config;
    }

    public override void Load(bool hotReload)
    {
    }
    
    [ConsoleCommand("css_hello", "Say hello in the player language")]
    public void OnCommandHello(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            string playerName = player.PlayerName;
            var playerLanguage = player.GetLanguage();
            ulong playerSteamId = player.SteamID;
            string commandString = command.GetArg(0).ToLower();

            _ = CheckPlayerCommand(commandString, playerName, playerSteamId);

            command.ReplyToCommand($"{Localizer["hello.player", playerName, playerLanguage]}");
        }
    }
}