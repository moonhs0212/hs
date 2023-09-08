using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp_Bunifu;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl
{
    public partial class UC_SettingMain_Include_Content_SerialPort_Setting : UserControl
    {
        public UC_SettingMain_Include_Content_SerialPort_Setting()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            mCommPath_Default_Controlbd = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                                                .getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_CONTROLBD);
            mCommPath_Default_RFReader = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                                                .getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_RFREADER);
            mCommPath_Default_AMI = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                                                .getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_AMI);

            label_compath_controlbd.Text = mCommPath_Default_Controlbd;
            label_compath_rfreader.Text = mCommPath_Default_RFReader;
            label_compath_ami.Text = mCommPath_Default_AMI;

            refreshCommpath();
        }

        const int SIZE_BUTTON_WIDTH = 120;
        const int SIZE_BUTTON_HEIGHT = 70;

        public void refreshCommpath()
        {
            label_compath_controlbd.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                                                .getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_CONTROLBD);
            label_compath_rfreader.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                                                .getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_RFREADER);
            label_compath_ami.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                                                .getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_AMI);

            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            //COM Port가 있는 경우에만 콤보 박스에 추가.\
            Button[] btnlist = new Button[3];
            for(int i = 0; i < 3; i++)
            {
                Button btn2 = new Button();
                btn2.BackColor = System.Drawing.Color.DarkGray;
                //btn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                btn2.FlatStyle = System.Windows.Forms.FlatStyle.System;
                btn2.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                btn2.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
                btn2.Text = "";
                btn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                btnlist[i] = btn2;
            }

            btnlist[0].Click += comlist_controlbd_Click;
            btnlist[1].Click += comlist_rfreader_Click;
            btnlist[2].Click += comlist_ami_Click;
            flowLayoutPanel1.Controls.Add(btnlist[0]);
            flowLayoutPanel2.Controls.Add(btnlist[1]);
            flowLayoutPanel3.Controls.Add(btnlist[2]);

            if (comlist.Length > 0)
            {
                for (int i = 0; i < comlist.Length; i++)
                {
                    Button btn = new Button();
                    btn.BackColor = System.Drawing.Color.White;
                    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_controlbd_Click;
                    flowLayoutPanel1.Controls.Add(btn);
                }
                for (int i = 0; i < comlist.Length; i++)
                {
                    Button btn = new Button();
                    btn.BackColor = System.Drawing.Color.White;
                    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_rfreader_Click;
                    flowLayoutPanel2.Controls.Add(btn);
                }
                for (int i = 0; i < comlist.Length; i++)
                {
                    Button btn = new Button();
                    btn.BackColor = System.Drawing.Color.White;
                    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_ami_Click;
                    flowLayoutPanel3.Controls.Add(btn);
                }

            }

            //for (int i = 0; i < 20; i++)
            //{
            //    Button btn = new Button();
            //    btn.BackColor = System.Drawing.Color.White;
            //    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            //    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            //    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            //    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
            //    btn.Text = "asdfsdaf";
            //    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //    btn.Click += comlist_io_Click;
            //    flowLayoutPanel1.Controls.Add(btn);
            //}

            touchScrollPanel1 = new TouchScrollPanel(flowLayoutPanel1);
            touchScrollPanel2 = new TouchScrollPanel(flowLayoutPanel2);
            touchScrollPanel3 = new TouchScrollPanel(flowLayoutPanel3);
        }
        TouchScrollPanel touchScrollPanel1 = null;
        TouchScrollPanel touchScrollPanel2 = null;
        TouchScrollPanel touchScrollPanel3 = null;

        private void button_refresh_list_Click(object sender, EventArgs e)
        {
            refreshCommpath();
        }

        private void button_init_Click(object sender, EventArgs e)
        {
            label_compath_controlbd.Text = mCommPath_Default_Controlbd;
            label_compath_rfreader.Text = mCommPath_Default_RFReader;
            label_compath_ami.Text = mCommPath_Default_AMI;
        }

        string mCommPath_Default_Controlbd = "";
        string mCommPath_Default_RFReader = "";
        string mCommPath_Default_AMI = "";

        private void comlist_controlbd_Click(object sender, EventArgs e)
        {
            label_compath_controlbd.Text = ((Button)sender).Text;
        }

        private void comlist_rfreader_Click(object sender, EventArgs e)
        {
            label_compath_rfreader.Text = ((Button)sender).Text;
        }

        private void comlist_ami_Click(object sender, EventArgs e)
        {
            label_compath_ami.Text = ((Button)sender).Text;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            
            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                .setSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_CONTROLBD, label_compath_controlbd.Text);

            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                .setSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_RFREADER, label_compath_rfreader.Text);

            EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0)
                .setSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_AMI, label_compath_ami.Text);


            mCommPath_Default_Controlbd = label_compath_controlbd.Text;
            mCommPath_Default_RFReader = label_compath_rfreader.Text;
            mCommPath_Default_AMI = label_compath_ami.Text;

        }
    }
}
