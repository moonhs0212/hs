using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerInfor;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.infor;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.packet.csms2cp;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    abstract public class EL_StateManager_OCPP_Channel : EL_StateManager_Channel_Base
    {

        public bool bOCPP_IsRemoteStartTransaction = false;
        public bool bOCPP_IsRemoteStartTransaction_Process = false;
        public bool bOCPP_IsReceivePacket_CallResult_StartTransaction = false;
        public bool bOCPP_IsReceivePacket_CallResult_StopTransaction = false;
        public bool bOCPP_IsReceive_Success_StartTransaction = false;

        public Conf_Authorize mOCPP_Conf_Authorize = null;

        protected OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE? mOCPP_AuthCache_State = null;

        protected void initVariable_After_Receive_BootNotification()
        {
            switch (mApplication.getPlatform_Operator())
            {
                case common.variable.EPlatformOperator.WEV:
                    mSendManager_StatusNotification.sendOCPP_CP_Req_StatusNotification_Wev_Booting();
                    break;
                default:
                    break;
            }
        }

        public void ocpp_setErrorOccured(int errorCode, Reason reason, ChargePointStatus status)
        {
            if (mOCPP_StopTransaction_Reason == null)
            {
                mOCPP_StopTransaction_Reason = reason;
                mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction(reason);
            }
            mSendManager_StatusNotification.setStop();
            mSendManager_StatusNotification.setOCPP_ChargePointStatus(status);

            if (mErrorCode < 1)
            {
                mErrorCode = errorCode;
                mErrorMessage = Const_ErrorCode.getErrorMessage(mErrorCode);
            }

            bIsErrorOccured = true;
        }
        public void moveToError(String errorMessage, Reason reason, ChargePointStatus status)
        {
            moveToError(errorMessage);
            if (mOCPP_StopTransaction_Reason == null)
            {
                mOCPP_StopTransaction_Reason = reason;
                mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction(reason);
            }
            mSendManager_StatusNotification.setStop();
            mSendManager_StatusNotification.setOCPP_ChargePointStatus(status);
        }


        public void moveToError(int errorCode, String errorMessage, Reason reason, ChargePointStatus status)
        {
            mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
            moveToError(errorCode, errorMessage);
            if (mOCPP_StopTransaction_Reason == null)
            {
                mOCPP_StopTransaction_Reason = reason;
                mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction(reason);
            }
            mSendManager_StatusNotification.setStop();
            mSendManager_StatusNotification.setOCPP_ChargePointStatus(status);

        }

        public void moveToError(int errorCode, Reason reason, ChargePointStatus status)
        {
            moveToError(errorCode);
            if (mOCPP_StopTransaction_Reason == null)
            {
                mOCPP_StopTransaction_Reason = reason;
                mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction(reason);
            }
            mSendManager_StatusNotification.setStop();
            mSendManager_StatusNotification.setOCPP_ChargePointStatus(status);

        }



        override public void moveToError(int errorCode)
        {
            if (errorCode > 0)
            {
                mErrorCode = errorCode;
                mErrorMessage = Const_ErrorCode.getErrorMessage(mErrorCode);
            }

            bIsErrorOccured = true;

            moveToError();
        }
        public override void moveToError()
        {
            if (bIsComplete_Payment && !bOCPP_IsReceivePacket_CallResult_StartTransaction)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION);
            }
            if (bIsCharging_MoreOnce)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
            }
            else if (bIsComplete_Payment)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);

            }
            else if (bIsComplete_RFCard_Read)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
            }
            else if (bIsComplete_Certification)
            {
                setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
            }
            else
            {
                if (bIsComplete_TransactionStart)
                {
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                }
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
        override protected bool processCondition_Charging()
        {
            bool isNeedComplete_StopOption = mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop().isNeedStopCharging();
            String errorMessage_StopOption = mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop().getMessage_StopOption();

            if (bIsChargingStart_By_ChargingWattage)
                mChannelTotalInfor.mChargingCharge.setCurrentWattage(mChannelTotalInfor.getAMI_PacketManager().getPositive_Active_Energy_Pluswh());

            if (mDelay_MeterValueSampleInterval > 0)
            {
                if (isTimer_Sec(TIMER_MeterValueSampleInterval, mDelay_MeterValueSampleInterval) && bIsChargingStart_By_ChargingWattage)
                {
                    mSendManager_OCPP_ChargingReq.setOCPP_MeterValue_Sample_Charging();
                }
            }

            if (isTimer_Sec(TIMER_10SEC, 5))
            {
                mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction();
                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_ChargingInfor_Temp(
                    "" + mTransactionInfor_DBId,
                    mChannelTotalInfor.getEV_State().getSOC(),
                    mChannelTotalInfor.mChargingTime.getSecond_WastedTime(),
                    mChannelTotalInfor.mChargingCharge.getChargedWattage(),
                    mChannelTotalInfor.getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                    mChannelTotalInfor.mChargingCharge.getChargedCharge(),
                    mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().getChargingUnit_Current()
                );
            }
            bool isChange = false;
            if (mTime_SetState.getSecond_WastedTime() > 5)
            {
                if (isNeedReset())
                {
                    if (bIsNeedReset_Charger_Hard)
                    {
                        moveToError(
                            Const_ErrorCode.CODE_0006_HARD_RESET,
                            Reason.HardReset,
                            ChargePointStatus.Finishing);
                    }
                    else
                    {
                        moveToError(
                            Const_ErrorCode.CODE_0005_SOFT_RESET,
                            Reason.SoftReset,
                            ChargePointStatus.Finishing);
                        bIsCommand_UsingStart = false;
                    }
                }
                else if (isNeedComplete_StopOption)
                {
                    mErrorReason = errorMessage_StopOption;
                    int reason = 0;
                    switch (mErrorReason)
                    {
                        case "결제금액 도달":
                            reason = Const_ErrorCode.CODE_0008_CHARGINGSTOP_BY_TARGET_VALUE;
                            break;
                        case "완충":
                            reason = Const_ErrorCode.CODE_0001_CAR_FULL_CHARGING_COMPLETE;
                            break;
                        case "커넥터 분리":
                            reason = Const_ErrorCode.CODE_0003_CONNECTOR_DISCONNECT;
                            break;
                        case "충전량(%) 도달":
                            reason = Const_ErrorCode.CODE_0009_CHARGINGSTOP_BY_TARGET_SOC;
                            break;
                        case "목표 충전량 도달":
                            reason = Const_ErrorCode.CODE_0007_CHARGINGSTOP_BY_TARGET_EP;
                            break;

                    }
                    moveToError(
                            reason,
                            Reason.Other,
                            ChargePointStatus.Finishing);


                    //mOCPP_StopTransaction_Reason = Reason.Other;
                    //mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);

                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                    isChange = true;
                }
                else if (
            !mChannelTotalInfor.getEV_State().isState_ConnectedCar()
                && !bIsTest_Car
                && !bIsErrorOccured
        )
                {
                    moveToError(
                            Const_ErrorCode.CODE_0003_CONNECTOR_DISCONNECT,
                            Reason.EVDisconnected,
                            ChargePointStatus.Finishing);
                }
                else if (
                        mChannelTotalInfor.getEV_State().isState_ChargingCompleteCar()
                                && !bIsTest_Car
                                && !bIsErrorOccured
                )
                {
                    moveToError(
                            Const_ErrorCode.CODE_0001_CAR_FULL_CHARGING_COMPLETE,
                            Reason.Local,
                            ChargePointStatus.Finishing);
                }
                else if (
                        mApplication.DI_Manager.isEmergencyPushed()
                                && !bIsErrorOccured)
                {
                    moveToError(
                            Const_ErrorCode.CODE_0116_EMERGENCY,
                            Reason.EmergencyStop,
                            ChargePointStatus.SuspendedEVSE);
                }
                else if (((bIsCommand_ChargingStop && isTimer_Sec(TIMER_90SEC, 7)) && !bIsErrorOccured))
                {
                    moveToError(
                            Const_ErrorCode.CODE_0002_USER_COMPLETE,
                            Reason.Other,
                            ChargePointStatus.Finishing);
                }
                else if (!bIsCommand_UsingStart
                        && !bIsErrorOccured)
                {
                    moveToError(
                            Const_ErrorCode.CODE_0004_SERVER_COMMAND_STOP,
                            Reason.Other,
                            ChargePointStatus.Finishing);
                }
                else if (bIsChargingStart_By_ChargingWattage &&
                    mChannelTotalInfor.getAMI_PacketManager().isOccured_LowVoltage())
                {
                    moveToError(
                            Const_ErrorCode.CODE_0104_LOW_VOLTAGE_OUTPUT,
                            Reason.Other,
                            ChargePointStatus.Finishing);
                }
                else if (bIsChargingStart_By_ChargingWattage &&
                        mChannelTotalInfor.getAMI_PacketManager().isOccured_OverVoltage())
                {
                    moveToError(
                            Const_ErrorCode.CODE_0100_OVER_VOLTAGE_INPUT,
                            Reason.Other,
                            ChargePointStatus.Finishing);
                }
                else if (bIsChargingStart_By_ChargingWattage &&
                        mChannelTotalInfor.getAMI_PacketManager().isOccured_OverCurrent())
                {
                    moveToError(
                            Const_ErrorCode.CODE_0105_OVER_CURRENT_OUTPUT,
                            Reason.Other,
                            ChargePointStatus.Finishing);
                }
            }

            return isChange;
        }





        override public async void initVariable_ChargingComplete()
        {
            if (bIsCharging_MoreOnce && !mSendManager_OCPP_ChargingReq.bOCPP_Conf_StopTransaction)
            {
                mSendManager_OCPP_ChargingReq.setOCPP_MeterValue_Sample_Before_ChargingComplete(mErrorCode, getErrorMessage());

            }

            bIsOffLine = false;
            bOCPP_IsRemoteStartTransaction_Process = false;
            mOCPP_ParentIdTag = null;
            bIsCommand_ChargingStop = false;

            //        if(bOCPP_IsRemoteStartTransaction)
            //        {
            //            if(
            //                    (mOCPP_TransactionID != null &&
            //                        mOCPP_CSMS_Req_RemoteStartTransaction != null &&
            //                        mOCPP_CSMS_Req_RemoteStartTransaction.chargingProfile != null &&
            //                        mOCPP_CSMS_Req_RemoteStartTransaction.chargingProfile.transactionId != null &&
            //                        mOCPP_TransactionID == mOCPP_CSMS_Req_RemoteStartTransaction.chargingProfile.transactionId)
            //                    ||
            //                    (mCardNumber_Member != null && mOCPP_CSMS_Req_RemoteStartTransaction != null
            //                        && mOCPP_CSMS_Req_RemoteStartTransaction.idTag != null
            //                        && mCardNumber_Member.equals(mOCPP_CSMS_Req_RemoteStartTransaction.idTag))
            //            )
            //            {
            //                bOCPP_IsRemoteStartTransaction = false;
            //                mOCPP_CSMS_Req_RemoteStartTransaction = null;
            //            }
            //        }


            if (bIsReservation)
            {
                if (
                        (mOCPP_ReservationId != null &&
                                mOCPP_CSMS_Req_ReserveNow != null &&
                                mOCPP_CSMS_Req_ReserveNow.reservationId != null)
                )
                {
                    mOCPP_ReservationId = null;
                    bIsReservation = false;
                    mOCPP_CSMS_Req_ReserveNow = null;

                }
            }

            bIsProcessing_Using = false;


            if (bIsCharging_MoreOnce)
            {
                mOCPP_MeterValue_ChargingWattage_Finish = (long)((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh();

                if (!mSendManager_OCPP_ChargingReq.bOCPP_Conf_StopTransaction)
                    mSendManager_OCPP_ChargingReq.sendOCPP_CP_Req_StopTransaction();                
                if (mErrorReason != null && mErrorReason.Equals("커넥터분리"))
                {
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing, "EV side disconnected");
                }
                else
                {
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);
                }
            }
            base.initVariable_ChargingComplete();
        }

        override public void initVariable()
        {

            setOCPP_initVariable();
            ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager()).setEV_State_ChangeListener(mSendManager_StatusNotification);
        }

        public override void initVariable_TransactionStart()
        {
            base.initVariable_TransactionStart();

            if (!bOCPP_IsReceivePacket_CallResult_StartTransaction)
            {
                mSendManager_OCPP_ChargingReq.sendOCPP_CP_Req_StartTransaction();
            }
        }


        public override void initVariable_TransactionStart_Complete()
        {
            base.initVariable_TransactionStart_Complete();
        }




        override public void initVariable_UsingComplete()
        {
            base.initVariable_UsingComplete();

            mLastDeal_Infor = null;
            mOCPP_Conf_Authorize = null;
            bOCPP_IsReceive_Success_StartTransaction = false;
            bOCPP_IsReceivePacket_CallResult_StartTransaction = false;
            bOCPP_IsReceivePacket_CallResult_StopTransaction = false;
            mOCPP_StopTransaction_Reason = null;
            mSendManager_OCPP_ChargingReq.bOCPP_Conf_StopTransaction = false;
            mSendManager_OCPP_ChargingReq.bOCPP_CSMS_Conf_StartTransaction = false;
            bIsOffLine = false;
            bOCPP_IsRemoteStartTransaction_Process = false;
            mOCPP_ParentIdTag = null;
            initVariable_ChargingComplete();

            if ((((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1)
                     .getControlbdComm_PacketManager().packet_1z.getState_CommunicationCar() < 2))
            {
                mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Available);
            }
            else
            {
                mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Preparing);
            }



            bIsCommand_ChargingStop = false;
            mOCPP_StopTransaction_Reason = null;
            mSendManager_OCPP_ChargingReq.mOCPP_CSMS_Conf_StartTransaction = null;
            bIsAutoProcess = false;

            mOCPP_ReservationId = null;
            mOCPP_TransactionID = null;
            mSendManager_OCPP_Authorize.mOCPP_CSMS_Conf_Authorize = null;
            mSendManager_OCPP_ChargingReq.clearAll();
            mSendManager_OCPP_Authorize.clearAll();
        }

        /*
        Flag
         */

        public void setOCPP_CSMS_Req_ReserveNow(Req_ReserveNow req)
        {
            mOCPP_CSMS_Req_ReserveNow = req;
            mOCPP_Time_Reservation.setTime(mOCPP_CSMS_Req_ReserveNow.expiryDate);
            if (bIsCommand_UsingStart && !bIsProcessing_Using)
                bIsCommand_UsingStart = false;
            bIsReservation = true;
        }
        public bool setOCPP_Cancel_Reservation(Req_CancelReservation req)
        {
            if (bIsReservation == true && mOCPP_CSMS_Req_ReserveNow != null)
            {
                if (mOCPP_CSMS_Req_ReserveNow.reservationId == req.reservationId)
                {
                    mOCPP_CSMS_Req_ReserveNow = null;
                    bIsReservation = false;
                    return true;
                }
            }
            return false;

        }




        public RemoteStartStopStatus setOCPP_CSMS_Req_RemoteStopTransaction(Req_RemoteStopTransaction data)
        {
            if (bIsProcessing_Using)
            {
                if (mOCPP_TransactionID == data.transactionId)
                {
                    mOCPP_StopTransaction_Reason = Reason.Remote;
                    bIsCommand_ChargingStop = true;
                    return RemoteStartStopStatus.Accepted;
                }
            }
            return RemoteStartStopStatus.Rejected;
        }




        ///////////////////////////

        protected Req_RemoteStartTransaction mOCPP_CSMS_Req_RemoteStartTransaction = null;
        public RemoteStartStopStatus setOCPP_CSMS_Req_RemoteStartTransaction(Req_RemoteStartTransaction data)
        {
            RemoteStartStopStatus result;
            if ((bIsProcessing_Using && !bIsState_WaitRemoteStart)
                || !bIsUseEnable_Channel
                || bIsNeedReset_Charger_Soft)
            {
                result = RemoteStartStopStatus.Rejected;
            }
            else if (bIsState_WaitRemoteStart)
            {
                result = RemoteStartStopStatus.Accepted;
            }
            else if (!bIsReservationProcessing || bIsReservation)
            {
                if (mOCPP_CSMS_Req_ReserveNow != null && data.idTag != null && mOCPP_CSMS_Req_ReserveNow.idTag != null)
                {
                    if (mOCPP_CSMS_Req_ReserveNow.idTag.Equals(data.idTag))
                        result = RemoteStartStopStatus.Accepted;
                    else
                        result = RemoteStartStopStatus.Rejected;
                }
                else
                {
                    result = RemoteStartStopStatus.Accepted;
                }
            }
            else if (bIsProcessing_Using
                    || !bIsUseEnable_Channel
                    || bIsNeedReset_Charger_Soft)
            {
                result = RemoteStartStopStatus.Rejected;
            }

            else
            {
                result = RemoteStartStopStatus.Accepted;
            }


            if (result == RemoteStartStopStatus.Accepted)
            {
                mOCPP_CSMS_Req_RemoteStartTransaction = data;
                bOCPP_IsRemoteStartTransaction = true;
            }
            else
            {

            }



            return result;
        }


        override public bool isReservation()
        {
            if (bIsReservation)
            {
                EL_Time currentTime = new EL_Time();
                currentTime.setTime();
                int second = currentTime.getSecond_WastedTime_NoAbs(mOCPP_Time_Reservation);
                if (second >= 0)
                    return true;

                return false;
            }


            return false;
        }









        public bool cancelReservation(Req_CancelReservation req)
        {
            if (bIsReservation)
            {
                if (mOCPP_CSMS_Req_ReserveNow.reservationId == req.reservationId)
                {
                    bIsReservation = false;
                    mOCPP_CSMS_Req_ReserveNow = null;
                    return true;
                }
            }
            return false;
        }







        protected OCPP_MainInfor mOCPP_MainInfor = null;
        protected OCPP_ChannelInfor mOCPP_ChannelInfor = null;
        virtual protected void setOCPP_initVariable()
        {
            mOCPP_Comm_Manager = mApplication.getOCPP_Comm_Manager();
            mOCPP_Comm_SendMgr = mApplication.getOCPP_Comm_Manager().getSendMgr();


            mOCPP_MainInfor = mApplication.getOCPP_MainInfor();
            mOCPP_ChannelInfor = mApplication.getChannelTotalInfor(mChannelIndex).getOCPP_ChannelInfor();

            mSendManager_StatusNotification = new EL_SendManager_StatusNotification_Channel(mOCPP_Comm_Manager, mChannelIndex, this);
            mSendManager_StatusNotification.initVariable();

            mSendManager_OCPP_ChargingReq = new EL_SendManager_OCPP_ChargingReq(mOCPP_Comm_Manager, mChannelIndex, this);
            mSendManager_OCPP_ChargingReq.initVariable();

            mSendManager_OCPP_Authorize = new EL_SendManager_OCPP_Authorize(mOCPP_Comm_Manager, mChannelIndex, this);
            mSendManager_OCPP_Authorize.initVariable();

        }


        protected int mDelay_MeterValueSampleInterval = 0;
        protected OCPP_Manager_Table_Setting mSettingData_OCPP_Table = null;



        public long mOCPP_MeterValue_ChargingWattage_Start = 0;
        public long mOCPP_MeterValue_ChargingWattage_Finish = 0;

        public long mOCPP_MeterValue_ChargingWattage_Current = 0;

        public long? mOCPP_ReservationId = null;
        public long? mOCPP_TransactionID = null;
        public String mOCPP_ParentIdTag = null;
        protected EL_Time mOCPP_MeterValue_Time_Sampled = new EL_Time();
        public Reason? mOCPP_StopTransaction_Reason;

        override protected void initVariable_ChargingStart()
        {
            mDelay_MeterValueSampleInterval = mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.MeterValueSampleInterval);
            if (!bIsCharging_MoreOnce)
            {

                if (mOCPP_MeterValue_ChargingWattage_Start <= 0)
                    mOCPP_MeterValue_ChargingWattage_Start = (long)(mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh();
                mOCPP_MeterValue_Time_Sampled.setTime();
                mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Charging);

                mSendManager_OCPP_ChargingReq.setOCPP_MeterValue_Sample_Charging();
            }

            base.initVariable_ChargingStart();
        }


        /*------------------------------------------------------------------------------------------
                 CP->CSMS CALL
                 ------------------------------------------------------------------------------------------*/






        protected OCPP_Comm_SendMgr mOCPP_Comm_SendMgr = null;

        protected Req_ReserveNow mOCPP_CSMS_Req_ReserveNow = null;

        public EL_SendManager_StatusNotification_Channel mSendManager_StatusNotification = null;
        public EL_SendManager_OCPP_ChargingReq mSendManager_OCPP_ChargingReq = null;
        public EL_SendManager_OCPP_Authorize mSendManager_OCPP_Authorize = null;


        public Req_ReserveNow getOCPP_CSMS_Req_ReserveNow()
        {
            return mOCPP_CSMS_Req_ReserveNow;
        }
        protected EL_Time mOCPP_Time_Reservation = new EL_Time();

        protected OCPP_Comm_Manager mOCPP_Comm_Manager = null;
        public OCPP_EL_Manager_Table_TransactionInfor mTable_TransactionInfor = null;
        protected OCPP_EL_Manager_Table_Transaction_Metervalues mTable_Transaction_Metervalues = null;



        public EL_StateManager_OCPP_Channel(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {
            mSettingData_OCPP_Table = ((OCPP_Manager_Table_Setting)mApplication.getManager_SQLite_Setting_OCPP().getList_Manager_Table()[0]);
            mSettingData_OCPP_Table.setSettingData((int)CONST_INDEX_OCPP_Setting.LocalPreAuthorize, false);


            mTable_TransactionInfor = mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor();
            mTable_Transaction_Metervalues = mApplication.getManager_SQLite_Setting_OCPP().getTable_Transaction_Metervalues();
        }

        protected class OCPP_ConfAuthorize_Listener_Charging11_Case1 : IOCPP_ConfAuthorize_Listener
        {
            EL_StateManager_OCPP_Channel mStateManager_Channel = null;
            public OCPP_ConfAuthorize_Listener_Charging11_Case1(EL_StateManager_OCPP_Channel stateManager_Channel)
            {
                mStateManager_Channel = stateManager_Channel;
            }



            public void onAuthorize(Conf_Authorize packet)
            {
                if (!mStateManager_Channel.bIsCharging_MoreOnce)
                    return;
                if (mStateManager_Channel.mCardNumber_Member != null
                        && mStateManager_Channel.mOCPP_ParentIdTag != null
                        && packet != null
                        && packet.idTagInfo != null
                        && packet.idTagInfo.parentIdTag != null
                        && mStateManager_Channel.mOCPP_ParentIdTag.Equals(packet.idTagInfo.parentIdTag))
                {

                    mStateManager_Channel.setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                    mStateManager_Channel.mCardNumber_Member = mStateManager_Channel.mCardNumber_Read_Temp;
                    mStateManager_Channel.mOCPP_StopTransaction_Reason = Reason.Local;
                    if (mStateManager_Channel.bIsOffLine)
                    {
                        if (!mStateManager_Channel.mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.StopTransactionOnInvalidId))
                        {
                            mStateManager_Channel.mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.SuspendedEVSE);
                        }
                    }
                    else
                    {

                    }
                    mStateManager_Channel.mSendManager_OCPP_Authorize.setOCPP_Authorize_Listener(null);
                }
                else
                {

                    mStateManager_Channel.setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);

                }

            }

        }

        protected class OCPP_ConfAuthorize_Listener_Charging11_Case2 : IOCPP_ConfAuthorize_Listener
        {
            EL_StateManager_OCPP_Channel mStateManager_Channel = null;
            public OCPP_ConfAuthorize_Listener_Charging11_Case2(EL_StateManager_OCPP_Channel stateManager_Channel)
            {
                mStateManager_Channel = stateManager_Channel;
            }



            public void onAuthorize(Conf_Authorize packet)
            {
                if (!mStateManager_Channel.bIsCharging_MoreOnce)
                    return;
                if (mStateManager_Channel.mCardNumber_Member != null
                        && packet != null
                        && packet.idTagInfo != null
                        && packet.idTagInfo.parentIdTag != null
                        && mStateManager_Channel.mCardNumber_Member.Equals(packet.idTagInfo.parentIdTag))
                {

                    mStateManager_Channel.setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                    mStateManager_Channel.mCardNumber_Member = mStateManager_Channel.mCardNumber_Read_Temp;

                    if (mStateManager_Channel.bIsOffLine)
                    {
                        if (!mStateManager_Channel.mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.StopTransactionOnInvalidId))
                        {
                            mStateManager_Channel.mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.SuspendedEVSE);
                        }
                    }
                    else
                    {
                        mStateManager_Channel.mOCPP_StopTransaction_Reason = Reason.Local;
                    }
                    mStateManager_Channel.mSendManager_OCPP_Authorize.setOCPP_Authorize_Listener(null);
                }
                else
                {

                    mStateManager_Channel.setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);

                }
            }
        }
    }

}
