
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.INI;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.Controller;
using EL_DC_Charger.EL_DC_Charger.Database;
using EL_DC_Charger.EL_DC_Charger.Manager_UserControl;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Applications
{


    public class EL_DC_Charger_MyApplication : EL_MyApplication_Base
    {
        public const string NAME_SERVICE = "elcharger";
        public const string DESCRIPTION = "elcharger application excute";

        public EL_DC_Charger_MyApplication() : base()
        {

        }

        public static EL_DC_Charger_MyApplication getInstance()
        {
            if (myApplication == null)
                myApplication = new EL_DC_Charger_MyApplication();
            return (EL_DC_Charger_MyApplication)myApplication;

        }
        protected override void setChannelTotalInfor()
        {
            mChannelTotalInfor = new EL_DC_Charger_ChannelTotalInfor[getChannelCount()];
            for (int i = 0; i < mChannelTotalInfor.Length; i++)
            {
                mChannelTotalInfor[i] = new EL_DC_Charger_ChannelTotalInfor(this, i + 1);
                mChannelTotalInfor[i].initVariable();
            }

        }

        public override void initVariable()
        {
            base.initVariable();

            //mhs
            //2023-05-30 변수 할당 추가 (충전기타입,제조사,플랫폼,플랫폼운영사,무료서비스 기간) 
            //DB에서 값 불러와서 변수에 셋팅            

        }
        protected override void setDataManager_CustomUC_Main()
        {
            switch (EL_MyApplication_Base.HMI_SCREEN_MODE)
            {
                case EHmi_Screen_Mode.P1080_1920:
                    mDataManager_CustomUC_Main = new EL_Charger_DataManager_CustomUserControl_Main_P1080_1920(this);
                    break;
                case EHmi_Screen_Mode.NONE:
                case EHmi_Screen_Mode.P1024_600:
                    mDataManager_CustomUC_Main = new EL_Charger_DataManager_CustomUserControl_Main_CertDC(this);
                    break;
            }
            mDataManager_CustomUC_Main.initVariable();
        }


        //public override void setController_Main()
        //{
        //    mController_Main = new Controller_Main_DC1CH(this);
        //    mController_Main.initVariable();

        //}

        //public override void setController_Channel()
        //{
        //    mController_Cert_Channel = new CharginController_Cert_Channel(this, 1);
        //    mController_Cert_Channel.initVariable();
        //}

        protected override void setStateManager_Main()
        {
            switch (getChargerType())
            {
                case common.variable.EChargerType.NONE:
                default:
                    mStateManager_Main = new SC_1CH_None_StateManager_Main(this);
                    break;
                case common.variable.EChargerType.CH1_CERT:
                    mStateManager_Main = new SC_1CH_OCPP_StateManager_Main(this);
                    break;
                case common.variable.EChargerType.CH2_CERT:
                    mStateManager_Main = new SC_1CH_OCPP_StateManager_Main(this);
                    break;
                case common.variable.EChargerType.CH1_NOT_PUBLIC:
                    mStateManager_Main = new EL_Charger_1CH_Private_StateManager_Main(this);
                    break;
                case common.variable.EChargerType.CH1_PUBLIC:
                    switch (getPlatform())
                    {
                        case common.variable.EPlatform.NONE:
                            mStateManager_Main = new SC_1CH_None_StateManager_Main(this);
                            break;
                        case common.variable.EPlatform.WEV:
                            mStateManager_Main = new SC_1CH_OCPP_StateManager_Main(this);
                            break;
                        default:
                            mStateManager_Main = new SC_1CH_OCPP_StateManager_Main(this);
                            break;
                            //case common.variable.EPlatform.OCTT_CERTIFICATION:
                            //case common.variable.EPlatform.SOFTBERRY:
                            //case common.variable.EPlatform.NICE_CHARGER:
                            //    break;
                    }
                    break;
            }

        }

        public override EAdminMode getAdminMode(string inputpassword)
        {
            return EAdminMode.NONE;
            //mManager_SQLite_Settings.getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_AMI)
        }

        protected override void setManager_SQLite_Setting()
        {
            mManager_SQLite_Setting = new EL_DC_Manager_SQLite_Setting();
            //mManager_SQLite_Setting.setManager_Table();
        }

    }
}
