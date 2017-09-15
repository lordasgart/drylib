namespace DryLib.Sql
{
    public interface ISettings
    {
        string GetConnectionString(string key);
    }
}