using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace CSSharpTemplates
{
    public class CSSharpTemplatesConfig : BasePluginConfig
    {
        public override int Version { get; set; } = 1;

		[JsonPropertyName("StringValue")]
		public string StringValue { get; set; } = "Hello";

		[JsonPropertyName("IntValue")]
		public int IntValue { get; set; } = 2024;

    }
}
