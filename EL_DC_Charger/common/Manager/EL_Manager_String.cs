using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.Manager
{
    public class EL_Manager_String
    {
        public static int getCount_Text(String text, String unit)
        {
            if (text == null || text.Length < 1)
                return 0;

            int lineCnt = 0;
            int fromIndex = -1;
            while ((fromIndex = text.IndexOf(unit, fromIndex + 1)) >= 0)
            {
                lineCnt++;
            }
            return lineCnt;
        }


        public static int getCount_Null(String[] data)
        {
            int count = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null || data[i].Length < 1)
                {
                    count++;
                }
            }

            return count;
        }

        public static int getCount_NotNull(String[] data)
        {
            int count = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != null && data[i].Length > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public static int getLastIndex_Null(String[] data)
        {
            int count = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null || data[i].Length < 1)
                {
                    count = i;
                }
            }

            return count;
        }

        public static int getLastIndex_NotNull(String[] data)
        {
            int count = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != null && data[i].Length > 0)
                {
                    count = i;
                }
            }

            return count;
        }

        public static bool isCorrectIP(String ip)
        {
            String[] ips = ip.Split(new String[] {"\\."}, StringSplitOptions.RemoveEmptyEntries);

            if (ips.Length != 4)
                return false;

            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].Length < 1)
                    return false;
            }
            return true;

        }
    }
}
