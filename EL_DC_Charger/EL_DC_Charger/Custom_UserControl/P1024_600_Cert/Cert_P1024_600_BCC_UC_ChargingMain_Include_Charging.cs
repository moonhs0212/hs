using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
using EL_DC_Charger.Interface_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600
{
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging : UserControl, IUC_Channel, IOnClickListener_Button
    {
        protected int mChannelIndex = 0;
        EL_ControlbdComm_PacketManager mPacketManager = null;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
            mPacketManager = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager();

            Wev_ImageButtonManager_ChargingStop imgButton = new Wev_ImageButtonManager_ChargingStop(pb_chargingstop);
            imgButton.setOnClickListener(this);

        }

        public int getChannelIndex()
        {
            return mChannelIndex;
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void initVariable()
        {

        }

        public void updateView()
        {
            updateChargingInfor();
        }
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public void updateChargingInfor()
        {
            progressBar_soc.Value = mPacketManager.packet_1z.mSOC;
            label_soc.Text = mPacketManager.packet_1z.mSOC + "%";
            label_chargingwattage.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingWattage.getChargingWattage_String();
            label_chrgingtime.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingTime.getChargingTime();
            label_amount.Text = "" + (Math.Round((EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingWattage.getChargingWattage() / 10000.0f) *
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Float(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST)));// + " 원";
            //bunifuProgressBar_voltage.Value = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getVoltage());
            //bunifuProgressBar_current.Value = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getCurrent()); ;

            label_chargingvoltagecurrent.Text = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getVoltage()) + "V, " + (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getCurrent()) + "A";

            label_amount_unit.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST);

            //label_charginginfor_voltage.Text = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getVoltage()) + " V";
            //label_charginginfor_current.Text = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getCurrent()) + " A";


            int remainMinute = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).getRemainTime_FullCharging_Minute();

            string temp = "";
            int tempInt = remainMinute / 60;
            if (tempInt >= 10)
                temp += tempInt;
            else
                temp += "0" + tempInt;
            temp += ":";
            tempInt = remainMinute % 60;
            if (tempInt >= 10)
                temp += tempInt;
            else
                temp += "0" + tempInt;
            temp += ":";
            temp += "00";
            label_remaintime.Text = temp;

        }

        public void setText(int indexArray, string text)
        {
            throw new NotImplementedException();
        }

        //public void onClick(object obj)
        //{
            
        //}

        public void onClick_Confirm(object sender)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsClick_ChargingStop_User = true;
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mErrorReason = "충전종료버튼";
            //bool bIsCharging_MoreOnce = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsCharging_MoreOnce;
            //bool bIsCommand_UsingStart = ((SC_1CH_MyApplication)mApplication).getChannelTotalInfor(1).mStateManager_Channel.bIsCommand_UsingStart;
            //bool bIsCharging_Current = ((SC_1CH_MyApplication)mApplication).getChannelTotalInfor(1).mStateManager_Channel.bIsCharging_Current;
            //if (bIsCharging_Current)
            //{
            //    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsClick_ChargingStop_User = true;
            //}
            //else
            //{
            //    ((SC_1CH_MyApplication)mApplication).getChannelTotalInfor(1).mStateManager_Channel.bIsSelected_Confirm_ChargingComplete = true;
            //}

            //EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().setErrorReason("충전종료버튼");
            //EL_DC_Charger_MyApplication.getInstance().Controller_Main.bIsClick_ChargingStop = true;
        }

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }
    }
}
