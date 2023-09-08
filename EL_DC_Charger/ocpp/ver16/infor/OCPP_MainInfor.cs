using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using EL_DC_Charger.ocpp.ver16.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.infor
{
    public class OCPP_MainInfor : EL_Object_Base
    {

        const String SERVER_IP = "192.168.0.6";
        const int SERVER_Port = 12200;
        OCPP_Manager_Table_Setting mTable_Setting = null;
        public OCPP_MainInfor(EL_MyApplication_Base application)
        : base(application)
        {
                
            mTable_Setting = (OCPP_Manager_Table_Setting)mApplication.getManager_SQLite_Setting_OCPP().getTable_Setting(0);

        }

            
        override public void initVariable()
        {

        }
        protected String mServerIP = "192.168.0.6";
        
        public String getServerIP()
        {
            return mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_csms_ip);
        }
        protected int mServerPort = 12200;
        
        public int getServerPort()
        {
            return mTable_Setting.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.infor_csms_port);
        }


        protected String mServerIP_FTP = "192.168.0.6";
        protected int mServerPort_FTP = 12200;
            
        public String getServerIP_FTP()
        {
            return mServerIP_FTP;
        }

        
        public int getServerPort_FTP()
        {
            return mServerPort_FTP;
        }








        public String getChargePoint_SerialNumber()
        {
            return mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointSerialNumber);
        }

        protected String mChargeBox_SerialNumber = "EL-Test-0002";
        //    @Override
        //    public String getChargeBox_SerialNumber() {
        //        return mTable_Setting.getSettingData(CONST_OCPP_Setting.infor_ChargeBoxSerialNumber.getValue());
        //    }
        
        public String getChargeBox_SerialNumber()
        {
            return mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);
        }


        protected String mChargePointVendor = "EL-ELECTRIC";
        
        public String getChargePointVendor()
        {
            return mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointVendor);
        }


        protected String mChargePointModel = "EL-Test1";
        //    @Override
        //    public String getChargePointModel() {
        //        return mChargePointModel;
        //    }
        
        public String getChargePointModel()
        {
            return mTable_Setting.getSettingData((int)CONST_INDEX_OCPP_Setting.infor_chargePointModel);
        }
        protected int mHeartBeat_Interval_Sec = 0;
        
        public int getHeartBeat_Interval_Sec()
        {
            return mHeartBeat_Interval_Sec;
        }

        
        public void setHeartBeat_Interval_Sec(int sec)
        {
            mHeartBeat_Interval_Sec = sec;
        }
    }
}
