using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Input_Payment_Amount : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;


        public P1080_1920_UC_ChargingMain_Include_Input_Payment_Amount(int channelIndex)
        {
            InitializeComponent();

            if (EL_MyApplication_Base.HMI_SCREEN_MODE == EHmi_Screen_Mode.P1024_600)
            {
                this.Width = 1024;
                this.Height = 600;
            }
             
            mChannelIndex = channelIndex;

            Wev02_ImageButtonManager_Back imgButton_Back
                = new Wev02_ImageButtonManager_Back(pb_back);

            Wev02_ImageButtonManager_Home imgButton_Home
                = new Wev02_ImageButtonManager_Home(pb_home);



            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }
        const int VALUE_DEFAULT = 10000;

        protected int mValue_Default = 10000;
        protected int mValue_Input = VALUE_DEFAULT;
        protected int mValue_Max = 30000;
        protected int mValue_Min = 100;

        public void setDefaultValue()
        {
            setValue(mValue_Default);


        }

        protected void addValue(int value)
        {
            int settingValue = mValue_Input + value;

            if (settingValue > mValue_Max)
                settingValue = mValue_Max;
            else if (settingValue < mValue_Min)
                settingValue = mValue_Min;

            setValue(settingValue);
        }

        protected void setValue(int value)
        {
            mValue_Input = value;
            label_amount.Text = string.Format("{0:#,###}", mValue_Input);
        }

        private void button_100_plus_Click(object sender, EventArgs e)
        {
            addValue(100);
        }

        private void button_100_minus_Click(object sender, EventArgs e)
        {
            addValue(-100);
        }

        private void button_500_plus_Click(object sender, EventArgs e)
        {
            addValue(500);
        }

        private void button_500_minus_Click(object sender, EventArgs e)
        {
            addValue(-500);
        }

        private void button_1000_plus_Click(object sender, EventArgs e)
        {
            addValue(1000);
        }

        private void button_1000_minus_Click(object sender, EventArgs e)
        {
            addValue(-1000);
        }

        private void button_10000_plus_Click(object sender, EventArgs e)
        {
            addValue(10000);
        }

        private void button_10000_minus_Click(object sender, EventArgs e)
        {
            addValue(-10000);
        }

        private void button_payment_start_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getManager_Option_ChargingStop()
                .addOption(EOption_ChargingStop.WON, mValue_Input);
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().mNonmember_Payment_Setting_First = mValue_Input;
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsClick_SettingComplete_Payment_Value = true;
        }

        private void button_default_Click(object sender, EventArgs e)
        {
            setValue(mValue_Default);
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
            setDefaultValue();
        }

        public void updateView()
        {
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

        }

        public void setText(int indexArray, string text)
        {
            throw new NotImplementedException();
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }


        private void label_amount_Click(object sender, EventArgs e)
        {

        }

        private void Cert_P1024_600_BCC_UC_ChargingMain_Include_Input_Payment_Amount_Load(object sender, EventArgs e)
        {

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button_select_amount_Click(object sender, EventArgs e)
        {

        }

        private void button_select_chargingep_Click(object sender, EventArgs e)
        {

        }
    }
}
