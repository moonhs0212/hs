using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.keypad;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
using EL_DC_Charger.EL_DC_Charger.Wev02;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920
{
    public partial class P1080_1920_UC_ChargingMain_Include_Touch_Screen : UserControl, IUC_Channel
    {
        DateTime lastTouch = new DateTime();
        public P1080_1920_UC_ChargingMain_Include_Touch_Screen()
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



            Wev02_ImageButtonManager_UsingStart imgButton_Back
     = new Wev02_ImageButtonManager_UsingStart(pb_start);
            switch (EL_DC_Charger_MyApplication.getInstance().getChargerType())
            {
                case EChargerType.NONE:
                case EChargerType.CH1_NOT_PUBLIC:
                    label_chargingunit.Visible = false;
                    break;
            }
            if (EL_DC_Charger_MyApplication.getInstance().getChargerType() == EChargerType.CH1_NOT_PUBLIC
                && EL_DC_Charger_MyApplication.getInstance().getPlatform() != EPlatform.NONE)
            {
                StringBuilder builder = new StringBuilder();
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().fromServiceMonth);
                builder.Append("6월");
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().fromServiceDay);
                builder.Append("1일 부터 ");
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().toServiceMonth);
                builder.Append("6월");
                //builder.Append(EL_DC_Charger_MyApplication.getInstance().toServiceDay);
                builder.Append("30일 까지는 시운전 기간으로 충전요금이 부과되지 않습니다.");

                label_prepare_service.Text = builder.ToString();
            }
            else
            {
                label_prepare_service.Visible = false;
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
            this.Invoke(new MethodInvoker(delegate ()
            {

                String[] memberChargeUnits = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getManager_Table_ChargeUnit().getChargeUnit(1, "ER");
                if (memberChargeUnits != null)
                {
                    EL_DC_Charger_MyApplication.getInstance().MemberAmount = float.Parse(memberChargeUnits[4 + EL_Time.GetCurrentHour()]);
                }

                memberChargeUnits = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting().getManager_Table_ChargeUnit().getChargeUnit(1, "NM");
                if (memberChargeUnits != null)
                {
                    EL_DC_Charger_MyApplication.getInstance().NonmemberAmount = float.Parse(memberChargeUnits[4 + EL_Time.GetCurrentHour()]);
                }



                label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";
                lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);
            }));


        }

        private void tableLayoutPanel2_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsCommand_UsingStart = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse = false;
        }
    }
}
