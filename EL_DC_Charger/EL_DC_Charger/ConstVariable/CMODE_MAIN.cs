using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ChargerVariable
{
    public class CMODE_MAIN
    {
        public const int MODE_BOOT_ON = 0;
        public const int MODE_FIRST_SETTING = 10000;
        public const int MODE_SELF_DIAGNOSIS_MAIN = 20000;

        public const int MODE_SELF_DIAGNOSIS_MAIN_TEST = 20010;

        public const int MODE_SELF_DIAGNOSIS_AUTO = 21000;
        public const int MODE_SELF_DIAGNOSIS_MANUAL = 22000;

        public const int MODE_PREPARE = 30000;

        public const int MODE_MAIN = 40000;

        public const int MODE_CERTIFICATION_WAIT = 41000;
        public const int MODE_CERTIFICATION_PROCESSING = 41100;
        public const int MODE_CERTIFICATION_COMPLETE = 42000;

        public const int MODE_CONNECTING_CONNECTOR_WAIT = 43000;

        public const int MODE_CONNECTING_CAR_PROCESSING = 44000;


        public const int MODE_ERROR_BEFORE_CHARGING = 44500;


        public const int MODE_CHARGING = 45000;

        public const int MODE_CHARGING_COMPLETE = 46000;

        public const int MODE_DISCONNECT_CONNECTOR_WAIT = 47000;

        //public const int MODE_DISCONNECT_CONNECTOR_COMPLETE = 48000;

        public const int MODE_USING_COMPLETE = 49000;

        public const int MODE_EMERGENCY = 80000;



        public const int MODE_RESTART_PROGRAM = 900000;

        public const int MODE_RESTART_SYSTEM = 910000;
    }
}
