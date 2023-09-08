using common.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Database
{
    class EL_DC_Manager_Table_ChannelSetting : EL_Manager_Table_Setting
    {

    static String[][] SETTING_DATA = new String[][]
            {
                    //////////////////////////////////////////////////////////
                    new string[]{"IS_ENABLE_USE", "1" },
            };

        public EL_DC_Manager_Table_ChannelSetting(EL_Manager_SQLite manager_SQLite, int channelIndex, bool maketable)
                : base(manager_SQLite, "Table_ChannelSetting_" + channelIndex, maketable)
        {
        
    }

    
    override public String[][] getData_FirstSetting()
    {
        return SETTING_DATA;
    }
}
}
