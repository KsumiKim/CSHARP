using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace School
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SchoolDBEntities();
            context.Database.Log = PrintToDebugWindow;

            Student std = context.Students.First(s => s.StudentID == 11);

            context.Students.Remove(std);
            
            context.SaveChanges();

            context.Dispose();
        }

        private static void PrintToDebugWindow(string log)
        {
            Debug.WriteLine(log);
        }
    }
}
