using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAnalyzer.Data
{
    public static class Helper
    {
        public static DateTime StringToDate(string rawDate)
        {
            return new DateTime(int.Parse(rawDate.Substring(0, 4)), int.Parse(rawDate.Substring(4, 2)), int.Parse(rawDate.Substring(6, 2)));
        }

        public static string DateToString(DateTime date)
        {
            return $"{date.Year,4:0000}{date.Month,2:00}{date.Day,2:00}";
        }
    }
}
