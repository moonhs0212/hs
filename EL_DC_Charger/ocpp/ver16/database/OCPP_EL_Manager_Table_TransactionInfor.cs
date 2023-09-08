using common.Database;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.Databases;
using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_EL_Manager_Table_TransactionInfor : EL_Manager_Table_ListType
    {
        public long getMaxId(int channelIndex)
        {
            long id = -1;

            String query = "SELECT MAX(_id) FROM " + mTableName + " where " + "ChannelIndex = " + channelIndex;
            SQLiteCommand command = new SQLiteCommand(query, mManager_SQLite.getConnection());
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                if (!rdr[0].ToString().Equals(""))
                    id = int.Parse(rdr[0].ToString());
            }
            rdr.Close();
            return id;
        }
        public void db_usingStop(long dbId, String time_Stop)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_UsingStop)
                    },
                    new String[]{
                        time_Stop
                    }
            );
        }
        public bool isNormalComplete(long dbId)
        {
            List<String[]> data = selectRow(
                    null,
                    CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX._id) + " = " + dbId
            );
            String[] result = null;
            if (data.Count() > 0)
            {
                result = data[0];
            }

            //다 비어있으면 정상
            if (!result[CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Starttransaction].Equals("") &&
                    result[CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Stoptransaction].Equals(""))
            {
                return false;
            }
            else if (!result[CONST_TRANSACTIONINFOR.INDEX.Infor_Conf_Starttransaction].Equals("") &&
                    result[CONST_TRANSACTIONINFOR.INDEX.Infor_Conf_Stoptransaction].Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public String[] db_NotCompleteTransaction(int channelIndex)
        //{
        //    DataRowCollection data = getColumnList(null,
        //            new String[] { "ChannelIndex", "IsNormalComplete", "IsReceiveConf_Starttransaction", "IsReceiveConf_Stoptransaction" },
        //            new String[] { "" + channelIndex, "0", "1", "0" },
        //            null, true, 0, 1);
        //    String[] result = null;
        //    if (data.Count > 0)
        //    {
        //        result = new String[data[0].ItemArray.Length];
        //        for (int i = 0; i < data[0].ItemArray.Length; i++)
        //        {
        //            result[i] = "" + data[0].ItemArray[i];
        //        }
        //    }
        //    return result;
        //}

        public long db_usingStart(int channelIndex)
        {
            long id = insertColumn(
                new String[]{
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.ChannelIndex),
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsNormalComplete),
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Starttransaction),
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Stoptransaction),
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_MeterValue_First),
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_MeterValue_Complete),
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_UsingStart)
                },
                new String[]{
                "" + channelIndex,
                "0",
                "0",
                    "0",
                    "0",
                    "0",
                    new EL_Time().getDateTime_DB(),
        }
            );
            return id;
        }


        public String[] db_NotCompleteTransaction(int channelIndex)
        {
            List<string[]> data = getColumnList(null,
                new string[] { "ChannelIndex", "IsNormalComplete", "IsReceiveConf_Starttransaction", "IsReceiveConf_Stoptransaction" },
                new string[] { channelIndex.ToString(), "0", "1", "0" },
                null, true, 0, 1);

            string[] result = null;
            if (data.Count > 0)
            {
                result = data[0];
            }

            return result;
        }

        public void db_Req_StartTransaction(long dbId, String Infor_Req_Starttransaction)
        {
            updateColumn(
                    CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX._id), "" + dbId,
                new String[]{
                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Infor_Req_Starttransaction),
                },
                new String[]{
                Infor_Req_Starttransaction
                }
            );
        }


        public void db_Conf_startTransaction(String dbId, String TransactionID, String Infor_Conf_Starttransaction)
        {
            updateColumn(dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.TransactionID),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Infor_Conf_Starttransaction),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Starttransaction),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_TransactionStart)},
                    new String[]{
                        TransactionID,
                        Infor_Conf_Starttransaction,
                        "1",
                        new EL_Time().getDateTime_DB()
                    });
        }

        public void db_Req_StopTransaction(String dbId, String Infor_Req_Stoptransaction)
        {
            updateColumn(dbId,
                    new String[] { "Infor_Req_Stoptransaction" },
                    new String[] { Infor_Req_Stoptransaction });

        }

        public void db_ProcessCompleteTransaction(String dbId)
        {
            updateColumn(
                    new String[] { "IsNormalComplete" },
                    new String[] { "1" });
        }
        public void db_Conf_StopTransaction(String dbId, String Infor_Conf_Stoptransaction)
        {
            updateColumn(dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Infor_Conf_Stoptransaction),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Stoptransaction),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_TransactionStop)},
                    new String[]{
                        Infor_Conf_Stoptransaction,
                        "1",
                        new EL_Time().getDateTime_DB()
        });
        }
        public void db_ChargingInfor_Temp(String dbId, int soc_Stop, long second_charging,
                                  long wattage_Charging, long wattage_ChargingStop, int charge_Charging, double currentChargeUnit)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.SOC_ChargingStop),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_Charging),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Wattage_Charging),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStop),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Charge_Charging),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.ChargingUnit_Current),
                    },
                    new String[]{
                        "" + soc_Stop,
                        "" + second_charging,
                        "" + wattage_Charging,
                        "" + wattage_ChargingStop,
                        "" + charge_Charging,
                        "" + currentChargeUnit
                    }
            );
        }
        public void db_Conf_StopTransaction_Temp(String dbId, String Infor_Conf_Stoptransaction)
        {
            updateColumn(dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Infor_Conf_Stoptransaction),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Stoptransaction),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_TransactionStop)},
                    new String[]{
                        Infor_Conf_Stoptransaction,
                        "0",
                        ""
                    });
        }
        public void db_Req_MeterValue_Start(String dbId, String meterValue_Start)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Infor_Req_MeterValue_First) },
                    new String[] { meterValue_Start });
        }
        public void db_Req_MeterValue_Complete(String dbId, String meterValue_Complete)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Infor_Req_MeterValue_Complete) },
                    new String[] { meterValue_Complete });
        }

        public void db_ChargingUnit_Start(String dbId, double chargingUnit_Start)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.ChargingUnit_Current) },
                    new String[] { "" + chargingUnit_Start });
        }
        public void db_Conf_MeterValue_Start(String dbId)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_MeterValue_First) },
                    new String[] { "1" });
        }

        public void db_Conf_MeterValue_Complete(String dbId)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_MeterValue_Complete) },
                    new String[] { "1" });
        }
        public void db_OperatorType(long dbId, String operatorType)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.OperatorType)
                    },
                    new String[]{
                        operatorType
                    }
            );
        }

        public void db_CardDevice_First_Cancel(long dbId, String jsonCardDevice_Deal_First_Cancel)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.PaymentData_Response_First_Cancel)
                    },
                    new String[]{
                    jsonCardDevice_Deal_First_Cancel
                    }
            );
        }
        public void db_Option_ChargingStop(long dbId, String option_chargingstop, String value, String jsonCardDevice_Deal_First)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Option_Chargingstop),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Value_Chargingstop),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.PaymentData_Response_First)
                    },
                    new String[]{
                        option_chargingstop,
                        value,
                        jsonCardDevice_Deal_First,

                    }
            );
        }


        public void db_ChargingStart(long dbId, String time_Start, long wattage_Start, int soc_Start, int chargingSequenceNumber,
                             double chargingUnit_Start)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_ChargingStart),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStart),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStop),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.SOC_ChargingStart),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.ChargingSequenceNumber),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Is_Charging_More_Once),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Is_Charging_Current)
                    },
                    new String[]{
                        time_Start,
                        "" + wattage_Start,
                        "" + wattage_Start,
                        "-1",
                        "" + chargingSequenceNumber,
                        "1",
                        ""
                    }
            );
        }
        public void db_ChargingStop(long dbId, String time_Stop, long wattage_Stop, int soc_Stop, long second_charging, long wattage_Charging,
                        int charge_Charging, String reason)
        {
            updateColumn(
            "_id", "" + dbId,
            new String[]{
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_ChargingStop),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStop),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.SOC_ChargingStop),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Time_Charging),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Wattage_Charging),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Charge_Charging),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason),
                                CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Is_Charging_Current)

                },
                new String[]{
                                    time_Stop,
                                    "" + wattage_Stop,
                                    "" + soc_Stop,
                                    "" + second_charging,
                                    "" + wattage_Charging,
                                    "" + charge_Charging,
                                    reason,
                                    "0"  });
        }


        public void db_Option_ChargingStop(long dbId, String option_chargingstop, String value)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Option_Chargingstop),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Value_Chargingstop)
                    },
                    new String[]{
                        option_chargingstop,
                        value
                    }
            );
        }
        public void db_PaymentOption_QrCode(long dbId)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                    CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Option_Payment)
                    },
                    new String[]{
                    EPaymentType.QRCODE.ToString()
                    }
            );
        }

        public void db_PaymentOption_CardDevice(long dbId)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Option_Payment)
                    },
                    new String[]{
                        EPaymentType.NONMEMBER_CARDDEVICE.ToString()
                    }
            );
        }

        public void db_idTag_Card(long dbId, String idTag, string Type)
        {
            updateColumn(
                    "_id", "" + dbId,
                    new String[]{
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.IdTag),
                        CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.Option_Payment)
                    },
                    new String[]{
                        idTag,
                        Type
                    }
            );
        }



        public void db_CardDevicePacket(String dbId, String packet)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.PaymentData_Response_First) },
                    new String[] { packet });
        }

        public void db_ChargingStopReason(string dbId, string reason)
        {
            updateColumn(dbId,
                    new String[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason) },
                    new String[] { reason });
        }

        protected const String NAME_TABLE = "OCPP_TransactionInfor";

        //public static String[][] COLUMN = new String[][]
        //{
        //        new string[]{"_id", TYPE_INTEGER},
        //        new string[]{"ChannelIndex", TYPE_INTEGER},
        //        new string[]{"IsNormalComplete", TYPE_INTEGER},
        //        new string[]{"IdTag", TYPE_TEXT},
        //        new string[]{"TransactionID", TYPE_INTEGER},
        //        new string[]{"Infor_Req_Starttransaction", TYPE_TEXT},
        //        new string[]{"Infor_Conf_Starttransaction", TYPE_TEXT},
        //        new string[]{"IsReceiveConf_Starttransaction", TYPE_INTEGER},
        //        new string[]{"Infor_Req_Stoptransaction", TYPE_TEXT},
        //        new string[]{"Infor_Conf_Stoptransaction", TYPE_TEXT},
        //        new string[]{"IsReceiveConf_Stoptransaction", TYPE_INTEGER},
        //};

        public OCPP_EL_Manager_Table_TransactionInfor(EL_Manager_SQLite manager_SQLite, String tableName_Add, bool maketable)
                  : base(manager_SQLite, NAME_TABLE + tableName_Add, maketable)
        {

        }


        override public String[][] getColumn()
        {
            return CONST_TRANSACTIONINFOR.COLUMN;
        }
    }
}
