using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Helpers
{
    public static class Extension
    {
        public static bool HasValue(this string value)
        {
            return string.IsNullOrEmpty(value) == false;
        }
    }
}
