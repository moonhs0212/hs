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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete : UserControl, IUC_Channel, IOnClickListener_Button
    {
        protected int mChannelIndex = 0;
        EL_ControlbdComm_PacketManager mPacketManager = null;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
            mPacketManager = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager();
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
            updateChargingCompleteInfor();
        }
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete()
        {
            InitializeComponent();
            Wev_ImageButtonManager_Confirm imageButton = new Wev_ImageButtonManager_Confirm(pb_chargingcomplete);
            imageButton.setOnClickListener(this);

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;
        }


        public void updateChargingCompleteInfor()
        {
            //.Value = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mSoc_Finish;
            label_chargingwattage.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingWattage.getChargingWattage_String();
            label_chrgingtime.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingTime.getChargingTime();
            label_amount.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingWattage.getChargingCharge() + " 원";
            //EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Float(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST)

            label_reason.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mErrorReason;
            //mPacketManager.packet_1z.mOutput_Voltage
        }



        public void setText(int indexArray, string text)
        {
            
        }

        public void onClick_Confirm(object sender)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsSelected_Confirm_ChargingComplete = true;

            //EL_DC_Charger_MyApplication.getInstance().Controller_Main.bIsClick_ChargingComplete = true;
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
