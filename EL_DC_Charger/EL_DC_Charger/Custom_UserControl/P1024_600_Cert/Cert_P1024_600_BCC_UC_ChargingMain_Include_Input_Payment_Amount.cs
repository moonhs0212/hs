using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Input_Payment_Amount : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;
        
        
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Input_Payment_Amount(int channelIndex)
        {
            InitializeComponent();
            mChannelIndex = channelIndex;

            Wev_ImageButtonManager_Home btn_Home = new Wev_ImageButtonManager_Home(pb_home);
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
