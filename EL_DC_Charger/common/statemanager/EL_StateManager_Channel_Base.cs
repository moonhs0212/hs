using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerInfor;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.statemanager
{
    abstract public class EL_StateManager_Channel_Base : EL_StateManager_Base, ISQLiteListener_Charging, IAddListener_Option_ChargingStop
    {
        protected bool bIsReboot_Suddenly = false;

        protected double mProcess_OperatorChargeUnit_Current = 0.0d;
        public void setProcess_OperatorChargeUnit_Current(double unit)
        {
            mProcess_OperatorChargeUnit_Current = unit;
        }

        protected Object mCardPayment_DealResult_First = null;
        protected Object mCardPayment_DealResult_Cancel = null;
        protected Object mCardPayment_DealResult_PartCancel = null;

        protected String[] mLastDeal_Infor = null;

        protected bool bIsComplete_Payment = false;
        protected bool bIsBootup_First = true;
        public String[] result = null;


        public EL_Time mTime_ChargingStart = new EL_Time();
        public EL_Time mTime_ChargingStop = new EL_Time();

        protected int mDelay_ConnectionTimeOut = 0;

        public int mDelay_Reset = 90;
        public bool bIsAutoProcess = false;
        public bool bIsComplete_PrepareChannel = false;


        public void setIsOffLine(bool setting)
        {
            bIsOffLine = setting;
            Logger.d("OffLine Change = " + setting);
        }
        public bool bIsOffLine = false;

        protected String mProcess_OperatorType = "";
        public void setProcess_OperatorType(String type)
        {
            mProcess_OperatorType = type;
        }
        public int mTarget_Charge = 0;

        public bool bIsReservationProcessing = false;//예약 진행중인지 체크

        public bool bIsReservation = false;
        abstract public bool isReservation();

        public bool bIsPrepareComplete_StateManager_Main = false;

        public string mErrorReason = null;

        public String mCardNumber_Read_Temp = "";
        public String mCardNumber_Member = "";

        public bool bIsCertificationSuccess = false;
        public bool bIsCertificationFailed = false;

        public long mTransactionInfor_DBId = 0;
        //public void setErrorOccured(String errorMessage)
        //{
        //    mErrorReason = errorMessage;
        //    bIsErrorOccured = true;
        //}

        protected String mIsCommand_UsingStart_Off_String = "";
        public void setCommand_UsingStart_Off(String text)
        {
            mIsCommand_UsingStart_Off_String = text;
            bIsCommand_UsingStart = false;
        }
        protected String mIsCommand_ChargingStop_String = "";
        protected bool bIsCommand_ChargingStop = false;
        public void setCommand_ChargingStop(bool command_ChargingStop, String text)
        {
            bIsCommand_ChargingStop = command_ChargingStop;
        }


        protected String mIsCommand_UsingStart_String = "";
        public void setCommand_UsingStart(String text)
        {
            mIsCommand_UsingStart_String = text;
            bIsCommand_UsingStart = true;
        }

        public bool bIsState_WaitRemoteStart = false;

        public bool bIsErrorOccured = false;
        public String mErrorMessage = "";
        public String mErrorMessage_Detail = "";
        public String getErrorMessage()
        {
            if (mErrorMessage.Length < 1)
                return mErrorMessage_Detail;

            return mErrorMessage;
        }

        public int mErrorCode = 0;
        //public String mErrorMessage = "";
        public bool bIsCertificationComplete = false;
        public bool bIsProcessing_Using = false;
        public bool bIsCommand_UsingStart = false; //사용시작시 true, 초기화면 이동 필요시 false, Off시 진행중인 프로세스를 종료한다.
        public bool bIsCharging_Current = false;
        public bool bIsCharging_MoreOnce = false;
        public bool bIsChargingStart_By_ChargingWattage = false;        


        //비정상종료 후 재시작인지
        public bool bIsRebootChargeYN = false;
        public bool bIsFirstToogle = true;

        //    public bool bIsCommand_ChargingStop_Other = false;


        public bool bIsReady_Reboot = false;
        public bool bIsComplete_Certification = false;
        public bool bIsComplete_RFCard_Read = false;

        public bool bIsSelected_ConnectorType = false;

        public EMemberType mMemberType = EMemberType.NONE;

        public bool bIsSelected_NonMemeber_PaymentType = false;

        public EPaymentType mPaymentType = EPaymentType.NONE;
        public bool bIsSelected_NonMemeber_PaymentType_CardDevice = false;

        public bool bIsSelected_Confirm_ChargingComplete = false;
        public bool bIsClick_BackButton = false;
        public bool bIsClick_HomeButton = false;

        public bool bIsTest_Car = false;


        public bool bIsUseEnable_Channel = true;
        EL_Time mTime_Manual = null;
        bool bIsCompleteMCOn = false;

        public bool bIsComplete_TransactionStart = false;

        public EL_Time mTIme_TransactionStart_Try = new EL_Time();

        virtual public void initVariable_TransactionStart()
        {
            mTIme_TransactionStart_Try.setTime();
        }
        protected EL_Time mTime_Transactiontart_Complete = new EL_Time();
        virtual public void initVariable_TransactionStart_Complete()
        {
            bIsComplete_TransactionStart = true;
            mTime_Transactiontart_Complete.setTime();
        }

        protected bool bIsEnter_NotExcute = false;
        virtual protected bool processCondition_Charging()
        {
            bool isChange = false;
            if (isNeedReset())
            {
                mErrorMessage = "리셋 요청";
                bIsCommand_UsingStart = false;
                isChange = true;
            }
            else if (mTime_SetState.getSecond_WastedTime() > 10 && !bIsChargingStart_By_ChargingWattage)
            {
                mErrorMessage = "전력량계 오류";
                isChange = true;
            }
            else if (
                    (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isState_ChargingCar()
                            && !bIsTest_Car)
            )
            {
                mErrorMessage = "차량 충전정지 요청 (" + mApplication.getChannelTotalInfor(1).getEV_State().getErrorCode() + ")";
                isChange = true;
            }
            else if (
                    (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isRequesting_ChargingStart_Car()
                            && !bIsTest_Car
                            && !bIsErrorOccured)
            )
            {
                mErrorMessage = "차량 충전정지 요청 (" + mApplication.getChannelTotalInfor(1).getEV_State().getErrorCode() + ")";
                isChange = true;
            }
            else if (
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed()
                            && !bIsErrorOccured)
            {
                mErrorMessage = "비상정지 눌림";
                isChange = true;
            }
            else if ((((bIsCommand_ChargingStop || bIsClick_ChargingStop_User) && isTimer_Sec(TIMER_90SEC, 7)) && !bIsErrorOccured))
            {
                mErrorMessage = "충전중지 누름";
                isChange = true;
            }
            else if (!bIsCommand_UsingStart
                   && !bIsErrorOccured)
            {
                mErrorMessage = "충전중지 눌림";
                isChange = true;
            }
            else if (
                    (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getEV_State().isState_ConnectedCar()
                            && !bIsTest_Car
                    )
            )
            {
                mErrorMessage = "커넥터 분리";
                isChange = true;
            }
            return isChange;
        }


        //ClickEvent
        public bool bIsClick_ChargingStop_User = false;
        public bool bIsClick_Notify_1Button = false;
        public bool bIsClick_SettingComplete_Payment_Value = false;

        public int mNonmember_Payment_Setting_First = 0;
        public void setNonmember_Payment_Setting_First(int value)
        {
            mNonmember_Payment_Setting_First = value;
        }
        public int getNonmember_Payment_Setting_First()
        {
            return mNonmember_Payment_Setting_First;
        }


        public bool process_GetAmiValue_Booting()
        {
            if (!mChannelTotalInfor.getControlbdComm_PacketManager().isConnected())
                return false;

            if (mChannelTotalInfor.getControlbdComm_PacketManager().isConnected() &&
                    mChannelTotalInfor.getEV_State().isState_ChargingCar())
                return true;

            if (!bIsCompleteMCOn)
            {
                if (mTime_Manual == null)
                {
                    mTime_Manual = new EL_Time();
                    mTime_Manual.setTime();
                    mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.setHMI_Manual_Control(true);
                    mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.bPowerRelay_Minus = true;
                    mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.bPowerRelay_Plus = true;
                }
                else
                {
                    if (mTime_Manual.getSecond_WastedTime() > 10)
                    {
                        mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.setHMI_Manual_Control(true);
                        mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.bPowerRelay_Minus = false;
                        mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.bPowerRelay_Plus = false;
                        bIsCompleteMCOn = true;
                    }
                }
            }

            if (bIsCompleteMCOn) return true;
            else return false;
        }

        public bool bIsNeedReset_Charger_Hard = false;
        public bool bIsNeedReset_Charger_Soft = false;

        protected bool isNeedShowBackButton()
        {
            if (bIsAutoProcess)
                return false;
            else
                return true;

        }

        public bool isNeedReset()
        {
            if (bIsNeedReset_Charger_Soft || bIsNeedReset_Charger_Hard)
                return true;
            else
                return false;
        }

        protected void initVariable_UsingStart()
        {

            bIsCommand_UsingStart = true;
            bIsProcessing_Using = true;

            mTransactionInfor_DBId = mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_usingStart(mChannelIndex);
        }
        virtual protected bool process_EmergencyButton()
        {
            if (mApplication.DI_Manager != null
                && mApplication.DI_Manager.isEmergencyPushed())
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                return true;
            }

            return false;
        }


        virtual protected void initVariable_ChargingStart()
        {
            bIsAutoProcess = false;
            if (!bIsCharging_MoreOnce)
            {
                (mApplication).getChannelTotalInfor(1).mChargingTime.onChargingStart();

                if (mLastDeal_Infor == null)
                    mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 1;
                mTime_ChargingStart.setTime();
                bIsCharging_MoreOnce = true;
            }
            if (mLastDeal_Infor != null)
                mChannelTotalInfor.mChargingTime.onChargingStart();
            bIsCharging_Current = true;
        }

        protected void initVariable_ChargingPause()
        {
            bIsCharging_Current = false;
        }

        //충전종료옵션 
        virtual protected void initVariable_ChargingOption_System()
        {
            mChannelTotalInfor.getManager_Option_ChargingStop().initVariable();
            mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop()
                .addOption(EOption_ChargingStop.FULL, 1);
        }

        protected void setChargingStop()
        {
            //((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
            bIsAutoProcess = false;
            ((EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager())
                        .packet_z1.mCommand_Output_Channel1 = 0;

            mTime_ChargingStop.setTime();
            //        MainActivity.bNeed_UpdateUI_ChargingStart = false;
            //        MainActivity.bNeed_UpdateUI_ChargingStop = true;
            if (bIsCharging_Current)
            {
                ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).mChargingTime.onChargingStop();
                if (bIsChargingStart_By_ChargingWattage)
                {
                    //((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).mChargingWattage.setChargignStop();
                    bIsChargingStart_By_ChargingWattage = false;
                }
            }
        }

        virtual public void initVariable_Pre_ChargingComplete()
        {
            mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
            if (bIsCharging_Current)
            {
                mChannelTotalInfor.mChargingCharge.setCurrentWattage(mChannelTotalInfor.getAMI_PacketManager().getPositive_Active_Energy_Pluswh());
                setChargingStop();

                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_ChargingStop(
                         mTransactionInfor_DBId,
                         mTime_ChargingStop.getDateTime_DB(),
                         mChannelTotalInfor.getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                         mChannelTotalInfor.getEV_State().getSOC(),
                         mChannelTotalInfor.mChargingTime.getSecond_WastedTime(),
                         mChannelTotalInfor.mChargingCharge.getChargedWattage(),

                         mChannelTotalInfor.mChargingCharge.getChargedCharge(),
                         getErrorMessage()
                 );

            }
        }

        virtual public void moveToError(int errorCode)
        {
            if (errorCode > 0)
            {
                mErrorCode = errorCode;
                mErrorMessage = Const_ErrorCode.getErrorMessage(mErrorCode);
            }

            bIsErrorOccured = true;

            moveToError();
        }

        virtual public void moveToError(String errorMessage)
        {
            if (errorMessage != null && errorMessage.Length > 0)
            {
                mErrorMessage_Detail = errorMessage;
            }

            bIsErrorOccured = true;

            moveToError();
        }

        virtual public void moveToError(int errorCode, String errorMessage)
        {
            if (errorCode > 0)
            {
                mErrorCode = errorCode;
                mErrorMessage = Const_ErrorCode.getErrorMessage(mErrorCode);
            }
            if (errorMessage != null && errorMessage.Length > 0)
            {
                mErrorMessage_Detail = errorMessage;
            }

            bIsErrorOccured = true;

            moveToError();
        }



        virtual public void moveToError()
        {
            if (bIsCharging_MoreOnce)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
            }
            else if (bIsComplete_Payment)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
            }
            else
            {
                if (bIsComplete_TransactionStart)
                {
                    if (mApplication.DI_Manager.isEmergencyPushed())
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    else
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                }
                else
                {
                    if (mApplication.DI_Manager.isEmergencyPushed())
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    else
                    {
                        if (mTransactionInfor_DBId >= 0)
                        {
                            mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_usingStop(
                                    mTransactionInfor_DBId, new EL_Time().getDateTime_DB());
                            mTransactionInfor_DBId = -1;
                        }
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }

                }
            }
        }

        virtual public void initVariable_ChargingComplete()
        {

            mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
            bIsState_WaitRemoteStart = false;
            bIsReservationProcessing = false;
            bIsProcessing_Using = false;

            bIsCharging_Current = false;

            ////////////로그////////////

            ////////////////////////////
        }




        virtual public void initVariable_UsingComplete()
        {
            bIsReboot_Suddenly = false;
            bIsComplete_Certification = false;
            mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().mMemberType_String = "";
            mProcess_OperatorChargeUnit_Current = 0.0d;
            mProcess_OperatorType = "";
            mTarget_Charge = 0;
            mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
            mChannelTotalInfor.mChargingCharge.initVariable();
            bIsState_WaitRemoteStart = false;
            bIsComplete_TransactionStart = false;
            bIsComplete_Payment = false;
            mErrorMessage = "";
            mErrorCode = 0;
            mErrorMessage_Detail = "";
            bIsReservationProcessing = false;

            bIsErrorOccured = false;
            bIsProcessing_Using = false;
            bIsCommand_UsingStart = false;
            bIsCertificationSuccess = false;
            bIsCertificationFailed = false;

            bIsCertificationComplete = false;
            bIsCharging_Current = false;
            bIsCharging_MoreOnce = false;
            bIsChargingStart_By_ChargingWattage = false;

            bIsClick_ChargingStop_User = false;
            bIsCommand_ChargingStop = false;
            bIsSelected_Confirm_ChargingComplete = false;

            mMemberType = EMemberType.NONE;
            mPaymentType = EPaymentType.NONE;

            (mApplication).getChannelTotalInfor(1).mChargingTime.onChargingStop();
            mCardPayment_DealResult_First = null;
            mCardPayment_DealResult_Cancel = null;
            //mCardPayment_DealResult_PartCancel = null;

            mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop().initVariable();
            initVariable_ChargingOption_System();

            if (mTransactionInfor_DBId >= 0)
            {
                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_usingStop(
                        mTransactionInfor_DBId, new EL_Time().getDateTime_DB());
                mTransactionInfor_DBId = -1;
            }

            mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.init();

            ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.setHMI_Manual_Control(false);
        }





        public EL_StateManager_Channel_Base(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {
            mChannelTotalInfor = application.getChannelTotalInfor(mChannelIndex);
            setIsUseEnable_Channel();
        }

        public void setIsUseEnable_Channel()
        {
            bIsUseEnable_Channel = mApplication.getManager_SQLite_Setting().getTable_Setting(1)
                    .getSettingData_Boolean(CONST_INDEX_CHANNELSETTING.IS_ENABLE_USE);
        }

        public void setIsUseEnable_Channel(bool setting)
        {
            bIsUseEnable_Channel = setting;
            mApplication.getManager_SQLite_Setting().getTable_Setting(mChannelIndex)
                    .setSettingData(CONST_INDEX_CHANNELSETTING.IS_ENABLE_USE, setting);
        }

        override public void setState(int state)
        {
            base.setState(state);
            bIsClick_ChargingStop_User = false;
            bIsClick_HomeButton = false;
            bIsClick_BackButton = false;
            bIsComplete_RFCard_Read = false;
            bIsClick_Notify_1Button = false;
            bIsSelected_ConnectorType = false;

            bIsSelected_Confirm_ChargingComplete = false;

            bIsClick_SettingComplete_Payment_Value = false;
        }

        public bool isUseEnable_Channel()
        {
            return bIsUseEnable_Channel;
        }

        public void onUseStart(long id)
        {
            //mDbId = id;
            //HistoryDBHelper.setSQLiteListener_Charging(null);
        }

        public void onAddOption_ChargingStop(EL_Option_ChargingStop option_ChargingStop)
        {
            if (mTransactionInfor_DBId > 0)
                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor()
                        .db_Option_ChargingStop(mTransactionInfor_DBId,
                                option_ChargingStop.getOption_ChargingStop().ToString(),
                                "" + option_ChargingStop.getValue_Target());
        }
    }
}
