using Application.Interfaces;

namespace Application.Services;

public class PostgreSqlBackup : IBackup //Need Implementation and data parser
{
    public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task RunBackup(string path, string dbname, string connectionString, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
