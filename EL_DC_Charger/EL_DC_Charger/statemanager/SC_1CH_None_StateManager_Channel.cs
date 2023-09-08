using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.statemanager
{
    public class SC_1CH_None_StateManager_Channel : EL_StateManager_Channel_Base
        , IRFCardReader_EventListener, IOnClickListener_Button
    {
        public SC_1CH_None_StateManager_Channel(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override void initVariable()
        {
            
        }

        public override void intervalExcuteAsync()
        {
            switch (mState)
            {
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING:
                    bIsComplete_PrepareChannel = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 1:
                    if (bIsPrepareComplete_StateManager_Main)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 2);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;
            }
        }

        public override bool isReservation()
        {
            return false;
        }

        public void onClick_Cancel(object sender)
        {
            
        }

        public void onClick_Confirm(object sender)
        {
            
        }

        public void onReceive(string rfCardNumber)
        {
            
        }

        public void onReceiveFailed(string result)
        {
            throw new NotImplementedException();
        }
    }
}
