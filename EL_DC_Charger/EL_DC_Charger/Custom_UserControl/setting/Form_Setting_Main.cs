using EL_DC_Charger.BatteryChange_Charger.ChargerVariable;
using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting
{
    public partial class Form_Setting_Main : Form
    {
        public Form_Setting_Main()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            Width = EL_MyApplication_Base.SCREEN_WIDTH;
            Height = EL_MyApplication_Base.SCREEN_HEIGHT;
            TopMost = true;
            initUserControl();
        
            

        }

        EL_Time mTime_Show = new EL_Time();

        public void openForm()
        {
            mTime_Show.setTime();
            changeContent_Setting_Main();
            this.Show();
            this.Visible = true;
            TopMost = true;
            timer1.Enabled = true;
            timer1.Interval = 60;
            timer1.Start();
        }

        public void hideForm()
        {
            this.Hide();
            this.Visible = false;
            timer1.Enabled = false;
            timer1.Stop();
        }

        


        public void setTextButton(string title, string subtitle)
        {
            label_title.Text = title;
            label_subtitle.Text = subtitle;
        }


        public void setContent_Add(UserControl control)
        {
            setContent(control);
            mList_VG_Main.Add(control);
        }

        public void setContent(UserControl control)
        {
            if(control.GetType() == typeof(UC_MainPage_Include_Cert_ManualControl_Output))
            {
                mUC_Content_ManualControl_Output.initVariable();
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = true;    
            }
            else
            {
                mUC_Content_ManualControl_Output.initVariable();
                ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = false;
            }

            panel_main.Controls.Clear();
            //control.Dock = DockStyle.Fill;
            panel_main.Controls.Add(control);
        }

        public void changeContent_Setting_Main()
        {
            setTextButton("설정", "");
            setContent_Add(mUC_Content_Setting_Main);

            
        }

        public void setContent_CommDeviceSetting()
        {
            //EL_DC_Charger_MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_SUB1_COMM_DEVICE_SETTING);

            setContent_Add(mUC_Content_CommDeviceSetting);

            setTextButton("설정", "통신 및 장치설정");
        }

        public void setContent_DoorSetting()
        {
            //EL_DC_Charger_MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_SUB1_DOOR_SETTING);
            //panel_main.Controls.Clear();
            //mUC_Content_Door_Setting.Dock = DockStyle.Fill;
            //panel_main.Controls.Add(mUC_Content_Door_Setting);

            //setTextButton("설정", "도어 설정");
        }

        public void setContent_TestMode_Charging()
        {

            mUC_Content_ManualControl_Output.initVariable();
            setContent_Add(mUC_Content_ManualControl_Output);
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = true;
            setTextButton("설정", "테스트모드 (충전)");
            //EL_DC_Charger_MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_SUB1_TESTMODE_CHARGING);
        }

        public void setContent_Socket_Setting()
        {

            //panel_main.Controls.Clear();

            //mUC_Content_Setting_Socket = new P788_BCC_UC_Setting_Socket();
            //mUC_Content_Setting_Socket.Dock = DockStyle.Fill;
            //panel_main.Controls.Add(mUC_Content_Setting_Socket);

            //setTextButton("설정", "소켓 설정");
        }

        public void setContent_Station_Setting()
        {
            setContent_Add(mUC_Content_Station);

            setTextButton("설정", "스테이션 설정");
        }

        public void setContent_CSMS_Infor()
        {
            setContent_Add(mUC_Content_CSMS_Infor);

            setTextButton("설정", "CSMS 설정");
        }


        public void setContent_TestMode_DoorOpen()
        {
            panel_main.Controls.Clear();
            //mUC_Content_TestMode_Charging.Dock = DockStyle.Fill;
            //panel_main.Controls.Add(mUC_Content_TestMode_Charging);

            setTextButton("설정", "테스트모드 (도어)");
        }

        void initUserControl()
        {
            mUC_Content_Setting_Main = new UC_Setting_Main();
            mUC_Content_CommDeviceSetting = new UC_SettingMain_Include_Content_SerialPort_Setting();
            mUC_Content_ManualControl_Output = new UC_MainPage_Include_Cert_ManualControl_Output();
            mUC_Content_ManualControl_Output.initVariable();
            mUC_Content_Station = new UC_Setting_Station();
            mUC_Content_CSMS_Infor = new UC_Setting_CSMS_Infor();
            mUC_Content_CSMS_Infor.initVariable();
        }

        List<UserControl> mList_VG_Main = new List<UserControl>();


        UC_Setting_Station mUC_Content_Station = null;
        UC_MainPage_Include_Cert_ManualControl_Output mUC_Content_ManualControl_Output = null;
        UC_SettingMain_Include_Content_SerialPort_Setting mUC_Content_CommDeviceSetting = null;
        protected UC_Setting_Main mUC_Content_Setting_Main = null;

        UC_Setting_CSMS_Infor mUC_Content_CSMS_Infor = null;

        private void button_windowclose_Click(object sender, EventArgs e)
        {
            if (mList_VG_Main.Count > 1)
            {
                mList_VG_Main.RemoveAt(mList_VG_Main.Count - 1);

                UserControl vgManager = mList_VG_Main[mList_VG_Main.Count-1];
                //vgManager.initVariable();
                setContent(vgManager);
            }
            else
            {
                
                hideForm();
                EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.NONE);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mTime_Show.getMinute_WastedTime() > 10)
                hideForm();
        }
    }
}
