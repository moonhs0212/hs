using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.LogSearch
{
    public partial class frmHistory : Form
    {
        public frmHistory()
        {
            InitializeComponent();

            SetDoubleBuffer(dataGridView1, true);
        }
        static void SetDoubleBuffer(Control dgv, bool DoubleBuffered)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { DoubleBuffered });
        }
        private void frmHistory_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnCount = 14;

            dataGridView1.Columns[0].DataPropertyName = "idx";
            dataGridView1.Columns[0].HeaderText = "순번";
            dataGridView1.Columns[0].FillWeight = 0.5f;


            dataGridView1.Columns[1].DataPropertyName = HistoryDBHelper.charge_id;
            dataGridView1.Columns[1].FillWeight = 2;
            dataGridView1.Columns[1].HeaderText = "충전고유번호";

            dataGridView1.Columns[2].DataPropertyName = HistoryDBHelper.usestarttime;
            dataGridView1.Columns[2].FillWeight = 2;
            dataGridView1.Columns[2].HeaderText = "사용시작시간";

            dataGridView1.Columns[3].DataPropertyName = HistoryDBHelper.useendttime;
            dataGridView1.Columns[3].FillWeight = 2;
            dataGridView1.Columns[3].HeaderText = "사용종료시간";

            dataGridView1.Columns[4].DataPropertyName = HistoryDBHelper.chargestarttime;
            dataGridView1.Columns[4].FillWeight = 2;
            dataGridView1.Columns[4].HeaderText = "충전시작시간";

            dataGridView1.Columns[5].DataPropertyName = HistoryDBHelper.chargeendtime;
            dataGridView1.Columns[5].FillWeight = 2;
            dataGridView1.Columns[5].HeaderText = "충전종료시간";

            dataGridView1.Columns[6].DataPropertyName = HistoryDBHelper.reason;
            dataGridView1.Columns[6].FillWeight = 1;
            dataGridView1.Columns[6].HeaderText = "종료 사유";

            dataGridView1.Columns[7].DataPropertyName = HistoryDBHelper.chargingwattage;
            dataGridView1.Columns[7].FillWeight = 1;
            dataGridView1.Columns[7].HeaderText = "충전전력";

            dataGridView1.Columns[8].DataPropertyName = HistoryDBHelper.price;
            dataGridView1.Columns[8].FillWeight = 1;
            dataGridView1.Columns[8].HeaderText = "결제 가격";

            dataGridView1.Columns[9].DataPropertyName = HistoryDBHelper.member_yn;
            dataGridView1.Columns[9].FillWeight = 0.5f;
            dataGridView1.Columns[9].HeaderText = "회원여부";

            dataGridView1.Columns[10].DataPropertyName = HistoryDBHelper.member_card_no;
            dataGridView1.Columns[10].FillWeight = 2;
            dataGridView1.Columns[10].HeaderText = "회원카드번호";


            dataGridView1.Columns[11].DataPropertyName = HistoryDBHelper.pay_id;
            dataGridView1.Columns[11].FillWeight = 2;
            dataGridView1.Columns[11].HeaderText = "결제고유번호";

            dataGridView1.Columns[12].DataPropertyName = HistoryDBHelper.pay_yn;
            dataGridView1.Columns[12].FillWeight = 0.5f;
            dataGridView1.Columns[12].HeaderText = "결제여부";

            dataGridView1.Columns[13].DataPropertyName = HistoryDBHelper.pay_how;
            dataGridView1.Columns[13].FillWeight = 1;
            dataGridView1.Columns[13].HeaderText = "결제방식";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HistoryDBHelper.SqlliteConn();
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            double totalWatt = 0;
            if (dataGridView1.DataSource != null)
                ((DataTable)dataGridView1.DataSource).Rows.Clear();

            SQLiteDataReader dr = HistoryDBHelper.SelectData(toDate.Text, fromDate.Text);

            DataTable dt = new DataTable();

            dt = GetTable(dr);
            dataGridView1.DataSource = dt;


            await Task.Run(() =>
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][HistoryDBHelper.chargingwattage].ToString() != "")
                        totalWatt += Convert.ToDouble(dt.Rows[i][HistoryDBHelper.chargingwattage]);
                }
            });

            lbl_kw.Text = "총 전력량 : " + totalWatt.ToString("F2") + " kw";
        }
        public DataTable GetTable(SQLiteDataReader reader)
        {
            DataTable table = reader.GetSchemaTable();
            DataTable dt = new System.Data.DataTable();
            DataColumn dc;
            DataRow row;
            System.Collections.ArrayList aList = new System.Collections.ArrayList();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dc = new DataColumn();

                if (!dt.Columns.Contains(table.Rows[i]["ColumnName"].ToString()))
                {
                    dc.ColumnName = table.Rows[i]["ColumnName"].ToString();
                    dc.Unique = Convert.ToBoolean(table.Rows[i]["IsUnique"]);
                    dc.AllowDBNull = Convert.ToBoolean(table.Rows[i]["AllowDBNull"]);
                    dc.ReadOnly = Convert.ToBoolean(table.Rows[i]["IsReadOnly"]);
                    aList.Add(dc.ColumnName);
                    dt.Columns.Add(dc);
                }
            }

            while (reader.Read())
            {
                row = dt.NewRow();
                for (int i = 0; i < aList.Count; i++)
                {
                    row[((string)aList[i])] = reader[(string)aList[i]];
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExportToCSV();
        }

        //엑셀 라이브러리를 쓰면 되지만 키오스크엔 엑셀이 없어 CSV로 저장 함.
        private void ExportToCSV()
        {
            SaveFileDialog saveFileDialog = GetCsvSave();

            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Save_csv(saveFileDialog.FileName, dataGridView1, true);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private SaveFileDialog GetCsvSave()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckPathExists = true;
            saveDialog.AddExtension = true;
            saveDialog.ValidateNames = true;

            string path = Assembly.GetExecutingAssembly().Location;
            string filepath = Path.GetDirectoryName(path);

            saveDialog.InitialDirectory = filepath;

            saveDialog.DefaultExt = ".csv";
            saveDialog.Filter = "csv (*.csv) | *.csv";
            saveDialog.FileName = "export".ToString();

            return saveDialog;
        }
        private void Save_csv(string fileName, DataGridView dgview, bool header)
        {
            string delimiter = ","; // 구분자
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter csvExport = new StreamWriter(fs, System.Text.Encoding.UTF8); //UTF8로 엔코딩
            if (dgview.Rows.Count == 0) return;
            // header가 true면 헤더정보 출력
            if (header)
            {
                for (int i = 0; i < dgview.Columns.Count; i++)
                {
                    csvExport.Write(dgview.Columns[i].HeaderText);
                    if (i != dgview.Columns.Count - 1)
                    {
                        csvExport.Write(delimiter);
                    }
                }
            }
            csvExport.Write(csvExport.NewLine); // add new line

            // 데이터 출력
            foreach (DataGridViewRow row in dgview.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int i = 0; i < dgview.Columns.Count; i++)
                    {
                        string data = row.Cells[i].Value.ToString();
                        data = data.Replace("\r\n", "");
                        csvExport.Write(data);
                        if (i != dgview.Columns.Count - 1)
                        {
                            csvExport.Write(delimiter);
                        }
                    }
                    csvExport.Write(csvExport.NewLine); // write new line
                }
            }
            csvExport.Flush();
            csvExport.Close();
            fs.Close();
            MessageBox.Show("CSV 파일 저장 완료！");
        }
    }
}