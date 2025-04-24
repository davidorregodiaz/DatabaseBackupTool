using DbBackupTool.Util.Interfaces;
using System.Text.Json.Nodes;


namespace Shared.Util
{
    public class UtilMethods
    {
        private readonly IJsonParser _parser;

        public UtilMethods(IJsonParser parser)
        {
            _parser = parser;
        }

        public JsonNode GetConfigAsJsonNode()
        {
            string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string settingsRoot = Path.Combine(projectRoot,"app.settings.json");
            
            var settingString = File.ReadAllText(settingsRoot);

            return _parser.Parse(settingString);
        }
        
    }
}