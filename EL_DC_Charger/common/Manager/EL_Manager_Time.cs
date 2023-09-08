using EL_DC_Charger.common.Manager;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EL_DC_Charger.Manager
{
    public class EL_Manager_Time
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear; public ushort wMonth; public ushort wDayOfWeek; public ushort wDay;
            public ushort wHour; public ushort wMinute; public ushort wSecond; public ushort wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetLocalTime(ref SystemTime st);
        // TODO 충전기 시간 설정(j1) 개발필
        protected static bool isSet = false;
        public static void SettingTime(DateTime tmpdate)
        {
            if (!isSet) isSet = true; else return;
            //ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //logger.Info("Time for server : " + tmpdate.ToString("yyyy-MM-dd HH:mm:ss"));
            //logger.Info("Time for PC : " + getTime_String_Include_Divider());
            //logger.Info("Set PC time based on server time.");

            //SystemTime st = new SystemTime();
            ////DateTime tmpdate = new DateTime(st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond); 
            //tmpdate = tmpdate.AddHours(-9); //KOR timezone adjust 
            //st.wDayOfWeek = (ushort)tmpdate.DayOfWeek; 
            //st.wMonth = (ushort)tmpdate.Month;
            //st.wDay = (ushort)tmpdate.Day; 
            //st.wHour = (ushort)tmpdate.Hour; 
            //st.wMinute = (ushort)tmpdate.Minute; 
            //st.wSecond = (ushort)tmpdate.Second; 
            //st.wMilliseconds = 0;
            //bool result = SetLocalTime(ref st);
            //if (!result) logger.Error("time setting error " + Marshal.GetLastWin32Error());
        }

        public static bool isInTime(int start_hour, int start_minute, int stop_hour, int stop_minute)
        {
            int start = start_hour * 60 + start_minute;
            int stop = stop_hour * 60 + stop_minute;
            int mode = 0;

            if (start == stop) return false;
            if (start < stop) mode = 1; else mode = 2;

            int current = (getHour() * 60) + getMinute();

            switch (mode)
            {
                case NIGHTMODE_SETTING_NORMAL:
                    if (start <= current && stop >= current)
                        return true;
                    else
                        return false;
                case NIGHTMODE_SETTING_DUAL:
                    if (start <= current && current <= DIVIDE_24)
                        return true;
                    if (0 <= current && current <= stop)
                        return true;
                    break;
            }
            return false;
        }

        protected const int DIVIDE_24 = 24 * 60;
        protected const int NIGHTMODE_SETTING_NORMAL = 0;
        protected const int NIGHTMODE_SETTING_DUAL = 1;

        public static int getDay() => DateTime.Now.Day;
        public static int getHour() => DateTime.Now.Hour;
        public static int getMinute() => DateTime.Now.Minute;
        public static string GetCurrentDate() => DateTime.Now.ToString("yyyyMMdd");
        public static string GetCurrentTime() => DateTime.Now.ToString("HHmmss");

        public static int GetCurrentDate_Int() => EL_Manager_Conversion.getInt(GetCurrentDate());
        public static int GetCurrentTime_Int() => EL_Manager_Conversion.getInt(GetCurrentTime());
        public static string getTime_String_Include_Divider() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public static string getTime_String_Exclude_Divider() => DateTime.Now.ToString("yyyyMMddHHmmss");

        public static byte[] getTime_ASCii_14Byte()
        {
            string n = DateTime.Now.ToString("yyyyMMddHHmmss");
            byte[] time = Encoding.ASCII.GetBytes(n);
            return time;
        }

        public static byte[] getTime_Bcd_7Bytes()
        {
            string n = DateTime.Now.ToString("yyyyMMddHHmmss");
            byte[] bcd_Date = new byte[7];

            int indexArray = 0;
            int bytearray = 0;
            for (int i = 0; i < 14; i += 2)
            {
                bcd_Date[bytearray] = Convert.ToByte(n[indexArray].ToString(), 16); indexArray++;
                bcd_Date[bytearray] <<= 4;
                bcd_Date[bytearray] |= Convert.ToByte(n[indexArray].ToString(), 16); indexArray++;
                bytearray++;
            }
            return bcd_Date;
        }

        public static byte[] getBcdCode3Byte_hhmmss_From_Second(double second)
        {
            string data = "";

            int hour = (int)second / 3600;
            int minute = (int)(second / 60) % 60;
            int remainsecond = (int)second % 60;

            if (hour < 10) data += "0";
            data += "" + hour;

            if (minute < 10) data += "0";
            data += "" + minute;

            if (remainsecond < 10) data += "0";
            data += "" + remainsecond;

            return EL_Manager_Bcd.stringToBCD(data);
        }



        public static byte[] getBcdCode3Byte_hhmmss_From_Second(int second)
        {
            string data = "";

            int hour = second / 3600;
            int minute = (second / 60) % 60;
            int remainsecond = second % 60;

            if (hour < 10) data += "0";
            data += "" + hour;

            if (minute < 10) data += "0";
            data += "" + minute;

            if (remainsecond < 10) data += "0";
            data += "" + remainsecond;

            return EL_Manager_Bcd.stringToBCD(data);
        }

        public static string gethhmmss_IncludeUnit_From_Second(double second) => gethhmmss_IncludeUnit_From_Second((int)second);
        public static string gethhmmss_IncludeUnit_From_Second(int second)
        {
            string data = "";

            int hour = second / 3600;
            int minute = (second / 60) % 60;
            int remainsecond = second % 60;

            if (hour < 10) data += "0";
            data += "" + hour;
            data += ":";

            if (minute < 10) data += "0";
            data += "" + minute;
            data += ":";

            if (remainsecond < 10) data += "0";
            data += "" + remainsecond;

            return data;
        }
    }
}

