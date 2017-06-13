using System.Data;
using System.IO;

namespace DryLib.Sql
{
    public interface IDal
    {
        int ExecuteNonQuery(string sqlText);
        DataTable ReadUncommitted(FileInfo sqlFile);
        DataTable ReadUncommitted(string sqlStatement);
    }
}