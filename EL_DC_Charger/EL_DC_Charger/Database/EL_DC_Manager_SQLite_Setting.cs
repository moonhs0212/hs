using common.Database;
using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Database
{
    public class EL_DC_Manager_SQLite_Setting : EL_Manager_SQLite_Setting
    {
        const string NAME_DB = @"..\Database_SystemSetting";
        const string NAME_TABLE = "systemsetting";


        public EL_DC_Manager_SQLite_Setting() : base(NAME_DB, NAME_TABLE)
        {
        }


        public override void setManager_Table()
        {
            EL_Manager_Table_Setting manager = new EL_DC_Manager_Table_MainSetting(this, mTableName + "_" + 0, bIsCreate);
            manager.insertFirstSetting();

            mList_Manager_Table.Add(manager);

            for (int i = 0; i < EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {

                manager = new EL_DC_Manager_Table_ChannelSetting(this, (i + 1), bIsCreate);
                manager.insertFirstSetting();
                mList_Manager_Table.Add(manager);
            }
            mManager_Table_ChargeUnit = new EL_DC_Manager_Table_Channel_ChargeUnit(this, "", bIsCreate);
        }
    }
}
