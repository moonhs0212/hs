using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerInfor;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.Applications;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Calculate_Charge : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 1;
        public P1080_1920_UC_ChargingMain_Include_Calculate_Charge(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
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

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public void updateView()
        {


                        
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargeUnit_Member();
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargeUnit_Nonmember();
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            EL_StateManager_Channel_Base stateManager = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel();
            switch (stateManager.mPaymentType)
            {
                case EPaymentType.NONE:
                    break;
                case EPaymentType.MEMBER_CARD:
                    break;
                case EPaymentType.QRCODE:
                    break;
                case EPaymentType.NONMEMBER_CARDDEVICE:
                    label_chargingwattage.Text = EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingWattage_String();
                    label_payment_first.Text = "" + stateManager.mNonmember_Payment_Setting_First;
                    label_payment_real.Text = "" + EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingCharge();
                    if (stateManager.mNonmember_Payment_Setting_First <= EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingCharge())
                        label_payment_partcancel.Text = "0";
                    else
                        label_payment_partcancel.Text = "" + (stateManager.mNonmember_Payment_Setting_First -
                            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingCharge());
                    break;
            }
        }
        public P1080_1920_UC_ChargingMain_Include_Calculate_Charge()
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;


            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }

        public void setText(int indexArray, string text)
        {
            switch (indexArray)
            {
                case 0:
                    //label_reason.Text = text;
                    break;
            }
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsClick_Notify_1Button = true;
        }
    }
}
