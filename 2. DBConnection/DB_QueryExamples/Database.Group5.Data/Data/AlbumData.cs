using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Group5.Data
{
    public class AlbumData
    {
        private static ChinookEntities CreateContext()
        {
            ChinookEntities context = new ChinookEntities();
            return context;
        }
        public int GetCount()
        {
            using (var c = CreateContext())
            {
                return c.Albums.Count();
            }
        }

        public List<Album> GetAll()
        {
            using (var c = CreateContext())
            {
                return c.Albums.ToList();
            }
        }

        public Album GetByPK(int albumId)
        {
            using (var c = CreateContext())
            {
                return c.Albums.FirstOrDefault(x => x.AlbumId == albumId);
            }
        }

        public void Insert(Album album)
        {
            using (var c = CreateContext())
            {
                c.Entry(album).State = EntityState.Added;
                c.SaveChanges();
            }
        }

        public void Update(Album album)
        {
            using (var c = CreateContext())
            {
                c.Entry(album).State = EntityState.Modified;
                c.SaveChanges();
            }
        }

        public void Delete(Album album)
        {
            using (var c = CreateContext())
            {
                c.Entry(album).State = EntityState.Deleted;
                c.SaveChanges();
            }
        }
        public int InsertData(string artistName, string title)
        {
            using (var c = CreateContext())
            {
                var query1 = from x in c.Artists
                             where x.Name == artistName
                             select x;

                bool isExist = query1.Any();

                int maxArtistId = (from x in c.Artists
                                   orderby x.ArtistId descending
                                   select x).First().ArtistId;

                if (!isExist)
                {

                    Artist a = new Artist()
                    {
                        ArtistId = maxArtistId + 1,
                        Name = artistName
                    };

                    DataRepository.Artist.Insert(a);
                }
                var query = from x in c.Albums
                            where x.Title == title && x.ArtistId == query1.FirstOrDefault().ArtistId
                            select x;
                isExist = query.Count() == 0 ? false : true;
                if (isExist)
                {
                    return 1;
                }
                int artistId = (from x in c.Artists
                                where x.Name.Contains(artistName)
                                select x).First().ArtistId;

                Album album = new Album
                {
                    Title = title,
                    ArtistId = artistId
                };
                c.Entry(album).State = EntityState.Added;
                c.SaveChanges();
                return 0;
            }
        }
        public Decimal GetTrackUnitpriceSum(string keyword)
        {
            using (var context = new ChinookEntities())
            {
                var query =
                    (from x in context.Tracks
                     where x.Album.Artist.Name == keyword
                     select x);
                Decimal sum = 0;
                foreach (var item in query)
                {
                    sum += item.UnitPrice;
                }

                return sum;
            }
        }

        public decimal CheckArtist(string name)
        {

            using (var context = new ChinookEntities())
            {
                //var query =
                //    (from x in context.Artists
                //     where x.Name == name
                //     select x);
                //if (query.Count() == 0)
                //{
                //    return 0;
                //}

                // 이름에 검색어가 포함된 아티스트가 발매한 앨범의 모든 곡
                var trackNameQuery = from x in context.Tracks
                                     where x.Album.Artist.Name.Contains(name)
                                     select x.Name;

                List<string> trackNames = trackNameQuery.ToList();

                //거기에 수록된 모든 곡의 가격의 합을 표시한다.
                var priceQuery = from y in context.InvoiceLines
                                 where trackNames.Contains(y.Track.Name)
                                 select y.UnitPrice;

                List<decimal> prices = priceQuery.ToList();

                decimal totalPrice = 0;

                foreach (decimal price in prices)
                {
                    totalPrice += price;
                }

                return totalPrice;

            }
        }
        public String GetTrack(String albumName)
        {
            using (var c = CreateContext())
            {
                var query = (from x in c.Tracks
                             where x.Album.Title.Contains(albumName)
                             select x);
                string Result = query.FirstOrDefault().Name;

                return Result;
            }
        }
    }
}
