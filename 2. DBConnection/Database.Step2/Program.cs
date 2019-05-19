using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step2
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "server=10.10.14.99;database=Chinook;uid=sa;pwd=3512";

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "Album_Search";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ArtistId";
            parameter.Value = 10;
            parameter.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Title";
            parameter.Value = "b";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            command.Parameters.Add(parameter);


            // 동기 vs.비동기
            SqlDataReader reader = command.ExecuteReader();


            int count = 0;
            int sum = 0;

            while (reader.Read())
            {
                //int albumId = reader.GetInt32(0);
                int albumId = (int)reader["AlbumId"]; // is vs. as
                string title = (string)reader["Title"];
                int artistId = (int)reader["ArtistId"];

                Console.WriteLine($"{albumId}\t{title}\t{artistId}");

                count++;
                sum += albumId;
            }

            connection.Close();

            Console.WriteLine(sum / count);
        }
    }
}
