using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Database
{
    abstract public class EL_Manager_Table_Setting : EL_Manager_Table
    {
        protected readonly string[][] COLUMN = new string[][]
        {
            new string[]{ "_id", TYPE_INTEGER},
            new string[]{ "SETTING_NAME", TYPE_TEXT },
            new string[]{ "VALUE", TYPE_TEXT},
            new string[]{ "EXPLAIN", TYPE_TEXT }
        };

        protected String[] mList_Temp = null;

        public EL_Manager_Table_Setting(EL_Manager_SQLite manager_SQLite, string tableName, bool maketable) : base(manager_SQLite, tableName, maketable)
        {
            //Hangul   
        }

        public override void dropTable()
        {
            base.dropTable();

        }

        protected override void makeTable()
        {
            base.makeTable();
            insertFirstSetting();
            bIsMakeTable = false;
        }

        public void setSettingData(int indexArray, bool value)
        {
            if (value)
                setSettingData(indexArray, "1");
            else
                setSettingData(indexArray, "0");
        }

        public void setSettingData(int indexArray, int value)
        {
            setSettingData(indexArray, "" + value);
        }

        public void setSettingData(int indexArray, string value)
        {
            updateRow(
                new string[] { COLUMN[2][0] },
                new string[] { value },
                "_id  = " + (indexArray + 1)
            );
            mList_Temp[indexArray] = value;
        }

        public float getSettingData_Float(int indexArray) { float value = float.Parse(mList_Temp[indexArray]); return value; }

        public int getSettingData_Int(int indexArray)
        {
            int value = Int32.Parse(mList_Temp[indexArray]); return value;
            //return 0;
        }
        public string getSettingData(int indexArray) { string temp = mList_Temp[indexArray]; return temp; }

        public bool getSettingData_Boolean(int indexArray)
        {
            if (mList_Temp[indexArray].Equals("false")
                || mList_Temp[indexArray].Equals("FALSE")
                || mList_Temp[indexArray].Equals("False"))
                return false;
            else if (mList_Temp[indexArray].Equals("true")
                    || mList_Temp[indexArray].Equals("True")
                    || mList_Temp[indexArray].Equals("TRUE"))
                return true;
            else
            {
                int value = Int32.Parse(mList_Temp[indexArray]);
                if (value > 0)
                    return true;
                else
                    return false;
            }
        }

        virtual public void insertFirstSetting()
        {

            int count = 0;
            if (!bIsMakeTable)
                count = getCount_Row("");

            string[][] data_FirstSetting = getData_FirstSetting();

            if (count >= data_FirstSetting.Length)
            {

            }
            else
            {
                for (int i = count; i < data_FirstSetting.Length; i++)
                {
                    insertRow(new string[] { getColumn()[1][0], getColumn()[2][0] },
                        new string[] { data_FirstSetting[i][0], data_FirstSetting[i][1], "" });
                }
            }



            string sql = "SELECT _id, VALUE FROM " + mTableName + " ORDER BY _id asc";

            count = getCount_Row("");
            mList_Temp = new string[count];

            SQLiteCommand command = new SQLiteCommand(sql, mManager_SQLite.getConnection());
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int indexArray = Int32.Parse("" + reader["_id"]) - 1;
                string value = "" + reader["VALUE"];
                mList_Temp[indexArray] = value;
            }
        }

        abstract public string[][] getData_FirstSetting();

        public override string[][] getColumn()
        {
            return COLUMN;
        }
    }
}
