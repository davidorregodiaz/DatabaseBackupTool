



namespace Application.Interfaces
{
    public interface IBackup
    {
        Task RunBackup(string path, string dbname, string connectionString, CancellationToken cancellationToken);
    }
}