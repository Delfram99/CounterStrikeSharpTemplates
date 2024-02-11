using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;

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
        Console.WriteLine($"{ModuleName} loaded successfully!");
    }

}