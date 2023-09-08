using EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
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
    public class EL_Charger_DataManager_CustomUserControl_Main_CertDC : EL_Charger_DataManager_CustomUserControl_Main_Base
    {

        public EL_Charger_DataManager_CustomUserControl_Main_CertDC(EL_MyApplication_Base application) : base(application)
        {

        }

        public override void initVariable()
        {
            mUC_MainPage = new Cert_P1024_600_BCC_UC_MainPage();
            mUC_Setting = new UC_MainPage_Include_SettingMain();
            mUC_Cert_ManualControl_Output = new UC_MainPage_Include_Cert_ManualControl_Output();

            ////////////////////////////////////
            mForm_Setting = new Form_Setting_Main();

            //mUC_SelectMember = new P1080_1920_UC_ChargingMain_Include_Select_Member();            





            mUC_Touch_Screen[0] = new P1080_1920_UC_ChargingMain_Include_Touch_Screen();

            mUC_Use_Complete[0] = new P1080_1920_UC_ChargingMain_Include_Use_Complete();
            mUC_Charging_CardTag[0] = new P1080_1920_UC_ChargingMain_Include_Search_Card();

            mUC_Charging_Charging[0] = new P1080_1920_UC_ChargingMain_Include_Charging(1);
            mUC_Charging_Charging_Complete[0] = new P1080_1920_UC_ChargingMain_Include_Charging_Complete(1);
            mUC_Charging_Disconnect_Connector[0] = new P1080_1920_UC_ChargingMain_Include_Wait_Disconnect_Connector();
            mUC_Charging_Preparing_Charging[0] = new P1080_1920_UC_ChargingMain_Include_Preparing_Charging();
            mUC_Charging_Process_Certification[0] = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Process_Certification();
            mUC_Charging_Certification_Complete[0] = new Cert_P1024_600_BCC_UC_ChargingMain_Include_User_Certification_Complete();
            mUC_Charging_Connect_Connector[0] = new P1080_1920_UC_ChargingMain_Include_Connector_Wait_Connect_Connector();


            mUC_Use_Error_Before_Charging[0] = new P1080_1920_UC_ChargingMain_Include_Error_Before_Charging();


            mUC_Notify_1Tv_System[0] = new P1080_1920_UC_ChargingMain_Include_Notify_1Tv_System();
            mUC_Notify_1Tv_1Btn[0] = new P1080_1920_UC_ChargingMain_Include_Notify_1Tv_1Btn();
            mUC_Notify_1Tv[0] = new P1080_1920_UC_ChargingMain_Include_Notify_1Tv();

            //mUC_Charging_Select_Member_Type = new P1080_1920_UC_ChargingMain_Include_Select_Member();
            mUC_Charging_Emergency = new P1080_1920_UC_ChargingMain_Include_Emergency();
            mUC_NonMember_Input_Payment_Amount = new P1080_1920_UC_ChargingMain_Include_Input_Payment_Amount(1);
            mUC_Charging_QRCode = new P1080_1920_UC_ChargingMain_Include_Qrcode();

            mUC_NonMember_Select_PaymnetType = new P1080_1920_UC_ChargingMain_Include_Select_PaymentType(1);

            mUC_Charging_Calculate_Charge = new P1080_1920_UC_ChargingMain_Include_Calculate_Charge(1);

        }
        //mUC_Setting = new UC_MainPage_Include_SettingMain();
        //mUC_Cert_ManualControl_Output = new UC_MainPage_Include_Cert_ManualControl_Output();

        //////////////////////////////////////
        //mForm_Setting = new Form_Setting_Main();


        //mUC_SelectMember = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member();
        //mUC_Charging_QRCode = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Qrcode();
        //mUC_Use_Complete = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Use_Complete();
        //mUC_Charging_CardTag = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Search_Card();

        //mUC_Touch_Screen = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Touch_Screen();

        //mUC_Charging_Charging = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging(1);
        //mUC_Charging_Charging_Complete = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Charging_Complete(1);
        //mUC_Charging_Disconnect_Connector = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Disconnect_Connector();
        //mUC_Charging_Preparing_Charging = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Preparing_Charging();
        //mUC_Charging_Process_Certification = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Process_Certification();
        //mUC_Charging_Certification_Complete = new Cert_P1024_600_BCC_UC_ChargingMain_Include_User_Certification_Complete();
        //mUC_Charging_Connect_Connector = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Wait_Connect_Connector();

        //mUC_Charging_Emergency = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Emergency();
        //mUC_Use_Error_Before_Charging = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Error_Before_Charging();


        //mUC_Notify_1Tv_System = new Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_System();
        //mUC_Notify_1Tv_1Btn = new Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv_1Btn();
        //mUC_Notify_1Tv = new Cert_P1024_600_BCC_UC_ChargingMain_Notify_1Tv();

        //mUC_Charging_Select_Member_Type = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_Member();

        //mUC_NonMember_Input_Payment_Amount = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Input_Payment_Amount(1);


        //mUC_NonMember_Select_PaymnetType = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Select_PaymentType(1);

        //mUC_Charging_Calculate_Charge = new Cert_P1024_600_BCC_UC_ChargingMain_Include_Calculate_Charge(1);            
    }







}

