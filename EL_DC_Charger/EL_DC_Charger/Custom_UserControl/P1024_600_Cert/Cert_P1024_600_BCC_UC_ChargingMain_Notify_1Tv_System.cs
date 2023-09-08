using EL_DC_Charger.common.interf.uiux;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager;
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
    public partial class Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_System : UserControl, IUC_Channel, Manager_AdminEnter.OnChange_AdminEnter
    {
        protected int mChannelIndex = 0;
        public Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_System(int channelIndex) : this()
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

        Manager_AdminEnter mManager_AdminEnter = null;

        public Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_System()
        {
            InitializeComponent();


            mManager_AdminEnter = new Manager_AdminEnter(this, 5);
            mManager_AdminEnter.setOnChange_AdminEnter(this);
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

        public void setVisibility(int indexArray, bool visible)
        {
            
        }

        public void onChange(List<Integer> touchList)
        {
            if (touchList[0] == 1
                && touchList[1] == 3
                && touchList[2] == 4
                && touchList[3] == 2
                && touchList[4] == 1)
            {
                mManager_AdminEnter.clearEvent();
                if (EL_DC_Charger_MyApplication.getInstance().getAdminMode() == EAdminMode.NONE)
                {
                    EL_DC_Charger_MyApplication.getInstance().setAdminMode(EAdminMode.ADMIN);
                    EL_DC_Charger_MyApplication.getInstance().getDataManager_CustomUC_Main().Form_Setting.openForm();
                }
                else
                {

                }
            }
            
        }
    }
}
