using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.ImgButtonManager;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Connector_Wait_Connect_Connector : UserControl, IUC_Channel
    {
        public P1080_1920_UC_ChargingMain_Include_Connector_Wait_Connect_Connector()
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
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            //EL_UserControl_Manager.SearchAllControl(this);

            Wev02_ImageButtonManager_Back imgButton_Back
                = new Wev02_ImageButtonManager_Back(pb_back);

            Wev02_ImageButtonManager_Home imgButton_Home
                = new Wev02_ImageButtonManager_Home(pb_home);

            switch (EL_DC_Charger_MyApplication.getInstance().getChargerType())
            {
                case EChargerType.NONE:
                case EChargerType.CH1_NOT_PUBLIC:
                    label_chargingunit.Visible = false;
                    break;
            }

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


        public UserControl getUserControl()
        {
            return this;
        }

        protected Image[] mImageResource = new Image[] { Properties.Resources.wev02_img_progress_connectconnector_01,
            Properties.Resources.wev02_img_progress_connectconnector_02,
            Properties.Resources.wev02_img_progress_connectconnector_03,
            Properties.Resources.wev02_img_progress_connectconnector_04};

        int mIndexArray = 0;
        EL_Time mTime_Update = new EL_Time();
        public void updateUI()
        {
            if (mTime_Update.getMiliSecond_WastedTime() > 400)
            {
                mTime_Update.setTime();
                pb_progress.Image = mImageResource[mIndexArray++];

                if (mIndexArray >= mImageResource.Length)
                    mIndexArray = 0;
            }

        }

        public void initVariable()
        {
            mIndexArray = 0;
        }

        public int getChannelIndex()
        {
            return 1;
        }

        public void updateView()
        {
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

            updateUI();
        }

        public void setText(int indexArray, string text)
        {

        }

        public void setVisibility(int indexArray, bool visible)
        {

        }

        private void ui_timer_500ms_Tick(object sender, EventArgs e)
        {

        }
    }
}
