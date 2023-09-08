using common.Database;
using EL_DC_Charger.common.variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Database
{
    public class EL_DC_Manager_Table_MainSetting : EL_Manager_Table_Setting
    {

        readonly string[][] SETTING_DATA = new string[][]
        {
            //////////////////////////////////////////////////////////
            //0 ~ 4

            //new string[]{"PATH_SERIAL_CONTROLBD", "COM12" },
            //new string[]{"PATH_SERIAL_RFREADER", "COM5" },
            //new string[]{"PATH_SERIAL_AMI", "COM13" },
            new string[]{"PATH_SERIAL_CONTROLBD", "COM5" },
            new string[]{"PATH_SERIAL_RFREADER", "COM3" },
            new string[]{"PATH_SERIAL_AMI", "COM6" },
            new string[]{"IS_SIMULATION_CAR", "0" },
            new string[]{"IS_SIMULATION_RFREADER", "0" },

            //5 ~ 9
            new string[]{"MANUALTEST_OUTPUT_VOLTAGE", "1000" },
            new string[]{"MANUALTEST_OUTPUT_CURRENT", "65" },
            new string[]{ "IS_USE_CARD_READER", "1" },
            new string[]{ "TYPE_CARD_READER", "1" },
            new string[]{"IS_COMPLETE_FIRSTSETTING", "0" },

            //10 ~ 14
            new string[]{ "IS_SHOW_TEST_FORM", "0" },
            new string[]{ "CHARGINGUNIT_MEMBER_TEST", "100" },
            new string[]{ "CHARGINGUNIT_NONMEMBER_TEST", "100" },

            new string[]{ "ADMIN_PASSWORD_HIGH", "009988" },
            new string[]{ "ADMIN_PASSWORD_MIDDLE", "976431" },
            
            //15 ~ 19
            new string[]{ "ADMIN_PASSWORD_LOW", "987654" },

            new string[]{ "COUNT_CHARGINGGUN", "1"},
            new string[]{ "COUNT_CHARGINGGUN_SAMETIME", "1"},

            new string[]{ "CHARGERMAKER", EChargerMaker.NONE.ToString()},
            new string[]{ "CHARGERTYPE", EChargerType.NONE.ToString()},
            //20 ~ 24
        
            
            new string[]{ "PLATFORM", EPlatform.NONE.ToString()},
            new string[]{ "PLATFORM_OPERATOR", EPlatformOperator.NONE.ToString()},
            new string[]{ "IS_SW_UPDATE_COMPLETE", "0"},
            new string[]{ "IS_USE_ENABLE", "1"},
            

    };
    
        public EL_DC_Manager_Table_MainSetting(EL_Manager_SQLite manager_SQLite, string tableName, bool maketable) : base(manager_SQLite, tableName, maketable)
        {

        }

        public override string[][] getData_FirstSetting()
        {
            return SETTING_DATA;
        }

    }
}
