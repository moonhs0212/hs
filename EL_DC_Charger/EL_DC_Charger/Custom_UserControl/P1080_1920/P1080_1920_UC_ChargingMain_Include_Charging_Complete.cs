using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.ocpp.ver16.database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Charging_Complete : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;
        EL_ControlbdComm_PacketManager mPacketManager = null;
        public P1080_1920_UC_ChargingMain_Include_Charging_Complete(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
            mPacketManager = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager();



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
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            updateChargingInfor();
        }
        public P1080_1920_UC_ChargingMain_Include_Charging_Complete()
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }
            else if ((EL_DC_Charger_MyApplication.getInstance().getChargerType()) == EChargerType.CH2_CERT)
            {
                this.Width = 540;
                this.Height = 720;
            }

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;

            switch (EL_DC_Charger_MyApplication.getInstance().getChargerType())
            {
                case EChargerType.NONE:
                case EChargerType.CH1_NOT_PUBLIC:
                    label_chargingunit.Visible = false;
                    break;
            }

            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }

        public void updateChargingInfor()
        {
            circularProgressBar1.Value = mPacketManager.packet_1z.mSOC;
            //label_soc.Text = mPacketManager.packet_1z.mSOC + "%";
            label_chargingwattage.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingWattage.getChargingWattage_String();
            label_chrgingtime.Text = EL_Time.getTime_HMS_6String_includeDivider((long)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getStateManager_Channel().mTime_ChargingStart.getSecond_WastedTime(), ":");
            label_amount.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingWattage.getChargingCharge();// + " 원";
                         

        }

        public void setText(int indexArray, string text)
        {
            label_reason.Text = text;
        }

        //public void onClick(object obj)
        //{

        //}

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }


        private void button_confirm_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsSelected_Confirm_ChargingComplete = true;            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse = false;
        }
    }
}

