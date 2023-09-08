using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.EL_DC_Charger.Wev02;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Notify_1Tv_System : UserControl,
        IUC_Channel
    {
        DateTime lastTouch= new DateTime();
        protected int mChannelIndex = 0;
        public P1080_1920_UC_ChargingMain_Include_Notify_1Tv_System(int channelIndex) : this()
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
            
        }
        public P1080_1920_UC_ChargingMain_Include_Notify_1Tv_System()
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


            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }
        public void setVisibility(int indexArray, bool visible)
        {
        }

        public void setText(int indexArray, string text)
        {
            switch (indexArray)
            {
                case 0:
                    tv_content_1.Text = text;
                    break;
            }
        }

        private void pn1_Click(object sender, EventArgs e)
        {
            lastTouch = DateTime.Now;
        }

        private void pn2_Click(object sender, EventArgs e)
        {
            if (lastTouch.AddSeconds(1) >= DateTime.Now)
            {
                Wev_Form_Keypad wev_Form_Keypad = new Wev_Form_Keypad();
                wev_Form_Keypad.Show();
            }
        }
    }
}
