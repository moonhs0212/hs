using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.Utils
{
    public static class HistoryDBHelper
    {
        static string path = Application.StartupPath + @"\History.db";
        static SQLiteConnection conn = null;

        public static ISQLiteListener_Charging mSQLiteListener_Charging = null;
        public static void setSQLiteListener_Charging(ISQLiteListener_Charging listener)
        {
            mSQLiteListener_Charging = listener;
        }
        #region 필드이름
        public static int lastIdx = 0;
        public const string charge_id = "charge_id";
        public const string usestarttime = "usestarttime";
        public const string useendttime = "useendtime";
        public const string chargingtime = "chargingtime";
        public const string chargestarttime = "chargestarttime";
        public const string chargeendtime = "chargeendtime";
        public const string reason = "reason";
        public const string chargingwattage = "chargingwattage";
        public const string price = "price"; //결제금액선택
        public const string startwattage = "startwattage";
        public const string current_price = "current_price"; //충전단가
        public const string deal = "deal"; //실결제금액
        public const string soc = "soc";
        public const string member_yn = "member_yn";
        public const string member_card_no = "member_card_no";
        public const string pay_id = "pay_id";
        public const string pay_yn = "pay_yn";
        public const string pay_how = "pay_how";
        public const string pay_info = "pay_info";
        public const string npq_paymentResult = "npq_paymentResult";
        public const string npq_chargingLimitProfile = "npq_chargingLimitProfile";
        public const string npq_unitAmount = "npq_unitAmount";
        public const string npq_sendOcpp = "npq_sendOcpp";
        public const string npq_recvOcpp = "npq_recvOcpp";
        public const string nps1_paymentResult = "nps1_paymentResult";
        public const string nps1_chargingLimitProfile = "nps1_chargingLimitProfile";
        public const string nps1_unitAmount = "nps1_unitAmount";
        public const string nps1_payCharge = "nps1_payCharge";
        public const string nps1_payTransactionDT = "nps1_payTransactionDT";
        public const string nps1_payApprovalNumber = "nps1_payApprovalNumber";
        public const string nps1_payUniqueNumber = "nps1_payUniqueNumber";
        public const string nps1_payResponseCode = "nps1_payResponseCode";
        public const string nps1_payResponseMessage = "nps1_payResponseMessage";
        public const string nps1_cardResponseCode = "nps1_cardResponseCode";
        public const string nps1_sendOcpp = "nps1_sendOcpp";
        public const string nps1_recvOcpp = "nps1_recvOcpp";
        public const string nps2_paymentType = "nps2_paymentType";
        public const string nps2_paymentResult = "nps2_paymentResult";
        public const string nps2_chargingLimitProfile = "nps2_chargingLimitProfile";
        public const string nps2_unitAmount = "nps2_unitAmount";
        public const string nps2_prepaymentCharge = "nps2_prepaymentCharge";
        public const string nps2_chargingCharge = "nps2_chargingCharge";
        public const string nps2_totalCharge = "nps2_totalCharge";
        public const string nps2_payCharge = "nps2_payCharge";
        public const string nps2_payTransactionDT = "nps2_payTransactionDT";
        public const string nps2_payApprovalNumber = "nps2_payApprovalNumber";
        public const string nps2_payUniqueNumber = "nps2_payUniqueNumber";
        public const string nps2_payResponseCode = "nps2_payResponseCode";
        public const string nps2_payResponseMessage = "nps2_payResponseMessage";
        public const string nps2_cardResponseCode = "nps2_cardResponseCode";
        public const string nps2_sendOcpp = "nps2_sendOcpp";
        public const string nps2_recvOcpp = "nps2_recvOcpp";

        #endregion


        public static void SqlliteConn()
        {
            if (conn == null)
            {
                conn = new SQLiteConnection("Data Source=" + path + ";Version=3;New=false;Compress=True;DSQLITE_THREADSAFE=2");
                conn.Open();
            }
        }

        public static void CreateHistoryTable()
        {
            string sql;

            sql = "select count(*) from sqlite_master where Name ='history'";

            SQLiteCommand command = new SQLiteCommand(sql, conn);

            int result = Convert.ToInt32(command.ExecuteScalar());

            if (result < 1)
            {
                sql =
                    "create table history (" +
                    "idx INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "usestarttime varchar(19)," + //사용 시작시간
                    "useendtime varchar(19)," + // 사용 종료시간
                    "chargestarttime varchar(19) , " + // 충전시작시간
                    "chargeendtime varchar(19), " + // 충전종료시간
                    "chargingtime varchar(15), " + //충전시간
                    "reason varchar(100)," + // 정지 사유
                    "startwattage varchar(15), " + //충전 시작시 전력량값
                    "chargingwattage varchar(10)," + //충전 전력량
                    "price varchar(10)," + //결제금액
                    "current_price varchar(10)," + //실시간 금액
                    "deal varchar(10)," + //실결제금액
                    "soc varchar(5)," + //soc
                    "pay_how varchar(20)," + //결제방법
                    "pay_yn varchar(1)," + //
                    "pay_info varchar(300)," + //
                    "member_yn varchar(1)," + //회원여부
                    "member_card_no varchar(15)" + //회원카드번호                    
                    ")";

                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
            }
        }

        public static void CreatebillingInfoTable()
        {
            string sql;

            sql = "select count(*) from sqlite_master where Name ='billingInfo'";

            SQLiteCommand command = new SQLiteCommand(sql, conn);

            int result = Convert.ToInt32(command.ExecuteScalar());

            if (result < 1)
            {
                sql =
                    "create table billingInfo (" +
                    "idx INTEGER," + //충전이력과 동일
                    "npq_paymentResult varchar(5)," +
                    "npq_chargingLimitProfile varchar(5)," +
                    "npq_unitAmount varchar(10)," +
                    "npq_sendOcpp varchar(200)," +
                    "npq_recvOcpp varchar(200)," +
                    "nps1_paymentResult varchar(1), " +
                    "nps1_chargingLimitProfile varchar(5), " +
                    "nps1_unitAmount varchar(10), " +
                    "nps1_payCharge varchar(10), " +
                    "nps1_payTransactionDT varchar(10)," +
                    "nps1_payApprovalNumber varchar(20), " +
                    "nps1_payUniqueNumber varchar(20)," +
                    "nps1_payResponseCode varchar(10)," +
                    "nps1_payResponseMessage varchar(200), " +
                    "nps1_cardResponseCode varchar(10), " +
                    "nps1_sendOcpp varchar(200)," +
                    "nps1_recvOcpp varchar(200)," +
                    "nps2_paymentType varchar(1)," +
                    "nps2_paymentResult varchar(1)," +
                    "nps2_chargingLimitProfile varchar(5)," +
                    "nps2_unitAmount varchar(10)," +
                    "nps2_prepaymentCharge varchar(10)," +
                    "nps2_chargingCharge varchar(10)," +
                    "nps2_totalCharge varchar(10)," +
                    "nps2_payCharge varchar(10)," +
                    "nps2_payTransactionDT varchar(19)," +
                    "nps2_payApprovalNumber varchar(20)," +
                    "nps2_payUniqueNumber varchar(20)," +
                    "nps2_payResponseCode varchar(20)," +
                    "nps2_payResponseMessage varchar(200)," +
                    "nps2_cardResponseCode varchar(10)," +
                    "nps2_sendOcpp varchar(200)," +
                    "nps2_recvOcpp varchar(200)" +
                    ")";

                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
            }
        }
        public static void CreateunitPriceTable()
        {
            string sql;

            sql = "select count(*) from sqlite_master where Name ='unitPrice'";

            SQLiteCommand command = new SQLiteCommand(sql, conn);

            int result = Convert.ToInt32(command.ExecuteScalar());

            if (result < 1)
            {
                sql =
                    "create table unitPrice (" +
                    "startDate varchar(19)," + //적용시작일시
                    "price varchar(15)" + //가격
                    ")";

                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
            }
        }
        //
        //public static void InsertData(string usestarttime, string useendtime, string reason, string wattage, string price)
        //{
        //    string sql = "insert into history (usestarttime, useendtime, reason, chargingwattage, price) values "
        //    + "("
        //    + " '" + usestarttime + "',"
        //    + " '" + useendttime + "',"
        //    + " '" + reason + "',"
        //    + " '" + wattage + "',"
        //    + " '" + price + "'"
        //    + ")";

        //    SQLiteCommand command = new SQLiteCommand(sql, conn);
        //    command.ExecuteNonQuery();
        //}
        //사용시작
        public async static void useStart(ISQLiteListener_Charging listener)
        {
            //추후 서버시간으로 변경 필요
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = "insert into history (usestarttime) values "
            + "("
            + " '" + date + "'"
            + ")";

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            await Task.Delay(100);

            mSQLiteListener_Charging = listener;

            if (mSQLiteListener_Charging != null)
            {
                long id = getIdx();
                mSQLiteListener_Charging.onUseStart(id);
            }

        }

        //사용종료
        public static void useEnd(long lastIdx, string reason)
        {
            //추후 서버시간으로 변경 필요
            string endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = "update history set reason = " + "'" + reason + "' ," +
                                             "useendtime ='" + endTime + "'" +
                                             " where idx = " + lastIdx;

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        //사용종료
        public static void useEnd(long lastIdx)
        {
            //추후 서버시간으로 변경 필요
            string endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = "update history set useendtime ='" + endTime + "'" +
                                             " where idx = " + lastIdx;

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
        public static SQLiteDataReader SelectData(string toDate, string fromDate)
        {
            string sql = "select * from history where usestarttime >= '" + toDate + " 00:00:00 " + "' AND usestarttime <= '" + fromDate + " 23:59:59" + "'";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            return command.ExecuteReader();
        }

        public static long getIdx()
        {
            string sql = "select max(idx) from history";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                lastIdx = int.Parse(rdr[0].ToString());
            }
            rdr.Close();
            return lastIdx;
        }

        public static float getLastWattage(long idx)
        {
            float wattage = 0;
            string sql = "select startwattage from history where idx = " + idx;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                wattage = float.Parse(rdr[0].ToString());
            }
            rdr.Close();
            return wattage;
        }

        //충전시작
        public static void ChargingStart(long idx, float price, float _startWattage, float _currentAmount)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = "update history set " +
                HistoryDBHelper.chargestarttime + "= '" + date + "' , " +
                HistoryDBHelper.price + "= '" + price + "' , " +
                HistoryDBHelper.startwattage + "= '" + _startWattage + "', " +
                HistoryDBHelper.current_price + "= '" + _currentAmount + "' " +
                "where idx =" + idx;
            HistoryDBHelper.Query(sql);
        }

        //충전종료 여부
        public static bool ChargingEndYN(long idx)
        {
            string result = "";
            string sql = "select chargestarttime, chargeendtime, case when(chargestarttime is not null and chargeendtime is null) then 'YES' else 'no' end as result from history where idx = " + idx;

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                result = rdr[2].ToString();
            }
            rdr.Close();
            return result.Equals("YES") ? true : false;
        }

        public static string getLastPayType(long idx)
        {
            string result = "";
            string sql = "select pay_how from history where idx = " + idx;

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                result = rdr[0].ToString();
            }
            rdr.Close();
            return result;
        }

        public static void getLastDealInfo(long idx)
        {
            string sql = "select " + chargestarttime + ", " + price + ", " + pay_how + ", " + current_price +
                " from history where idx = " + idx;
            SQLiteCommand command = new SQLiteCommand(sql, conn);

            SQLiteDataReader rdr = command.ExecuteReader();
            rdr.Read();
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                EL_DC_Charger_MyApplication.getInstance().ListPrice.Add(rdr[i].ToString());
            }
            rdr.Close();
        }

        //충전종료
        public static void ChargingEnd(long idx, string chargingtime, string reason, string chargingwattage, string deal, string soc)
        {
            string endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql =
                "update history set " +
                HistoryDBHelper.chargeendtime + " = '" + endTime + "' ," +
                HistoryDBHelper.chargingtime + " = '" + chargingtime + "' ," +
                HistoryDBHelper.reason + " = '" + reason + "' , " +
                HistoryDBHelper.chargingwattage + " = '" + chargingwattage + "' ," +
                HistoryDBHelper.deal + " = '" + deal + "', " +
                HistoryDBHelper.soc + " = '" + soc + "'" +
                " where idx =" + idx;
            HistoryDBHelper.Query(sql);
        }

        //비정상 종료 시 사용할 이전 결제 정보 저장용
        public static void insertPayInfo(long idx, string data)
        {
            string sql =
                "update history set " +
                HistoryDBHelper.pay_info + " = '" + data + "'" +
                " where idx =" + idx;
            HistoryDBHelper.Query(sql);
        }
        public static string getPayInfo(long idx)
        {
            string result = "";
            string sql = "select pay_info from history where idx = " + idx;

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                result = rdr[0].ToString();
            }
            rdr.Close();
            return result;
        }


        public static void insert_NPQ3(long idx, string _npq_paymentResult, string _npq_chargingLimitProfile, string _npq_unitAmount, string _npq_sendOcpp)
        {
            string sql = "insert into billingInfo (" +
                "idx," +
                npq_paymentResult + "," +
                npq_chargingLimitProfile + "," +
                npq_unitAmount + "," +
                npq_sendOcpp + ") " +
                "values (" +
                "'" + idx + "'," +
                "'" + _npq_paymentResult + "', " +
                "'" + _npq_chargingLimitProfile + "', " +
                "'" + _npq_unitAmount + "', " +
                "'" + _npq_sendOcpp + "'" +
                ")";

            Query(sql);
        }

        public static void insert_NPS1(
            long idx,
            string _paymentResult,
            string _chargingLimitProfile,
            string _unitAmount,
            string _payCharge,
            string _payTransactionDT,
            string _payApprovalNumber,
            string _payUniqueNumber,
            string _payResponseCode,
            string _payResponseMessage,
            string _cardResponseCode,
            string _sendOcpp)
        {
            string sql = "insert into billingInfo (" +
                "idx," +
                nps1_paymentResult + "," +
                nps1_chargingLimitProfile + "," +
                nps1_unitAmount + "," +
                nps1_payCharge + "," +
                nps1_payTransactionDT + "," +
                nps1_payApprovalNumber + "," +
                nps1_payUniqueNumber + "," +
                nps1_payResponseCode + "," +
                nps1_payResponseMessage + "," +
                nps1_cardResponseCode + "," +
                nps1_sendOcpp + ") " +
                "values (" +
                "'" + idx + "'," +
                "'" + _paymentResult + "', " +
                "'" + _chargingLimitProfile + "', " +
                "'" + _unitAmount + "', " +
                "'" + _payCharge + "', " +
                "'" + _payTransactionDT + "', " +
                "'" + _payApprovalNumber + "', " +
                "'" + _payUniqueNumber + "', " +
                "'" + _payResponseCode + "', " +
                "'" + _payResponseMessage + "', " +
                "'" + _cardResponseCode + "', " +
                "'" + _sendOcpp + "' )";

            Query(sql);


        }


        public static void insert_NPS2(
            long idx,
            string _paymentResult,
            string _chargingLimitProfile,
            string _unitAmount,
            string _prepaymentCharge,
            string _chargingCharge,
            string _totalCharge,
            string _payCharge,
            string _payTransactionDT,
            string _payApprovalNumber,
            string _payUniqueNumber,
            string _payResponseCode,
            string _payResponseMessage,
            string _cardResponseCode,
            string _sendOcpp)
        {
            string sql = "insert into billingInfo (" +
                "idx," +
                nps2_paymentResult + "," +
                nps2_chargingLimitProfile + "," +
                nps2_unitAmount + "," +
                nps2_prepaymentCharge + "," +
                nps2_chargingCharge + "," +
                nps2_totalCharge + "," +
                nps2_payCharge + "," +
                nps2_payTransactionDT + "," +
                nps2_payApprovalNumber + "," +
                nps2_payUniqueNumber + "," +
                nps2_payResponseCode + "," +
                nps2_payResponseMessage + "," +
                nps2_cardResponseCode + "," +
                nps2_sendOcpp + ") " +
                "values (" +
                "'" + idx + "'," +
                "'" + _paymentResult + "', " +
                "'" + _chargingLimitProfile + "', " +
                "'" + _unitAmount + "', " +
                "'" + _prepaymentCharge + "', " +
                "'" + _chargingCharge + "', " +
                "'" + _totalCharge + "', " +
                "'" + _payCharge + "', " +
                "'" + _payTransactionDT + "', " +
                "'" + _payApprovalNumber + "', " +
                "'" + _payUniqueNumber + "', " +
                "'" + _payResponseCode + "', " +
                "'" + _payResponseMessage + "', " +
                "'" + _cardResponseCode + "', " +
                "'" + _sendOcpp + "' )";

            Query(sql);


        }
        //서버 에서 응답받은 메시지
        public static void insert_NPQ3_RECV(long idx, string _npq_recvOcpp)
        {
            string sql = "update billingInfo set '" + npq_recvOcpp + "' = '" + _npq_recvOcpp + "' where idx =" + idx;
            Query(sql);
        }

        public static void insert_NPS1_RECV(long idx, string recvOcpp)
        {
            string sql = "update billingInfo set '" + nps1_recvOcpp + "' = '" + recvOcpp + "' where idx =" + idx;
            Query(sql);
        }

        public static void insert_NPS2_RECV(long idx, string recvOcpp)
        {
            string sql = "update billingInfo set '" + nps2_recvOcpp + "' = '" + recvOcpp + "' where idx =" + idx;
            Query(sql);
        }


        public static void setHistoryUpdate(long idx, string colName, string data)
        {
            string sql = "update history set " + colName + " = '" + data + "' where idx =" + idx;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
        public static void Query(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
    }
}
