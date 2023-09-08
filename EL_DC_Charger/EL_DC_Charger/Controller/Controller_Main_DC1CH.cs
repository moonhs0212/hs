using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.Controller;
using EL_DC_Charger.common.chargingcontroller;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using EL_DC_Charger.Interface_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common.interf;

namespace EL_DC_Charger.EL_DC_Charger.Controller
{
    public class Controller_Main_DC1CH : ChargingController_Base, IRFCardReader_EventListener
    {
        public Controller_Main_DC1CH(EL_DC_Charger_MyApplication application) : base(application, 0)
        {
            setTime_NextTime(60);
            mChannelIndex = 1;
        }
        EL_DC_Charger_MyApplication getMyApplication()
        {
            return (EL_DC_Charger_MyApplication)mApplication;
        }

        public override void process()
        {

            ///////////////////////////////////////////////////////////////
            switch (mMode)
            {
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_BOOT_ON:
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Select_Member();
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Main();
                    setMode(CMODE_MAIN.MODE_BOOT_ON + 1);
                    break;
                case CMODE_MAIN.MODE_BOOT_ON + 1:
                    //setMode(CMODE_MAIN.MODE_FIRST_SETTING);
                    setMode(CMODE_MAIN.MODE_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_FIRST_SETTING:
                    setMode(CMODE_MAIN.MODE_FIRST_SETTING + 1);
                    break;
                case CMODE_MAIN.MODE_FIRST_SETTING + 1:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN + 1:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MAIN_TEST + 1:
                    setMode(CMODE_MAIN.MODE_PREPARE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_AUTO:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_AUTO + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_AUTO + 1:
                    setMode(CMODE_MAIN.MODE_PREPARE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MANUAL:
                    setMode(CMODE_MAIN.MODE_SELF_DIAGNOSIS_MANUAL + 1);
                    break;
                case CMODE_MAIN.MODE_SELF_DIAGNOSIS_MANUAL + 1:
                    setMode(CMODE_MAIN.MODE_PREPARE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_PREPARE:
                    setMode(CMODE_MAIN.MODE_PREPARE + 1);
                    break;
                case CMODE_MAIN.MODE_PREPARE + 1:
                    setMode(CMODE_MAIN.MODE_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_MAIN:
                    initChargingVariable();
                    setChargingStop();
                    getMyApplication().SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Select_Member();
                    if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                        getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(0);
                    setMode(CMODE_MAIN.MODE_MAIN + 1);
                    break;
                case CMODE_MAIN.MODE_MAIN + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_UsingStart)
                        setMode(CMODE_MAIN.MODE_CERTIFICATION_WAIT);
                    break;



                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CERTIFICATION_WAIT:
                    bIsReceive_CardNumber = false;
                    bIsReceive_RFCard_Failed = false;
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_CardTag();
                    getMyApplication().RFCardReader_Manager.setCommand_Search_RFCard();
                    getMyApplication().RFCardReader_Manager.setRFCardReader_Listener(this);
                    if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                        getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(1);
                    setMode(CMODE_MAIN.MODE_CERTIFICATION_WAIT + 1);
                    break;
                case CMODE_MAIN.MODE_CERTIFICATION_WAIT + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_Back || isTimer(TIMER_NEXTSTEP))
                        setMode(CMODE_MAIN.MODE_MAIN);
                    else if (bIsReceive_CardNumber == true)
                        setMode(CMODE_MAIN.MODE_CERTIFICATION_PROCESSING);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CERTIFICATION_PROCESSING:
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Process_Certification();
                    getMyApplication().RFCardReader_Manager.setCommand_Ready();
                    getMyApplication().RFCardReader_Manager.setRFCardReader_Listener(null);
                    setMode(CMODE_MAIN.MODE_CERTIFICATION_PROCESSING + 1);
                    break;
                case CMODE_MAIN.MODE_CERTIFICATION_PROCESSING + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_Back)
                        setMode(CMODE_MAIN.MODE_MAIN);
                    else if (isTimer(TIMER_3S))
                        setMode(CMODE_MAIN.MODE_CERTIFICATION_COMPLETE);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CERTIFICATION_COMPLETE:
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Certification_Complete();
                    setMode(CMODE_MAIN.MODE_CERTIFICATION_COMPLETE + 1);
                    break;
                case CMODE_MAIN.MODE_CERTIFICATION_COMPLETE + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_Back || isTimer(TIMER_NEXTSTEP))
                        setMode(CMODE_MAIN.MODE_MAIN);
                    else if (isTimer(TIMER_3S))
                        setMode(CMODE_MAIN.MODE_CONNECTING_CONNECTOR_WAIT);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CONNECTING_CONNECTOR_WAIT:
                    if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                        getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(2);
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Connect_Connector();
                    setMode(CMODE_MAIN.MODE_CONNECTING_CONNECTOR_WAIT + 1);
                    break;
                case CMODE_MAIN.MODE_CONNECTING_CONNECTOR_WAIT + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_Back || isTimer(TIMER_NEXTSTEP))
                        setMode(CMODE_MAIN.MODE_MAIN);
                    else if (
                            getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                        )
                        setMode(CMODE_MAIN.MODE_CONNECTING_CAR_PROCESSING);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CONNECTING_CAR_PROCESSING:
                    ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager())
                        .packet_z1.mCommand_Output_Channel1 = 1;
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Preparing_Charging();
                    setMode(CMODE_MAIN.MODE_CONNECTING_CAR_PROCESSING + 1);
                    break;
                case CMODE_MAIN.MODE_CONNECTING_CAR_PROCESSING + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_Back)
                        setMode(CMODE_MAIN.MODE_MAIN);
                    else if (isTimer(TIMER_NEXTSTEP))
                    {
                        mErrorReson = "충전 준비 시간 초과";
                        setMode(CMODE_MAIN.MODE_ERROR_BEFORE_CHARGING);
                    }
                    else if (
                            getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().isState_ChargingCar()
                        )
                    {
                        setMode(CMODE_MAIN.MODE_CHARGING);
                    }
                    else if (
                            (
                                ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState == 102
                                ||
                                ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mErrorCode > 0
                                )
                        )
                    {
                        if (getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().getErrorCode() > 0)
                            mErrorReson = "오류발생 (" + getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().getErrorCode() + ")";
                        else
                            mErrorReson = "정상종료";
                        setMode(CMODE_MAIN.MODE_ERROR_BEFORE_CHARGING);
                    }

                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_ERROR_BEFORE_CHARGING:
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Error_Before_Charging(mErrorReson);
                    bIsClick_Confirm_ErrorReson_BeforeCharging = false;
                    setChargingStop();
                    setMode(CMODE_MAIN.MODE_ERROR_BEFORE_CHARGING + 1);
                    break;

                case CMODE_MAIN.MODE_ERROR_BEFORE_CHARGING + 1:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_Confirm_ErrorReson_BeforeCharging || isTimer(TIMER_NEXTSTEP))
                        setMode(CMODE_MAIN.MODE_MAIN);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CHARGING:
                    if (!bIsCharging_MoreOnce)
                    {
                        bIsCharging_MoreOnce = true;
                    }
                    bIsChargingStart_Current = true;
                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).mChargingTime.onChargingStart();
                    if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                        getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(3);
                    bIsClick_ChargingStop = false;
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Charging();
                    setMode(CMODE_MAIN.MODE_CHARGING + 1);
                    break;
                case CMODE_MAIN.MODE_CHARGING + 1:
                    if (!bIsChargingStart_By_ChargingWattage)
                    {
                        //bIsChargingStart_By_ChargingWattage = (mApplication).getChannelTotalInfor(mChannelIndex).mChargingWattage.setChargingStart();
                    }

                    if (isTimer(TIMER_1S))
                    {
                        getMyApplication().getDataManager_CustomUC_Main().mUC_Charging_Charging[mChannelIndex - 1].updateView();
                    }
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_ChargingStop
                        ||
                        (
                            getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            (
                                ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState != 100
                                ||
                                ((EL_ControlbdComm_PacketManager)getMyApplication().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mErrorCode > 0
                                )
                        )
                    )
                    {
                        if (bIsClick_ChargingStop)
                            mErrorReson = "충전정지 버튼 누름";
                        else if (getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            !getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().isState_ChargingCar())
                        {
                            mErrorReson = "오류 발생 (" + getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().getErrorCode_String() + ")";
                        }


                        ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager())
                        .packet_z1.mCommand_Output_Channel1 = 0;

                        setMode(CMODE_MAIN.MODE_CHARGING_COMPLETE);
                    }

                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_CHARGING_COMPLETE:
                    ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager())
                        .packet_z1.mCommand_Output_Channel1 = 0;
                    if (mTime_Mode.getSecond_WastedTime() >= 2)
                        setMode(CMODE_MAIN.MODE_CHARGING_COMPLETE + 1);

                    break;

                case CMODE_MAIN.MODE_CHARGING_COMPLETE + 1:

                    if (bIsChargingStart_Current)
                    {
                        mApplication.getChannelTotalInfor(mChannelIndex).mSoc_Finish = ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager())
                        .packet_1z.mSOC;
                        bIsChargingStart_Current = false;
                        setChargingStop();
                        getMyApplication().getDataManager_CustomUC_Main().mUC_Charging_Charging_Complete[0].updateView();
                    }
                    if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                        getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(4);
                    bIsClick_ChargingComplete = false;
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Charging_Complete(mErrorReson);
                    setMode(CMODE_MAIN.MODE_CHARGING_COMPLETE + 2);
                    break;
                case CMODE_MAIN.MODE_CHARGING_COMPLETE + 2:
                    if (process_EmergencyButton())
                    {

                    }
                    else if (bIsClick_ChargingComplete)
                        setMode(CMODE_MAIN.MODE_DISCONNECT_CONNECTOR_WAIT);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_DISCONNECT_CONNECTOR_WAIT:
                    if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                        getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(5);
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Disconnect_Connector();
                    setMode(CMODE_MAIN.MODE_DISCONNECT_CONNECTOR_WAIT + 1);
                    break;
                case CMODE_MAIN.MODE_DISCONNECT_CONNECTOR_WAIT + 1:
                    if (
                        bIsClick_Back
                        ||
                        (
                            getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State() != null &&
                            !getMyApplication().getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                        )
                        ||
                        isTimer(TIMER_NEXTSTEP)
                    )
                        setMode(CMODE_MAIN.MODE_USING_COMPLETE);
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_USING_COMPLETE:
                    if (bIsCharging_MoreOnce)
                    {
                        if (getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager != null)
                            getMyApplication().getDataManager_CustomUC_Main().BottomBar_Step_Manager.setBottomBar_Step(4);
                    }


                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_UseComplete();
                    setMode(CMODE_MAIN.MODE_USING_COMPLETE + 1);
                    break;
                case CMODE_MAIN.MODE_USING_COMPLETE + 1:
                    if (isTimer(TIMER_3S))
                        setMode(CMODE_MAIN.MODE_MAIN);
                    break;
                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_EMERGENCY:
                    getMyApplication().getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Emergency();
                    setMode(CMODE_MAIN.MODE_EMERGENCY + 1);
                    break;
                case CMODE_MAIN.MODE_EMERGENCY + 1:
                    if (!mApplication.DI_Manager.isEmergencyPushed())
                    {
                        if (bIsCharging_MoreOnce)
                            setMode(CMODE_MAIN.MODE_CHARGING_COMPLETE);
                        else
                        {
                            mErrorReson = "비상정지";
                            setMode(CMODE_MAIN.MODE_ERROR_BEFORE_CHARGING);
                        }

                    }

                    break;



                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_RESTART_PROGRAM:
                    setMode(CMODE_MAIN.MODE_RESTART_PROGRAM + 1);
                    break;
                case CMODE_MAIN.MODE_RESTART_PROGRAM + 1:
                    EL_Manager_Application.restartApplication();
                    break;

                ///////////////////////////////////////////////////////////////
                case CMODE_MAIN.MODE_RESTART_SYSTEM:
                    setMode(CMODE_MAIN.MODE_RESTART_SYSTEM + 1);
                    break;
                case CMODE_MAIN.MODE_RESTART_SYSTEM + 1:
                    EL_Manager_Application.finishApplication();
                    EL_Manager_Application.restartSystem();
                    break;


            }
        }



        protected void setChargingStop()
        {
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager())
                        .packet_z1.mCommand_Output_Channel1 = 0;

            bIsChargingStart_Current = false;
            //        MainActivity.bNeed_UpdateUI_ChargingStart = false;
            //        MainActivity.bNeed_UpdateUI_ChargingStop = true;
            ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).mChargingTime.onChargingStop();
            if (bIsChargingStart_By_ChargingWattage)
            {
                if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).mChargingWattage != null)
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).mChargingWattage.setChargignStop();

                bIsChargingStart_By_ChargingWattage = false;
            }
        }


        public void onReceive(string rfCardNumber)
        {
            bIsReceive_CardNumber = true;
        }

        public void onReceiveFailed(string result)
        {
            bIsReceive_CardNumber = false;
        }

        protected override bool process_EmergencyButton()
        {
            if (mApplication.DI_Manager != null
                && mApplication.DI_Manager.isEmergencyPushed())
            {
                setMode(CMODE_MAIN.MODE_EMERGENCY);
                return true;
            }

            return false;
        }
    }
}
