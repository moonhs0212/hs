using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
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
    public partial class P1080_1920_UC_ChargingMain_Include_Notify_1Tv_1Btn : UserControl, IUC_Channel_Button
    {
        protected int mChannelIndex = 0;
        public P1080_1920_UC_ChargingMain_Include_Notify_1Tv_1Btn(int channelIndex) : this()
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
            label_chargingunit.Text = "회원 " + EL_DC_Charger_MyApplication.getInstance().MemberAmount + " 원/kWh | 비회원 " + EL_DC_Charger_MyApplication.getInstance().NonmemberAmount + "원 / kWh";;
            lbl_cpid.Text = EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_Setting(0).getSettingData((int)CONST_INDEX_OCPP_Setting.infor_ChargeBoxSerialNumber);

        }

        public P1080_1920_UC_ChargingMain_Include_Notify_1Tv_1Btn()
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


        public void setText(int indexArray, string text)
        {
            switch (indexArray)
            {
                case 0:
                    tv_content_1.Text = text;
                    break;
            }
        }

        protected IOnClickListener_Button mOnClickListener_NotifyButton = null;


        public void onClick(object obj)
        {
            if (mOnClickListener_NotifyButton != null)
            {
                mOnClickListener_NotifyButton.onClick_Confirm(obj);
            }
            else
            {
                EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel().bIsClick_Notify_1Button = true;
            }


        }

        public void setOnClickListener(IOnClickListener_Button listener)
        {
            mOnClickListener_NotifyButton = listener;
        }

        public void setVisibility(int indexArray, bool visible)
        {
            throw new NotImplementedException();
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            onClick(sender);
        }
    }
}
