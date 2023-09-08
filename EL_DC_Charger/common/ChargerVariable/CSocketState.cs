using System;
using System.Collections.Generic;
using System.Text;

namespace EL_DC_Charger.ChargerVariable
{
    public class CSocketState
    {
        public static readonly string[] TEXT_SOCKETSTATE = {"", /*"충전대기중"*/"", /*"충전중"*/"", /*"충전완료"*/"", "충전실패", "", "" };


        public const int BLANK = 0;
        public const int WAIT = 1;
        public const int CHARGING = 2;
        public const int CHARGING_COMPLETE = 3;
        public const int CHARGING_FAIL = 4;
        public const int NOTIFY_ALARM = 5;
        public const int NOTIFY_ALARM_COMPLETE = 6;

    }
}
