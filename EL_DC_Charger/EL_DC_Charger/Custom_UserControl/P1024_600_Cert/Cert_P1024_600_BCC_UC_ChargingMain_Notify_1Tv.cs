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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv : UserControl, 
        IUC_Channel
    {

        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv(int channelIndex) : this()
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
        public Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv()
        {
            InitializeComponent();


            Wev_ImageButtonManager_Home imageButton = new Wev_ImageButtonManager_Home(pb_home);
        }
        public void setVisibility(int indexArray, bool visible)
        {
            switch (indexArray)
            {
                case 0:
                    pb_home.Visible = visible;
                    break;
            }
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
    }
}
