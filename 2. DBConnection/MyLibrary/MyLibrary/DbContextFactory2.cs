using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    class DbContextFactory2
    {
        public static void Initialize(string connectionString, string logFilePath = null)
        {
            _connectionString = connectionString;
            _logFilePath = logFilePath;
        }

        private static string _connectionString;

        private static string _logFilePath;

        public static DbContext Create()
        {
            DbContext context = new DbContext(_connectionString);
            context.Database.Log = x => Debug.WriteLine(x);

            return context;
        }

        public static T Create<T>() where T:DbContext
        {
            return (T)Activator.CreateInstance(typeof(T), _connectionString);
        }

        private static void WriteLog(string log)
        {
            if (_logFilePath == null)
                return;

            File.WriteAllText(_logFilePath, log);
        }
    }
}
