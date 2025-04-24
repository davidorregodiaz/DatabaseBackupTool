
using System.Text.Json.Nodes;
using DbBackupTool.Util.Interfaces;

namespace Shared.Util
{
    public class JsonParser : IJsonParser, IParser<JsonNode,string>
    {
        public JsonNode Parse(string value)
        {
            return JsonNode.Parse(value)!;
        }
    }
}