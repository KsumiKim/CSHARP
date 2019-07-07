using System;
using System.Data.Entity;
using System.IO;

namespace MyLibrary
{
    public class DbContextFactory
    {
        public static void Initialize(string connectionString, string logFilePath = null)
        {
            _connectionString = connectionString;
            _logFilePath = logFilePath;
        }

        private static string _connectionString;

        private static string _logFilePath;


        // 모든 dataEntities의 부모인 DbContext로 타입 설정
        public static DbContext Create()
        {
            DbContext context = new DbContext(
                _connectionString);

            context.Database.Log = WirteLog;
            
            return context;
        }

        public static T Create<T>() where T:DbContext
        {
            return (T) Activator.CreateInstance(typeof(T), _connectionString);
        }

        private static void WirteLog(string log)
        {
            if (_logFilePath == null)
                return;

            File.WriteAllText(_logFilePath, log);
        }
    }
}