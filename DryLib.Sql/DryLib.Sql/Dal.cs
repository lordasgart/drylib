using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryLib.Sql
{
    public class Dal : IDal
    {
        private readonly string _connectionString;

        public Dal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int ExecuteNonQuery(string sqlText)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var sqlCommand = new SqlCommand(sqlText, sqlConnection);

                return sqlCommand.ExecuteNonQuery();
            }
        }

        public DataTable ReadUncommitted(FileInfo sqlFile)
        {
            return ReadUncommitted(File.ReadAllText(sqlFile.FullName, Encoding.UTF8));
        }

        public DataTable ReadUncommitted(string sqlStatement)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            var readUncommitedTransaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            var dataSet = new DataSet();
            using (sqlConnection)
            {
                try
                {
                    var sqlCommand = new SqlCommand(sqlStatement, sqlConnection) { Transaction = readUncommitedTransaction };
                    var sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    sqlDataAdapter.Fill(dataSet);

                    readUncommitedTransaction.Commit();

                    sqlConnection.Close();
                }
                catch
                {
                    readUncommitedTransaction.Rollback();
                    throw;
                }
            }
            return dataSet.Tables[0];
        }
    }
}
