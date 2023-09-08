using common.Database;
using EL_DC_Charger.common.Databases;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EL_DC_Charger.EL_DC_Charger.Database
{
    public class EL_DC_Manager_Table_Channel_ChargeUnit : EL_Manager_Table_ListType
    {
        protected static String NAME_TABLE = "Table_ChargingUnit";
        public string[] getChargeUnit(int channelIndex, string operatorType)
        {
            string dateTime = EL_Manager_DateTime.GetDateTime_yyyy_mm_dd();
            List<string[]> columnList = getColumnList(
                new string[] { "StartDate" },
                new string[] { "OperatorType", "ChannelIndex" },
                new string[] { operatorType, channelIndex.ToString() },
                null, false, -1, 0);

            if (columnList.Count < 1)
                return null;

            int index = -1;
            DateTime toDay = DateTime.Now;
            List<DateTime> dateArray = new List<DateTime>();
            for (int i = 0; i < columnList.Count; i++)
            {
                string startDate = columnList[i][0];
                dateArray.Add(DateTime.Parse(startDate));
            }

            DateTime closestDate = DateTime.MinValue;
            long minDifference = long.MaxValue;
            int countIndex = -1;
            for (int i = 0; i < dateArray.Count; i++)
            {
                DateTime date = dateArray[i];
                long difference = Math.Abs((long)toDay.Subtract(date).TotalDays);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestDate = date;
                    countIndex = i;
                    break;
                }
            }

            if (countIndex >= 0)
            {
                string closesDateString = columnList[countIndex][0];
                columnList.Clear();

                columnList = getColumnList(null, new string[] { "OperatorType", "StartDate" }, new string[] { operatorType, closesDateString },
                    null, false, -1, 0);

                if (columnList.Count > 0)
                {
                    string[] data = columnList[0];
                    return data;
                }
            }

            return null;
        }

        public string[] GetChargeUnit(string operatorType)
        {
            string dateTime = EL_Manager_DateTime.GetDateTime_yyyy_mm_dd();
            List<string[]> columnList = getColumnList(
                new string[] { "StartDate" },
                new string[] { "OperatorType" },
                new string[] { operatorType },
                null, false, -1, 0);

            if (columnList.Count < 1)
                return null;

            int index = -1;
            DateTime toDay = DateTime.Now;
            List<DateTime> dateArray = new List<DateTime>();
            for (int i = 0; i < columnList.Count; i++)
            {
                string startDate = columnList[i][0];
                dateArray.Add(DateTime.Parse(startDate));
            }

            DateTime closestDate = DateTime.MinValue;
            long minDifference = long.MaxValue;
            int countIndex = -1;
            for (int i = 0; i < dateArray.Count; i++)
            {
                DateTime date = dateArray[i];
                long difference = Math.Abs((long)toDay.Subtract(date).TotalDays);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestDate = date;
                    countIndex = i;
                    break;
                }
            }

            if (countIndex >= 0)
            {
                string closesDateString = columnList[countIndex][0];
                columnList.Clear();

                columnList = getColumnList(null, new string[] { "OperatorType", "StartDate" }, new string[] { operatorType, closesDateString },
                    null, false, -1, 0);

                if (columnList.Count > 0)
                {
                    string[] data = columnList[0];
                    return data;
                }
            }

            return null;
        }


        public void addChargeUnit(int channelIndex, string operatorType, List<UnitPriceTable> listTable)
        {
            int result = removeColumn("ChannelIndex = " + channelIndex +
                    " and OperatorType = '" + operatorType + "'");
            Logger.d("DataBase : Remove Column Count = " + result);

            foreach (UnitPriceTable table in listTable)
            {
                bool exist = isExist(
                    new string[] { "_id" },
                    new string[] { "ChannelIndex", "OperatorType", "StartDate" },
                    new string[] { "" + channelIndex, operatorType, table.startDate },
                    null);

                if (exist)
                {
                    updateColumnByWhereClause("ChannelIndex = @channelIndex"
                        + " and " + "OperatorType = @operatorType"
                        + " and " + "StartDate = @startDate",
                        new string[] { channelIndex.ToString(), operatorType, table.startDate },
                        new string[]
                        {
                        "Hour_00", "Hour_01", "Hour_02", "Hour_03", "Hour_04", "Hour_05", "Hour_06", "Hour_07", "Hour_08", "Hour_09",
                        "Hour_10", "Hour_11", "Hour_12", "Hour_13", "Hour_14", "Hour_15", "Hour_16", "Hour_17", "Hour_18", "Hour_19",
                        "Hour_20", "Hour_21", "Hour_22", "Hour_23"
                        },
                        new string[]
                        {
                            table.price[0], table.price[1], table.price[2], table.price[3], table.price[4],
                            table.price[5], table.price[6], table.price[7], table.price[8], table.price[9],
                            table.price[10], table.price[11], table.price[12], table.price[13], table.price[14],
                            table.price[15], table.price[16], table.price[17], table.price[18], table.price[19],
                            table.price[20], table.price[21], table.price[22], table.price[23]
                        }
                    );
                }
                else
                {
                    insertColumn(
                        new string[]
                        {
                    "ChannelIndex", "OperatorType", "StartDate",
                    "Hour_00", "Hour_01", "Hour_02", "Hour_03", "Hour_04", "Hour_05", "Hour_06", "Hour_07", "Hour_08", "Hour_09",
                    "Hour_10", "Hour_11", "Hour_12", "Hour_13", "Hour_14", "Hour_15", "Hour_16", "Hour_17", "Hour_18", "Hour_19",
                    "Hour_20", "Hour_21", "Hour_22", "Hour_23"},
                        new string[]
                        {
                    ""+channelIndex, operatorType, table.startDate,
                    table.price[0], table.price[1], table.price[2], table.price[3], table.price[4],
                    table.price[5], table.price[6], table.price[7], table.price[8], table.price[9],
                    table.price[10], table.price[11], table.price[12], table.price[13], table.price[14],
                    table.price[15], table.price[16], table.price[17], table.price[18], table.price[19],
                    table.price[20], table.price[21], table.price[22], table.price[23]
                        }
                    );
                }
            }
        }


        public static string[][] COLUMN = new string[][]
         {
            new string[] {"_id", TYPE_INTEGER},
            new string[] {"ChannelIndex", TYPE_INTEGER},
            new string[] {"OperatorType", TYPE_TEXT},
            new string[] {"StartDate", TYPE_TEXT},
            new string[] {"Hour_00", TYPE_TEXT},
            new string[] {"Hour_01", TYPE_TEXT},
            new string[] {"Hour_02", TYPE_TEXT},
            new string[] {"Hour_03", TYPE_TEXT},
            new string[] {"Hour_04", TYPE_TEXT},
            new string[] {"Hour_05", TYPE_TEXT},
            new string[] {"Hour_06", TYPE_TEXT},
            new string[] {"Hour_07", TYPE_TEXT},
            new string[] {"Hour_08", TYPE_TEXT},
            new string[] {"Hour_09", TYPE_TEXT},
            new string[] {"Hour_10", TYPE_TEXT},
            new string[] {"Hour_11", TYPE_TEXT},
            new string[] {"Hour_12", TYPE_TEXT},
            new string[] {"Hour_13", TYPE_TEXT},
            new string[] {"Hour_14", TYPE_TEXT},
            new string[] {"Hour_15", TYPE_TEXT},
            new string[] {"Hour_16", TYPE_TEXT},
            new string[] {"Hour_17", TYPE_TEXT},
            new string[] {"Hour_18", TYPE_TEXT},
            new string[] {"Hour_19", TYPE_TEXT},
            new string[] {"Hour_20", TYPE_TEXT},
            new string[] {"Hour_21", TYPE_TEXT},
            new string[] {"Hour_22", TYPE_TEXT},
            new string[] {"Hour_23", TYPE_TEXT}
        };

        public EL_DC_Manager_Table_Channel_ChargeUnit(EL_Manager_SQLite manager_SQLite, string tableName_Add, bool maketable)
         : base(manager_SQLite, NAME_TABLE + tableName_Add, maketable)
        {

        }


        public override String[][] getColumn()
        {
            return COLUMN;
        }
    }
}