using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1024_600_Cert;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.Manager_UserControl
{
    public class EL_Charger_DataManager_CustomUserControl_Main_P1080_1920 : EL_Charger_DataManager_CustomUserControl_Main_Base
    {

        public EL_Charger_DataManager_CustomUserControl_Main_P1080_1920(EL_MyApplication_Base application) : base(application)
        {

        }

        public override void initVariable()
        {


            mUC_MainPage = new P1080_1920_UC_MainPage();
            mUC_Setting = new UC_MainPage_Include_SettingMain();
            mUC_Cert_ManualControl_Output = new UC_MainPage_Include_Cert_ManualControl_Output();
            mUC_Charging_QRCode = new P1080_1920_UC_ChargingMain_Include_Qrcode();
            ////////////////////////////////////
            mForm_Setting = new Form_Setting_Main();
            for (int i = 0; i < EL_DC_Charger_MyApplication.getInstance().getChannelCount(); i++)
            {
                mUC_Touch_Screen[i] = new P1080_1920_UC_ChargingMain_Include_Touch_Screen();
                mUC_Use_Complete[i] = new P1080_1920_UC_ChargingMain_Include_Use_Complete();
                mUC_Charging_CardTag[i] = new P1080_1920_UC_ChargingMain_Include_Search_Card();
                mUC_Charging_Charging[i] = new P1080_1920_UC_ChargingMain_Include_Charging(1);
                mUC_Charging_Charging_Complete[i] = new P1080_1920_UC_ChargingMain_Include_Charging_Complete(1);
                mUC_Charging_Disconnect_Connector[i] = new P1080_1920_UC_ChargingMain_Include_Wait_Disconnect_Connector();
                mUC_Charging_Preparing_Charging[i] = new P1080_1920_UC_ChargingMain_Include_Preparing_Charging();
                mUC_Charging_Process_Certification[i] = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Process_Certification();
                mUC_Charging_Certification_Complete[i] = new Cert_P1024_600_BCC_UC_ChargingMain_Include_User_Certification_Complete();
                mUC_Charging_Connect_Connector[i] = new P1080_1920_UC_ChargingMain_Include_Connector_Wait_Connect_Connector();
                mUC_Use_Error_Before_Charging[i] = new P1080_1920_UC_ChargingMain_Include_Error_Before_Charging();
                mUC_Notify_1Tv_System[i] = new P1080_1920_UC_ChargingMain_Include_Notify_1Tv_System();
                mUC_Notify_1Tv_1Btn[i] = new P1080_1920_UC_ChargingMain_Include_Notify_1Tv_1Btn();
                mUC_Notify_1Tv[i] = new P1080_1920_UC_ChargingMain_Include_Notify_1Tv();
            }
            //mUC_Charging_Select_Member_Type = new P1080_1920_UC_ChargingMain_Include_Select_Member();

            mUC_NonMember_Input_Payment_Amount = new P1080_1920_UC_ChargingMain_Include_Input_Payment_Amount(1);


            mUC_NonMember_Select_PaymnetType = new P1080_1920_UC_ChargingMain_Include_Select_PaymentType(1);

            mUC_Charging_Calculate_Charge = new P1080_1920_UC_ChargingMain_Include_Calculate_Charge(1);
        }







    }
}
