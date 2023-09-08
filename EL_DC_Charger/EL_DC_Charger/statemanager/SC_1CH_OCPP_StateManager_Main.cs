using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.ocpp.ver16.packet.csms2cp;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using EL_DC_Charger.ocpp.ver16.statemanager;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.statemanager
{
    public class SC_1CH_OCPP_StateManager_Main : EL_StateManager_OCPP_Main
    {

        public OCPP_EL_Manager_Table_TransactionInfor mTable_TransactionInfor = null;

        public SC_1CH_OCPP_StateManager_Main(EL_MyApplication_Base application)
            : base(application)
        {
            mTable_TransactionInfor = mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor();
            setState(CONST_MAINSTATE.INIT);
        }



        override public void setState(int state)
        {
            initVariable_By_ChangeMode();
            base.setState(state);
        }


        override public void intervalExcuteAsync()
        {
            if (!isNeedExcute())
            {
                return;
            }


            switch (mState)
            {
                case CONST_MAINSTATE.INIT:
                    bIsNormalState = false;
                    //mOCPP_Comm_SendMgr.getComm_Manager().openComm();
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                        "서버 접속중입니다....", false
                        );
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("서버 접속중입니다....");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                    setState(CONST_MAINSTATE.INIT + 1);
                    break;

                case CONST_MAINSTATE.INIT + 1:
                    int length = mApplication.getChannelTotalInfor().Length;
                    int count = 0;
                    for (int i = 0; i < length; i++)
                    {
                        if (mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel().bIsComplete_PrepareChannel)
                            count++;
                    }
                    if (length == count)
                        setState(CONST_MAINSTATE.INIT + 2);
                    break;
                case CONST_MAINSTATE.INIT + 2:
                    setState(CONST_MAINSTATE.BOOT_NOTIFICATION);
                    break;

                case CONST_MAINSTATE.BOOT_NOTIFICATION:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                    "충전 중 일 경우 충전이 재개됩니다. \n\n재접속 시도중입니다.(" + mOCPP_Count_Send_BootNotification + ")", false, mChannelIndex
                );
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("재접속 시도중입니다.(" + mOCPP_Count_Send_BootNotification + ")");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                    mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_BootNotification_Directly();
                    setState(CONST_MAINSTATE.BOOT_NOTIFICATION + 1);
                    break;
                case CONST_MAINSTATE.BOOT_NOTIFICATION + 1:
                    if (isNeedReset())
                        setState(CONST_MAINSTATE.REBOOT);
                    else if (bOCPP_IsReceivePacket_CallResult_BootNotification)
                    {
                        if (mOCPP_CSMS_Conf_BootNotification.status == RegistrationStatus.Accepted)
                        {
                            if (EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.GetType() == typeof(EL_Charger_1CH_OCPP_StateManager_Channel_Wev))
                            {
                                ((EL_Charger_1CH_OCPP_StateManager_Channel_Wev)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel)
                                        .mSendManager_OCPP_Wev.sendOCPP_CP_Req_CTP1_Req(OperatorType.ER);
                                ((EL_Charger_1CH_OCPP_StateManager_Channel_Wev)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel)
                                        .mSendManager_OCPP_Wev.sendOCPP_CP_Req_CTP1_Req(OperatorType.NM);
                            }

                            initVariable_After_Receive_BootNotification();
                            setState(CONST_MAINSTATE.MAIN);
                        }
                        else if (mOCPP_CSMS_Conf_BootNotification.status == RegistrationStatus.Pending)
                        {
                            mTimers[TIMER_SEND_TO_SERVER].setTime();
                            bIsPendingState = true;
                            setState(CONST_MAINSTATE.BOOT_NOTIFICATION + 10);
                            bOCPP_IsReceivePacket_CallResult_BootNotification = false;
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                                "서버에서 데이터 설정중입니다.", false
                                );
                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("서버에서 데이터 설정중입니다.");
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                            mOCPP_Count_Send_BootNotification = 1;
                            //                        setState(CONST_MAINSTATE.BOOT_NOTIFICATION + 10);
                        }
                        else
                        {
                            mTimers[TIMER_SEND_TO_SERVER].setTime();
                            bIsPendingState = true;
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                                "재접속 시도중입니다.(" + mOCPP_Count_Send_BootNotification + ")", false);
                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("재접속 시도중입니다.(" + mOCPP_Count_Send_BootNotification + ")");
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                            mOCPP_Count_Send_BootNotification++;
                            bOCPP_IsReceivePacket_CallResult_BootNotification = false;
                        }
                    }
                    else if (isTimer_Sec(TIMER_SEND_TO_SERVER, getOCPP_Interval_Send_BootNotification()))
                    {
                        mOCPP_Count_Send_BootNotification++;
                        if (mOCPP_Count_Send_BootNotification > 10)
                        {
                            excuteResetNormal();
                        }
                        else
                            setState(CONST_MAINSTATE.BOOT_NOTIFICATION);
                    }
                    else if (isTimer_Sec(TIMER_120SEC, 1200))
                    {
                        excuteResetNormal();
                    }
                    break;
                //                asdf
                //Pending
                case CONST_MAINSTATE.BOOT_NOTIFICATION + 10:
                    setState(CONST_MAINSTATE.BOOT_NOTIFICATION + 11);
                    mOCPP_Comm_SendMgr.setOCPP_ConfChangeConfiguration_Listener(new Conf_ChangeConfiguration_Listener(this));
                    break;
                case CONST_MAINSTATE.BOOT_NOTIFICATION + 11:
                    if (mTime_SetState.getSecond_WastedTime() > 2)
                    {
                        if (isNeedReset())
                            setState(CONST_MAINSTATE.REBOOT);
                    }
                    //안해도 됨
                    //                if(isTimer_Sec(TIMER_2SEC, 2))
                    //                {
                    //                    if(bIsNeedReset_Charger_Hard)
                    //                        setState(CONST_MAINSTATE.REBOOT);
                    //                    else if(bIsNeedReset_Charger_Soft)
                    //                    {
                    //                        int countReady_Reboot = 0;
                    //                        for(int i = 0; i < mApplication.getChannelInfor_ChargerTotal().length; i++)
                    //                        {
                    //                            if(mApplication.getChannelInfor_ChargerTotal(i+1).getStateManager_Channel().bIsReady_Reboot)
                    //                            {
                    //                                countReady_Reboot ++;
                    //                            }
                    //                        }
                    //                        if(countReady_Reboot == mApplication.getChannelInfor_ChargerTotal().length)
                    //                            setState(CONST_MAINSTATE.REBOOT);
                    //                    }else if(!bIsUseEnable)
                    //                    {
                    //                        setState(CONST_MAINSTATE.STATE_DISABLE);
                    //                    }
                    //                }
                    break;


                case CONST_MAINSTATE.MAIN:

                    bIsPendingState = false;
                    mTime_Send_HeartBeat.setTime();
                    mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_Heartbeat_Directly();

                    for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                    {
                        mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel().bIsPrepareComplete_StateManager_Main = true;
                    }
                    mOCPP_Count_Send_BootNotification = 1;
                    //                EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv.setTv_Content_1("정보 교환중입니다.\r\n잠시만 기다려 주세요.");
                    //                MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1 = true;
                    if (bIsUseEnable)
                        mSendManager_StatusNotification_Main.setOCPP_ChargePointStatus(ChargePointStatus.Available);
                    setState(CONST_MAINSTATE.MAIN + 1);

                    bIsNormalState = true;
                    break;
                case CONST_MAINSTATE.MAIN + 1:
                    if (mTime_SetState.getSecond_WastedTime() > 2)
                    {
                        if (isNeedReset())
                        {
                            setState(CONST_MAINSTATE.REBOOT);
                        }
                        else if (!bIsUseEnable)
                        {
                            bIsNormalState = false;
                            setState(CONST_MAINSTATE.STATE_DISABLE);
                        }
                    }


                    break;
                //////////////////////////////////


                //////////////////////////////////



                case CONST_MAINSTATE.STATE_DISABLE:
                    mSendManager_StatusNotification_Main.setOCPP_ChargePointStatus(ChargePointStatus.Unavailable);
                    bIsNormalState = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                        "사용이 불가능합니다.\r\n관리자에게 문의해 주세요.", false);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("사용이 불가능합니다.\r\n관리자에게 문의해 주세요.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                    setState(CONST_MAINSTATE.STATE_DISABLE + 1);
                    break;
                case CONST_MAINSTATE.STATE_DISABLE + 1:
                    if (mTime_SetState.getSecond_WastedTime() > 2)
                    {
                        if (isNeedReset())
                            setState(CONST_MAINSTATE.REBOOT);
                        else if (bIsUseEnable && isTimer_Sec(TIMER_3SEC, 3))
                        {
                            setState(CONST_MAINSTATE.MAIN);
                        }
                    }
                    break;


                case CONST_MAINSTATE.REBOOT:
                    bIsNormalState = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                        "재부팅이 요청되었습니다..\r\n잠시 후 재부팅 합니다.", false);

                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("재부팅이 요청되었습니다..\r\n잠시 후 재부팅 합니다.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                    setState(CONST_MAINSTATE.REBOOT + 1);

                    mApplication.getChannelTotalInfor(1).
                                    getStateManager_Channel().bIsReady_Reboot = true;

                    for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                    {
                        if (bIsNeedReset_Charger_Hard)
                        {
                            mApplication.getChannelTotalInfor(i + 1).
                                    getStateManager_Channel().bIsNeedReset_Charger_Hard = true;

                            mTable_TransactionInfor.db_ChargingStopReason(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mTransactionInfor_DBId.ToString(), Reason.HardReset.ToString());

                        }

                        if (bIsNeedReset_Charger_Soft)
                        {
                            mTable_TransactionInfor.db_ChargingStopReason(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mTransactionInfor_DBId.ToString(), Reason.SoftReset.ToString());
                            mApplication.getChannelTotalInfor(i + 1).
                                    getStateManager_Channel().bIsNeedReset_Charger_Soft = true;

                        }
                    }

                    break;

                case CONST_MAINSTATE.REBOOT + 1:
                    if (mTime_SetState.getSecond_WastedTime() >= 600)
                    {
                        EL_Manager_Application.restartSystem();
                    }
                    else if (mTime_SetState.getSecond_WastedTime() >= 7)
                    {
                        for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                        {
                            if (!(mApplication.getChannelTotalInfor(i + 1).
                                    getStateManager_Channel()).bIsReady_Reboot)
                            {
                                return;
                            }
                        }
                        if (bIsNeedReset_Charger_Hard)
                        {
                            //EL_Manager_Application.restartApplication();
                            EL_Manager_Application.restartSystem();
                        }
                        else if (bIsNeedReset_Charger_Soft)
                        {
                            EL_Manager_Application.restartApplication();
                            //EL_Manager_Application.restartSystem(mApplication.getActivity_Main(), MainActivity.class);
                        }
                        else
                        {
                            EL_Manager_Application.restartApplication();
                        }
                    }
                    break;
            }
        }


        protected class Conf_ChangeConfiguration_Listener : IOCPP_ConfChangeConfiguration_Listener
        {
            protected SC_1CH_OCPP_StateManager_Main mOCPP_StateManager_Main = null;
            public Conf_ChangeConfiguration_Listener(SC_1CH_OCPP_StateManager_Main stateManager_Main)
            {
                mOCPP_StateManager_Main = stateManager_Main;
            }

            public void receive_ConfChangeConfiguration(Req_ChangeConfiguration data, Conf_ChangeConfiguration data_Result)
            {
                if (mOCPP_StateManager_Main.mState == CONST_MAINSTATE.BOOT_NOTIFICATION + 11)
                {
                    mOCPP_StateManager_Main.bOCPP_IsReceivePacket_CallResult_BootNotification = false;
                    mOCPP_StateManager_Main.setState(CONST_MAINSTATE.BOOT_NOTIFICATION);
                    mOCPP_StateManager_Main.mOCPP_Comm_SendMgr.setOCPP_ConfChangeConfiguration_Listener(null);
                }
            }
        }
    }
}
