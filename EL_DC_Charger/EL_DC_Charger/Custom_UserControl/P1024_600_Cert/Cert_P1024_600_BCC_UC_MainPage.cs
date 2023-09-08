using EL_DC_Charger.common.interf.uiux;
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
    public partial class Cert_P1024_600_BCC_UC_MainPage : UserControl, IUC_Main
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_MainPage(int channelIndex) : this()
        {
            mChannelIndex = channelIndex;
        }

        public Cert_P1024_600_BCC_UC_MainPage()
        {
            InitializeComponent();
        }

        public Panel getPanel_Main()
        {
            return panel_main;
        }


        public void setContent(int channelIndex, UserControl control)
        {
            panel_main.Controls.Clear();
            panel_main.Controls.Add(control);
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public void setBottombar_Weather()
        {
            
        }

        public void setBottombar_ProcessStep()
        {
            
        }

        public void setPanel_Main_CustomUserControl(UserControl control)
        {
            
        }

        

        public void setVisible_Button_Back(bool visible)
        {
            
        }

        public void setText(int indexArray, string text)
        {
            
        }

        public void setVisible_Button_Home(bool visible)
        {
            throw new NotImplementedException();
        }

        public void setHomeBackBtn_Manager(IHomeBackBtn_Manager manager)
        {
            
        }

        protected IHomeBackBtn_Manager mHomeBackBtn_Manager = null;
        //public void setHomeBackBtn_Manager()
    }
}
