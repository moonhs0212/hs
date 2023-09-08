using common.Database;
using EL_DC_Charger.ocpp.ver16.profile;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_Manager_Table_Setting : EL_Manager_Table_Setting
    {
        static String[][] SETTING_DATA = new String[][]
        {
                ////////////////////////////////////////////0
                new string[]{"AllowOfflineTxForUnknownId", "false", "RW" },
                new string[]{"AuthorizationCacheEnabled", "false", "RW" },
                new string[]{"AuthorizeRemoteTxRequests", "false", "RW" },
                new string[]{"BlinkRepeat", "0" , "RW"},
                new string[]{"ClockAlignedDataInterval", "0" , "RW"},

                new string[]{"ConnectionTimeOut", "90" , "RW"},
                new string[]{"ConnectorPhaseRotation", ConnectorPhaseRotation.Unknown.ToString(), "RW"},/*CSL*/
                new string[]{"ConnectorPhaseRotationMaxLength", "0" , "R"},
                new string[]{"GetConfigurationMaxKeys", "0" , "R"},
                new string[]{"HeartbeatInterval", "60" , "RW"},
                ////////////////////////////////////////////10
                new string[]{"LightIntensity", "0" , "RW"},
                new string[]{"LocalAuthorizeOffline", "false" , "RW"},
                new string[]{"LocalPreAuthorize", "false" , "RW"},
                new string[]{"MaxEnergyOnInvalidId", "0" , "RW"},
                new string[]{"MeterValuesAlignedData", "0", "RW"},/*CSL*/

                new string[]{"MeterValuesAlignedDataMaxLength", "50" , "R"},
                new string[]{"MeterValuesSampledData", "Energy.Active.Import.Register", "RW"},/*CSL*/
                new string[]{"MeterValuesSampledDataMaxLength", "50" , "R"},
                new string[]{"MeterValueSampleInterval", "300" , "RW"},
                new string[]{"MinimumStatusDuration", "10" , "RW"},
                ////////////////////////////////////////////20
                new string[]{"NumberOfConnectors", "1" , "R"},
                new string[]{"ResetRetries", "0" , "RW"},
    //            {"StopTransactionOnEVSideDisconnect", "false" , "RW"},//기본상태, 아래는 급속을 위한 설정
                new string[]{"StopTransactionOnEVSideDisconnect", "false" , "R"}, //커넥터가 분리되면 트랜잭션을 정지한다. 트랜잭션 != 충전
                new string[]{"StopTransactionOnInvalidId", "false" , "RW"},
                new string[]{"StopTxnAlignedData",  "0", "RW"},/*CSL*/

                new string[]{"StopTxnAlignedDataMaxLength", "5" , "R"},
                new string[]{"StopTxnSampledData",  "0", "RW"},/*CSL*/
                new string[]{"StopTxnSampledDataMaxLength", "50", "R" },
                new string[]{"SupportedFeatureProfiles",  "Core", "R"},/*CSL*/
                new string[]{"SupportedFeatureProfilesMaxLength", "5" , "R"},
                ////////////////////////////////////////////30
                new string[]{"TransactionMessageAttempts", "0" , "RW"},
                new string[]{"TransactionMessageRetryInterval", "10" , "RW"},
                new string[]{"UnlockConnectorOnEVSideDisconnect", "0" , "RW"},
                new string[]{"WebSocketPingInterval", "0" , "RW"},
                //9.2. Local Auth List Management Profile
                new string[]{"LocalAuthListEnabled", "false" , "RW"},

                new string[]{"LocalAuthListMaxLength", "5" , "R"},
                new string[]{"SendLocalListMaxLength", "5" , "R"},
                //9.3. Reservation Profile
                new string[]{"ReserveConnectorZeroSupported", "false" , "R"},
                //9.4. Smart Charging Profile
                new string[]{"ChargeProfileMaxStackLevel", "5" , "R"},
                new string[]{"ChargingScheduleAllowedChargingRateUnit", ChargingScheduleAllowedChargingRateUnit.Power.ToString(), "R"},/*CSL*/
                ////////////////////////////////////////////
                new string[]{"ChargingScheduleMaxPeriods", "5" , "R"},
                new string[]{"ConnectorSwitch3to1PhaseSupported", "false" , "R"},/*CSL*/
                new string[]{"MaxChargingProfilesInstalled", "1" , "R"},

                new string[]{"LocalListVersion", "-1" , "x"},
                new string[]{"infor_ChargeBoxSerialNumber", "NYJ-TEST0001" , "x"},
                new string[]{"infor_chargePointModel", "WEV-A07PW" , "x"},
                new string[]{"infor_chargePointSerialNumber", "EL202204243523T3" , "x"},
                new string[]{"infor_chargePointVendor", "ELELECTRIC-NYJ" , "x"},

                new string[]{"infor_iccid", "" , "x"},
                new string[]{"infor_imsi", "" , "x"},
                new string[]{"infor_meterSerialNumber", "" , "x"},
                new string[]{"infor_meterType", "" , "x"},
                new string[]{"infor_interval_Heartbeat", "" , "x"},
                new string[]{"infor_csms_ip", "dev.wev-charger.com" , "x"},
                new string[]{"infor_csms_port", "12200" , "x"},

                new string[]{"infor_csms_ip_more", "/v1.6" , "x"},
                new string[]{"infor_csms_ip_header", "ws" , "x"},
        };


        //    ws://222.106.156.156:12200

        //String softberry_connectionInfor = "wss://dev.wev-charger.com:12200/v1.6/NYJ-TEST0001";
        //"wss://dev.wev-charger.com:12200/v1.6/FT0001"
        public const String NAME_TABLE = "OCPP_Setting";

        public OCPP_Manager_Table_Setting(EL_Manager_SQLite manager_SQLite, String tableName_Add, bool maketable)
            : base(manager_SQLite, NAME_TABLE + tableName_Add, maketable)
        {
            
        }

        
        override public String[][] getData_FirstSetting()
        {
            return SETTING_DATA;
        }

        protected String[] mList_Temp_Explain = null;
        protected String[] mList_Temp_Name = null;

        public String getSettingData_Explain(int indexArray) { String temp = mList_Temp_Explain[indexArray]; return temp; }
        public String getSettingData_Name(int indexArray)
        {
            String temp = mList_Temp_Name[indexArray];
            return temp;
        }
        override public void insertFirstSetting()
        {

            int count = 0;
            if (!bIsMakeTable)
                count = getCount_Row("");

            String[][] data_FirstSetting = getData_FirstSetting();

            if (count >= data_FirstSetting.Length)
            {

            }
            else
            {
                for (int i = count; i < data_FirstSetting.Length; i++)
                {
                    String[][] COLUMN = getColumn();
                    insertRow(new String[] { COLUMN[1][0], COLUMN[2][0], COLUMN[3][0] },
                            new String[] { data_FirstSetting[i][0], data_FirstSetting[i][1], data_FirstSetting[i][2] });
                }
            }

            String sql = "SELECT _id, SETTING_NAME, VALUE, EXPLAIN FROM " + mTableName + " ORDER BY _id asc";

            count = getCount_Row("");
            mList_Temp = new string[count];
            mList_Temp_Name = new String[count];
            mList_Temp_Explain = new String[count];
            mList_Temp_Name = new String[count];
            SQLiteCommand command = new SQLiteCommand(sql, mManager_SQLite.getConnection());
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int indexArray = Int32.Parse("" + reader["_id"]) - 1;
                
                String value = "" + reader["VALUE"];
                mList_Temp[indexArray] = value;

                if (indexArray < 43)
                {
                    String name = "" + reader["SETTING_NAME"];
                    mList_Temp_Name[indexArray] = name;
                    String explain = "" + reader["EXPLAIN"];
                    mList_Temp_Explain[indexArray] = explain;
                }
            }
        }



        //override public void insertFirstSetting()
        //{
        //    int count = 0;
        //    count = getCount_Row("");

        //    String[][] data_FirstSetting = getData_FirstSetting();

        //    if (count >= data_FirstSetting.Length)
        //    {

        //    }
        //    else
        //    {
        //        for (int i = count; i < data_FirstSetting.L; i++)
        //        {
        //            String[][] COLUMN = getColumn();
        //            insertRow(new String[] { COLUMN[1][0], COLUMN[2][0], COLUMN[3][0] },
        //                    new String[] { data_FirstSetting[i][0], data_FirstSetting[i][1], data_FirstSetting[i][2] });
        //        }
        //    }

        //    String sql = "SELECT _id, SETTING_NAME, VALUE, EXPLAIN FROM " + mTableName + " ORDER BY _id asc";

        //    Cursor cursor = mManager_SQLite.getReadableDatabase().rawQuery(sql, null);
        //    count = cursor.getCount();
        //    mList_Temp = new String[count];
        //    mList_Temp_Explain = new String[count];
        //    mList_Temp_Name = new String[count];
        //    cursor.moveToFirst();

        //    for (int i = 0; i < count; i++)
        //    {
        //        cursor.moveToPosition(i);
        //        int indexArray = cursor.getInt(0) - 1;

        //        String value = cursor.getString(2);
        //        mList_Temp[indexArray] = value;

        //        if (i < 43)
        //        {
        //            String name = cursor.getString(1);
        //            mList_Temp_Name[indexArray] = name;
        //            String explain = cursor.getString(3);
        //            mList_Temp_Explain[indexArray] = explain;
        //        }

        //    }
        //}
    }

}
