using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Util;

namespace Configure;

public class AppConfiguration
{
    
   private readonly IServiceCollection _services = new ServiceCollection();
   private readonly ConfigurationManager _configuration = new();
   public IConfiguration Configuration => _configuration;
   public IServiceProvider? ServiceProvider {get;set;} = null;

    public AppConfiguration AddJsonFile(string rootPath)
    {
        _configuration.AddJsonFile(rootPath , optional:false, reloadOnChange:true);
        return this;    
    }

    public AppConfiguration ConfigureServices(Action<IServiceCollection,IConfiguration> configure)
    {
        _services.AddSingleton<IConfiguration>(_configuration);

        configure(_services,_configuration);
        return this;
    }

    public AppConfiguration Build()
    {
        ServiceProvider = _services.BuildServiceProvider();
        return this;
    }

    public async Task<AppConfiguration> Run(CancellationToken cancellationToken = default)
    {
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true; // previene que se cierre abruptamente
            Console.WriteLine("\nCancelación solicitada...");
        };

        try
        {
            await StartupApp(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operación cancelada por el usuario.");
        }

        return this;
    }

    private async Task StartupApp(CancellationToken cancellationToken)
    {
        Print.InfoMessage("Backup Tool (escribe 'salir' para cancelar o Ctrl+C)");

        while (!cancellationToken.IsCancellationRequested)
        {
            var cmd = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cmd)) continue;

            if (cmd.Trim().ToLower() == "salir")
            {
                Console.WriteLine("Saliendo por orden del usuario...");
                break;
            }

            var cmds = cmd.Split(" ");
            using var scope = ServiceProvider!.CreateScope();
            var commandHandler = scope.ServiceProvider.GetRequiredService<CommandHandler>();
            await commandHandler.HandleCommand(cmds, cancellationToken);
        }
    }
    
}
