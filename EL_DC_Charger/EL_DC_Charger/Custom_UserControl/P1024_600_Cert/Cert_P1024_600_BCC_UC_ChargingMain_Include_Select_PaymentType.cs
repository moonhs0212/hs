using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType : UserControl, IUC_Channel, IOnClickListener_Button
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
        }

        #region custombutton
        private void button_qrcode_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.QRCODE;
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsSelected_NonMemeber_PaymentType = true;
        }

        private void button_carddevice_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.NONMEMBER_CARDDEVICE;
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsSelected_NonMemeber_PaymentType = true;
        }
        #endregion

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
            
        }

        public void setText(int indexArray, string text)
        {
            throw new NotImplementedException();
        }


        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;

            Wev_ImageButtonManager_Home btn_Home = new Wev_ImageButtonManager_Home(pb_home);
        }

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        public void onClick_Confirm(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
