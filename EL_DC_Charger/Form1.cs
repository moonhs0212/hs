using EL_DC_Charger.Interface_Common;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet;
using EL_DC_Charger.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.thread;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.common.INI;
using EL_DC_Charger.common.ChargerVariable;
using System.Drawing.Text;

namespace EL_DC_Charger
{
    public partial class Form1 : Form, IMainForm
    {
        byte[] arrays = new byte[] {0x02

,0x4b ,0x49 ,0x4f ,0x53 ,0x4b ,0x31 ,0x31 ,0x31 ,0x34 ,0x39 ,0x31 ,0x35 ,0x35 ,0x34 ,0x35 ,0x00

,0x32 ,0x30 ,0x32 ,0x32 ,0x31 ,0x31 ,0x30 ,0x34 ,0x31 ,0x31 ,0x32 ,0x35 ,0x35 ,0x32

,0x44

,0x00

,0x00 ,0x00

,0x03

,0x1a };
        public Form1()
        {
            InitializeComponent();

            PrivateFontCollection privateFont = new PrivateFontCollection();

            Smartro_TL3500BS_Checksum.setCheckSum(arrays);

            EL_Mananger_System.startService_TimeSynchronization();
            EL_Mananger_System.startService_Plz_AdminMode();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
            //StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            //EL_DC_Charger_MyApplication.getInstance().getChargerType()
            Screen[] scr = Screen.AllScreens;
            int index = 0;

            if (EL_MyApplication_Base.IS_HMI_SCREEN_SIZE_VARIABLE)
            {
                if (scr.Length >= 3)
                    index = 1;
                else index = 0;
                
                if (scr[index].Bounds.Width == 1080)
                    EL_MyApplication_Base.HMI_SCREEN_MODE = EHmi_Screen_Mode.P1080_1920;
                else
                    EL_MyApplication_Base.HMI_SCREEN_MODE = EHmi_Screen_Mode.P1024_600;
            }
            else index = EL_MyApplication_Base.HMI_SCREEN_INDEX;

            Width = EL_MyApplication_Base.SCREEN_WIDTH;
            Height = EL_MyApplication_Base.SCREEN_HEIGHT;

            //this.Location = scr[index].WorkingArea.Location;
            //StartPosition = FormStartPosition.CenterScreen;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = scr[index].WorkingArea.Location;
            // 모니터위치 변경

            //if (scr.Length > 2)
            //{

            //    Screen screen = (scr[0].WorkingArea.Contains(this.Location)) ? scr[1] : scr[0]; // 현재모니터 찾기

            //}
            if (EL_MyApplication_Base.IS_HMI_SCREEN_TOP) TopMost = true;
            else TopMost = false;


            EL_DC_Charger_MyApplication.getInstance().MainForm = this;

            //this.WindowState = FormWindowState.Maximized;
            mIntervalExcute_List_For_Timer = new EL_IntervalExcute_Thread(EL_DC_Charger_MyApplication.getInstance(), true);
            timer_init.Start();

            

        }

        private void timer_init_Tick(object sender, EventArgs e)
        {
            initVariable();
            timer_init.Stop();
            timer_process.Start();
        }

        public void initVariable()
        {
            EL_DC_Charger_MyApplication.getInstance().MainForm = this;
            EL_DC_Charger_MyApplication.getInstance().initVariable();

            //if (EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Boolean(CONST_INDEX_MAINSETTING.IS_SHOW_TEST_FORM))
            //{
            //    Form_Test form_Test = new Form_Test();
            //    form_Test.Show();
            //}



            ////////////////////////////////////////////////////////////////////////////////////////

        }

        public static EL_IntervalExcute_Thread mIntervalExcute_List_For_Timer = null;


        private void timer_process_Tick(object sender, EventArgs e)
        {
            if (EL_DC_Charger_MyApplication.getInstance().Controller_Main != null)
                EL_DC_Charger_MyApplication.getInstance().Controller_Main.process();

            if (mIntervalExcute_List_For_Timer != null)
                mIntervalExcute_List_For_Timer.intervalExcute();

            //if (EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader != null)
            //    EL_DC_Charger_MyApplication.getInstance().SerialPort_Smartro_CardReader.getManager_Send().process();

            if (EL_DC_Charger_MyApplication.getInstance().Controller_Cert_Channel != null)
                EL_DC_Charger_MyApplication.getInstance().Controller_Cert_Channel.process();
        }

        public Panel getPanel_Main()
        {
            return panel_main;
        }

        public Form getForm_Main()
        {
            return this;
        }

        protected UserControl mIncludeControl = null;
        public void setPanel_Main_CustomUserControl(UserControl control)
        {
            if (mIncludeControl == null)
            {
                mIncludeControl = control;
                panel_main.Controls.Add(control);
            }
            else
            {
                if (mIncludeControl == control)
                {

                }
                else
                {
                    panel_main.Controls.Clear();

                    panel_main.Controls.Add(control);
                }
            }

        }
    }
}
