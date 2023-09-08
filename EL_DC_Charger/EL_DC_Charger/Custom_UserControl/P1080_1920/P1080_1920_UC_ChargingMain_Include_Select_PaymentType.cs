using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.EL_DC_Charger.Wev.ImgButtonManager;
using EL_DC_Charger.EL_DC_Charger.Wev02;
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
using static System.Net.Mime.MediaTypeNames;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Select_PaymentType : UserControl, IUC_Channel, IOnClickListener_Button
    {

        protected int mChannelIndex = 0;
        public P1080_1920_UC_ChargingMain_Include_Select_PaymentType(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
        }

        public P1080_1920_UC_ChargingMain_Include_Select_PaymentType()
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }

            //Wev02_ImageButtonManager_Select_MemberType manager_Member = new Wev02_ImageButtonManager_Select_MemberType(pB_Select_qr);
            //manager_Member.setOnClickListener(this);
            //Wev02_ImageButtonManager_Select_MemberType manager_Nonmember = new Wev02_ImageButtonManager_Select_MemberType(pB_Select_carddevice);
            //manager_Nonmember.setOnClickListener(this);

            Wev02_ImageButtonManager_Home manager_Home = new Wev02_ImageButtonManager_Home(btn_home);
            Wev02_ImageButtonManager_Back manager_Back = new Wev02_ImageButtonManager_Back(btn_back);

            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);

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
            return 1;
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void initVariable()
        {

        }

        public void onClick_Cancel(object sender)
        {

        }

        public void onClick_Confirm(object sender)
        {
            if (String.Equals(((Control)sender).Name, "pB_Select_qr"))
            {
                EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.QRCODE;
                EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsSelected_NonMemeber_PaymentType = true;
            }
            else if (String.Equals(((Control)sender).Name, "pB_Select_carddevice"))
            {
                EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.NONMEMBER_CARDDEVICE;
                EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsSelected_NonMemeber_PaymentType = true;
            }

        }

        public void setText(int indexArray, string text)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        public void updateView()
        {
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);            
        }
        private void pn_Click(object sender, EventArgs e)
        {
            string _name;
            if (e == null)
                _name = sender.ToString();
            else
                _name = ((Panel)sender).Name.ToString();

            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsSelected_NonMemeber_PaymentType = true;
            switch (_name)
            {
                case "pn_memberCard":
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.MEMBER_CARD;
                    break;
                case "pn_qr":
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.QRCODE;
                    break;
                case "pn_nonCard":
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mPaymentType = EPaymentType.NONMEMBER_CARDDEVICE;
                    break;
            }
        }
        private void lblClick(object sender, EventArgs e)
        {
            string name = ((Label)sender).Name.ToString();
            switch (name)
            {
                case
                    "lbl_mem":
                    pn_Click("pn_memberCard", null);
                    break;
                case
                    "lbl_qr":
                    pn_Click("pn_qr", null);
                    break;
                case
                    "lbl_non":
                    pn_Click("pn_nonCard", null);
                    break;
            }
        }


        private void pn_MouseDown(object sender, MouseEventArgs e)
        {
            ((Panel)sender).BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked;
        }

        private void pn_hover(object sender, EventArgs e)
        {
            ((Panel)sender).BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked;
        }
        private void pn_leave(object sender, EventArgs e)
        {
            ((Panel)sender).BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_normal;
        }

        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            switch (((Label)sender).Name)
            {
                case "lbl_mem": pn_memberCard.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked; break;
                case "lbl_qr": pn_qr.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked; break;
                case "lbl_non": pn_nonCard.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked; break;
            }

        }

        private void label_hover(object sender, EventArgs e)
        {
            switch (((Label)sender).Name)
            {                
                case "lbl_mem": pn_memberCard.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked; break;
                case "lbl_qr": pn_qr.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked; break;
                case "lbl_non": pn_nonCard.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_clicked; break;
            }
        }
        private void label_leave(object sender, EventArgs e)
        {
            switch (((Label)sender).Name)
            {
                case "lbl_mem": pn_memberCard.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_normal; break;
                case "lbl_qr": pn_qr.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_normal; break;
                case "lbl_non": pn_nonCard.BackgroundImage = Properties.Resources.wev02_horizontal_img_btn_membertype_normal; break;
            }
        }
    }
}
