using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Database.Step3.Data
{
    public class AlbumData : EntityData<Album>
    {
        protected override void FillEntity(SqlDataReader reader, Album entity)
        {
            entity.AlbumId = (int)reader["AlbumId"];
            entity.Title = (string)reader["Title"];
            entity.ArtistId = (int)reader["ArtistId"];
        }

        public List<Album> Search(int artistId, string title)
        {
            SqlCommand command = CreateCommand("Album_Search");

            AddParameter(command, "@ArtistId", artistId, SqlDbType.Int);
            AddParameter(command, "@Title", title, SqlDbType.NVarChar, 50);
            
            SqlDataReader reader = command.ExecuteReader();

            List<Album> list = new List<Album>();
            
            while (reader.Read())
            {
                Album album = new Album();
                FillEntity(reader, album);

                list.Add(album);
            }

            command.Connection.Close();

            return list;
        }

        public Album GetByPK(int albumId)
        {
            return GetByPKCore("Album_GetByPK", "@AlbumId", albumId);
        }

        public List<Album> GetAll()
        {
            return GetAllCore("Album_GetAll");
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

            int count = (int)command.ExecuteScalar();

            command.Connection.Close();

            return count;
        }
    }
}
