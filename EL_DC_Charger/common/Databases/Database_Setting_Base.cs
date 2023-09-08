using EL_DC_Charger.Manager;
using EL_DC_Charger.common.Manager;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace EL_DC_Charger.Databases
{
    abstract public class Database_Setting_Base
    {
        protected string mFilePath = "";
        protected string[][] mCulumnInfor = null;
        protected string mTableName = "";

        private readonly string[] CULUMN = new string[] { "_id", "IndexArray", "Name", "Value" };

        private const int INDEXARRAY_ID = 0;
        private const int INDEXARRAY_INDEX = 1;
        private const int INDEXARRAY_NAME = 2;
        private const int INDEXARRAY_VALUE = 3;

        protected string[] mData = null;
        protected int mRowCount = 0;
        public int getRowCount()
        {
            return mRowCount;
        }

        public void initSettingData(int indexArray)
        {
            setSettingData_Database("" + indexArray, mCulumnInfor[indexArray][1]);
        }

        public string getSettingData(int indexArray)
        {
            if (mData == null || mData.Length <= indexArray)
                return "";

            string returnData = mData[indexArray];
            return returnData;
        }

        public int getSettingData_Int(int indexArray)
        {
            if (mData == null || mData.Length <= indexArray)
                return -1;

            int returnData = Convert.ToInt32(mData[indexArray]);
            return returnData;
        }

        public byte getSettingData_Byte(int indexArray)
        {
            if (mData == null || mData.Length <= indexArray)
                return 0;

            byte returnData = Convert.ToByte(mData[indexArray]);
            return returnData;
        }


        public bool getSettingData_Boolean(int indexArray)
        {
            if (mData == null || mData.Length <= indexArray)
                return false;

            int returnData = Convert.ToInt32(mData[indexArray]);
            if (returnData >= 1)
                return true;
            return false;
        }


        public double getSettingData_Double(int indexArray)
        {
            if (mData == null || mData.Length <= indexArray)
                return 0;

            double returnData = Convert.ToDouble(mData[indexArray]);
            return returnData;
        }


        public Database_Setting_Base(string filePath, string tableName)
        {

            mTableName = tableName;
            mCulumnInfor = getCulumnContents();
            if (filePath == null || filePath.Length < 5)
            {
                return;
            }

            mFilePath = filePath;
            generateDatabase();
        }

        abstract public string[][] getCulumnContents();


        /*-------------------------------------------------------------------------------
            * 기본 저장 메소드
            -------------------------------------------------------------------------------*/
        public void insertFirstColumn()
        {
            createConnection();
            mSql_cmd = mSql_con.CreateCommand();
            mSql_cmd.CommandText = "select count(" + CULUMN[0] + ") from '" + mTableName + "'";

            mRowCount = Convert.ToInt32(mSql_cmd.ExecuteScalar());

            mSql_cmd = mSql_con.CreateCommand();
            mSql_cmd.CommandText = "SELECT rowid,* FROM 'main'.'" + mTableName + "' ORDER BY '" + mCulumnInfor[1] + "';";
            mSql_reader = mSql_cmd.ExecuteReader();



            int totalLength = mCulumnInfor.Length;
            int dbLength = mRowCount;

            if (dbLength < totalLength)
            {
                for (int i = dbLength; i < totalLength; i++)
                {
                    insertCulumn("" + i, mCulumnInfor[i][0], mCulumnInfor[i][1]);
                }
            }
            mRowCount = mCulumnInfor.Length;
            closeConnection();
        }



        //public void dropTable()
        //{
        //    mSql_cmd = mSql_con.CreateCommand();

        //    mSql_cmd = mSql_con.CreateCommand();
        //    mSql_cmd.CommandText = "SELECT rowid,* FROM 'main'.'" + mTableName + "' ORDER BY '" + mCulumnInfor[1] + "';";
        //    mSql_reader = mSql_cmd.ExecuteReader();



        //    int totalLength = mCulumnInfor.Length;
        //    int dbLength = 0;

        //    if (dbLength < totalLength)
        //    {
        //        for (int i = dbLength; i < totalLength; i++)
        //        {
        //            setSettingData_Database("" + i, mCulumnInfor[i][1]);
        //        }
        //    }
        //    mRowCount = mCulumnInfor.Length;
        //}

        public void setTempData()
        {
            createConnection();
            string[] returnData = new string[mRowCount];
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM '" + mTableName + "' ORDER BY '" + mCulumnInfor[1] + "'", mSql_con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read()) // Reading Rows
                    {
                        returnData[Convert.ToInt32(rdr.GetValue(1))] = rdr.GetValue(3).ToString();
                    }
                }
            }
            mData = returnData;
        }

        public string getSettingData_Database(string indexArray)
        {
            createConnection();
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT " + CULUMN[3] + " FROM " + mTableName + " WHERE " + CULUMN[1] + " = '" + indexArray + "'", mSql_con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    string returnData = rdr.GetValue(0).ToString();
                    return returnData;
                }
            }
            //closeConnection();
        }

        public bool getSettingData_Database_Boolean(string indexArray)
        {
            string value = getSettingData_Database(indexArray);
            if (value.Equals("1"))
                return true;

            return false;
        }

        public int getSettingData_Database_Int(string indexArray)
        {
            string value = getSettingData_Database(indexArray);
            int value_Int = EL_Manager_Conversion.getInt(value);
            return value_Int;

        }


        protected void insertCulumn(string indexArray, string name, string value)
        {
            createConnection();
            SQLiteCommand command = mSql_con.CreateCommand();
            string CreateSQL = "INSERT INTO 'main'.'" + mTableName + "'"
                + "('" + CULUMN[1] + "', "
                + "'" + CULUMN[2] + "', "
                + "'" + CULUMN[3] + "') "
                + " VALUES ("
                + "'" + indexArray + "', "
                + "'" + name + "', "
                + "'" + value + "');";
            command.CommandText = CreateSQL;
            command.ExecuteNonQuery();
            closeConnection();
        }

        protected void setSettingData_Database(string indexArray, string value)
        {
            if (value.Length < 1)
                return;

            createConnection();
            SQLiteCommand command = mSql_con.CreateCommand();
            string CreateSQL = "UPDATE 'main'.'" + mTableName + "'"
                + " SET " + CULUMN[3] + " = '" + value + "'"
                + " WHERE " + CULUMN[1] + " = " + "'" + indexArray + "';";
            command.CommandText = CreateSQL;
            command.ExecuteNonQuery();
            closeConnection();
        }

        public void setSettingData_Database(int indexArray, bool value)
        {
            if (value == true)
            {
                setSettingData_Database("" + indexArray, "1");
            }
            else
            {
                setSettingData_Database("" + indexArray, "0");
            }
        }

        public void setSettingData(int indexArray, bool value)
        {
            if (value == true)
            {
                setSettingData(indexArray, "1");
            }
            else
            {
                setSettingData(indexArray, "0");
            }
        }


        public void setSettingData(int indexArray, string value)
        {
            mData[indexArray] = value;
            setSettingData_Database(indexArray, value);
        }

        public void setSettingData_Database(int indexArray, string value)
        {
            setSettingData_Database("" + indexArray, value);
        }

        public void setSettingData_Database(int indexArray, int value)
        {
            setSettingData_Database("" + indexArray, "" + value);
        }

        public void regenerateDatabase()
        {
            dropTable();
            createTable();
        }

        protected void dropTable()
        {
            string query = "drop table '" + mTableName + "'";
            CommandTextAfterNonQuery(query);
        }

        protected void CommandTextAfterNonQuery(string str)
        {
            createConnection();
            SQLiteCommand command = mSql_con.CreateCommand();

            command.CommandText = str;
            command.ExecuteNonQuery();
            closeConnection();

        }



        protected void generateDatabase()
        {
            bool isFirst = false;
            if (!File.Exists(mFilePath))
            {
                SQLiteConnection.CreateFile(mFilePath);
                isFirst = true;
            }

            createConnection();

            if (isFirst)
                createTable();

            insertFirstColumn();
            closeConnection();
        }

















        protected void createTable()
        {
            createConnection();
            mSql_cmd = mSql_con.CreateCommand();
            string createSQL = "CREATE TABLE '" + mTableName + "' (_id INTEGER PRIMARY KEY AUTOINCREMENT,";

            createSQL += "'" + CULUMN[1] + "' " + " text, ";
            createSQL += "'" + CULUMN[2] + "' " + " text, ";
            createSQL += "'" + CULUMN[3] + "' " + " text";

            createSQL += ");";
            mSql_cmd.CommandText = createSQL;
            mSql_cmd.ExecuteNonQuery();
        }
        protected SQLiteCommand mSql_cmd = null;



        protected SQLiteConnection mSql_con = null;
        protected SQLiteDataReader mSql_reader;
        protected void createConnection()
        {
            if (mSql_con == null)
            {
                mSql_con = new SQLiteConnection("Data Source=" + mFilePath + ";Version=3;New=false;Compress=True;DSQLITE_THREADSAFE=2");
                mSql_con.Open();
            }


        }

        public void closeConnection()
        {
            if (mSql_reader != null)
            {
                mSql_reader.Close();
                mSql_reader = null;
            }

            if (mSql_cmd != null)
            {
                mSql_cmd.Cancel();
                mSql_cmd = null;
            }


            if (mSql_con != null)
            {
                mSql_con.Cancel();
                mSql_con = null;
            }
        }
    }
}
