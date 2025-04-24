using Application.Interfaces;
using Application.Services;
using Configure;
using DbBackupTool.Util.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Shared.Util;

string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
string settingsRoot = Path.Combine(projectRoot, "app.settings.json");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(Path.Combine(projectRoot, "Logs/log-.txt"), 
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

//Configure App
var cts = new CancellationTokenSource();
var app = new AppConfiguration()
.AddJsonFile(settingsRoot)
.ConfigureServices((services, configuration) =>
{
    services.AddScoped<IJsonParser, JsonParser>();
    services.AddScoped<CommandHandler>();
    services.AddScoped<UtilMethods>();
    services.AddScoped<SqlServerBackup>();
    services.AddScoped<PostgreSqlBackup>();
    services.AddScoped<IBackupFactory, BackupFactory>();

    services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(Log.Logger, dispose: true);
        });
})
.Build()
.Run(cts.Token).GetAwaiter().GetResult();
