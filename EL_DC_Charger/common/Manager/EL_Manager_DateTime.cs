using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.Manager
{
    public class EL_Manager_DateTime
    {

        public static DateTime FindClosestDate(DateTime targetDate, List<DateTime> dateArray)
        {
            DateTime closestDate = DateTime.MinValue;
            long minDifference = long.MaxValue;

            foreach (DateTime date in dateArray)
            {
                long difference = Math.Abs((targetDate - date).Ticks);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestDate = date;
                }
            }

            return closestDate;
        }

        public static string GetDateTime_yyyy_mm_dd()
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("yyyy-MM-dd");
            return formattedDate;
        }
    }
}