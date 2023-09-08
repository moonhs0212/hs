using EL_DC_Charger.common.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class CONST_TRANSACTIONINFOR : CConst_Database
    {
        public static string GetColumnName(int indexArray)
        {
            return COLUMN[indexArray][0];
        }

        public static class INDEX
        {
            public static int _id = 0;
            public static int ChannelIndex = 1;
            public static int ChargingListID = 2;
            public static int IsNormalComplete = 3;
            public static int IdTag = 4;

            /* 5 */
            public static int Option_Payment = 5;
            public static int Time_Charging = 6;
            public static int Wattage_Charging = 7;
            public static int Option_Chargingstop = 8;
            public static int Value_Chargingstop = 9;

            /* 10 */
            public static int SOC_ChargingStart = 10;
            public static int SOC_ChargingStop = 11;
            public static int Time_UsingStart = 12;
            public static int Time_TransactionStart = 13;
            public static int Time_ChargingStart = 14;

            /* 15 */
            public static int Time_ChargingStop = 15;
            public static int Time_TransactionStop = 16;
            public static int Time_UsingStop = 17;
            public static int ChargingStop_Reason = 18;
            public static int TransactionID = 19;
            /* 20 */
            public static int Infor_Req_Starttransaction = 20;
            public static int Infor_Conf_Starttransaction = 21;
            public static int IsReceiveConf_Starttransaction = 22;

            public static int Infor_Req_Stoptransaction = 23;
            public static int Infor_Conf_Stoptransaction = 24;

            /* 25 */
            public static int IsReceiveConf_Stoptransaction = 25;

            public static int Infor_Req_MeterValue_First = 26;
            public static int IsReceiveConf_MeterValue_First = 27;

            public static int Infor_Req_MeterValue_Complete = 28;
            public static int IsReceiveConf_MeterValue_Complete = 29;

            /* 30 */
            public static int IdTag_Password = 30;

            public static int Wattage_ChargingStart = 31;
            public static int Wattage_ChargingStop = 32;

            public static int PaymentData_Response_First = 33;
            public static int PaymentData_Response_First_Cancel = 34;

            /* 35 */
            public static int ChargingSequenceNumber = 35;

            public static int OperatorType = 36;
            public static int Is_Charging_More_Once = 37;
            public static int Is_Charging_Current = 38;
            public static int ChargingUnit_Current = 39;
            public static int Charge_Charging = 40;


        }

        public static string[][] COLUMN = new string[][]
        {


            /* 0 */
              new string[]{"_id", TYPE_INTEGER},
              new string[]{"ChannelIndex", TYPE_INTEGER},
              new string[]{"ChargingListID", TYPE_INTEGER},
              new string[]{"IsNormalComplete", TYPE_INTEGER},
              new string[]{"IdTag", TYPE_TEXT},

            /* 5 */
              new string[]{"Option_Payment", TYPE_TEXT},
              new string[]{"Time_Charging", TYPE_TEXT},
              new string[]{"Wattage_Charging", TYPE_TEXT},
              new string[]{"Option_Chargingstop", TYPE_TEXT},
              new string[]{"Value_Chargingstop", TYPE_TEXT},

            /* 10 */
              new string[]{"SOC_ChargingStart", TYPE_TEXT},
              new string[]{"SOC_ChargingStop", TYPE_TEXT},
              new string[]{"Time_UsingStart", TYPE_TEXT},
              new string[]{"Time_TransactionStart", TYPE_TEXT},
              new string[]{"Time_ChargingStart", TYPE_TEXT},

            /* 15 */
              new string[]{"Time_ChargingStop", TYPE_TEXT},
              new string[]{"Time_TransactionStop", TYPE_TEXT},
              new string[]{"Time_UsingStop", TYPE_TEXT},
              new string[]{"ChargingStop_Reason", TYPE_TEXT},
              new string[]{"TransactionID", TYPE_INTEGER},

            /* 20 */
              new string[]{"Infor_Req_Starttransaction", TYPE_TEXT},
              new string[]{"Infor_Conf_Starttransaction", TYPE_TEXT},
              new string[]{"IsReceiveConf_Starttransaction", TYPE_INTEGER},
              new string[]{"Infor_Req_Stoptransaction", TYPE_TEXT},
              new string[]{"Infor_Conf_Stoptransaction", TYPE_TEXT},

            /* 25 */
              new string[]{"IsReceiveConf_Stoptransaction", TYPE_INTEGER},
              new string[]{"Infor_Req_MeterValue_First"    , TYPE_TEXT},
              new string[]{"IsReceiveConf_MeterValue_First", TYPE_INTEGER},
              new string[]{"Infor_Req_MeterValue_Complete"    , TYPE_TEXT},
              new string[]{"IsReceiveConf_MeterValue_Complete", TYPE_INTEGER},

            /* 30 */
              new string[]{"IdTag_Password"    , TYPE_TEXT},
              new string[]{"Wattage_ChargingStart"    , TYPE_TEXT},
              new string[]{"Wattage_ChargingStop"    , TYPE_TEXT},
              new string[]{"PaymentData_Response_First"    , TYPE_TEXT},
              new string[]{"PaymentData_Response_First_Cancel"    , TYPE_TEXT},

            /* 35 */
              new string[]{"ChargingSequenceNumber"    , TYPE_INTEGER},
              new string[]{"OperatorType"    , TYPE_TEXT},
              new string[]{"Is_Charging_More_Once"    , TYPE_INTEGER},
              new string[]{"Is_Charging_Current"    , TYPE_INTEGER},
              new string[]{"ChargingUnit_Start"    , TYPE_INTEGER},

            /* 35 */
              new string[]{"ChargingUnit_Current"    , TYPE_INTEGER},

        };
    }
}
