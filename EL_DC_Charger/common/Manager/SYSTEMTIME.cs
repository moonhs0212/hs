using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.Manager
{
    public struct SYSTEMTIME
    {
        #region Field

        /// <summary>
        /// 연도
        /// </summary>
        public ushort Year;

        /// <summary>
        /// 월
        /// </summary>
        public ushort Month;

        /// <summary>
        /// 요일
        /// </summary>
        public ushort DayOfWeek;

        /// <summary>
        /// 일
        /// </summary>
        public ushort Day;

        /// <summary>
        /// 시
        /// </summary>
        public ushort Hour;

        /// <summary>
        /// 분
        /// </summary>
        public ushort Minute;

        /// <summary>
        /// 초
        /// </summary>
        public ushort Second;

        /// <summary>
        /// 밀리초
        /// </summary>
        public ushort Millisecond;

        #endregion
    }
}
