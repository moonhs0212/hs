using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Charging : UserControl, IUC_Channel, IUISetting_ChageUnit
    {
        protected int mChannelIndex = 0;
        EL_ControlbdComm_PacketManager mPacketManager = null;
        public P1080_1920_UC_ChargingMain_Include_Charging(int channelIndex) : this()
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
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh"; ;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            updateChargingInfor();
        }
        public P1080_1920_UC_ChargingMain_Include_Charging()
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }
            else
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
            if (EL_DC_Charger_MyApplication.getInstance().getChargerType() == EChargerType.CH1_NOT_PUBLIC
                && EL_DC_Charger_MyApplication.getInstance().getPlatform() != EPlatform.NONE)
            {
                StringBuilder builder = new StringBuilder();
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().fromServiceMonth);
                builder.Append("6월");
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().fromServiceDay);
                builder.Append("1일 부터 ");
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().toServiceMonth);
                builder.Append("6월");
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().toServiceDay);
                builder.Append("30일 까지는 시운전 기간으로 충전요금이 부과되지 않습니다.");

                label_prepare_service.Text = builder.ToString();
            }
            else
            {
                label_prepare_service.Visible = false; ;
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
                                                                                                                                                        //bunifuProgressBar_voltage.Value = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getVoltage());
                                                                                                                                                        //bunifuProgressBar_current.Value = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getCurrent()); ;

            //label_chargingvoltagecurrent.Text = (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getVoltage()) + "V, " + (int)(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getCurrent()) + "A";

            float ouputPower = (EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getVoltage()
                                * EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getCurrent()) / 1000.0f;

            label_chargingvoltagecurrent.Text = ouputPower.ToString("F2");
            //label_amount_unit.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST);

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

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsClick_ChargingStop_User = true;
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mErrorReason = "충전종료버튼";
        }

        public void setUX_ChargeUnit(bool isMember, float chargeUnit)
        {
            if (isMember)
            {

            }
            else
            {

            }
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse = false;
        }
    }
}

