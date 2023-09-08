using EL_DC_Charger.common.Manager;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace common.Database
{
    abstract public class EL_Manager_Table
    {
        //1) 연결지향형
        //연결지향형은 DataReader를 사용

        //2) 비연결지향형
        //비연결지향형은 DataAdapter를 사용

        protected const string TYPE_INTEGER = "INTEGER";
        protected const string TYPE_REAL = "REAL";
        protected const string TYPE_TEXT = "TEXT";
        protected const string TYPE_BLOB = "BLOB";

        protected string mTableName = "";
        protected EL_Manager_SQLite mManager_SQLite = null;
        protected bool bIsMakeTable = false;
        public EL_Manager_Table(EL_Manager_SQLite manager_SQLite, string tableName, bool maketable)
        {
            mManager_SQLite = manager_SQLite;
            mTableName = tableName;
            bIsMakeTable = maketable;
            if (maketable)
            {
                makeTable();
            }
        }

        //public DataRowCollection getColumnList(String[] select_column, String[] where_column, String[] where_data, String condition, bool isDesc, int startIndex, int length)
        //{
        //    DataSet ds = null;
        //    String select = getSelectString(select_column);
        //    String where = getWhereString(where_column, where_data, condition);
        //    String query = new StringBuilder(
        //            "SELECT DISTINCT " + select + " FROM " + mTableName + where).ToString();

        //    if (isDesc)
        //    {
        //        query += " Order By _id DESC";
        //    }
        //    if (length > 0)
        //        query += " LIMIT " + length;
        //    if (startIndex > 0)
        //        query += " OFFSET " + startIndex;



        //    SQLiteCommand cmd = new SQLiteCommand(query.ToString());
        //    cmd.Connection = mManager_SQLite.getConnection();
        //    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
        //    ds = new DataSet();
        //    try
        //    {
        //        da.Fill(ds);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //    }
        //    cmd.Dispose();

        //    return ds.Tables[0].Rows;
        //}

        //public DataSet selectRow(string[] select, string where)
        //{
        //    DataSet ds = null;
        //    string selectString = "";
        //    if (select == null || select.Length < 1) selectString = "*";
        //    else
        //    {
        //        for (int i = 0; i < select.Length; i++)
        //        {
        //            if (selectString.Length > 0)
        //                selectString += ", ";
        //            selectString += select[i];
        //        }
        //    }

        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("SELECT ").Append(selectString);
        //    builder.Append(" FROM ").Append(mTableName);
        //    if (where != null && where.Length > 0)
        //        builder.Append(" Where ").Append(where);

        //    string query = builder.ToString();

        //    SQLiteCommand cmd = new SQLiteCommand(query);
        //    cmd.Connection = mManager_SQLite.getConnection();
        //    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
        //    ds = new DataSet();
        //    try
        //    {
        //        da.Fill(ds);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //    }
        //    cmd.Dispose();

        //    return ds;
        //}

        public List<string[]> selectRow(string[] select, string where)
        {
            DataSet ds = null;
            string selectstring = "";
            if (select == null || select.Length < 1) selectstring = "*";
            else
            {
                for (int i = 0; i < select.Length; i++)
                {
                    if (selectstring.Length > 0)
                        selectstring += ", ";
                    selectstring += select[i];
                }
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ").Append(selectstring);
            builder.Append(" FROM ").Append(mTableName);
            if (where != null && where.Length > 0)
                builder.Append(" Where ").Append(where);

            string query = builder.ToString();

            SQLiteCommand cmd = new SQLiteCommand(query);
            cmd.Connection = mManager_SQLite.getConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            cmd.Dispose();
            List<string[]> list = new List<string[]>();
            string[] lowArray = null;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lowArray = new string[ds.Tables[0].Columns.Count];

                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    lowArray[j] = ds.Tables[0].Rows[i][j].ToString();
                }
                list.Add(lowArray);
            }
            return list;
        }

        public int removeColumn(string removeWhere)
        {
            string query = "DELETE FROM " + mTableName + " WHERE " + removeWhere;
            SQLiteConnection database = mManager_SQLite.getConnection();
            int count = 0;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(query, database))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            query = "SELECT changes()";
            using (SQLiteCommand command = new SQLiteCommand(query, database))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
            }
            return count;
        }
        public List<string[]> getColumnList(string[] select_column, string[] where_column, string[] where_data, string condition, bool isDesc, int startIndex, int length)
        {
            SQLiteConnection db = mManager_SQLite.getConnection();
            SQLiteDataReader cursor = null;

            string select = getSelectString(select_column);
            string where = getWhereString(where_column, where_data, condition);
            string query = "SELECT DISTINCT " + select + " FROM " + mTableName + where;

            if (isDesc)
            {
                query += " Order By _id DESC";
            }
            if (length > 0)
                query += " LIMIT " + length;
            if (startIndex >= 0)
                query += " OFFSET " + startIndex;

            using (SQLiteCommand command = new SQLiteCommand(query, db))
            {
                cursor = command.ExecuteReader();
            }

            List<string[]> temp = new List<string[]>();

            while (cursor.Read())
            {
                if (select_column != null)
                {
                    string[] column = new string[select_column.Length];

                    for (int j = 0; j < select_column.Length; j++)
                    {
                        column[j] = cursor[j].ToString();
                    }
                    temp.Add(column);
                }
                else
                {
                    string[] column = new string[cursor.FieldCount];

                    for (int j = 0; j < column.Length; j++)
                    {
                        column[j] = cursor[j].ToString();
                    }
                    temp.Add(column);
                }
            }
            cursor.Close();
            //db.Close();
            return temp;
        }

        public int deleteRow(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM ").Append(mTableName);
            if (where != null && where.Length > 0)
                builder.Append(" where ").Append(where);

            string query = builder.ToString();
            int resultCount = excuteQuery_NonResult(query);
            return resultCount;
        }

        public long getRowId_Recently()
        {
            string query = "SELECT _id FROM " + mTableName + " ORDER BY _id DESC LIMIT 1";
            SQLiteCommand cmd = new SQLiteCommand(query.ToString());
            cmd.Connection = mManager_SQLite.getConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            cmd.Dispose();

            return (long)ds.Tables[0].Rows[0].ItemArray[0];
        }

        public long insertRow(string[] name, string[] value)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO ").Append(mTableName).Append(" ");
            int count = 0;
            if (name.Length >= value.Length) count = value.Length;
            else count = name.Length;
            StringBuilder builder_name = new StringBuilder(); builder_name.Append("(");
            StringBuilder builder_value = new StringBuilder(); builder_value.Append("(");
            for (int i = 0; i < count; i++)
            {
                builder_name.Append(name[i]);
                builder_value.Append("'");
                builder_value.Append(value[i]);
                builder_value.Append("'");
                if (i < count - 1)
                {
                    builder_name.Append(",");
                    builder_value.Append(",");
                }
            }
            builder_name.Append(")");
            builder_value.Append(")");
            builder.Append(builder_name.ToString()).Append(" values ").Append(builder_value.ToString());
            string query = builder.ToString();
            excuteQuery_NonResult(query);
            long rowId = getRowId_Recently();

            return rowId;
        }

        public long insertColumn(string[] data, string[] column)
        {
            int length = (data.Length > column.Length) ? column.Length : data.Length;

            string columns = "";
            string values = "";

            for (int i = 0; i < length; i++)
            {
                columns += data[i];
                values += "'" + column[i] + "'";

                if (i < length - 1)
                {
                    columns += ", ";
                    values += ", ";
                }
            }

            string query = $"INSERT INTO {mTableName} ({columns}) VALUES ({values});";
            using (SQLiteCommand cmd = new SQLiteCommand(query, mManager_SQLite.getConnection()))
            {
                cmd.ExecuteNonQuery();
            }

            long id = -1;
            string getIdQuery = $"SELECT max(_id) FROM {mTableName};";
            using (SQLiteCommand cmd = new SQLiteCommand(getIdQuery, mManager_SQLite.getConnection()))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = (long)reader[0];
                    }
                }
            }

            return id;
        }
        //public long insertColumn(string[] name, string[] value)
        //{
        //    return insertRow(name, value);
        //}

        //public bool isExist(String[] select_column, String[] where_column, String[] where_data, String condition)
        //{
        //    DataRowCollection collection = getColumnList(select_column, where_column, where_data, condition, false, 0, 10);

        //    if (collection.Count >= 1)
        //        return true;
        //    return false;
        //}

        public bool isExist(String[] select_column, String[] where_column, String[] where_data, String condition)
        {
            SQLiteConnection database = mManager_SQLite.getConnection();

            string select = getSelectString(select_column);
            string where = getWhereString(where_column, where_data, condition);
            string query = $"SELECT DISTINCT {select} FROM {mTableName} {where}";

            using (SQLiteCommand command = new SQLiteCommand(query, database))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        return true;
                    return false;
                }
            }
        }

        public static string getSelectString(string[] select)
        {
            string str = "";

            if (select == null)
                return "*";

            int lastIndex = select.Length - 1;

            if (lastIndex < 0)
                return "*";

            for (int i = 0; i < select.Length; i++)
            {
                if (i == select.Length - 1)
                {
                    str += select[i];
                }
                else
                {
                    str += select[i] + ", ";
                }
            }
            return str;
        }

        public static string getWhereString(string[] where, string[] data, string condition)
        {
            if (where == null || data == null)
            {
                if (string.IsNullOrEmpty(condition))
                    return "";
                else
                    return " where " + condition;
            }

            string str = " where ";

            int lastIndex = EL_Manager_String.getLastIndex_NotNull(data);
            int notNullCount = EL_Manager_String.getCount_NotNull(data);

            if (lastIndex < 0 && notNullCount < 1)
                return "";

            bool isAdd = false;

            for (int i = 0; i < where.Length; i++)
            {
                if (string.IsNullOrEmpty(data[i]))
                    continue;

                if (i == lastIndex)
                {
                    str += where[i] + " = '" + data[i] + "' ";
                }
                else
                {
                    str += where[i] + " = '" + data[i] + "' and ";
                }
                isAdd = true;
            }

            if (!string.IsNullOrEmpty(condition))
            {
                if (isAdd)
                {
                    str += " and " + condition;
                }
                else
                {
                    str += " " + condition;
                }
            }

            return str;
        }


        public bool insertColumn_NotReturnId(String[] data, String[] column)
        {
            insertRow(data, column);

            return true;
        }
        public void updateColumnByWhereClause(string whereClause, string[] whereArgs, string[] data, string[] column)
        {
            using (SQLiteConnection database = mManager_SQLite.getConnection())
            {
                using (SQLiteCommand command = database.CreateCommand())
                {
                    command.CommandText = $"UPDATE {mTableName} SET ";

                    int length = Math.Min(data.Length, column.Length);

                    for (int i = 0; i < length; i++)
                    {
                        if (!string.IsNullOrEmpty(column[i]))
                        {
                            command.Parameters.AddWithValue($"@column{i}", column[i]);
                            command.CommandText += $"{data[i]} = @column{i}, ";
                        }
                    }

                    command.CommandText = command.CommandText.TrimEnd(',', ' '); // Remove trailing comma and space
                    command.CommandText += $" WHERE {whereClause}";

                    foreach (var arg in whereArgs)
                    {
                        command.Parameters.AddWithValue($"@arg", arg);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        public int updateColumn(string where_column, string where_value, string[] name, string[] value)
        {
            int row = updateRow(name, value, where_column + " = '" + where_value + "'");
            return row;
        }

        public int updateColumn(string where_value_id, string[] name, string[] value)
        {
            int row = updateRow(name, value, "_id = " + where_value_id);
            return row;
        }
        public int updateColumn(string[] name, string[] value)
        {
            int row = updateRow(name, value, null);
            return row;
        }
        public int updateColumn(string[] name, string[] value, string where)
        {
            int row = updateRow(name, value, where);
            return row;
        }
        public int updateRow(string[] name, string[] value, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ").Append(mTableName).Append(" SET ");
            int count = 0;
            if (name.Length >= value.Length) count = value.Length;
            else count = name.Length;

            for (int i = 0; i < count; i++)
            {
                builder.Append(name[i]);
                builder.Append(" = ");
                builder.Append("'").Append(value[i]).Append("'");
                if (i < count - 1)
                {
                    builder.Append(",");
                }
            }

            if (where != null && where.Length > 0)
                builder.Append(" where ").Append(where);

            string query = builder.ToString();
            int resultCount = excuteQuery_NonResult(query);
            return resultCount;
        }


        virtual public void dropTable()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DROP TABLE ").Append(mTableName);
            excuteQuery_NonResult(builder.ToString());
            makeTable();
        }

        //public int getCount_Row(string )
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("SELECT COUNT(").Append(getColumn()[][] _id) FROM ").Append(mTableName);
        //    string sql = builder.ToString();
        //    SQLiteCommand command = new SQLiteCommand(mManager_SQLite.getConnection());
        //    command.CommandText = sql;

        //    object obj = command.ExecuteScalar();

        //    int count = Int32.Parse("" + obj);
        //    return count;
        //}

        public int getCount_Row(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(_id) FROM ").Append(mTableName);

            if (where != null && where.Length > 0)
                builder.Append(" where ").Append(where);

            string sql = builder.ToString();
            SQLiteCommand command = new SQLiteCommand(mManager_SQLite.getConnection());
            command.CommandText = sql;

            try
            {
                object obj = command.ExecuteScalar();
                int count = Int32.Parse("" + obj);
                return count;
            }
            catch (SQLiteException e)

            {
                makeTable();
                object obj = command.ExecuteScalar();
                int count = Int32.Parse("" + obj);
                return count;
            }



        }

        virtual protected void makeTable()
        {
            string[][] column = getColumn();
            StringBuilder builder = new StringBuilder();
            builder.Append("CREATE TABLE ").Append(mTableName).Append(" (_id INTEGER PRIMARY KEY, ");

            for (int i = 1; i < column.Length; i++)
            {
                builder.Append(column[i][0]).Append(" ").Append(column[i][1]);
                if (i == column.Length - 1)
                {
                    builder.Append(")");
                }
                else
                {
                    builder.Append(", ");
                }
            }

            string query = builder.ToString();
            excuteQuery_NonResult(query);
        }


        public int excuteQuery_NonResult(string query)
        {
            var cmd = new SQLiteCommand(mManager_SQLite.getConnection());
            cmd.CommandText = query;
            int count = cmd.ExecuteNonQuery();
            return count;
        }

        //string[][] 형식 : {컬럼명, 기본 데이터}
        abstract public string[][] getColumn();

    }
}
