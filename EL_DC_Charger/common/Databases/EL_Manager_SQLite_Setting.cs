using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace common.Database
{
    abstract public class EL_Manager_SQLite_Setting : EL_Manager_SQLite
    {
        static String NAME_DB = "Database_SystemSetting";
        static String NAME_TABLE = "systemsetting";

        protected EL_Manager_SQLite_Setting(string dbFile, string tableName) : base(dbFile, tableName)
        {
        }
        public EL_Manager_Table_Setting getList_Manager_Table(int indexArray)
        {
            return (EL_Manager_Table_Setting)(mList_Manager_Table[indexArray]);
        }
        public EL_Manager_Table_Setting getTable_Setting(int indexArray)
        {
            return (EL_Manager_Table_Setting)(mList_Manager_Table[indexArray]);
        }

        protected EL_DC_Manager_Table_Channel_ChargeUnit mManager_Table_ChargeUnit = null;
        public EL_DC_Manager_Table_Channel_ChargeUnit getManager_Table_ChargeUnit()
        {
            return mManager_Table_ChargeUnit;
        }

    }


}
