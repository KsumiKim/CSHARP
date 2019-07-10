using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class FirstCard
    {
        private FirstCard()
        {
        }

        private static FirstCard _instance;

        public static FirstCard Instance
        {
            get
            {
                if (_instance == null)
                    return new FirstCard();

                return _instance;
            }
        }
    }
}
