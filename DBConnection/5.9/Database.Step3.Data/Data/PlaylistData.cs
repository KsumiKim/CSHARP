using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Database.Step3.Data
{
    public class PlaylistData : EntityData<Playlist>
    {
        protected override void FillEntity(SqlDataReader reader, Playlist entity)
        {
            entity.PlaylistId = (int)reader[nameof(Playlist.PlaylistId)];
            entity.Name = (string)reader[nameof(Playlist.Name)];
        }

        public List<Playlist> Search(string name)
        {
            SqlCommand command = CreateCommand("Playlist_Search");

            AddParameter(command, "@Name", name, SqlDbType.NVarChar, 50);

            SqlDataReader reader = command.ExecuteReader();

            List<Playlist> list = new List<Playlist>();

            while (reader.Read())
            {
                Playlist playlist = new Playlist();
                FillEntity(reader, playlist);

                list.Add(playlist);
            }

            command.Connection.Close();

            return list;
        }

        public Playlist GetByPK(int playlistId)
        {
            return GetByPKCore("Playlist_GetByPK", 
                "@PlaylistId", playlistId);
        }

        public List<Playlist> GetAll()
        {
            return GetAllCore("Playlist_GetAll");
        }

        public void Insert(Playlist playlist)
        {
            SqlCommand command = CreateCommand("Playlist_Insert");

            AddParameter(command, "@PlaylistId", playlist.PlaylistId, SqlDbType.Int);
            AddParameter(command, "@Name", playlist.Name, SqlDbType.NVarChar, 120);

            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void Update(Playlist playlist)
        {
            SqlCommand command = CreateCommand("Playlist_Update");

            AddParameter(command, "@PlaylistId", playlist.PlaylistId, SqlDbType.Int);
            AddParameter(command, "@Name", playlist.Name, SqlDbType.NVarChar, 160);

            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public void DeleteByPK(int playlistId)
        {
            SqlCommand command = CreateCommand("Playlist_Delete");

            AddParameter(command, "@PlaylistId", playlistId, SqlDbType.Int);

            command.ExecuteNonQuery();

            command.Connection.Close();
        }

        public int GetCount()
        {
            SqlCommand command = CreateCommand("Playlist_GetCount");

            int count = (int)command.ExecuteScalar();

            command.Connection.Close();

            return count;
        }
    }
}
