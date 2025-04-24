

namespace DbBackupTool.Util.Interfaces
{
    public interface IParser <T,Y>
    {
        T Parse(Y value);
    }
}