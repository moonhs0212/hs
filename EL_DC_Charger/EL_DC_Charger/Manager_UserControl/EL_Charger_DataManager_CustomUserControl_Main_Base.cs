using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting;
using EL_DC_Charger.Interface_Common;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Manager_UserControl
{
    abstract public class EL_Charger_DataManager_CustomUserControl_Main_Base : EL_Object_Channel_Base
    {
        protected IMainForm mMainForm = null;
        public EL_Charger_DataManager_CustomUserControl_Main_Base(EL_MyApplication_Base application) : base(application, 0)
        {
            mMainForm = application.MainForm;
        }


        /// --------------------------------------------------------------------<summary>
        /// 
        /// ------------------------------------------------------------------- </summary>
        public Form_Setting_Main mForm_Setting = null;
        public Form_Setting_Main Form_Setting
        {
            get { return mForm_Setting; }
        }
        /// --------------------------------------------------------------------<summary>
        /// 
        /// ------------------------------------------------------------------- </summary>







        public void setView_MainPanel_Include_ChargingMain_Calculate_Charge()
        {
            mUC_Charging_Calculate_Charge.initVariable();
            mUC_Charging_Calculate_Charge.updateView();
            mUC_MainPage.setContent(0, mUC_Charging_Calculate_Charge.getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());
        }

        public void setView_MainPanel_Include_1Tv_System(string text, bool visibleSetting_BackButton, int channel = 0)
        {
            mUC_Notify_1Tv_System[channel].setText(0, text);
            mUC_MainPage.setContent(channel, mUC_Notify_1Tv_System[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(visibleSetting_BackButton);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());

            mUC_Notify_1Tv_System[channel].updateView();
        }

        public void setView_MainPanel_Include_1Tv_1Btn(string text, bool visibleSetting_BackButton, IOnClickListener_Button listener, int channel = 0)
        {
            mUC_Notify_1Tv_1Btn[channel].setText(0, text);
            mUC_Notify_1Tv_1Btn[channel].setOnClickListener(listener);
            mUC_MainPage.setContent(channel, mUC_Notify_1Tv_1Btn[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(visibleSetting_BackButton);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());

            mUC_Notify_1Tv_1Btn[channel].updateView();
        }

        public void setView_MainPanel_Include_1Tv(string text, bool visibleSetting_BackButton, int channel = 0)
        {
            mUC_Notify_1Tv[channel].setText(0, text);
            mUC_MainPage.setContent(channel, mUC_Notify_1Tv[channel].getUserControl());
            mUC_Notify_1Tv[channel].setVisibility(0, visibleSetting_BackButton);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());

            mUC_Notify_1Tv[channel].updateView();
        }


        public void setView_MainPanel_Include_ChargingMain_QRCode(string uri, int channel = 0)
        {
            ((P1080_1920_UC_ChargingMain_Include_Qrcode)mUC_Charging_QRCode).makeQRcode_Base64(uri);
            mUC_MainPage.setContent(channel, mUC_Charging_QRCode.getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_QRCode.updateView();
            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());
        }

        public void setView_MainPanel_Include_ChargingMain_UseComplete(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Use_Complete[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());
            mUC_Use_Complete[channel].updateView();
        }

        public void setView_MainPanel_Include_ChargingMain_CardTag(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Charging_CardTag[channel].getUserControl());
            mUC_MainPage.setBottombar_ProcessStep();
            mUC_MainPage.setVisible_Button_Back(true);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());

            mUC_Charging_CardTag[channel].updateView();
        }



        public void setView_MainPanel_Include_ChargingMain_Charging(int channel = 0)
        {
            mUC_Charging_Charging[channel].updateView();
            mUC_MainPage.setContent(channel, mUC_Charging_Charging[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);

            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());
        }
        public void setView_MainPanel_Include_ChargingMain_Charging_Complete(string reason, int channel = 0)
        {
            mUC_Charging_Charging_Complete[channel].updateView();
            mUC_Charging_Charging_Complete[channel].setText(0, reason);
            mUC_MainPage.setContent(channel, mUC_Charging_Charging_Complete[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);
        }
        public void setView_MainPanel_Include_ChargingMain_Disconnect_Connector(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Charging_Disconnect_Connector[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_Disconnect_Connector[channel].updateView();
        }

        public void setView_MainPanel_Include_ChargingMain_Preparing_Charging(int channel = 0)
        {
            mUC_Charging_Preparing_Charging[channel].initVariable();
            mUC_MainPage.setContent(channel, mUC_Charging_Preparing_Charging[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_Preparing_Charging[channel].updateView();
        }
        public void setView_MainPanel_Include_ChargingMain_Process_Certification(int channel = 0)
        {
            mUC_Charging_Process_Certification[channel].initVariable();
            mUC_MainPage.setContent(channel, mUC_Charging_Process_Certification[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_Process_Certification[channel].updateView();
        }
        public void setView_MainPanel_Include_ChargingMain_Certification_Complete(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Charging_Certification_Complete[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_Certification_Complete[channel].updateView();
        }
        public void setView_MainPanel_Include_ChargingMain_Connect_Connector(int channel = 0)
        {
            mUC_Charging_Connect_Connector[channel].initVariable();
            mUC_MainPage.setContent(channel, mUC_Charging_Connect_Connector[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_Connect_Connector[channel].updateView();
        }

        public void setView_MainPanel_Include_ChargingMain_Emergency(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Charging_Emergency.getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);
            mUC_Charging_Emergency.updateView();
        }
        public void setView_MainPanel_Include_ChargingMain_Error_Before_Charging(string errorCode, int channel = 0)
        {
            mUC_Use_Error_Before_Charging[channel].setText(0, errorCode);
            mUC_MainPage.setContent(channel, mUC_Use_Error_Before_Charging[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);
            mUC_Use_Error_Before_Charging[channel].updateView();
        }

        public void setView_MainPanel_Include_ChargingMain_Select_Member_Type(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Charging_Select_Member_Type.getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Charging_Select_Member_Type.updateView();
        }





        /// --------------------------------------------------------------------<summary>
        /// 
        /// ------------------------------------------------------------------- </summary>
        public void setView_MainPanel_Select_Member(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_SelectMember.getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);
            mUC_SelectMember.updateView();
        }

        virtual public void setView_MainPanel_Main(int channel = 0)
        {
            mMainForm.setPanel_Main_CustomUserControl(mUC_MainPage.getUserControl());
            mUC_MainPage.setVisible_Button_Back(false);
        }
        /// --------------------------------------------------------------------<summary>
        /// 
        /// ------------------------------------------------------------------- </summary>


        public void setView_MainContent_Setting(int channel = 0)
        {
            mUC_MainPage.setPanel_Main_CustomUserControl(mUC_Setting);
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = false;
            mUC_Cert_ManualControl_Output.initVariable();
        }

        public void setView_MainContent_Cert_ManualControl_Output(int channel = 0)
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager())
                .packet_z1.bHMI_Manual_Control = true;
            mUC_Cert_ManualControl_Output.initVariable();
            mUC_MainPage.setPanel_Main_CustomUserControl(mUC_Cert_ManualControl_Output);

        }

        public void setView_NonMember_Input_Payment_Amount(int channel = 0)
        {
            mUC_NonMember_Input_Payment_Amount.initVariable();

            mUC_MainPage.setContent(channel, mUC_NonMember_Input_Payment_Amount.getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_NonMember_Input_Payment_Amount.updateView();
        }

        public void setView_NonMember_Select_PaymnetType(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_NonMember_Select_PaymnetType.getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_NonMember_Select_PaymnetType.updateView();
        }

        public void setView_Select_Touch_Screen(int channel = 0)
        {
            mUC_MainPage.setContent(channel, mUC_Touch_Screen[channel].getUserControl());
            mUC_MainPage.setVisible_Button_Back(true);
            mUC_Touch_Screen[channel].updateView();

        }




        IBottomBar_Step_Manager mBottomBar_Step_Manager = null;
        public IBottomBar_Step_Manager BottomBar_Step_Manager
        {
            get { return mBottomBar_Step_Manager; }
            set { mBottomBar_Step_Manager = value; }
        }



        public IUC_Channel mUC_Charging_Calculate_Charge = null;

        static int mChannelCount = EL_DC_Charger_MyApplication.getInstance().getChannelCount();

        public IUC_Channel[] mUC_Notify_1Tv_System = new IUC_Channel[mChannelCount];
        public IUC_Channel_Button[] mUC_Notify_1Tv_1Btn = new IUC_Channel_Button[mChannelCount];
        public IUC_Channel[] mUC_Notify_1Tv = new IUC_Channel[mChannelCount];

        public IUC_Channel[] mUC_Touch_Screen = new IUC_Channel[mChannelCount];

        public IUC_Channel[] mUC_Charging_Charging = new IUC_Channel[mChannelCount];
        public IUC_Channel[] mUC_Charging_Charging_Complete = new IUC_Channel[mChannelCount];
        public IUC_Channel[] mUC_Charging_Disconnect_Connector = new IUC_Channel[mChannelCount];
        public IUC_Channel[] mUC_Charging_Preparing_Charging = new IUC_Channel[mChannelCount];
        protected IUC_Channel[] mUC_Charging_Process_Certification = new IUC_Channel[mChannelCount];
        protected IUC_Channel[] mUC_Charging_Certification_Complete = new IUC_Channel[mChannelCount];
        public IUC_Channel[] mUC_Charging_Connect_Connector = new IUC_Channel[mChannelCount];

        protected IUC_Channel[] mUC_Charging_CardTag = new IUC_Channel[mChannelCount];
        protected IUC_Channel mUC_Charging_Select_Member_Type = null;
        protected IUC_Channel mUC_Charging_QRCode = null;


        protected IUC_Channel mUC_NonMember_Input_Payment_Amount = null;

        protected IUC_Channel mUC_NonMember_Select_PaymnetType = null;

        public IUC_Channel getUC_Charging_QRCode()
        {
            return mUC_Charging_QRCode;
        }
        protected IUC_Channel[] mUC_Use_Complete = new IUC_Channel[mChannelCount];

        protected IUC_Channel mUC_Charging_Emergency = null;
        protected IUC_Channel[] mUC_Use_Error_Before_Charging = new IUC_Channel[mChannelCount];
        protected IUC_Channel mUC_SelectMember = null;

        protected IUC_Main mUC_MainPage = null;




        /// --------------------------------------------------------------------<summary>
        /// 
        /// ------------------------------------------------------------------- </summary>

        protected UC_MainPage_Include_SettingMain mUC_Setting = null;
        protected UC_MainPage_Include_Cert_ManualControl_Output mUC_Cert_ManualControl_Output = null;
    }
}
