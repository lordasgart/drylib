using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DryLib.Sql
{
    public class Settings : ISettings
    {
        private static int _maxParamCountForSingleInOperation = 1000;

        public static int MaxParamCountForSingleInOperation
        {
            get => _maxParamCountForSingleInOperation;

            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxParamCountForSingleInOperation));

                _maxParamCountForSingleInOperation = value;
            }
        }

        /// <summary>
        /// Path to UTF8 file with lines of Key|ConnectionString pairs
        /// </summary>
        public static string ConnectionStringsFile { get; set; }

        /// <summary>
        /// The separator char to split the Key|ConnectionString pair of the ConnectionStringFile
        /// </summary>
        public static char ConnectionStringsFileSeparatorChar { get; set; } = '|';

        static Settings()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            ConnectionStringsFile = Path.Combine(docs, "ConnectionStrings.lst");
        }

        public string GetConnectionString(string key)
        {
            var lines = File.ReadAllLines(ConnectionStringsFile, Encoding.UTF8);

            foreach (var line in lines)
            {
                var parts = line.Split(ConnectionStringsFileSeparatorChar);

                var connectionStringKey = parts[0];
                var connectionString = parts[1];

                if (connectionStringKey == key) return connectionString;
            }

            throw new KeyNotFoundException($"No ConnectionString with key {key} found in \"{ConnectionStringsFile}\"");
        }
    }
}
