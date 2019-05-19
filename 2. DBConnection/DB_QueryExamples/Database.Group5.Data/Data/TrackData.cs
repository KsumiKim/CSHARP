using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Database.Group5.Data
{
    public class TrackData
    {
        private static ChinookEntities CreateContext()
        {
            var context = new ChinookEntities();

            return context;
        }

        public List<string> GetTrackGenres(string keyword)
        {
            using (var context = CreateContext())
            {
                var genreQuery = from x in context.Tracks
                                 where x.Name.Contains(keyword) && x.GenreId != null
                                 select x.GenreId.Value;

                List<int> genreIds = genreQuery.ToList();

                var query = from x in context.Genres
                            where genreIds.Contains(x.GenreId)
                            select x.Name;
                return query.ToList();
            }
        }
        // 키워드를 입력하면 해당 키워드를 노래 제목으로 가지는 노래를 가져오고 
        // 체크 박스로 클릭하면 해당 노래의 총합을 구한다.
        public List<string> GetTrack(string Trackkeyword)
        {
            using (var context = CreateContext())
            {
                var playlistQuery = from x in context.Tracks
                                    where x.Name.Contains(Trackkeyword)
                                    select x.Name;

                return playlistQuery.ToList();
            }
        }

        // 클릭한 값이 키워드로 들어간다. 
        public decimal GetTotalPrice(string keyword)
        {
            using (var context = CreateContext())
            {
                var trackIdQuery = from x in context.Tracks
                                   where x.Name.Contains(keyword)
                                   select x.UnitPrice;
                // 클릭한 노래의 가격을 구한다.
                var price = trackIdQuery.FirstOrDefault();

                return price;
            }
        }

        public int GetTrackTime(string keyword)
        {
            using (var context = new ChinookEntities())
            {
                //var albumQuery = from x in context.Albums
                //                 where x.Title == keyword
                //                 select x.AlbumId;

                //int firstAlbumId = albumQuery.FirstOrDefault();

                //// 유효성 테스트: 사용자가 입력한 검색어를 가진 앨범 제목이 없을 때 
                //if (firstAlbumId == 0)
                //    return -1;

                var playtimeQuery = from x in context.Tracks
                                    where x.Name == keyword
                                    select x.Milliseconds;


                //var trackSecondQuery = from x in context.Tracks
                //                                   where x.AlbumId == firstAlbumId

                List<int> playtime = playtimeQuery.ToList();

                var totalPlaytime = playtime.Sum();

                return totalPlaytime;
            }
        }

        public Tuple<int, List<Track>> getTracksByCustomerName(string keyword)
        {
            using (var context = new ChinookEntities())
            {


                // 검색된 고객아이디
                var customerQuery = from x in context.Customers
                                    where x.FirstName.Contains(keyword) || x.LastName.Contains(keyword)
                                    select x.CustomerId;
                // 검색된 서로 다른 고객
                var customerGroup = from x in customerQuery
                                    group x by x into uniqueCustomers
                                    select new { };

                int customerCount = customerGroup.Count();

                // 고객 아이디가 포함된 인보이스에 포함된 트랙
                List<int> customerIds = customerQuery.ToList();

                var trackByCustomerQuery = from x in context.InvoiceLines
                                           where customerIds.Contains(x.Invoice.CustomerId)
                                           select x.Track;

                // 서로 다른 고객 수, 트랙 반환
                return Tuple.Create(customerCount, trackByCustomerQuery.ToList());
            }
        }

        public int GetTotalNoOfTracks(string keyword)
        {
            using (var context = CreateContext())
            {
                var trackQuery = from x in context.Tracks
                                 where x.Name.Contains(keyword)
                                 select x;

                int totalNumberOfTracks = trackQuery.Count();

                return totalNumberOfTracks;
            }
        }

        public List<Track> GetTop5TracksByCustomer(string firstName, string lastName)
        {
            using (var context = CreateContext())
            {
                var trackByCustomer = from x in context.InvoiceLines
                                      where x.Invoice.Customer.FirstName.Equals(firstName) &&
                                      x.Invoice.Customer.LastName.Equals(lastName)
                                      select x.Track;

                List<Track> tracks = trackByCustomer.ToList();

                return tracks.Take(5).ToList();

                // context.Tracks.Where(track => track.AlbumId.Equals(albumId)).Take(5).ToList();

            }
        }


        public void Update(Track track)
        {
            using (var context = CreateContext())
            {
                context.Entry(track).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Track GetByPK(int trackId)
        {
            using (var context = CreateContext())
            {
                return context.Tracks.FirstOrDefault(x => x.TrackId == trackId);
            }
        }
        public List<Track> GetAll()
        {
            using (var c = CreateContext())
            {
                return c.Tracks.ToList();
            }
        }
    }
}
