using common.Database;
using EL_DC_Charger.EL_DC_Charger.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_Manager_SQLite_Setting : EL_Manager_SQLite
    {
        const string NAME_DB = @"..\Database_OCPPSetting";
        
        const String NAME_TABLE = "OCPP_Setting_Table";

        public OCPP_Manager_SQLite_Setting()
            : base(NAME_DB, NAME_TABLE)
        {
            
        }



        
        override public void setManager_Table()
        {
            OCPP_Manager_Table_Setting manager = new OCPP_Manager_Table_Setting(this, "", bIsCreate);
            manager.insertFirstSetting();
            mList_Manager_Table.Add(manager);


            mTable_ChargingProfile = new OCPP_EL_Manager_Table_ChargingProfile(this, "", bIsCreate);
            mTable_AuthCache = new OCPP_EL_Manager_Table_AuthCache(this, bIsCreate);

            mTable_TransactionInfor = new OCPP_EL_Manager_Table_TransactionInfor(this, "", bIsCreate);
            mTable_Transaction_Metervalues = new OCPP_EL_Manager_Table_Transaction_Metervalues(this, "", bIsCreate);            

            //mTable_ChargingProfile = new OCPP_EL_Manager_Table_ChargingProfile(this, "", bIsCreate);
            //mTable_AuthCache = new OCPP_EL_Manager_Table_AuthCache(this, bIsCreate);

            //mTable_TransactionInfor = new OCPP_EL_Manager_Table_TransactionInfor(this, "", bIsCreate);
            //mTable_Transaction_Metervalues = new OCPP_EL_Manager_Table_Transaction_Metervalues(this, "", bIsCreate);
        }
        protected OCPP_EL_Manager_Table_ChargingProfile mTable_ChargingProfile = null;
        public OCPP_EL_Manager_Table_ChargingProfile getTable_ChargingProfile()
        {
            return mTable_ChargingProfile;
        }
        protected OCPP_EL_Manager_Table_AuthCache mTable_AuthCache = null;
        public OCPP_EL_Manager_Table_AuthCache getTable_AuthCache()
        {
            return mTable_AuthCache;
        }

        protected OCPP_EL_Manager_Table_TransactionInfor mTable_TransactionInfor = null;
        public OCPP_EL_Manager_Table_TransactionInfor getTable_TransactionInfor()
        {
            return mTable_TransactionInfor;
        }

        protected OCPP_EL_Manager_Table_Transaction_Metervalues mTable_Transaction_Metervalues = null;
        public OCPP_EL_Manager_Table_Transaction_Metervalues getTable_Transaction_Metervalues()
        {
            return mTable_Transaction_Metervalues;
        }



        public EL_Manager_Table_Setting getList_Manager_Table(int indexArray)
        {
            return (EL_Manager_Table_Setting)(mList_Manager_Table[indexArray]);
        }
        public EL_Manager_Table_Setting getTable_Setting(int indexArray)
        {
            return (EL_Manager_Table_Setting)(mList_Manager_Table[indexArray]);
        }

    }

}
