using EL_DC_Charger.common.application;
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
    public class SC_1CH_None_StateManager_Main : EL_StateManager_Main_Base
    {
        public SC_1CH_None_StateManager_Main(EL_MyApplication_Base application) : base(application)
        {
        }

        public override void initVariable()
        {

        }

        public override void intervalExcuteAsync()
        {
            if (!isNeedExcute())
            {
                return;
            }

            switch (mState)
            {
                case CONST_MAINSTATE.INIT:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                        "충전기 타입 설정이 필요합니다.\n설정을 확인 해 주세요.", false
                        );
                    setState(CONST_MAINSTATE.MAIN);
                    break;
                case CONST_MAINSTATE.MAIN:

                    bIsPendingState = false;

                    for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                    {
                        mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel().bIsPrepareComplete_StateManager_Main = true;
                    }
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
            }
        }
    }
}
