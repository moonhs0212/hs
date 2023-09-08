using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EL_DC_Charger.BatteryChange_Charger.ChargerVariable
{
    public class CSystemMode
    {
        public const int NORMAL = 0;
        public const int SETTINGMODE_MAIN = 1000;
        public const int SETTINGMODE_SUB1_COMM_DEVICE_SETTING = 1100;
        public const int SETTINGMODE_SUB1_DOOR_SETTING = 1101;
        public const int SETTINGMODE_SUB1_TESTMODE_CHARGING = 1102;

    }
}
