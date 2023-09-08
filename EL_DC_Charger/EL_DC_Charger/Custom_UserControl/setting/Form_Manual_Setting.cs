using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.INI;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    public partial class Form_Manual_Setting : Form
    {
        UC_Setting_Manual_Common UC_Setting_Manual_Common = new UC_Setting_Manual_Common();
        UC_Setting_Manual_Each UC_Setting_Manual_Each_Left;
        UC_Setting_Manual_Each UC_Setting_Manual_Each_Right;
        int currentChenal = 0;

        //EL_DC_Charger_MyApplication mApplication = EL_DC_Charger_MyApplication.getInstance();
        //채널 화면
        List<UC_Setting_Manual_Each> list_ch = new List<UC_Setting_Manual_Each>();
        string current = "";
        public Form_Manual_Setting()
        {
            InitializeComponent();


            //테스트용임 모니터 문현성PC 기준

            Screen[] scr = Screen.AllScreens;
            if (scr.Length > 1)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = scr[1].WorkingArea.Location;
            }
            //////////////////////////////////////////////


            UC_Setting_Manual_Common.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(UC_Setting_Manual_Common, 0, 1);
            tableLayoutPanel1.SetColumnSpan(UC_Setting_Manual_Common, 2);

            UC_Setting_Manual_Each_Left = new UC_Setting_Manual_Each(this, 1);
            UC_Setting_Manual_Each_Left.Name = ECharge_CH_Type.LEFT.ToString();
            UC_Setting_Manual_Each_Left.Dock = DockStyle.Fill;
            UC_Setting_Manual_Each_Left.lbl_title.Text = "좌측";


            if (EL_DC_Charger_MyApplication.getInstance().getChannelCount() > 1)
            {
                UC_Setting_Manual_Each_Right = new UC_Setting_Manual_Each(this, 2);
                UC_Setting_Manual_Each_Right.Name = ECharge_CH_Type.RIGHT.ToString();
                UC_Setting_Manual_Each_Right.Dock = DockStyle.Fill;
                UC_Setting_Manual_Each_Right.lbl_title.Text = "우측";
            }

            setControl(EL_DC_Charger_MyApplication.getInstance().mChargeCH_Type);


        }

        public ECharge_CH_Type mCharge_CH_Type;

        public void enableUITimer(bool setting)
        {
            timer_uiupdate.Enabled = setting;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!enableChangeMode)
            {
                MessageBox.Show("충전 상태 종료 후 닫기 버튼을 누르세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Wev_Form_Keypad wev_Form_Keypad = new Wev_Form_Keypad(Wev_Keypad_Type.CONFIRM);
                if (wev_Form_Keypad.ShowDialog() == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void addControl()
        {

        }

        private void setControl(ECharge_CH_Type type)
        {
            mCharge_CH_Type = type;

            if (ECharge_CH_Type.ALL == type)
                tableLayoutPanel1.RowCount = 4;

            else
                tableLayoutPanel1.RowCount = 3;

            list_ch.Clear();
            tableLayoutPanel1.Controls.RemoveByKey(ECharge_CH_Type.LEFT.ToString());
            tableLayoutPanel1.Controls.RemoveByKey(ECharge_CH_Type.RIGHT.ToString());

            switch (type)
            {
                case ECharge_CH_Type.LEFT:

                    tableLayoutPanel1.Controls.Add(UC_Setting_Manual_Each_Left, 0, 2);
                    tableLayoutPanel1.SetColumnSpan(UC_Setting_Manual_Each_Left, 2);

                    list_ch.Add(UC_Setting_Manual_Each_Left);

                    UC_Setting_Manual_Each_Left.cb_SUM_relay_plus.Visible = true;
                    UC_Setting_Manual_Each_Left.cb_SUM_relay_minus.Visible = true;

                    UC_Setting_Manual_Each_Left.setOutputInfor(500, 50);

                    rb_left.Checked = true;
                    break;
                case ECharge_CH_Type.RIGHT:

                    if (EL_DC_Charger_MyApplication.getInstance().getChannelCount() == 1)
                    {
                        return;
                    }

                    tableLayoutPanel1.Controls.Add(UC_Setting_Manual_Each_Right, 0, 2);
                    tableLayoutPanel1.SetColumnSpan(UC_Setting_Manual_Each_Right, 2);

                    list_ch.Add(UC_Setting_Manual_Each_Right);

                    UC_Setting_Manual_Each_Right.cb_SUM_relay_plus.Visible = true;
                    UC_Setting_Manual_Each_Right.cb_SUM_relay_minus.Visible = true;
                    UC_Setting_Manual_Each_Right.setOutputInfor(500, 50);

                    rb_right.Checked = true;
                    break;
                case ECharge_CH_Type.ALL:

                    if (EL_DC_Charger_MyApplication.getInstance().getChannelCount() == 1)
                    {
                        return;
                    }
                    //UC_Setting_Manual_Each_Left = new UC_Setting_Manual_Each();
                    //UC_Setting_Manual_Each_Left.Name = ECharge_CH_Type.LEFT.ToString();
                    //UC_Setting_Manual_Each_Left.Dock = DockStyle.Fill;
                    //UC_Setting_Manual_Each_Left.lbl_title.Text = "좌측";
                    tableLayoutPanel1.Controls.Add(UC_Setting_Manual_Each_Left, 0, 2);
                    tableLayoutPanel1.SetColumnSpan(UC_Setting_Manual_Each_Left, 2);


                    //UC_Setting_Manual_Each_Right = new UC_Setting_Manual_Each();
                    //UC_Setting_Manual_Each_Right.Name = ECharge_CH_Type.RIGHT.ToString();
                    //UC_Setting_Manual_Each_Right.Dock = DockStyle.Fill;
                    //UC_Setting_Manual_Each_Right.lbl_title.Text = "우측";
                    tableLayoutPanel1.Controls.Add(UC_Setting_Manual_Each_Right, 0, 3);
                    tableLayoutPanel1.SetColumnSpan(UC_Setting_Manual_Each_Right, 2);

                    list_ch.Add(UC_Setting_Manual_Each_Left);
                    list_ch.Add(UC_Setting_Manual_Each_Right);

                    UC_Setting_Manual_Each_Left.cb_SUM_relay_plus.Visible = false;
                    UC_Setting_Manual_Each_Left.cb_SUM_relay_minus.Visible = false;
                    UC_Setting_Manual_Each_Right.cb_SUM_relay_plus.Visible = false;
                    UC_Setting_Manual_Each_Right.cb_SUM_relay_minus.Visible = false;
                    UC_Setting_Manual_Each_Left.setOutputInfor(500, 50);
                    UC_Setting_Manual_Each_Right.setOutputInfor(500, 50);
                    rb_all.Checked = true;
                    break;
            }
        }

        private void rb_Click(object sender, EventArgs e)
        {
            string clickName = (sender as RadioButton).Name;

            if (current.Equals(clickName))
                return;
            else
            {
                //개별 DO 종료
                for (int i = 0; i < list_ch.Count; i++)
                {
                    list_ch[i].cb_SUM_relay_plus.Checked = false;
                    list_ch[i].cb_SUM_relay_minus.Checked = false;
                    list_ch[i].cb_do_relay_plus.Checked = false;
                    list_ch[i].cb_do_relay_minus.Checked = false;
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager().packet_z1.bSumRelay_Plus = false;
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager().packet_z1.bSumRelay_Minus = false;
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager().packet_z1.bPowerRelay_Plus = false;
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager().packet_z1.bPowerRelay_Minus = false;
                }

                //MC,FAN 종료                

                //for (int i = 1; i <= 2; i++)
                //{
                //    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager().packet_z1.bMC_On = false;
                //    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager().packet_z1.bFAN_On = false;
                //}

            }

            switch (clickName)
            {
                case "rb_left":
                    CsINIManager.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Config.ini", "MODE", "EACH", "LEFT");
                    EL_DC_Charger_MyApplication.getInstance().mChargeCH_Type = ECharge_CH_Type.LEFT;
                    setControl(ECharge_CH_Type.LEFT);
                    break;
                case "rb_right":
                    CsINIManager.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Config.ini", "MODE", "EACH", "RIGHT");
                    EL_DC_Charger_MyApplication.getInstance().mChargeCH_Type = ECharge_CH_Type.RIGHT;
                    setControl(ECharge_CH_Type.RIGHT);
                    break;
                case "rb_all":
                    CsINIManager.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Config.ini", "MODE", "EACH", "ALL");
                    EL_DC_Charger_MyApplication.getInstance().mChargeCH_Type = ECharge_CH_Type.ALL;
                    setControl(ECharge_CH_Type.ALL);
                    break;
            }
            current = clickName;
        }
        bool enableChangeMode = true;
        private void timer_uiupdate_Tick(object sender, EventArgs e)
        {
            enableChangeMode = true;
            int idx = 1;
            for (int i = 0; i < list_ch.Count; i++)
            {
                if (rb_right.Checked)
                    idx = 2;

                list_ch[i].label_total_active_evergy_finish.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).mChargingWattage.getCurrentWattage_String();
                list_ch[i].label_total_active_evergy_charging.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).mChargingWattage.getChargingWattage_String();


                list_ch[i].label_chargingtime.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).mChargingTime.getChargingTime();
                list_ch[i].label_chargingvoltage.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getAMI_PacketManager().getVoltage();
                list_ch[i].label_chargingcurrent.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getAMI_PacketManager().getCurrent();

                list_ch[i].label_chargingstate.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState;

                list_ch[i].label_errorcode.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.mErrorCode;
                list_ch[i].label_soc.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.mSOC;
                list_ch[i].label_remaintime.Text = "" + ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.mRemainTime_Minute;


                list_ch[i].cb_di_relay_plus.Checked = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.bFlag1_PowerRelay_Plus;


                if (list_ch[i].cb_di_relay_plus.Checked)
                {
                    list_ch[i].cb_di_relay_plus.Text = "ON";
                    list_ch[i].cb_di_relay_plus.ForeColor = Color.Red;
                }
                else
                {
                    list_ch[i].cb_di_relay_plus.Text = "OFF";
                    list_ch[i].cb_di_relay_plus.ForeColor = Color.Blue;
                }

                list_ch[i].cb_di_plc_status.Checked = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager().packet_1z.bFlag1_PlcStatus;
                if (list_ch[i].cb_di_plc_status.Checked)
                {
                    list_ch[i].cb_di_plc_status.Text = "ON";
                    list_ch[i].cb_di_plc_status.ForeColor = Color.Red;
                }
                else
                {
                    list_ch[i].cb_di_plc_status.Text = "OFF";
                    list_ch[i].cb_di_plc_status.ForeColor = Color.Blue;
                }

                list_ch[i].cb_di_relay_minus.Checked = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager().packet_1z.bFlag1_PowerRelay_Minus;
                if (list_ch[i].cb_di_relay_minus.Checked)
                {
                    list_ch[i].cb_di_relay_minus.Text = "ON";
                    list_ch[i].cb_di_relay_minus.ForeColor = Color.Red;
                }
                else
                {
                    list_ch[i].cb_di_relay_minus.Text = "OFF";
                    list_ch[i].cb_di_relay_minus.ForeColor = Color.Blue;
                }
                list_ch[i].cb_di_gunDetect.Checked = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.bFlag1_GunDetect;
                if (list_ch[i].cb_di_gunDetect.Checked)
                {
                    list_ch[i].cb_di_gunDetect.Text = "ON";
                    list_ch[i].cb_di_gunDetect.ForeColor = Color.Red;
                }
                else
                {
                    list_ch[i].cb_di_gunDetect.Text = "OFF";
                    list_ch[i].cb_di_gunDetect.ForeColor = Color.Blue;
                }


                int high = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager()).packet_1z.
                mPWM1_High;

                if (high >= 110)
                {
                    list_ch[i].label_comstate_gun.Text = "커넥터분리";
                }
                else if (high >= 80)
                {
                    list_ch[i].label_comstate_gun.Text = "커넥터연결";
                }
                else if (high >= 50)
                {
                    list_ch[i].label_comstate_gun.Text = "충전요청중";
                }
                else
                {
                    list_ch[i].label_comstate_gun.Text = "알수없음(" + high + ")";
                }



                list_ch[i].cb_comstate_ami.Checked = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getAMI_PacketManager().isConnected();
                if (EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getAMI_PacketManager().isConnected())
                {
                    list_ch[i].cb_comstate_ami.Text = "ON";
                    list_ch[i].cb_comstate_ami.ForeColor = Color.Red;
                }
                else
                {
                    list_ch[i].cb_comstate_ami.Text = "OFF";
                    list_ch[i].cb_comstate_ami.ForeColor = Color.Blue;
                }

                if (((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + idx).getControlbdComm_PacketManager())
                .packet_z1.mCommand_Output_Channel1 != 0)
                    enableChangeMode = false;
            }

            groupBox1.Enabled = enableChangeMode;
        }

        private void Form_Manual_Setting_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = true;
            }
        }

        private void Form_Manual_Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 1; i <= EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = false;
            }

            UC_Setting_Manual_Common.bck_camera.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                UC_Setting_Manual_Common.cctvEnable = false;
                UC_Setting_Manual_Common.pb_camera.Image = null;
                UC_Setting_Manual_Common.pb_camera.Visible = false;
                GC.Collect();
            }
            else
            {
                if (!UC_Setting_Manual_Common.bck_camera.IsBusy)
                    UC_Setting_Manual_Common.bck_camera.RunWorkerAsync();
                UC_Setting_Manual_Common.pb_camera.Visible = true;
                UC_Setting_Manual_Common.cctvEnable = true;
            }
        }
    }
}

