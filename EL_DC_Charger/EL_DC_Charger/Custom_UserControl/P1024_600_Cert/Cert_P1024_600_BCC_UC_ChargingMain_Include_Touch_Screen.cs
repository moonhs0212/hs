using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
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

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert
{
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Include_Touch_Screen : UserControl, IUC_Channel
    {
        public Cert_P1024_600_BCC_UC_ChargingMain_Include_Touch_Screen()
        {
            InitializeComponent();

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
                label_prepare_service.Visible = false; ;
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
            
        }

        public void setText(int indexArray, string text)
        {
            
        }

        public void setVisibility(int indexArray, bool visible)
        {
            
        }

        public void updateView()
        {
            
        }

        private void Cert_P1024_600_BCC_UC_ChargingMain_Include_Touch_Screen_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Click(object sender, EventArgs e)
        {
            EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsCommand_UsingStart = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
