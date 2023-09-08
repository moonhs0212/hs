using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.platform.wev.packet;
using EL_DC_Charger.ocpp.ver16.statemanager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.statemanager
{
    abstract public class Wev_StateManager_OCPP_Channel : EL_StateManager_OCPP_Channel
    {
        protected Wev_StateManager_OCPP_Channel(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
        }


        
    protected bool processCondition_AfterUsing_BeforeCharging(bool isUseBackButtonClick, bool isUseHomeButtonClick, int waitTime)
        {
            bool isChange = false;

            if (!bIsCommand_UsingStart)
            {
                moveToError(mIsCommand_UsingStart_Off_String,
                        Reason.Other, ChargePointStatus.Faulted);
                isChange = true;
            }
            else if (waitTime > 0 && isTimer_Sec(TIMER_WAITTIME, waitTime))
            {
                moveToError(Const_ErrorCode.CODE_0018_OVERWAITTIME,
                        Reason.Other, ChargePointStatus.Faulted);
                isChange = true;
            }
            else if (isUseHomeButtonClick && bIsClick_HomeButton)
            {
                moveToError(Const_ErrorCode.CODE_0016_CLICK_BACKBUTTON,
                        Reason.Other, ChargePointStatus.Faulted);
                isChange = true;
            }
            else if (isUseBackButtonClick && bIsClick_BackButton)
            {
                moveToError(Const_ErrorCode.CODE_0016_CLICK_BACKBUTTON,
                        Reason.Other, ChargePointStatus.Faulted);
                isChange = true;
            }
            else if (bIsCommand_ChargingStop)
            {
                moveToError(mIsCommand_ChargingStop_String,
                        Reason.Other, ChargePointStatus.Faulted);
            }
            else if (mApplication.DI_Manager.isEmergencyPushed())
            {
                moveToError(Const_ErrorCode.CODE_0116_EMERGENCY, Reason.EmergencyStop, ChargePointStatus.Faulted);
            }
            else if (bIsErrorOccured)
            {
                moveToError(
                        mErrorMessage,
                        Reason.Other,
                        ChargePointStatus.Finishing);
            }



            return isChange;
        }

        public const int DELAY_OFFLINE = 30;

        protected bool wev_Nonmember_Receive_NPQ1_Conf = false;
        protected bool wev_bNonmember_Receive_NPQ1_Conf = false;
        public Conf_NPQ1 wev_mNonmember_Receive_NPQ1_Conf = null;
        public void wev_setNonmember_Receive_NPQ1_Conf(Conf_NPQ1 npq1)
        {
            wev_mNonmember_Receive_NPQ1_Conf = npq1;
            wev_bNonmember_Receive_NPQ1_Conf = true;
        }
        public bool wev_bNonmember_Receive_NPQ2_Req = false;
        protected bool wev_bNonmember_Receive_NPQ3_Req = false;
        protected Req_NPQ3 wev_mNonmember_Receive_NPQ3_Req = null;

        public void wev_setNonmember_Receive_NPQ3_Req(Req_NPQ3 npq3)
        {
            bIsState_WaitRemoteStart = true;
            wev_mNonmember_Receive_NPQ3_Req = npq3;
            wev_bNonmember_Receive_NPQ3_Req = true;
        }

        public EL_SendManager_OCPP_Wev mSendManager_OCPP_Wev = null;
        public EL_SendManager_OCPP_Wev getSendManager_OCPP_Wev()
        {
            return mSendManager_OCPP_Wev;
        }

        public EL_DC_Charger_MyApplication getMyApplication()
        {
            return (EL_DC_Charger_MyApplication)mApplication;
        }

        override protected void setOCPP_initVariable()
        {
            base.setOCPP_initVariable();

            mSendManager_OCPP_Wev = new EL_SendManager_OCPP_Wev(mOCPP_Comm_Manager, mChannelIndex, this);
            mSendManager_OCPP_Wev.initVariable();
        }
    }
}
