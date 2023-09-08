using common.Database;
using EL_DC_Charger.common.Databases;
using EL_DC_Charger.common.item;
using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.database
{
    public class OCPP_EL_Manager_Table_AuthCache : EL_Manager_Table_ListType
    {

        public const String DIVIDE_AUTHORIZE = "A";
        public const String DIVIDE_LOCALLIST = "L";

        protected const String NAME_TABLE = "OCPP_AuthCache";

        public static String[][] COLUMN = new String[][]
        {
                new string[]{"_id", TYPE_INTEGER},
                new string[]{"idTag", TYPE_TEXT},
                new string[]{"expiryDate", TYPE_TEXT},
                new string[]{"parentIdTag", TYPE_TEXT},
                new string[]{"AuthorizationStatus", TYPE_TEXT},
                new string[]{"UpdateDatetime", TYPE_TEXT},
                new string[]{"Divide", TYPE_TEXT},
        };

        public static String[] INSERT_COLUMN = new String[]
        {
        "idTag", "expiryDate", "parentIdTag", "AuthorizationStatus","UpdateDatetime", "Divide"
        };

        public static String[] UPDATE_COLUMN = new String[]
                {
                    "expiryDate", "parentIdTag", "AuthorizationStatus","UpdateDatetime", "Divide"
                };

        public static String[] GET_COLUMN = new String[]
                {
                    "idTag", "expiryDate", "parentIdTag"
                };

        public OCPP_EL_Manager_Table_AuthCache(EL_Manager_SQLite manager_SQLite, bool maketable) : base(manager_SQLite, NAME_TABLE, maketable)
        {

        }
        public void updateIdTagInfo_Authorize(String idTag, String expiryDate, String parentIdTag, String AuthorizationStatus,
                                                    String UpdateDatetime)
        {
            updateRow(UPDATE_COLUMN,
                    new String[] { expiryDate, parentIdTag, AuthorizationStatus, UpdateDatetime, DIVIDE_AUTHORIZE },
                    "idTag = '" + idTag + "'"
                    );
        }

        public void updateIdTagInfo_LocalList(String idTag, String expiryDate, String parentIdTag, String AuthorizationStatus,
                                                    String UpdateDatetime)
        {
            updateRow(UPDATE_COLUMN,
                    new String[] { expiryDate, parentIdTag, AuthorizationStatus, UpdateDatetime, DIVIDE_LOCALLIST },
                    "idTag = '" + idTag + "'"
            );
        }

        public void deleteRow_LocalList()
        {
            deleteRow("Divide = '" + DIVIDE_LOCALLIST + "'");
        }

        public void deleteRow_ClearCache()
        {
            deleteRow("Divide = '" + DIVIDE_AUTHORIZE + "'");
        }

        public void deleteRow_Where(string where)
        {
            deleteRow(where);
        }

        public void updateOrInsertIdTagInfo_Authorize(String idTag, IdTagInfo idTagInfo, String time_String)
        {
            if (isExist(
                    new String[] { INSERT_COLUMN[0] },
                    new String[] { INSERT_COLUMN[0] },
                    new String[] { idTag },
                    null))
            {
                updateIdTagInfo_Authorize(idTag, idTagInfo, time_String);
            }
            else
            {
                insertIdTagInfo_Authorize(idTag, idTagInfo, time_String);
            }
        }

        public void updateOrInsertIdTagInfo_LocalList(String idTag, IdTagInfo idTagInfo, String time_String)
        {
            if (isExist(
                    new String[] { INSERT_COLUMN[0] },
                    new String[] { INSERT_COLUMN[0] },
                    new String[] { idTag },
                    null))
            {
                updateIdTagInfo_LocalList(idTag, idTagInfo, time_String);
            }
            else
            {
                insertIdTagInfo_LocalList(idTag, idTagInfo, time_String);
            }
        }


        public enum EIDTAG_DB_STATE
        {
            NOT_EXIST,
            EXIST,
            EXIST_EXPIRY,
            ERROR
        }

        public EIDTAG_DB_STATE getParentIdTag_DB_State(string parentIdTag)
        {
            //"idTag", "expiryDate", "parentIdTag"
            EL_Time time_Current = new EL_Time(); time_Current.setTime();
            EL_Time time_Expiry = new EL_Time();
            List<string[]> result = getColumnList(GET_COLUMN, new string[] { "parentIdTag" }, new string[] { parentIdTag },
                        null, false, 0, 1);
            if (result.Count < 1)
                return EIDTAG_DB_STATE.NOT_EXIST;
            string[] infor = result[0];

            if (infor[1] != null)
            {
                time_Expiry.setTime(infor[1]);
                int remainTime = time_Current.getSecond_WastedTime_NoAbs(time_Expiry);
                if (remainTime < 1)
                    return EIDTAG_DB_STATE.EXIST_EXPIRY;

                return EIDTAG_DB_STATE.EXIST;
            }
            else
                return EIDTAG_DB_STATE.ERROR;
        }

        public string getOperatorType(string idTag)
        {
            List<string[]> list = getColumnList(new string[] { INSERT_COLUMN[6] }, new string[] { INSERT_COLUMN[0] }, new string[] { idTag },
                    null, false, 0, 1);
            if (list.Count < 1 || list[0] == null || list[0][0] == null)
                return null;

            return list[0][0];
        }

        //public EIDTAG_DB_STATE getIdTag_DB_State(String idTag)
        //{
        //    //"idTag", "expiryDate", "parentIdTag"
        //    EL_Time time_Current = new EL_Time(); time_Current.setTime();
        //    EL_Time time_Expiry = new EL_Time();
        //    DataRowCollection result = getColumnList(GET_COLUMN, new String[] { INSERT_COLUMN[0] }, new String[] { idTag },
        //            null, false, 0, 10);
        //    if (result.Count < 1)
        //        return EIDTAG_DB_STATE.NOT_EXIST;
        //    DataRow infor = result[0];

        //    if (infor[1] != null)
        //    {
        //        time_Expiry.setTime((string)infor.ItemArray[1]);
        //        int remainTime = time_Current.getSecond_WastedTime_NoAbs(time_Expiry);
        //        if (remainTime < 1)
        //            return EIDTAG_DB_STATE.EXIST_EXPIRY;

        //        return EIDTAG_DB_STATE.EXIST;
        //    }
        //    else
        //        return EIDTAG_DB_STATE.ERROR;
        //}
        public EIDTAG_DB_STATE getIdTag_DB_State(string idTag)
        {
            //"idTag", "expiryDate", "parentIdTag"
            EL_Time time_Current = new EL_Time();
            time_Current.setTime();
            EL_Time time_Expiry = new EL_Time();
            List<string[]> result = getColumnList(GET_COLUMN, new string[] { INSERT_COLUMN[0] }, new string[] { idTag },
                    null, false, 0, 10);
            if (result.Count < 1)
                return EIDTAG_DB_STATE.NOT_EXIST;
            string[] infor = result[0];

            if (infor[1] != null && infor[1].Length > 5)
            {
                time_Expiry.setTime(infor[1]);
                int remainTime = time_Current.getSecond_WastedTime_NoAbs(time_Expiry);
                if (remainTime < 1)
                    return EIDTAG_DB_STATE.EXIST_EXPIRY;

                return EIDTAG_DB_STATE.EXIST;
            }
            else
                return EIDTAG_DB_STATE.EXIST;
        }
        public void updateIdTagInfo_Authorize(String idTag, IdTagInfo idTagInfo, String time_String)
        {
            String expiryDate = "";
            String parentIdTag = "";
            String authorizationStatus = "";
            String updateDatetime = time_String;

            if (idTag == null)
                idTag = "";
            if (idTagInfo != null)
            {
                if (idTagInfo.expiryDate != null)
                    expiryDate = idTagInfo.expiryDate;
                if (idTagInfo.parentIdTag != null)
                    parentIdTag = idTagInfo.parentIdTag;
                if (idTagInfo.status != null)
                    authorizationStatus = idTagInfo.status.ToString();
            }

            updateIdTagInfo_Authorize(idTag,
                    expiryDate, parentIdTag, authorizationStatus, updateDatetime);
        }

        public void updateIdTagInfo_LocalList(String idTag, IdTagInfo idTagInfo, String time_String)
        {
            String expiryDate = "";
            String parentIdTag = "";
            String authorizationStatus = "";
            String updateDatetime = time_String;

            if (idTag == null)
                idTag = "";
            if (idTagInfo != null)
            {
                if (idTagInfo.expiryDate != null)
                    expiryDate = idTagInfo.expiryDate;
                if (idTagInfo.parentIdTag != null)
                    parentIdTag = idTagInfo.parentIdTag;
                if (idTagInfo.status != null)
                    authorizationStatus = idTagInfo.status.ToString();
            }

            updateIdTagInfo_LocalList(idTag,
                    expiryDate, parentIdTag, authorizationStatus, updateDatetime);
        }


        public void insertIdTagInfo_Authorize(String idTag, String expiryDate, String parentIdTag, String AuthorizationStatus,
                                                String UpdateDatetime)
        {
            insertColumn_NotReturnId(INSERT_COLUMN, new String[] { idTag, expiryDate, parentIdTag, AuthorizationStatus, UpdateDatetime, DIVIDE_AUTHORIZE });
        }

        public void insertIdTagInfo_LocalList(String idTag, IdTagInfo idTagInfo, String time_String)
        {
            String expiryDate = "";
            String parentIdTag = "";
            String authorizationStatus = "";
            String updateDatetime = time_String;

            if (idTag == null)
                idTag = "";
            if (idTagInfo != null)
            {
                if (idTagInfo.expiryDate != null)
                    expiryDate = idTagInfo.expiryDate;
                if (idTagInfo.parentIdTag != null)
                    parentIdTag = idTagInfo.parentIdTag;
                if (idTagInfo.status != null)
                    authorizationStatus = idTagInfo.status.ToString();
            }

            insertIdTagInfo_LocalList(idTag,
                    expiryDate, parentIdTag, authorizationStatus, updateDatetime);
        }

        public void insertIdTagInfo_Authorize(String idTag, IdTagInfo idTagInfo, String time_String)
        {
            String expiryDate = "";
            String parentIdTag = "";
            String authorizationStatus = "";
            String updateDatetime = time_String;

            if (idTag == null)
                idTag = "";
            if (idTagInfo != null)
            {
                if (idTagInfo.expiryDate != null)
                    expiryDate = idTagInfo.expiryDate;
                if (idTagInfo.parentIdTag != null)
                    parentIdTag = idTagInfo.parentIdTag;
                if (idTagInfo.status != null)
                    authorizationStatus = idTagInfo.status.ToString();
            }

            insertIdTagInfo_Authorize(idTag,
                    expiryDate, parentIdTag, authorizationStatus, updateDatetime);
        }

        public void insertIdTagInfo_LocalList(String idTag, String expiryDate, String parentIdTag, String AuthorizationStatus,
                                                String UpdateDatetime)
        {
            insertColumn_NotReturnId(INSERT_COLUMN, new String[] { idTag, expiryDate, parentIdTag, AuthorizationStatus, UpdateDatetime, DIVIDE_LOCALLIST });
        }


        override public String[][] getColumn()
        {
            return COLUMN;
        }
    }
}
