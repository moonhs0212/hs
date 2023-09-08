using common.Database;
using EL_DC_Charger.common.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_EL_Manager_Table_ChargingProfile : EL_Manager_Table_ListType
    {

        protected const String NAME_TABLE = "ChargingProfile";

        protected static String[][] COLUMN = new String[][]
        {
                new string[]{"_id", TYPE_INTEGER},
                new string[]{"SETTING_NAME", TYPE_TEXT},
                new string[]{"VALUE", TYPE_TEXT},
                new string[]{"EXPLAIN", TYPE_TEXT}
        };

        public OCPP_EL_Manager_Table_ChargingProfile(EL_Manager_SQLite manager_SQLite, String tableName_Add, bool maketable)
            : base(manager_SQLite, NAME_TABLE + tableName_Add, maketable)
        {

        }


        override public String[][] getColumn()
        {
            return COLUMN;
        }
    }
}
