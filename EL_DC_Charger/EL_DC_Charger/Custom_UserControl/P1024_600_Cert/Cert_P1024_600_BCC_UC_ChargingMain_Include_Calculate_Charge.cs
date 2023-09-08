using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge : UserControl, IUC_Channel, IOnClickListener_Button
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge(int channelIndex) : this()
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

        public void updateView()
        {
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
                    label_payment_real.Text = ""+EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingCharge();
                    if (stateManager.mNonmember_Payment_Setting_First <= EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingCharge())
                        label_payment_partcancel.Text = "0";
                    else
                        label_payment_partcancel.Text = "" + (stateManager.mNonmember_Payment_Setting_First -
                            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mChargingWattage.getChargingCharge());
                    break;
            }
        }
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;

            Wev_ImageButtonManager_Confirm imageButton = new Wev_ImageButtonManager_Confirm(pb_chargingcomplete);
            imageButton.setOnClickListener(this);
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

        public void onClick_Confirm(object sender)
        {
            //EL_DC_Charger_MyApplication.getInstance().Controller_Main.bIsClick_Confirm_ErrorReson_BeforeCharging = true;

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsClick_Notify_1Button = true;
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
