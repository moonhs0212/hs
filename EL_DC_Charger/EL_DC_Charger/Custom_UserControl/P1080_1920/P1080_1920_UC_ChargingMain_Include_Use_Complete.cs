using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Use_Complete : UserControl, IUC_Channel
            
    {
        public P1080_1920_UC_ChargingMain_Include_Use_Complete()
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

            switch (EL_DC_Charger_MyApplication.getInstance().getChargerType())
            {
                case EChargerType.NONE:
                case EChargerType.CH1_NOT_PUBLIC:
                    label_chargingunit.Visible = false;
                    break;
            }
            Wev02_ImageButtonManager_Back imgButton_Back
                = new Wev02_ImageButtonManager_Back(pb_back);

            Wev02_ImageButtonManager_Home imgButton_Home
                = new Wev02_ImageButtonManager_Home(pb_back);

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
            throw new NotImplementedException();
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
    }
}
