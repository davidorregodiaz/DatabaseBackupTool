using System.CommandLine;
using Application.Interfaces;

namespace Shared.Util
{
    public class CommandHandler
    {
        private readonly UtilMethods _utilMethods;
        private readonly IBackupFactory _backupFactory;
        public CommandHandler(UtilMethods utilMethods, IBackupFactory backupFactory)
        {
            _utilMethods = utilMethods;
            _backupFactory = backupFactory;
        }

        public bool IsRequired { get; private set; }

        public async Task HandleCommand(string[] cmds, CancellationToken cancellationToken)
        {
            var rootCommand = new RootCommand("Root Command");
            var backupCommand = CreatebackupCommand(cancellationToken);
            rootCommand.Add(backupCommand);

            await rootCommand.InvokeAsync(cmds);
        }

        private Command CreatebackupCommand(CancellationToken cancellationToken)
        {
            
            var backupCommand = new Command("backup", "Made a database backup");
            var connectionStringOption = new Option<string>("--connection", "Specifies the Connection String") { IsRequired = true };
            var providerOption = new Option<string>("--provider", "Set the Database Provider") { IsRequired = true };
            var pathOption = new Option<string>("--path", "Give the path for the backup storage");
            var databaseNameOption = new Option<string>("--name", "sets the name of the database to backup");

            backupCommand.AddOption(connectionStringOption);
            backupCommand.AddOption(providerOption);
            backupCommand.AddOption(pathOption);
            backupCommand.AddOption(databaseNameOption);

            backupCommand.SetHandler(async (string provider, string connectionString, string pathOption, string databaseName) =>
            {
                var appsettings = _utilMethods.GetConfigAsJsonNode();
                string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
                string settingsRoot = Path.Combine(projectRoot, "app.settings.json");
                appsettings["Database"]!["Provider"] = provider;
                appsettings["Database"]!["ConnectionString"] = connectionString;
                await File.WriteAllTextAsync(settingsRoot, appsettings.ToString());

                var backup = _backupFactory.Create(provider);
                await backup.RunBackup(pathOption,databaseName,connectionString,cancellationToken);

            }, providerOption, connectionStringOption, pathOption, databaseNameOption);

            return backupCommand;
        }
    }
}