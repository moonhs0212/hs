using EL_DC_Charger.common.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EL_DC_Charger.common.item
{
    public class EL_Time
    {
        protected DateTime mTime;
        public EL_Time() => mTime = DateTime.Now;
        public EL_Time(DateTime time) => mTime = time;
        public DateTime getTime() => mTime;
        public void setTime(int year, int month, int day, int hour, int minute, int second)
        {
            mTime = new DateTime(year, month, day, hour, minute, second);
            EL_Manager_SystemTime.SetSystemTime_Korea(mTime);
        }

        public void setTime(String time)
        {
            mTime = DateTime.Parse(time);
        }

        public int getSecond_WastedTime_NoAbs(EL_Time time)
        {
            TimeSpan compareTime = time.getTime() - mTime;

            double second = compareTime.TotalSeconds;

            return (int)second;
        }
        public static int GetCurrentHour()
        {
            DateTime rightNow = DateTime.Now;

            return rightNow.Hour;
        }

        public string getDate() => mTime.ToString("yyyyMMdd");
        public string getDateShort() => mTime.ToString("yyMMdd");
        public int getDate_Int() => EL_Manager_Conversion.getInt(getDate());
        public string getDateTime() => mTime.ToString("yyyyMMddHHmmss");

        public string getDateTime_DB() => mTime.ToString("yyyy-MM-dd HH:mm:ss");

        public byte[] getTime_Bcd_7Bytes()
        {
            string n = mTime.ToString("yyyyMMddHHmmss");
            return EL_Manager_Bcd.stringToBCD(n);
        }

        public static String getTime_HMS_6String_includeDivider(long second, String divider)
        {
            StringBuilder time = new StringBuilder();
            if (second < 1)
            {
                time.Append("00:00:00");
            }
            else
            {
                long temp = 0;
                temp = second / 3600;
                if (temp < 10)
                    time.Append("0");
                time.Append("" + temp);
                time.Append(divider);


                temp = (second / 60) % 60;
                if (temp < 10)
                    time.Append("0");
                time.Append("" + temp);
                time.Append(divider);

                temp = second % 60;
                if (temp < 10)
                    time.Append("0");

                time.Append("" + temp);
            }
            return time.ToString();
        }

        public void setTime(DateTime time) =>  mTime = time;
        public void setTime() => mTime = DateTime.Now;
        public double getSecond_WastedTime()
        {
            if (mTime == null) return 0;

            TimeSpan result = DateTime.Now - mTime;
            double second = result.TotalSeconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }

        public double getMinute_WastedTime()
        {
            if (mTime == null) return 0;

            TimeSpan result = DateTime.Now - mTime;
            double second = result.TotalMinutes;
            if (second < 1)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }

        public string getSecond_WastedTime_hhMMss()
        {
            int second = (int)getSecond_WastedTime();
            int temp = second / 3600;
            string text = "";
            if (temp < 10)
                text = "0";

            text += temp;

            text += ":";

            temp = (second % 3600) / 60;

            if (temp < 10)
                text += "0";

            text += temp;

            text += ":";

            temp = (second % 60);

            if (temp < 10)
                text += "0";

            text += temp;

            return text;
        }

        public double getSecond_WastedTime(EL_Time time) => getSecond_WastedTime(time.getTime());

        public double getSecond_WastedTime(DateTime time)
        {
            if (time == null) return 0;

            if (mTime == null) return 0;

            TimeSpan result = mTime - time;
            double second = result.TotalSeconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }

        public double getMiliSecond_WastedTime()
        {
            if (mTime == null) return 0;

            TimeSpan result = DateTime.Now - mTime;
            double second = result.TotalMilliseconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }

        public double getMiliSecond_WastedTime(EL_Time time) => getMiliSecond_WastedTime(time.getTime());

        public double getMiliSecond_WastedTime(DateTime time)
        {
            if (time == null) return 0;

            if (mTime == null) return 0;

            TimeSpan result = time - mTime;
            double second = result.TotalMilliseconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }


        public String toString_OCPP()
        {
            DateTime dt = new DateTime();
            String ocpp = mTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            //String ocpp = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss:fff'z'");
            return ocpp;
        }
    }
}
