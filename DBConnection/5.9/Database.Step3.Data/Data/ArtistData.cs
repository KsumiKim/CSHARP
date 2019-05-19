using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Step3.Data
{
    public class ArtistData : EntityData<Artist>
    {
        protected override void FillEntity(SqlDataReader reader, Artist entity)
        {
            entity.ArtistId = (int)reader[nameof(Artist.ArtistId)];
            entity.Name = (string)reader[nameof(Artist.Name)];
        }

        public Artist GetByPK(int albumId)
        {
            return GetByPKCore("Artist_GetByPK", "@ArtistId", albumId);
        }

        public List<Artist> GetAll()
        {
            return GetAllCore("Artist_GetAll");
        }
    }
}
