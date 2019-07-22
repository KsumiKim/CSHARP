using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAnalyzer.Data
{
    public class DataRepository
    {
        static DataRepository()
        {

#if DEBUG
            //"metadata=res://*/MovieAnalyzer.csdl|res://*/MovieAnalyzer.ssdl|res://*/MovieAnalyzer.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=MovieAnalyzer;user id=sa;password=3512;MultipleActiveResultSets=True;App=EntityFramework&quot;"


            string connectionString = "metadata=res://*/MovieAnalyzer.csdl|res://*/MovieAnalyzer.ssdl|res://*/MovieAnalyzer.msl;provider=System.Data.SqlClient;provider connection string=\"data source=10.10.14.72;initial catalog=MovieAnalyzer;persist security info=True;user id=sa;password=3512;MultipleActiveResultSets=True;App=EntityFramework\"";
#else
            string connectionString = "metadata=res://*/Chinook.csdl|res://*/Chinook.ssdl|res://*/Chinook.msl;provider=System.Data.SqlClient;provider connection string=\"data source=10.10.14.100;initial catalog=Chinook;persist security info=True;user id=sa;password=3512;MultipleActiveResultSets=True;App=EntityFramework\"";
#endif
            DbContextFactory.Initialize(connectionString);
        }

        public static MovieData Movie { get; } = new MovieData();
    }
}
