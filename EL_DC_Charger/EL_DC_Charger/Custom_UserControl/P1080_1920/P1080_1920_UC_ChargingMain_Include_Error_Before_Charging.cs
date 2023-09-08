using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Error_Before_Charging : UserControl, IUC_Channel, IOnClickListener_Button
    {
        public P1080_1920_UC_ChargingMain_Include_Error_Before_Charging()
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
            Wev_ImageButtonManager_Confirm imageButton = new Wev_ImageButtonManager_Confirm(pb_confirm);
            imageButton.setOnClickListener(this);


            EL_DC_Charger_MyApplication.getInstance().setTouchManger(this);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                //cp.ExStyle |= 0x00080000;
                return cp;
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
            
        }

        public UserControl getUserControl()
        {
            return this;
        }

        public int getChannelIndex()
        {
            return 1;
        }

        public void initVariable()
        {
            throw new NotImplementedException();
        }

        public void updateView()
        {
            
        }

        public void setText(int indexArray, string text)
        {
            label_reason.Text = text;
        }
    }
}
