using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step3.Data
{
    public class AlbumData
    {
        // 어차피 이 값은 안바뀔거라서 상수로 뺐다. 
        // 추후에 DB가 변경되면 상수 값만 수정하면 된다. 
        private const string ConnectionString = "server=10.10.14.99;database=Chinook;uid=sa;pwd=3512";

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;

            connection.Open();

            return connection;
        }

        private SqlCommand CreateCommand(string procedureName)
        {
            SqlConnection connection = OpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }
                                                                                              //    nullable int type
        private void AddParameter(SqlCommand command, string name, object value, SqlDbType dbType, int? size = null)
        {
            // Value의 size는 타입(int인지 nvarchar 인지)에 따라 값이 있을 수도 있고 없을수도 있다. 
            // Null을 허용해놔야 하는데 mssql과 달리 c#에서는 int 타입에 null을 허용할 수 없다. 
            // (nullable int를 사용한다.)
            
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.SqlDbType = dbType;

            if (size.HasValue)
                parameter.Size = size.Value;

            command.Parameters.Add(parameter);
        }

        private void FillAlbum(SqlDataReader reader, Album album)
        {
            album.AlbumId = (int)reader["AlbumId"];
            album.Title = (string)reader["Title"];
            album.ArtistId = (int)reader["ArtistId"];
        }

        public List<Album> Search(int artistId, string title)
        {
            SqlCommand command = CreateCommand("Album_Search");

            AddParameter(command, "@ArtistId", artistId, SqlDbType.Int);
            AddParameter(command, "@Title", title, SqlDbType.NVarChar, 50);
            
            // ExecuteReader : 앨범에 있는 모든 항목을 가져와라와
            // 즉, 하나의 값을 뜻하는 scala가 아닌 그 외의 것들
            SqlDataReader reader = command.ExecuteReader();

            List<Album> list = new List<Album>();
            
            while (reader.Read())
            {
                Album album = new Album();
                FillAlbum(reader, album);

                list.Add(album);
            }

            command.Connection.Close();

            return list;
        }

        public Album GetByPK(int albumId)
        {
            SqlCommand command = CreateCommand("Album_GetByPK");

            AddParameter(command, "@AlbumId", albumId, SqlDbType.Int);

            SqlDataReader reader = command.ExecuteReader();

            Album album = new Album();

            while (reader.Read())
            {
                FillAlbum(reader, album);
            }

            command.Connection.Close();

            return album;
        }

        public void Insert(Album album)
        {
            SqlCommand command = CreateCommand("Album_Insert");

            AddParameter(command, "@Title", album.Title, SqlDbType.NVarChar, 160);
            AddParameter(command, "@ArtistId", album.ArtistId, SqlDbType.Int);

            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void Update(Album album)
        {
            SqlCommand command = CreateCommand("Album_Update");

            AddParameter(command, "@AlbumId", album.AlbumId, SqlDbType.Int);
            AddParameter(command, "@Title", album.Title, SqlDbType.NVarChar, 160);
            AddParameter(command, "@ArtistId", album.ArtistId, SqlDbType.Int);
        
            // select가 아닌 것들은 ExecuteNonQuery로 실행한다.
            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void DeleteByPK(int albumId)
        {
            SqlCommand command = CreateCommand("Album_Delete");

            AddParameter(command, "@AlbumId", albumId, SqlDbType.Int);

            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public int GetCount()
        {
            SqlCommand command = CreateCommand("Album_GetCount");

            // Scalar는 가져오는 값이 딱 하나일 때 사용한다. 
            // 예) 앨범의 개수를 가져와라. 
            int count = (int)command.ExecuteScalar();

            command.Connection.Close();

            return count;
        }
    }
}
