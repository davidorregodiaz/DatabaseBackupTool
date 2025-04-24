namespace Application.Interfaces;

public interface IBackupFactory
{
    IBackup Create(string type);
}
