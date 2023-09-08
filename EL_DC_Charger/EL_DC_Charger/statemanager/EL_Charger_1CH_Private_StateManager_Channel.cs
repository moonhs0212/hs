using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.setting;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.statemanager
{
    public class EL_Charger_1CH_Private_StateManager_Channel : EL_StateManager_Channel_Base
        , IOnClickListener_Button
    {

        public const int DELAY_OFFLINE = 30;

        override public bool isReservation()
        {
            return false;
        }

        public EL_Charger_1CH_Private_StateManager_Channel(EL_DC_Charger_MyApplication application, int channelIndex)
            : base(application, channelIndex)
        {

            setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING);



        }


        protected bool bTemp_Skip_NotCompletePacketSend = false;
        protected bool bTemp_Skip_NotCompletePacketSend_Processing = false;

        override public async void intervalExcuteAsync()
        {
            //        ((EL_DC_Charger_MyApplication) mApplication).getChannelInfor_ChargerTotal(1).getControlbdComm_PacketManager().packet_z1.bMC_ON_Manual = true;

            if (!isNeedExcute())
            {
                if (!bIsEnter_NotExcute)
                {
                    bIsEnter_NotExcute = true;
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.bHMI_Manual_Control = false;
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.bPowerRelay_Minus = false;
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.bPowerRelay_Plus = false;
                }
                return;
            }

            switch (mState)
            {
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING:
                    //if (process_GetAmiValue_Booting())

                    if (EL_DC_Charger_MyApplication.getInstance().calibrationMode && !EL_DC_Charger_MyApplication.getInstance().bManualScreenDone)
                    {
                        EL_DC_Charger_MyApplication.getInstance().bManualScreenDone = true;
                        Form_Manual_Setting form_Manual_Setting = new Form_Manual_Setting();
                        form_Manual_Setting.Show();
                        form_Manual_Setting.enableUITimer(true);
                    }

                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 1:
                    bIsComplete_PrepareChannel = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 10);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 10:
                    if (isTimer_Sec(TIMER_3SEC, 3) && bIsPrepareComplete_StateManager_Main)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;


                case CONST_STATE_UIFLOWTEST.STATE_READY:
                    initVariable_UsingComplete();
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    bIsTest_Car = false;

                    Manager_SoundPlay.getInstance().stop();

                    bIsCertificationFailed = false;
                    bIsCertificationSuccess = false;
                    setState(CONST_STATE_UIFLOWTEST.STATE_READY);

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_Select_Touch_Screen();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_TouchScreen = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_READY + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_READY + 1:
                    if (bTemp_Skip_NotCompletePacketSend && !bTemp_Skip_NotCompletePacketSend_Processing)
                    {
                        bTemp_Skip_NotCompletePacketSend_Processing = true;
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    else if (!bIsUseEnable_Channel)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_DISABLE);
                    }
                    else if (mApplication.DI_Manager.isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }
                    else if (bIsCommand_UsingStart)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                        //                  safd
                    }



                    if (mState != CONST_STATE_UIFLOWTEST.STATE_READY + 1
                        && mState != CONST_STATE_UIFLOWTEST.STATE_PREFARING
                        && mState != CONST_STATE_UIFLOWTEST.STATE_RESET
                        && mState != CONST_STATE_UIFLOWTEST.STATE_DISABLE
                        && mState != CONST_STATE_UIFLOWTEST.STATE_EMERGENCY
                        )
                    {
                        initVariable_UsingStart();
                    }

                    // 1. 커넥터 연결이 되어있고                    
                    if ((((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_1z.getState_CommunicationCar() >= 2)
                        && mState == CONST_STATE_UIFLOWTEST.STATE_READY + 1)
                    {
                        // 2. 충전시작 시간이 있는데 충전종료시간이 없으면 비정상 종료라고 판단.
                        //mDbId = HistoryDBHelper.getIdx();
                        //if (HistoryDBHelper.ChargingEndYN(mDbId))
                        //{
                        //    bIsCommand_UsingStart = true;
                        //    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                        //}
                    }
                    //if (mState != (CONST_STATE_UIFLOWTEST.STATE_READY + 1))
                    //{
                    //    MainActivity.bNeed_UpdateUI_Main_VersionDisplay_Invisible = true;        
                    //}

                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR:
                    Manager_SoundPlay.getInstance().play(Manager_SoundPlay.noti_service_connect_connector);
                    mCardNumber_Member = "9999999999999999";
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Connect_Connector();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_ConnectConnectorChangeLayout = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1);
                    mDelay_ConnectionTimeOut = 90;
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1:
                    if (bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (
                                !bIsCommand_UsingStart
                                        || isTimer_Sec(TIMER_WAITTIME, mDelay_ConnectionTimeOut))
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                        else if (bIsCommand_ChargingStop)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                        else if ((((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isState_ConnectedCar()
                              && isTimer_Sec(TIMER_3SEC, 3))
                              || bIsTest_Car)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                        }
                        else if (mApplication.DI_Manager.isEmergencyPushed())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        }
                    }

                    if (mState == (CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800))
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().mUC_Charging_Connect_Connector[mChannelIndex - 1].updateView();
                        //MainActivity.bNeed_UpdateUI_Main_Update_ProgressBar_ConnectConnector = true;
                    }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE:
                    Manager_SoundPlay.getInstance().play(Manager_SoundPlay.noti_connected_connector);
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 1;
                    //initVariable_TransactionStart_Complete();
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Preparing_Charging();
                    //MainActivity.bNeed_UpdateUI_Main_Charging_Prepare = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1);
                    ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Charging_Preparing_Charging[mChannelIndex - 1].initVariable();
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1:

                    if (isTimer_Sec(TIMER_3SEC, 2))
                    {
                        if (
                                ((!bIsCommand_UsingStart)
                                        || isTimer_Sec(TIMER_WAITTIME, mDelay_Reset))
                                        && !isNeedReset()
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                        else if (bIsErrorOccured)
                        {
                            if (mErrorReason == null)
                                mErrorReason = "오류발생 (" + mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().getErrorCode() + ")";
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                        }
                        else if ((bIsClick_BackButton || bIsCommand_ChargingStop) && isTimer_Sec(TIMER_90SEC, 3))
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                        }
                        else if (mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                                        &&
                                        !mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                                && !bIsTest_Car)
                        {
                            bIsErrorOccured = true;
                            mErrorReason = "커넥터분리";
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                        }
                        else if (
                            mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ChargingCar()
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                        }
                        else if (mApplication.DI_Manager.isEmergencyPushed())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        }
                        else if (
                            (typeof(P1080_1920_UC_ChargingMain_Include_Preparing_Charging)
                                == ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Charging_Preparing_Charging[mChannelIndex - 1].GetType())
                                &&
                            ((P1080_1920_UC_ChargingMain_Include_Preparing_Charging)((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Charging_Preparing_Charging[mChannelIndex - 1]).getRemainTime() < 0)
                        {
                            mErrorMessage = "대기시간 초과";
                            setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                        }
                    }



                    if (isTimer_MiliSec(TIMER_800_UIUPDATE, 800)
                            && mState == (CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1))
                    {
                        //MainActivity.bNeed_UpdateUI_Main_Update_ProgressBar_PrepareCharging = true;
                        ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Charging_Preparing_Charging[mChannelIndex - 1].updateView();
                    }

                    //                if(mState != (CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1))
                    //                {
                    //                    MainActivity.bNeed_UpdateUI_Main_Update_ProgressBar_PrepareCharging_Stop = true;
                    ////                    ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main.mContentLayout_Prepare_Charging.stopAnimation();
                    //                }
                    break;
                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING:
                    initVariable_Pre_ChargingComplete();
                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1);
                    initVariable_ChargingComplete();

                    bIsClick_Notify_1Button = false;
                    mApplication.getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Error_Before_Charging("충전 준비 중 오류가 발생했습니다..\r\n오류 내용을 확인 후 확인버튼을 눌러 주세요.\n(" + mErrorMessage + ")");

                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                    //        .setTv_Content_1("충전 준비 중 오류가 발생했습니다..\r\n오류 내용을 확인 후 확인버튼을 눌러 주세요.\n(" + mErrorMessage + ")", this);
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1);
                    initVariable_UsingComplete();
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1:
                    if (isTimer_Sec(TIMER_WAITTIME, 10) || bIsClick_Notify_1Button)
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    break;


                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING:
                    Manager_SoundPlay.getInstance().play(Manager_SoundPlay.noti_start_charging);
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    //MainActivity.bNeed_UpdateUI_Main_Charging = true;
                    mApplication.getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Charging();

                    initVariable_ChargingStart();
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1:
                    if (!bIsChargingStart_By_ChargingWattage)
                    {
                        //bIsChargingStart_By_ChargingWattage = ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).mChargingWattage.setChargingStart();

                    }

                    if (processCondition_Charging())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                    }
                    else if (bIsClick_ChargingStop_User)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                        //                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 10);
                    }

                    if (mState == (CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800) && bIsChargingStart_By_ChargingWattage)
                    {
                        mApplication.getDataManager_CustomUC_Main().mUC_Charging_Charging[mChannelIndex - 1].updateView();
                        //MainActivity.bNeed_UpdateUI_Main_Update_ChargingInfor = true;
                    }
                    break;


                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE:
                    Manager_SoundPlay.getInstance().play(Manager_SoundPlay.noti_stop_charging);
                    bIsSelected_Confirm_ChargingComplete = false;
                    initVariable_ChargingComplete();
                    mApplication.getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Charging_Complete(mErrorReason);
                    //MainActivity.bNeed_UpdateUI_Main_Update_ChargingInfor = true;
                    //MainActivity.bNeed_UpdateUI_Main_Charging_Complete = true;
                    if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    else if (mApplication.DI_Manager.isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }
                    else
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1);
                    }

                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1:
                    if (mTime_SetState.getSecond_WastedTime() >= 2)
                    {
                        if (
                            (!bIsCommand_UsingStart) || bIsSelected_Confirm_ChargingComplete
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                        }
                        else if (mApplication.DI_Manager.isEmergencyPushed())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        }
                        else if (isTimer_Sec(TIMER_60SEC, 30) &&
                                (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isState_ConnectedCar()))
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                        }
                        else if (isTimer_Sec(TIMER_WAITTIME, 120) &&
                                (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isState_ConnectedCar()))
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                        }

                    }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR:
                    Manager_SoundPlay.getInstance().play(Manager_SoundPlay.noti_disconnect_connector);
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    mApplication.getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Disconnect_Connector();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_SeperateConnector = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (
                                (!bIsCommand_UsingStart ||
                                        (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isState_ConnectedCar()
                                                &&
                                                !bIsTest_Car)
                                        ||
                                        bIsTest_Car)
                                        || bIsClick_BackButton
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE);
                        }
                    }
                    if (mState == (CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800))
                    {
                        mApplication.getDataManager_CustomUC_Main().mUC_Charging_Disconnect_Connector[mChannelIndex - 1].updateView();
                        //MainActivity.bNeed_UpdateUI_Main_Update_ProgressBar_DisconnectConnector = true;
                    }
                    //                else if(((EL_DC_Charger_MyApplication)mApplication).getChannelInfor_ChargerTotal(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    //                {
                    //                    setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    //                }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE:
                    Manager_SoundPlay.getInstance().play(Manager_SoundPlay.noti_use_complete);
                    mApplication.getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_UseComplete();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_UseComplete = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE + 1:
                    if ((isTimer_Sec(TIMER_90SEC, 5)))
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY:
                    mApplication.getDataManager_CustomUC_Main().setView_MainPanel_Include_ChargingMain_Emergency();
                    //MainActivity.bNeed_UpdateUI_Main_EMERGENCY = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1:
                    if (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        //MainActivity.bNeed_UpdateUI_Main_EMERGENCY = false;
                        //MainActivity.bNeed_UpdateUI_Main_EMERGENCY_REALEASE = true;
                        if (bIsCharging_MoreOnce)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                        }
                        else
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }

                    break;

                case CONST_STATE_UIFLOWTEST.STATE_DISABLE:

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                "사용이 비활성화 되었습니다.\n\n관리자에게 문의해 주세요.", false);
                    //EL_DC_Charger_MyApplication.getInstance().get mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("사용이 비활성화 되었습니다.\n\n관리자에게 문의해 주세요.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_DISABLE + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_DISABLE + 1:
                    if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    else if (bIsUseEnable_Channel && isTimer_Sec(TIMER_3SEC, 3))
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_RESET:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                "재부팅 대기중입니다..\n\n관리자에게 문의해 주세요.", false);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("재부팅 대기중입니다..\n\n관리자에게 문의해 주세요.");

                    setState(CONST_STATE_UIFLOWTEST.STATE_RESET + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_RESET + 1:
                    if ((isTimer_Sec(TIMER_WAITTIME, 2)))
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET + 2);
                        bIsReady_Reboot = true;
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_RESET + 2:
                    if (mTime_SetState.getSecond_WastedTime() > 2)
                    {
                        if (!isNeedReset())
                        {
                            bIsReady_Reboot = false;
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                    }

                    break;
            }
        }








        override public void initVariable()
        {

        }

        public void onClick_Confirm(object sender)
        {
            bIsClick_Notify_1Button = true;
        }

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
