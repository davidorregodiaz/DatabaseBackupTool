using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class BackupFactory : IBackupFactory
{
    
    private readonly IServiceProvider _serviceProvider;
    public BackupFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IBackup Create(string type)
    {
        if (!string.IsNullOrWhiteSpace(type))
        {
            switch (type.Trim().ToLowerInvariant())
            {
                case "sqlserver":
                    return _serviceProvider.GetRequiredService<SqlServerBackup>();
                case "postgresql":
                    return _serviceProvider.GetRequiredService<PostgreSqlBackup>();
                default:
                    throw new ArgumentException("Type not supported");
            }
        }
        else
        {
            throw new ArgumentException("Type not supported");
        }
    }
}
