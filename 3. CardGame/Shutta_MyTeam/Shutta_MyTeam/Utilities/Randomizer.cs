using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    internal class Randomizer : Random
    {
        private Randomizer()
        {
        }

        private static Randomizer _instance;

        public static Randomizer Instance
        {
            get
            {
                if (_instance == null)
                    return _instance = new Randomizer();
                else
                    return _instance;
            }
        }
    }
}
