using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.EL_DC_Charger.Applications;
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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Search_Card : UserControl, IUC_Channel
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Search_Card(int channelIndex) : this()
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

        public void setText(int indexArray, string text)
        {
            
        }

        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Search_Card()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.None;

            pictureBox1.Parent = this;

            Wev_ImageButtonManager_Home btn_Home = new Wev_ImageButtonManager_Home(pb_home);

        }


        private void label2_Click(object sender, EventArgs e)
        {
            //EL_DC_Charger_MyApplication.getInstance().Controller_Main.bIsReceive_CardNumber = true;
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mCardNumber_Read_Temp = "1111222233334444";
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsComplete_RFCard_Read = true;


        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }
    }
}
