using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Group5.Data
{
    public class PlaylistData
    {
        private static ChinookEntities CreateContext()
        {
            var context = new ChinookEntities();

            return context;
        }

        // 국가명을 검색하면 해당 국가에서 가장 많이 구매한 트랙과 앨범명을 가져온다. 
        public void GetPlaylistByCountry(string country)
        {
            using (var context = CreateContext())
            {
                //// country가 USA인 사람들의 customerId를 가져온다.  
                //var customerQuery = from x in context.Invoices
                //                    where x.BillingCountry.Contains(country)
                //                    select x.InvoiceId;

                //List<int> InvoiceQuery = customerQuery.ToList();

                //// USA에 사는 customerId들이 구매한 track의 Name들을 가져온다. 
                //var trackIdQuery = from y in context.InvoiceLines
                //                     where InvoiceQuery.Contains(y.InvoiceId)
                //                     select y.TrackId;
                //int mostSoldTrack = trackIdQuery.FirstOrDefault();

                //var trackNameQuery = from t in context.Tracks
                //                     where t.TrackId.Equals(mostSoldTrack)
                //select t.Name;


                var countryQuery = from x in context.Invoices
                                    where x.BillingCountry.Contains(country)
                                    select x.InvoiceId;

                List<int> InvoiceQuery = countryQuery.ToList();

                var trackIdQuery = from y in context.InvoiceLines
                                   where InvoiceQuery.Contains(y.InvoiceId)
                                   select y.TrackId;

                // usa에서 산 track들의 trackid를 구한다.
                // trackid가 위의 trackid와 같을 때(invoiceline에서) 그 합을 count를 한다.
                // count 값을 비교한다.





                // 그 중 가장 많은 값을 가져온다. 
                // return trackNameQuery.ToString();
            }
        }
    }
}
