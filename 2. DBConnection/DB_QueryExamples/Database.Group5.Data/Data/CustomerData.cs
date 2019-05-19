using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Group5.Data.Data
{
    public class CustomerData
    {
        private static ChinookEntities CreateContext()
        {
            var context = new ChinookEntities();
            return context;
        }

    }
}
