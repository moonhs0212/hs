using common.Database;
using EL_DC_Charger.common.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_EL_Manager_Table_Transaction_Metervalues : EL_Manager_Table_ListType
    {

        public void db_Req_MeterValues(String DBId_TransactionInfor, String Unique_Id, int ChannelIndex, String TransactionID, String Infor_MeterValues)
        {
            insertColumn(
                new String[] { "DBId_TransactionInfor", "Unique_Id", "ChannelIndex", "TransactionID", "Infor_MeterValues" },
                new String[] { DBId_TransactionInfor, Unique_Id, "" + ChannelIndex, TransactionID, Infor_MeterValues }
            );
        }

        public void db_Conf_MeterValues(String Unique_Id)
        { 
            updateColumn("Unique_Id", Unique_Id, new String[] { "IsReceiveConf" },
                    new String[] { "1" });
        }



        protected const String NAME_TABLE = "OCPP_Transaction_Metervalues";

        public static String[][] COLUMN = new String[][]
        {
                new string[]{"_id", TYPE_INTEGER},
                new string[]{"DBId_TransactionInfor", TYPE_TEXT},
                new string[]{"Unique_Id", TYPE_TEXT},
                new string[]{"ChannelIndex", TYPE_INTEGER},
                new string[]{"TransactionID", TYPE_TEXT},
                new string[]{"Infor_MeterValues", TYPE_TEXT},
                new string[]{"IsReceiveConf", TYPE_INTEGER},
        };

        public OCPP_EL_Manager_Table_Transaction_Metervalues(EL_Manager_SQLite manager_SQLite, String tableName_Add, bool maketable)
            : base(manager_SQLite, NAME_TABLE + tableName_Add, maketable)
        {

        }


        override public String[][] getColumn()
        {
            return COLUMN;
        }

        public void insertMeterValue()
        {

        }

    }

}
