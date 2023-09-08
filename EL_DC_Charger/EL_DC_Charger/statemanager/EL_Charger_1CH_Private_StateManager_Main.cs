using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.statemanager
{
    public class EL_Charger_1CH_Private_StateManager_Main : EL_StateManager_Main_Base
    {



        public EL_Charger_1CH_Private_StateManager_Main(EL_MyApplication_Base application) : base(application)
        {


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
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                "준비중입니다.\n\n 잠시만 기다려 주세요....", false);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("준비중입니다.\n\n 잠시만 기다려 주세요....");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                    setState(CONST_MAINSTATE.INIT + 1);
                    break;

                case CONST_MAINSTATE.INIT + 1:
                    int length = mApplication.getChannelTotalInfor().Length;
                    int count = 0;
                    for (int i = 0; i < length; i++)
                    {
                        if (EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getStateManager_Channel() != null
                                && EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getStateManager_Channel().bIsComplete_PrepareChannel)
                            count++;
                    }
                    if (length == count)
                        setState(CONST_MAINSTATE.INIT + 2);
                    break;
                case CONST_MAINSTATE.INIT + 2:
                    setState(CONST_MAINSTATE.MAIN);
                    break;


                case CONST_MAINSTATE.MAIN:
                    bIsNormalState = true;
                    bIsUseEnable = true;

                    for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                    {
                        mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel().bIsPrepareComplete_StateManager_Main = true;
                    }
                    //                EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv.setTv_Content_1("정보 교환중입니다.\r\n잠시만 기다려 주세요.");
                    //                MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1 = true;
                    setState(CONST_MAINSTATE.MAIN + 1);
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
                    bIsNormalState = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                "사용이 불가능합니다.\r\n관리자에게 문의해 주세요.", false);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System
                    //    .setTv_Content_1("사용이 불가능합니다.\r\n관리자에게 문의해 주세요.");
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
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_System.setTv_Content_1("재부팅이 요청되었습니다..\r\n잠시 후 재부팅 합니다.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_System = true;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                "재부팅이 요청되었습니다..\r\n잠시 후 재부팅 합니다.", false);
                    setState(CONST_MAINSTATE.REBOOT + 1);

                    for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                    {
                        if (bIsNeedReset_Charger_Hard)
                        {
                            mApplication.getChannelTotalInfor(i + 1).
                                    getStateManager_Channel().bIsNeedReset_Charger_Hard = true;
                        }

                        if (bIsNeedReset_Charger_Soft)
                        {
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
                    else if (mTime_SetState.getSecond_WastedTime() >= 15)
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
                            EL_Manager_Application.restartSystem();
                        }
                        else if (bIsNeedReset_Charger_Soft)
                        {
                            EL_Manager_Application.restartSystem();
                        }
                        else
                        {
                            EL_Manager_Application.restartSystem();
                        }
                    }
                    break;
            }
        }



        override public void initVariable()
        {

        }
    }
}