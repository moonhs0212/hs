using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ChargerVariable
{
    public class CONST_INDEX_MAINSETTING
    {
        public const int PATH_SERIAL_CONTROLBD = 0;
        public const int PATH_SERIAL_RFREADER = 1;
        public const int PATH_SERIAL_AMI = 2;
        public const int IS_SIMULATION_CAR = 3;
        public const int IS_SIMULATION_RFREADER = 4;

        /*5~9*/
        public const int MANUALTEST_OUTPUT_VOLTAGE = 5;
        public const int MANUALTEST_OUTPUT_CURRENT = 6;
        public const int IS_USE_CARD_READER = 7;
        public const int TYPE_CARD_READER = 8;
        public const int IS_COMPLETE_FIRSTSETTING = 9;

        /*10~14*/
        public const int IS_SHOW_TEST_FORM = 10;
        public const int CHARGINGUNIT_MEMBER_TEST = 11;
        public const int CHARGINGUNIT_NONMEMBER_TEST = 12;

        public const int ADMIN_PASSWORD_HIGH = 13;
        public const int ADMIN_PASSWORD_MIDDLE = 14;

        //15 ~ 19
        public const int ADMIN_PASSWORD_LOW = 15;


        public const int COUNT_CHARGINGGUN = 16;
        public const int COUNT_CHARGINGGUN_SAMETIME = 17;

        public const int CHARGERMAKER = 18;
        public const int CHARGERTYPE = 19;
        //20 ~ 24
        public const int PLATFORM = 20;
        public const int PLATFORMOPERATOR = 21;
        public const int IS_SW_UPDATE_COMPLETE = 22;
        public const int IS_USE_ENABLE = 23;
        public const int AMICOMPANY = 24;

    }
}
