using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.ocpp.ver16.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_BootNotification
    {
        public String chargePointSerialNumber;
        public String chargePointModel;
        public String chargeBoxSerialNumber;
        public String chargePointVendor;
        public String firmwareVersion;
        public String iccid;
        public String imsi;
        public String meterType;
        public String meterSerialNumber;

        public void setRequiredValue(String chargePointModel, String chargePointVendor)
        {
            String temp = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).
                    getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);
            if (temp != null && temp.Length > 0)
                this.chargeBoxSerialNumber = temp;

            temp = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).
                    getSettingData((int)CONST_INDEX_OCPP_Setting.infor_iccid);
            if (temp != null && temp.Length > 0)
                this.iccid = temp;

            temp = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).
                    getSettingData((int)CONST_INDEX_OCPP_Setting.infor_imsi);
            if (temp != null && temp.Length > 0)
                this.imsi = temp;


            temp = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).
                    getSettingData((int)CONST_INDEX_OCPP_Setting.infor_meterType);
            if (temp != null && temp.Length > 0)
                this.meterType = temp;

            temp = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).
                    getSettingData((int)CONST_INDEX_OCPP_Setting.infor_meterSerialNumber);
            if (temp != null && temp.Length > 0)
                this.meterSerialNumber = temp;

            this.chargePointModel = chargePointModel;
            this.chargePointVendor = chargePointVendor;


        }
    }
}
