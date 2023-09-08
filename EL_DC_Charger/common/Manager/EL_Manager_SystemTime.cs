﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.Manager
{

    public static class EL_Manager_SystemTime
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Import
        ////////////////////////////////////////////////////////////////////////////////////////// Staitc
        //////////////////////////////////////////////////////////////////////////////// Public

        #region 시스템 시간 설정하기 - SetSystemTime(systemTime)

        /// <summary>
        /// 시스템 시간 설정하기
        /// </summary>
        /// <param name="systemTime">시스템 시간</param>
        /// <returns>처리 결과</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern int SetSystemTime(ref SYSTEMTIME systemTime);

        #endregion

        #region 시스템 시간 구하기 - GetSystemTime(systemTime)

        /// <summary>
        /// 시스템 시간 구하기
        /// </summary>
        /// <param name="systemTime">시스템 시간</param>
        /// <returns>처리 결과</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern int GetSystemTime(out SYSTEMTIME systemTime);

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Public
        ///

        public static void SetSystemTime_Korea(string timeString)
        {
            DateTime time = DateTime.Parse(timeString);
            SetSystemTime_Korea(time);
        }

        public static void SetSystemTime_Korea(DateTime sourceDateTime)
        {
            DateTime localDateTime;
            int timeZone = 9;
            localDateTime = sourceDateTime.AddHours(timeZone * -1);
            

            SYSTEMTIME systemTime = new SYSTEMTIME();

            systemTime.Year = Convert.ToUInt16(localDateTime.Year);
            systemTime.Month = Convert.ToUInt16(localDateTime.Month);
            systemTime.Day = Convert.ToUInt16(localDateTime.Day);
            systemTime.Hour = Convert.ToUInt16(localDateTime.Hour);
            systemTime.Minute = Convert.ToUInt16(localDateTime.Minute);
            systemTime.Second = Convert.ToUInt16(localDateTime.Second);

            SetSystemTime(ref systemTime);
        }


        #region 시스템 시간 설정하기 - SetSystemTime(timeZone, sourceDateTime)

        /// <summary>
        /// 시스템 시간 설정하기
        /// </summary>
        /// <param name="timeZone">시간대</param>
        /// <param name="sourceDateTime">소스 일시</param>
        public static void SetSystemTime(int timeZone, DateTime sourceDateTime)
        {
            DateTime localDateTime;

            if (timeZone > 0)
            {
                localDateTime = sourceDateTime.AddHours(timeZone * -1);
            }
            else
            {
                localDateTime = sourceDateTime.AddHours(timeZone);
            }

            SYSTEMTIME systemTime = new SYSTEMTIME();

            systemTime.Year = Convert.ToUInt16(localDateTime.Year);
            systemTime.Month = Convert.ToUInt16(localDateTime.Month);
            systemTime.Day = Convert.ToUInt16(localDateTime.Day);
            systemTime.Hour = Convert.ToUInt16(localDateTime.Hour);
            systemTime.Minute = Convert.ToUInt16(localDateTime.Minute);
            systemTime.Second = Convert.ToUInt16(localDateTime.Second);

            SetSystemTime(ref systemTime);
        }

        #endregion
        #region 시스템 시간 설정하기 - SetSystemTime(sourceDateTime)

        /// <summary>
        /// 시스템 시간 설정하기
        /// </summary>
        /// <param name="sourceDateTime">설정 일시</param>
        public static void SetSystemTime(DateTime sourceDateTime)
        {
            SetSystemTime(9, sourceDateTime); // 한국 시간대
        }

        #endregion
        #region 시스템 시간 구하기 - GetSystemTime(timeZone)

        /// <summary>
        /// 시스템 시간 구하기
        /// </summary>
        /// <param name="timeZone">시간대</param>
        /// <returns>시스템 시간</returns>
        public static DateTime GetSystemTime(int timeZone)
        {
            SYSTEMTIME systemTime = new SYSTEMTIME();

            GetSystemTime(out systemTime);

            DateTime localDateTime = new DateTime
            (
                Convert.ToInt32(systemTime.Year),
                Convert.ToInt32(systemTime.Month),
                Convert.ToInt32(systemTime.Day),
                Convert.ToInt32(systemTime.Hour),
                Convert.ToInt32(systemTime.Minute),
                Convert.ToInt32(systemTime.Second),
                Convert.ToInt32(systemTime.Millisecond)
            );

            if (timeZone > 0)
            {
                return localDateTime.AddHours(timeZone);
            }
            else
            {
                return localDateTime.AddHours(timeZone * -1);
            }
        }

        #endregion
        #region 시스템 시간 구하기 - GetSystemTime()

        /// <summary>
        /// 시스템 시간 구하기
        /// </summary>
        /// <returns>시스템 시간</returns>
        public static DateTime GetSystemTime()
        {
            return GetSystemTime(9);
        }

        #endregion
    }
}
