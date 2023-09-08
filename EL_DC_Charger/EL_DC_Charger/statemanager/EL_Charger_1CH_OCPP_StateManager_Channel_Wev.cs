using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerInfor;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.EL_DC_Charger.Custom_UserControl.P1080_1920;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using EL_DC_Charger.ocpp.ver16.platform.wev.packet;
using EL_DC_Charger.ocpp.ver16.platform.wev.statemanager;
using EL_DC_Charger.ocpp.ver16.statemanager;
using EL_DC_Charger.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.QrCode.Internal;
using static EL_DC_Charger.ocpp.ver16.database.OCPP_EL_Manager_Table_AuthCache;

namespace EL_DC_Charger.EL_DC_Charger.statemanager
{
    public class EL_Charger_1CH_OCPP_StateManager_Channel_Wev : Wev_StateManager_OCPP_Channel
        , IRFCardReader_EventListener, IOnClickListener_Button, IAddListener_Option_ChargingStop
    {


        public EL_Charger_1CH_OCPP_StateManager_Channel_Wev(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {

            setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING);
            //mPageManager_Main.mContentLayout_Notify_1Tv_1Btn.setOnClickListener_NotifyButton(this);

        }



        //public String[] result = null;
        protected bool bTemp_Skip_NotCompletePacketSend = false;
        protected bool bTemp_Skip_NotCompletePacketSend_Processing = false;

        private DateTime rebootDT;

        EL_ControlbdComm_PacketManager mPacketManager = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager();

        public override void initVariable()
        {
            getMyApplication().mPageManager_Main().mUC_Notify_1Tv_1Btn[mChannelIndex - 1].setOnClickListener(this);
            base.initVariable();
        }
        //    {"_id", TYPE_INTEGER},
        //    {"ChannelIndex", TYPE_INTEGER},
        //    {"IsNormalComplete", TYPE_INTEGER},
        //    {"IdTag", TYPE_TEXT},
        //    {"TransactionID", TYPE_INTEGER},
        //    {"Infor_Req_Starttransaction", TYPE_TEXT},
        //    {"Infor_Conf_Starttransaction", TYPE_TEXT},
        //    {"IsReceiveConf_Starttransaction", TYPE_INTEGER},
        //    {"Infor_Req_Stoptransaction", TYPE_TEXT},
        //    {"Infor_Conf_Stoptransaction", TYPE_TEXT},
        //    {"IsReceiveConf_Stoptransaction", TYPE_INTEGER},\

        EL_Time mTime_Manual = null;
        bool bIsComplete_MCOn = false;

        override public void intervalExcuteAsync()
        {
            if (!isNeedExcute())
            {
                return;
            }

            switch (mState)
            {

                #region >> case CONST_STATE_UIFLOWTEST.STATE_PREFARING:
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING:
                    //if (process_GetAmiValue_Booting())
                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 1:

                    result = mTable_TransactionInfor.db_NotCompleteTransaction(mChannelIndex);
                    if (result != null)
                        Logger.d("NotSendData=" + result.ToString());
                    else
                        Logger.d("NotSendData=Nothing");
                    if (result != null)
                    {
                        if (!result.Equals("") && !result[8].Equals(""))
                        {
                            JArray jsonArray = null;
                            Req_StopTransaction req_stopTransaction = null;
                            try
                            {
                                jsonArray = JArray.Parse(result[8]);
                                req_stopTransaction = JsonConvert.DeserializeObject<Req_StopTransaction>(((JObject)jsonArray[3]).ToString());
                            }
                            catch (Exception e)
                            {
                                Console.Write(e.Message);
                            }
                            if (req_stopTransaction != null)
                            {
                                if (req_stopTransaction.reason == Reason.PowerLoss
                                        && !bTemp_Skip_NotCompletePacketSend
                                        && !bTemp_Skip_NotCompletePacketSend_Processing)
                                {
                                    bTemp_Skip_NotCompletePacketSend = true;
                                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 101);//asdf
                                }
                                else
                                {
                                    mSendManager_OCPP_ChargingReq.addReq_By_AllPacket(result[0], result[8]);
                                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 2);
                                }
                            }
                        }
                        else
                        {
                            mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Conf_StopTransaction(result[0], "Temp Complete");
                            setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                        }
                    }
                    else
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 2:
                    if (mSendManager_OCPP_ChargingReq.bOCPP_Conf_StopTransaction)
                    {
                        mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Conf_StopTransaction(result[0], "Temp Complete");
                    }
                    else if (isTimer_Sec(TIMER_90SEC, 15))
                    {
                        mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);
                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Conf_StopTransaction(result[0], "Temp Complete");
                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, 60))
                    {
                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Conf_StopTransaction(result[0], "Temp Complete");
                        mSendManager_OCPP_ChargingReq.clearAll();
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100:
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100:
                    bIsComplete_PrepareChannel = true;
                    if (isTimer_Sec(TIMER_WAITTIME, 3))
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 101);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 101:
                    bIsComplete_PrepareChannel = true;
                    //if (mChannelTotalInfor.getControlbdComm_PacketManager().isConnected())
                    setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 102);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 102:
                    bIsComplete_PrepareChannel = true;
                    if (mTime_SetState.getSecond_WastedTime() > 3 && bIsPrepareComplete_StateManager_Main)
                    {
                        initVariable_After_Receive_BootNotification();

                        mTransactionInfor_DBId = mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().getMaxId(mChannelIndex);
                        initVariable_ChargingOption_System();

                        if (bIsBootup_First
                         && mTransactionInfor_DBId > 0
                         && !mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().isNormalComplete(mTransactionInfor_DBId))

                        {
                            bool isNeedRestart = true;
                            String isNeedRestartStop_Text = "";
                            mLastDeal_Infor = mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().selectRow(null, "_id = " + mTransactionInfor_DBId)[0];
                            mOCPP_TransactionID = EL_Manager_Conversion.getLong(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.TransactionID]);
                            float value = -1.0f;
                            EOption_ChargingStop option_ChargingStop;
                            //결제금액 충전시작시간,결제타입 가져옴.
                            if (mOCPP_TransactionID < 0)
                            {
                                isNeedRestart = false;
                                isNeedRestartStop_Text = "TransactionID 없음";
                            }
                            else
                            {

                            }
                            try
                            {
                                value = EL_Manager_Conversion.getFloat(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Value_Chargingstop]);
                                mPaymentType = (EPaymentType)Enum.Parse(typeof(EPaymentType), mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Option_Payment]);
                                if (mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Option_Chargingstop] != null)
                                {
                                    option_ChargingStop = (EOption_ChargingStop)Enum.Parse(typeof(EOption_ChargingStop), mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Option_Chargingstop].ToString().ToUpper());

                                    mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop().setAddListener_Option_ChargingStop(this);
                                    mChannelTotalInfor.getManager_Option_ChargingStop().addOption(option_ChargingStop, value);

                                }
                            }
                            catch (Exception e)
                            {
                                //isNeedRestart = false;
                                isNeedRestartStop_Text = "Option Setting 실패 (" + e.Message + ")";
                            }
                            mCardNumber_Member =
                                mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.IdTag];
                            mChannelTotalInfor.getStateManager_Channel().mNonmember_Payment_Setting_First = (int)value;

                            ((EL_StateManager_OCPP_Channel)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getStateManager_Channel())
               .mSendManager_OCPP_ChargingReq.mOCPP_CSMS_Conf_StartTransaction.transactionId = 1;


                            switch (mPaymentType)
                            {
                                case EPaymentType.MEMBER_CARD:
                                case EPaymentType.MEMBER_NUMBER:
                                    break;
                                case EPaymentType.QRCODE:
                                    break;
                                case EPaymentType.NONMEMBER_CARDDEVICE:

                                    mChannelTotalInfor.getStateManager_Channel().mNonmember_Payment_Setting_First = (int)value;
                                    mChannelTotalInfor.getStateManager_Channel().bIsClick_SettingComplete_Payment_Value = true;

                                    mCardPayment_DealResult_First = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_AddInfor_Deal_Request_Receive();
                                    mCardPayment_DealResult_Cancel = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_DealCancel_Receive();
                                    //EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getManager_Option_ChargingStop().addOption(EOption_ChargingStop.WON, value);                                                                                                                                          

                                    //byte[] bytes = EL_Manager_Conversion.HexStringToByteHex(mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().selectRow(new string[] { CONST_TRANSACTIONINFOR.GetColumnName(CONST_TRANSACTIONINFOR.INDEX.PaymentData_Response_First) }, "_id = " + mTransactionInfor_DBId)[0][0]);
                                    byte[] bytes = EL_Manager_Conversion.HexStringToByteHex(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.PaymentData_Response_First]);

                                    EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(mChannelIndex).getSmartro_PacketManager().Packet_DealCancel_Receive.setReceiveData(bytes);
                                    break;
                                case EPaymentType.NONE:
                                    isNeedRestart = false;
                                    isNeedRestartStop_Text = ")";
                                    break;
                            }
                            bIsComplete_Payment = true;
                            bOCPP_IsReceive_Success_StartTransaction = EL_Manager_Conversion.getBoolean(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Starttransaction]);
                            bOCPP_IsReceivePacket_CallResult_StopTransaction = EL_Manager_Conversion.getBoolean(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Stoptransaction]);
                            bOCPP_IsReceivePacket_CallResult_StartTransaction = bOCPP_IsReceive_Success_StartTransaction;

                            mSendManager_OCPP_ChargingReq.bOCPP_CSMS_Conf_StartTransaction = bOCPP_IsReceive_Success_StartTransaction;
                            mSendManager_OCPP_ChargingReq.bOCPP_Conf_StopTransaction = bOCPP_IsReceivePacket_CallResult_StopTransaction;

                            if (!bOCPP_IsReceive_Success_StartTransaction)
                            {
                                isNeedRestart = false;
                                isNeedRestartStop_Text = "!bOCPP_IsReceive_Success_StartTransaction";
                            }
                            else if (bOCPP_IsReceive_Success_StartTransaction && bOCPP_IsReceivePacket_CallResult_StopTransaction)
                            {
                                isNeedRestart = false;
                                isNeedRestartStop_Text = "bOCPP_IsReceive_Success_StartTransaction && bOCPP_IsReceivePacket_CallResult_StopTransaction";
                            }

                            mSendManager_OCPP_ChargingReq.bIsSendMeterValue_First = EL_Manager_Conversion.getBoolean(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_MeterValue_First]);
                            long tempStart = EL_Manager_Conversion.getLong(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStart]);

                            if (tempStart > 0)
                            {
                                //mChannelTotalInfor.mChargingCharge.setCurrentWattage(tempStart);
                                mOCPP_MeterValue_ChargingWattage_Start = tempStart;
                            }

                            mProcess_OperatorChargeUnit_Current = EL_Manager_Conversion.getDouble(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.ChargingUnit_Current]);
                            mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().setChargeUnit(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.OperatorType]);
                            bIsCommand_UsingStart = true;
                            bIsProcessing_Using = true;
                            bIsCharging_MoreOnce = EL_Manager_Conversion.getBoolean(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Is_Charging_More_Once]);
                            bIsCharging_Current = EL_Manager_Conversion.getBoolean(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Is_Charging_Current]);

                            mChannelTotalInfor.mChargingCharge.setChargedWattage(EL_Manager_Conversion.getLong(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_Charging]));

                            if (mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStop] != null)
                            {
                                mChannelTotalInfor.mChargingCharge.setCurrentWattage(EL_Manager_Conversion.getInt(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStop]));
                            }
                            else if (mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStart] != null)
                            {
                                mChannelTotalInfor.mChargingCharge.setCurrentWattage(EL_Manager_Conversion.getLong(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStart]));
                            }
                            else
                            {
                                mChannelTotalInfor.mChargingCharge.setCurrentWattage(mChannelTotalInfor.getAMI_PacketManager().getPositive_Active_Energy_Pluswh());
                            }

                            mChannelTotalInfor.mChargingCharge.setChargedCharge(EL_Manager_Conversion.getInt(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Charge_Charging]));

                            if (mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.ChargingSequenceNumber] == null
                                || EL_Manager_Conversion.getInt(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.ChargingSequenceNumber]) !=
                                mChannelTotalInfor.getControlbdComm_PacketManager().packet_1z.m_Charge_SEQ_NUM)
                            {
                                mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
                            }
                            bIsReboot_Suddenly = true;
                            mChannelTotalInfor.mChargingTime.setWastedSecond(EL_Manager_Conversion.getLong(mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Time_Charging]));
                            if (isNeedRestartStop_Text.Length > 0)
                                Logger.d("NormalCheck => " + isNeedRestartStop_Text);

                            if (!isNeedRestart)
                            {
                                if (mTransactionInfor_DBId >= 0)
                                {
                                    mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_usingStop(
                                            mTransactionInfor_DBId, new EL_Time().getDateTime_DB());
                                    mTransactionInfor_DBId = -1;
                                }
                                setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                            }
                            else
                            {
                                if (mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.IdTag] != null &&
                                        mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Time_ChargingStart] != null &&
                                        mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.IsReceiveConf_Starttransaction] == null
                                )
                                {
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION);
                                }
                                else
                                {
                                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                                }
                            }
                        }
                        else
                        {
                            mTransactionInfor_DBId = -1;
                            mLastDeal_Infor = null;
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_READY:
                case CONST_STATE_UIFLOWTEST.STATE_READY:

                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);

                    mTransactionInfor_DBId = -1;
                    mLastDeal_Infor = null;
                    initVariable_ChargingOption_System();
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.setHMI_Manual_Control(false);
                    initVariable_UsingComplete();


                    bIsTest_Car = false;
                    Manager_SoundPlay.getInstance().stop();
                    bIsCertificationFailed = false;
                    bIsCertificationSuccess = false;

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_Select_Touch_Screen();
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Main();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_TouchScreen = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_READY + 1);
                    //                MainActivity.bNeed_UpdateUI_Main_MenuButton_Visible = true;

                    if (mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                                        &&
                                        mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar())
                        mSendManager_StatusNotification.onConnected_By_Car();
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
                    else if (getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        moveToError(Const_ErrorCode.CODE_0116_EMERGENCY, Reason.EmergencyStop, ChargePointStatus.Faulted);
                    }
                    else if (isReservation())
                    {
                        bIsReservationProcessing = true;
                        mOCPP_ReservationId = mOCPP_CSMS_Req_ReserveNow.reservationId;
                        mCardNumber_Member = mOCPP_CSMS_Req_ReserveNow.idTag;
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION);
                    }
                    else if (bOCPP_IsRemoteStartTransaction)
                    {
                        bOCPP_IsRemoteStartTransaction = false;
                        bOCPP_IsRemoteStartTransaction_Process = true;
                        initVariable_UsingStart();
                        setState(CONST_STATE_UIFLOWTEST.STATE_REMOTE_START_TRANSACTION);
                    }
                    else if (bIsCommand_UsingStart)
                    {
                        initVariable_UsingStart();
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE);
                        //                  safd
                    }
                    //if (mState != CONST_STATE_UIFLOWTEST.STATE_READY + 1
                    //    && mState != CONST_STATE_UIFLOWTEST.STATE_PREFARING
                    //    && mState != CONST_STATE_UIFLOWTEST.STATE_RESET
                    //    && mState != CONST_STATE_UIFLOWTEST.STATE_DISABLE
                    //    && mState != CONST_STATE_UIFLOWTEST.STATE_EMERGENCY
                    //    )
                    //{
                    //    initVariable_UsingStart();
                    //}


                    rebootDT = DateTime.Now;
                    //하루 1번 재부팅 03시부터 07시 사이
                    if (isTimer_Sec(TIMER_120SEC, 60) && int.Parse(rebootDT.ToString("HH")) >= 03 && int.Parse(rebootDT.ToString("HH")) < 07)
                    {
                        if (EL_DC_Charger_MyApplication.getInstance().lastRebootDate != rebootDT.ToString("yyMMdd"))
                        {
                            EL_DC_Charger_MyApplication.getInstance().lastRebootDate = rebootDT.ToString("yyMMdd");
                            EL_Manager_Application.restartSystem();
                        }
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_REMOTE_START_TRANSACTION:
                case CONST_STATE_UIFLOWTEST.STATE_REMOTE_START_TRANSACTION:
                    //                MainActivity.bNeed_UpdateUI_Main_MenuButton_Invisible = true;
                    bIsAutoProcess = true;
                    bool AuthorizeRemoteTxRequests = mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.AuthorizeRemoteTxRequests);
                    if (
                            mOCPP_CSMS_Req_RemoteStartTransaction != null &&
                                    mOCPP_CSMS_Req_RemoteStartTransaction.chargingProfile != null &&
                                    mOCPP_CSMS_Req_RemoteStartTransaction.chargingProfile.transactionId != null
                    )
                    {
                        mOCPP_TransactionID = mOCPP_CSMS_Req_RemoteStartTransaction.chargingProfile.transactionId;
                    }
                    mCardNumber_Read_Temp = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                    //인증 후 충전
                    if (AuthorizeRemoteTxRequests)
                    {


                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                            "서버에서 요청한 사용자 인증중입니다.\r\n잠시만 기다려주세요.", false);
                        //getMyApplication().mPageManager_Main.mContentLayout_Notify_1Tv
                        //        .setTv_Content_1("서버에서 요청한 사용자 인증중입니다.\r\n잠시만 기다려주세요.");
                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;

                        //mSendManager_StatusNotification.setDelay_First(0);
                        //mSendManager_OCPP_Authorize.setDelay_First(5);
                        mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2);
                    }
                    else//즉시 충전
                    {
                        mCardNumber_Member = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_RESERVATION:
                case CONST_STATE_UIFLOWTEST.STATE_RESERVATION:
                    bIsReservationProcessing = true;
                    bIsReservation = false;
                    bIsClick_Notify_1Button = false;
                    bIsComplete_RFCard_Read = false;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Reserved);

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                            "예약중입니다.\r\n확인 버튼을 눌러 인증을 시작해 주세요.", false, this);
                    //getMyApplication().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                    //        .setTv_Content_1("예약중입니다.\r\n확인 버튼을 눌러 인증을 시작해 주세요.", this);
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                    //getMyApplication().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn.setOnClickListener_NotifyButton(this);
                    //                MainActivity.bNeed_UpdateUI_Main_MenuButton_Invisible = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 1:

                    if (bOCPP_IsRemoteStartTransaction)
                    {
                        bOCPP_IsRemoteStartTransaction = false;
                        bOCPP_IsRemoteStartTransaction_Process = true;
                        if (mOCPP_CSMS_Req_RemoteStartTransaction.idTag.Equals(mOCPP_CSMS_Req_ReserveNow.idTag))
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 3);
                            bIsCertificationSuccess = true;
                        }
                        else
                        {
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag();
                            //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                            mCardNumber_Read_Temp = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                            setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 2);
                            bIsComplete_RFCard_Read = true;
                        }
                    }
                    else if (!isReservation()
                          || isNeedReset()
                          || isTimer_Sec(TIMER_2SEC, 2))
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsClick_Notify_1Button)
                    {
                        //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag();
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(this);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.CARD_CHECK);
                        //getMyApplication().getCommPort_RFIDReader().setRFCardReader_Listener(this);
                        //getMyApplication().getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_FOREVER;
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 2);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 2:
                    if (!isReservation())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION);
                    }
                    else if (bIsComplete_RFCard_Read)
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv("사용자 인증중입니다.\r\n잠시만 기다려주세요.", true);

                        //getMyApplication().mPageManager_Main().mContentLayout_Notify_1Tv
                        //        .setTv_Content_1("사용자 인증중입니다.\r\n잠시만 기다려주세요.");
                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                        mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 3);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 3:
                    if (!isReservation())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION);
                    }
                    else if (bIsCertificationSuccess)
                    {
                        if (bOCPP_IsRemoteStartTransaction_Process)
                        {
                            mCardNumber_Member = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                        }
                        else
                        {
                            initVariable_UsingStart();
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                        }
                    }
                    else if (bIsCertificationFailed)
                    {
                        if (bOCPP_IsRemoteStartTransaction_Process)
                        {
                            bIsClick_Notify_1Button = false;

                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "잘못된 RemoteStartTransaction입니다..", true, this);

                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                            //        .setTv_Content_1("잘못된 RemoteStartTransaction입니다..", this);
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                            setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 4);
                        }
                        else
                        {
                            bIsClick_Notify_1Button = false;
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n예약된 사용자가 아닙니다.", true, this);
                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                            //        .setTv_Content_1("카드를 확인 해 주세요.\r\n예약된 사용자가 아닙니다.", this);
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                            setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 4);
                        }
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 4:
                    if (!isReservation())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsClick_Notify_1Button || isTimer_Sec(TIMER_WAITTIME, 30))
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_SELECT_CONNECTORTYPE:
                case CONST_STATE_UIFLOWTEST.STATE_SELECT_CONNECTORTYPE:
                    //                MainActivity.bNeed_UpdateUI_Main_MenuButton_Invisible = true;
                    //                if (!bIsAutoProcess)
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Visible = true;
                    //                else
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    //
                    //                MainActivity.bNeed_UpdateUI_Main_MenuButton_Invisible = true;



                    //MainActivity.bNeed_UpdateUI_Main_Select_ConnectorType = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_SELECT_CONNECTORTYPE + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_SELECT_CONNECTORTYPE + 1:
                    if (
                            !bIsCommand_UsingStart
                                    || isTimer_Sec(TIMER_WAITTIME, mDelay_Reset)
                                    || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsCommand_ChargingStop)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsSelected_ConnectorType)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE);
                    }
                    else if (getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE:
                //case CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE:

                //    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                //    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                //    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Select_Member();
                //    setState(CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE + 1);
                //    break;
                //case CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE + 1:
                //    if (!bIsCommand_UsingStart
                //            || isTimer_Sec(TIMER_WAITTIME, mDelay_Reset) || bIsClick_BackButton || bIsClick_HomeButton)
                //    {
                //        setState(CONST_STATE_UIFLOWTEST.STATE_READY);                        
                //    }
                //    else if (mMemberType != EMemberType.NONE)
                //    {
                //        switch (mMemberType)
                //        {
                //            default:
                //            case EMemberType.MEMBER:
                //                setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG);                                
                //                break;
                //            case EMemberType.NONMEMBER:
                //                setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE);
                //                HistoryDBHelper.setHistoryUpdate(mDbId, HistoryDBHelper.member_yn, "N");
                //                break;
                //        }
                //    }

                //    //if (mState != (CONST_STATE_UIFLOWTEST.STATE_READY + 1))
                //    //{
                //    //    MainActivity.bNeed_UpdateUI_Main_VersionDisplay_Invisible = true;
                //    //}
                //    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE:
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE:
                    getMyApplication().mPageManager_Main().setView_NonMember_Select_PaymnetType();
                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE + 1:
                    if (mPaymentType != EPaymentType.NONE)
                    {
                        switch (mPaymentType)
                        {
                            case EPaymentType.NONE:
                                break;
                            case EPaymentType.MEMBER_CARD:
                                setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG);
                                break;
                            case EPaymentType.QRCODE:
                                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_PaymentOption_QrCode(mTransactionInfor_DBId);
                                setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE);
                                break;
                            case EPaymentType.NONMEMBER_CARDDEVICE:
                                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_PaymentOption_CardDevice(mTransactionInfor_DBId);
                                setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE);
                                setProcess_OperatorType(OperatorType.NM.ToString());
                                break;
                        }
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, mDelay_Reset))
                    {

                    }
                    break;
                #endregion

                //QR코드 요청
                #region >> case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE:
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE:
                    wev_bNonmember_Receive_NPQ1_Conf = false;
                    wev_bNonmember_Receive_NPQ2_Req = false;
                    wev_bNonmember_Receive_NPQ3_Req = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                            "접속정보를 확인중입니다.\r\n잠시 후 간편결제를 위한 QR코드가 표시됩니다.", true);
                    mSendManager_OCPP_Wev.sendOCPP_CP_Req_NPQ1_Req();
                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 1:
                    if (wev_bNonmember_Receive_NPQ1_Conf)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 2);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, 600))
                    {

                    }
                    break;

                //사용자 결제 시작 (NPQ2 대기)
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 2:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().
                        setView_MainPanel_Include_ChargingMain_QRCode(wev_mNonmember_Receive_NPQ1_Conf.qr);
                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 3);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 3:
                    if (wev_bNonmember_Receive_NPQ2_Req)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 4);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, 600))
                    {

                    }
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 4:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main()
                        .setView_MainPanel_Include_1Tv("결제 진행중입니다.\n결제가 완료되면 자동으로 사용 시작됩니다.\n" +
                                "정산은 결제방법에 따라 2~7 영업일 소요될 수 있습니다.", true);
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 5);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 5:
                    if (wev_bNonmember_Receive_NPQ3_Req)
                    {
                        if (wev_mNonmember_Receive_NPQ3_Req.paymentResult == 1)
                        {
                            setProcess_OperatorType(wev_mNonmember_Receive_NPQ3_Req.operatorType);

                            mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Option_ChargingStop(
                                    mTransactionInfor_DBId,
                                    wev_mNonmember_Receive_NPQ3_Req.chargingLimitProfile,
                                    wev_mNonmember_Receive_NPQ3_Req.unitAmount);

                            mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop().setAddListener_Option_ChargingStop(this);
                            mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop()
                                    .addOption(wev_mNonmember_Receive_NPQ3_Req);

                            setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 6);
                            bIsComplete_Payment = true;
                        }
                        else
                        {
                            moveToError(Const_ErrorCode.CODE_0013_NONMEMBER_PAYMENT_ERROR,
                                    "QR코드 결제 오류",
                                    Reason.Other, ChargePointStatus.Faulted);
                        }

                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, 600))
                    {

                    }
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_QRCODE + 6:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main()
                        .setView_MainPanel_Include_1Tv("결제가 완료되었습니다..\n자동으로 충전을 시작하기 전까지 기다려 주세요.", true);
                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_WAIT_CERTIFICATION);
                    //.                    
                    break;

                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE:
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE:

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_NonMember_Input_Payment_Amount();

                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 1:
                    if (bIsClick_SettingComplete_Payment_Value)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 2);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, 600))
                    {

                    }
                    break;
                //카드결제 시작
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 2:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                        "신용카드 혹은 스마트폰, 간편결제를 이용해서 결제해 주세요. \r\n (" + mNonmember_Payment_Setting_First + "원)",
                        true);

                    mApplication.SerialPort_Smartro_CardReader.setCommand_Pay_First(mNonmember_Payment_Setting_First);
                    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 3);
                    break;
                //카드결제 진행 여부 확인
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 3:
                    //if (isTimer_Sec(TIMER_WAITTIME, 90) || bIsClick_BackButton || bIsClick_HomeButton)
                    //{
                    //    setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE);
                    //    //결제대기

                    //}
                    if (mApplication.SerialPort_Smartro_CardReader.isCommand_Complete())
                    {
                        if (mApplication.CardDevice_Manager.isCommand_ErrorOccured())
                        {

                            mCardPayment_DealResult_First = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_AddInfor_Deal_Request_Receive();
                            mErrorMessage = mApplication.CardDevice_Manager.getErrorCode_String();
                            ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                            moveToError(Const_ErrorCode.CODE_0013_NONMEMBER_PAYMENT_ERROR,
                            mErrorMessage,
                            Reason.Other, ChargePointStatus.Faulted);


                            //ocpp_setErrorOccured(Const_ErrorCode.CODE_0013_NONMEMBER_PAYMENT_ERROR,
                            //    Reason.Other, ChargePointStatus.Faulted);
                            //setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                        }
                        else
                        {
                            mCardPayment_DealResult_First = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_AddInfor_Deal_Request_Receive();
                            bIsComplete_Payment = true;
                            Req_NPS1 nps1 = new Req_NPS1();
                            nps1.setRequiredValue(mChannelIndex, "Won", (Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request)mCardPayment_DealResult_First);
                            mSendManager_OCPP_Wev.sendOCPP_CP_Req_NPS1_Req(nps1);
                            setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 4);
                            bIsState_WaitRemoteStart = true;
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main()
                                .setView_MainPanel_Include_1Tv("서버 충전시작 대기중입니다.\r\n잠시만 기다려 주세요..", false);
                        }
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, 600))
                    {

                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 4:
                    if (bOCPP_IsRemoteStartTransaction)
                    {
                        bOCPP_IsRemoteStartTransaction = false;
                        bOCPP_IsRemoteStartTransaction_Process = true;
                        mCardNumber_Member = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT);
                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, 60))
                    {
                        //                    bOCPP_IsTransactionStart_Manual = true;
                        moveToError(Const_ErrorCode.CODE_0018_OVERWAITTIME,
                                "카드결제완료 후 충전시작명령 없음(TimeOut)",
                                Reason.Other, ChargePointStatus.Faulted);
                    }

                    if (mState != (CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE + 4))
                        bIsState_WaitRemoteStart = false;
                    break;
                #endregion

                #region STATE_NONMEMBER_WAIT_CERTIFICATION
                case CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_WAIT_CERTIFICATION:
                    if (bOCPP_IsRemoteStartTransaction)
                    {
                        bOCPP_IsRemoteStartTransaction = false;
                        bOCPP_IsRemoteStartTransaction_Process = true;
                        mCardNumber_Member = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, 600))
                    {

                    }
                    else if (bIsClick_Notify_1Button)
                    {
                        moveToError(Const_ErrorCode.CODE_0002_USER_COMPLETE,
                                Reason.Other, ChargePointStatus.Faulted);
                    }

                    if (mState != (CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_WAIT_CERTIFICATION))
                        bIsState_WaitRemoteStart = false;
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG:
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG:
                    bIsCertificationSuccess = false;
                    bIsCertificationFailed = false;
                    //                if (!bIsAutoProcess)
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Visible = true;
                    //                else
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(this);
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.CARD_CHECK);

                    //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(this);
                    //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_FOREVER;
                    bIsComplete_RFCard_Read = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 1:
                    if (bIsComplete_RFCard_Read)
                    {
                        bIsComplete_Certification = true;
                        //mCardNumber_Read_Temp = "1600000003310954";
                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_idTag_Card(mTransactionInfor_DBId, mCardNumber_Read_Temp, EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mPaymentType.ToString());
                        if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.LocalPreAuthorize))
                        {
                            mOCPP_AuthCache_State
                                = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().getIdTag_DB_State(mCardNumber_Read_Temp);
                            switch (mOCPP_AuthCache_State)
                            {
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.NOT_EXIST:
                                    if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.AllowOfflineTxForUnknownId))
                                    {
                                        //캐쉬에 인증 내역이 없을 경우 그냥 충전진행
                                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT);
                                        bIsCertificationComplete = true;
                                    }
                                    else
                                    {
                                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                            "회원 인증중입니다.\r\n잠시만 기다려주세요.", true);
                                        //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                                        //        .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");

                                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                                        //mSendManager_OCPP_Authorize.setOCPP_Authorize_Listener(null);
                                        mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2);
                                    }
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST:
                                    mProcess_OperatorType = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().getOperatorType(mCardNumber_Read_Temp);
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT);
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST_EXPIRY:
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.ERROR:

                                    bIsClick_Notify_1Button = false;
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", false, this);
                                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                                    //        .setTv_Content_1("카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", this);
                                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                                    break;
                            }
                        }
                        else
                        {
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "회원 인증중입니다.\r\n잠시만 기다려주세요.", true);
                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                            //        .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                            mCardNumber_Member = mCardNumber_Read_Temp;
                            mSendManager_OCPP_Authorize.setOCPP_Authorize_Listener(null);
                            mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2);
                        }


                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, mDelay_Reset))
                    {

                    }

                    if (mState != CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 1)
                    {
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                        //getMyApplication().getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
                        //getMyApplication().getCommPort_RFIDReader().setRFCardReader_Listener(null);
                    }
                    break;


                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2:
                    if (bIsCertificationSuccess)
                    {
                        if (mCardNumber_Member == null)
                            mCardNumber_Member = mCardNumber_Read_Temp;

                        if (mOCPP_Conf_Authorize != null && mOCPP_Conf_Authorize.moreAuthorizeConf != null)
                        {
                            mApplication.getChannelTotalInfor(mChannelIndex).getManager_Option_ChargingStop().setAddListener_Option_ChargingStop(this);
                            mSendManager_OCPP_Wev.setChargeUnit_ChangeListener(mChannelTotalInfor.mChargingCharge);
                            setProcess_OperatorType(mOCPP_Conf_Authorize.moreAuthorizeConf.operatorType);
                        }



                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                    }
                    else if (bIsCertificationFailed)
                    {
                        bIsClick_Notify_1Button = false;
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아닙니다.", false, this);
                        //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                        //        .setTv_Content_1("카드를 확인 해 주세요.\r\n회원카드가 아닙니다.", this);
                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, DELAY_OFFLINE))
                    {
                        //사용자 인증 데이터가 오지 않았기 때문에 Offline으로 판단함
                        if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.LocalAuthorizeOffline))
                        {
                            setIsOffLine(true);
                            mOCPP_AuthCache_State
                                    = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().getIdTag_DB_State(mCardNumber_Read_Temp);
                            switch (mOCPP_AuthCache_State)
                            {
                                case EIDTAG_DB_STATE.NOT_EXIST:
                                    if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.AllowOfflineTxForUnknownId))
                                    {
                                        //캐쉬에 인증 내역이 없을 경우 그냥 충전진행
                                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT);
                                        bIsCertificationComplete = true;
                                    }
                                    else
                                    {
                                        bIsClick_Notify_1Button = false;
                                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "인증 시간이 지났습니다.\r\n다시 시도해 주세요.", false, this);
                                        //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                                        //        .setTv_Content_1("인증 시간이 지났습니다.\r\n다시 시도해 주세요.", this);
                                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                                    }
                                    break;
                                case EIDTAG_DB_STATE.EXIST:
                                    bIsCertificationComplete = true;
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT);
                                    break;
                                case EIDTAG_DB_STATE.EXIST_EXPIRY:
                                case EIDTAG_DB_STATE.ERROR:
                                    bIsClick_Notify_1Button = false;
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", false, this);
                                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                                    //        .setTv_Content_1("카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", this);
                                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                                    break;
                            }
                        }
                        else
                        {
                            bIsClick_Notify_1Button = false;
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "인증 시간이 지났습니다.\r\n다시 시도해 주세요.", false, this);
                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                            //        .setTv_Content_1("인증 시간이 지났습니다.\r\n다시 시도해 주세요.", this);
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                        }
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3:
                    if (!bIsCommand_UsingStart
                            || bIsClick_Notify_1Button || isTimer_Sec(TIMER_WAITTIME, 60))
                    {
                        moveToError(Const_ErrorCode.CODE_0016_CLICK_BACKBUTTON);
                    }
                    break;
                #endregion













                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT:

                    if (mTime_SetState.getSecond_WastedTime() < 3) return;

                    bIsCertificationComplete = true;
                    bool settingUnit = mChannelTotalInfor.mChargingCharge.setCurrentChargeUnitByDatabase(mProcess_OperatorType);
                    mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_OperatorType(
                            mTransactionInfor_DBId, mProcess_OperatorType);
                    if (!settingUnit ||
                            (
                                mProcess_OperatorChargeUnit_Current != mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().getChargingUnit_Current()
                                    && mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().mMemberType_String.Length <= 0
                            ) || !mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().IsSettingValue
                    )
                    {
                        mProcess_OperatorChargeUnit_Current = -1;
                        mSendManager_OCPP_Wev.setChargeUnit_ChangeListener(mChannelTotalInfor.mChargingCharge);
                        mSendManager_OCPP_Wev.sendOCPP_CP_Req_CTP1_Req(mProcess_OperatorType);
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main()
                        .setView_MainPanel_Include_1Tv("단가 정보를 수신중입니다.\r\n잠시만 기다려주세요.", true);
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT + 1);
                    }
                    else
                    {

                        mProcess_OperatorChargeUnit_Current = mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().getChargingUnit_Current();
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main()
                        .setView_MainPanel_Include_1Tv("아래 충전단가로 충전을 진행합니다..\r\n잠시만 기다려주세요.\r\n\r\n(" + mProcess_OperatorChargeUnit_Current + "원)", true);
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT + 2);

                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_ChargingUnit_Start("" + mTransactionInfor_DBId, mProcess_OperatorChargeUnit_Current);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT + 1:
                    if (mProcess_OperatorChargeUnit_Current <= 0 && mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().IsSettingValue)
                    {
                        mProcess_OperatorChargeUnit_Current = mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().getChargingUnit_Current();
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv
                                ("아래 충전단가로 충전을 진행합니다..\r\n잠시만 기다려주세요.\n\r\n" + mProcess_OperatorChargeUnit_Current + "원", true);
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT + 2);
                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_ChargingUnit_Start("" + mTransactionInfor_DBId, mProcess_OperatorChargeUnit_Current);
                    }
                    else if (mTime_SetState.getSecond_WastedTime() > 15)
                    {
                        mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().setChargeUnit(mProcess_OperatorType);
                        mProcess_OperatorChargeUnit_Current = mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().getChargingUnit_Current();

                        if (!mChannelTotalInfor.mChargingCharge.getChargeUnit_Process().IsSettingValue)
                        {
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv
                                ("단가정보는 정보 수신 후 충전중 화면에서 표시됩니다.\r\n충전 후 충전단가를 확인 해 주세요.", true);
                        }
                        else
                        {
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv
                                ("아래 충전단가로 충전을 진행합니다..\r\n잠시만 기다려주세요.\n\r\n" + mProcess_OperatorChargeUnit_Current + "원", true);
                        }
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT + 2);
                        mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_ChargingUnit_Start("" + mTransactionInfor_DBId, mProcess_OperatorChargeUnit_Current);

                    }
                    else if (processCondition_AfterUsing_BeforeCharging(false, false, -1))
                    {

                    }
                    break;


                case CONST_STATE_UIFLOWTEST.STATE_WAIT_RECEIVE_CHARGEUNIT + 2:
                    if (mTime_SetState.getSecond_WastedTime() > 5)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(false, false, -1))
                    {

                    }
                    break;

                //////////////////////////////////////////////////////////////
                ///
                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR:


                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR:
                    mSendManager_OCPP_Authorize.clearAll();
                    if (mCardNumber_Member == null || mCardNumber_Member.Length < 1)
                    {
                        mCardNumber_Member = mCardNumber_Read_Temp;
                    }
                    if (mSendManager_OCPP_Authorize.mOCPP_CSMS_Conf_Authorize != null
                            && mSendManager_OCPP_Authorize.mOCPP_CSMS_Conf_Authorize.idTagInfo != null
                            && mSendManager_OCPP_Authorize.mOCPP_CSMS_Conf_Authorize.idTagInfo.parentIdTag != null)
                    {
                        mOCPP_ParentIdTag = mSendManager_OCPP_Authorize.mOCPP_CSMS_Conf_Authorize.idTagInfo.parentIdTag;
                    }
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Preparing);
                    //                if (!bIsAutoProcess)
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Visible = true;
                    //                else
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Connect_Connector();

                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1);
                    mDelay_ConnectionTimeOut = mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.ConnectionTimeOut);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1:
                    if (mTime_SetState.getSecond_WastedTime() < 2)
                        return;

                    if (
                        (
                            (mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                                    &&
                                    mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                                    &&
                                    !bIsTest_Car)
                                    ||
                                    bIsTest_Car)
                                ||
                            bIsTest_Car)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, mDelay_Reset))
                    {

                    }


                    if (mState == (CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800))
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().mUC_Charging_Connect_Connector[mChannelIndex - 1].updateView();
                    }
                    break;
                #endregion
                //////////////////////////////////////////////////////////////


                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION:
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                        "거래 진행중입니다.\r\n잠시만 기다려주세요.", true);
                    initVariable_TransactionStart();
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION + 1:
                    if (mTime_SetState.getSecond_WastedTime() < 3) return;
                    if (mSendManager_OCPP_ChargingReq.bOCPP_CSMS_Conf_StartTransaction)
                    {
                        switch (mSendManager_OCPP_ChargingReq.getOCPP_CSMS_Conf_StartTransaction().idTagInfo.status)
                        {

                            case AuthorizationStatus.Accepted:
                                setIsOffLine(false);
                                bOCPP_IsReceive_Success_StartTransaction = true;
                                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                                break;
                            case AuthorizationStatus.Blocked:
                            case AuthorizationStatus.Expired:
                            case AuthorizationStatus.Invalid:
                            case AuthorizationStatus.ConcurrentTx:
                                bOCPP_IsReceive_Success_StartTransaction = false;
                                moveToError(
                                        Const_ErrorCode.CODE_0012_SERVER_CERTIFICATION_ERROR,
                                        Reason.DeAuthorized,
                                        ChargePointStatus.Finishing);
                                break;
                        }
                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, DELAY_OFFLINE))
                    {
                        if (bIsOffLine)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                        }
                        else
                        {
                            if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.LocalAuthorizeOffline))
                            {
                                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                            }
                            else
                            {
                                setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                            }
                        }
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, -1))
                    {

                    }

                    break;
                #endregion






                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE:
                    if (mLastDeal_Infor == null)
                        getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 1;

                    if (bOCPP_IsReceive_Success_StartTransaction)
                        initVariable_TransactionStart_Complete();

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Preparing_Charging();
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1);
                    //getMyApplication().mPageManager_Main().mContentLayout_Prepare_Charging.setRemainSecond(60);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1:

                    if (mTime_SetState.getSecond_WastedTime() < 3)
                        return;
                    if (
                        !mApplication.getChannelTotalInfor(1).getEV_State().isState_ConnectedCar()
                                && !bIsTest_Car
                                && mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.StopTransactionOnEVSideDisconnect)
                        )
                    {
                        moveToError(
                                Const_ErrorCode.CODE_0003_CONNECTOR_DISCONNECT,
                                Reason.EVDisconnected,
                                ChargePointStatus.Finishing);
                    }
                    else if (
                            (mApplication.getChannelTotalInfor(1).getEV_State().isState_ChargingCar()
                                    && !bIsTest_Car
                            )
                                    ||
                                    bIsTest_Car)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                    }
                    else if (processCondition_AfterUsing_BeforeCharging(true, true, -1))
                    {

                    }
                    else if (
                       (typeof(P1080_1920_UC_ChargingMain_Include_Preparing_Charging)
                           == ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Charging_Preparing_Charging.GetType())
                           &&
                       ((P1080_1920_UC_ChargingMain_Include_Preparing_Charging)((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Charging_Preparing_Charging[mChannelIndex - 1]).getRemainTime() < 0)
                    {
                        moveToError(
                                Const_ErrorCode.CODE_0014_OVERWAITTIME_CONNECTCONNECTOR,
                                Reason.Other,
                                ChargePointStatus.Finishing);
                    }
                    else if (mChannelTotalInfor.getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 == 0)
                    {
                        moveToError(
                                "충전명령 없음.",
                                Reason.Other,
                                ChargePointStatus.Finishing);
                    }


                    if (isTimer_MiliSec(TIMER_800_UIUPDATE, 800)
                            && mState == (CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1))
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().mUC_Charging_Preparing_Charging[mChannelIndex - 1].updateView();
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING:
                case CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING:
                    initVariable_Pre_ChargingComplete();
                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1);
                    bIsClick_Notify_1Button = false;
                    initVariable_ChargingComplete();

                    StringBuilder errorMessage = new StringBuilder("충전 준비 중 오류가 발생했습니다..\r\n오류 내용을 확인 후 확인버튼을 눌러 주세요.\r\n");
                    if (mErrorMessage.Length < 1)
                        errorMessage.Append(mErrorMessage_Detail);
                    else
                        errorMessage.Append(mErrorMessage);


                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Error_Before_Charging(errorMessage.ToString());
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                    //        .setTv_Content_1("충전 준비 중 오류가 발생했습니다..\r\n오류 내용을 확인 후 확인버튼을 눌러 주세요.", this);
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1);
                    initVariable_UsingComplete();
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1:
                    if (isTimer_Sec(TIMER_WAITTIME, 100) || bIsClick_Notify_1Button)
                    {
                        if (bIsComplete_Payment)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                        }
                        else
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                    }

                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING:

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Charging();

                    initVariable_ChargingStart();
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1);

                    break;
                #endregion


                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1:
                    if (!bIsChargingStart_By_ChargingWattage)
                    {
                        //if (mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_1z.bFlag1_AMI_Comm_Normal)
                        bIsChargingStart_By_ChargingWattage = true;//mApplication.getChannelTotalInfor(1).mChargingWattage.setChargingStart();

                        if (bIsChargingStart_By_ChargingWattage)
                        {
                            if (mTransactionInfor_DBId > 0)
                            {
                                if (mLastDeal_Infor == null
                                    || (mLastDeal_Infor != null && mLastDeal_Infor[CONST_TRANSACTIONINFOR.INDEX.Wattage_ChargingStart] == null)
                                )
                                {
                                    mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_ChargingStart(
                                            mTransactionInfor_DBId, mTime_ChargingStart.getDateTime_DB(),
                                            mApplication.getChannelTotalInfor(1).getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                                            mChannelTotalInfor.getEV_State().getSOC(),
                                            mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_1z.m_Charge_SEQ_NUM,
                                            mProcess_OperatorChargeUnit_Current
                                    );
                                }
                            }
                        }
                        mSendManager_OCPP_ChargingReq.setOCPP_MeterValue_Sample_Charging();
                        setTimer_Sec(TIMER_MeterValueSampleInterval);
                        mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction();
                    }

                    if (processCondition_Charging())
                    {

                    }
                    else if (bIsClick_ChargingStop_User)
                    {
                        moveToError(
                            Const_ErrorCode.CODE_0002_USER_COMPLETE,
                            Reason.Local,
                            ChargePointStatus.Finishing
       );
                    }

                    if (mState == (CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800))
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().mUC_Charging_Charging[mChannelIndex - 1].updateView();

                        //MainActivity.bNeed_UpdateUI_Main_Update_ChargingInfor = true;
                    }
                    break;


                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 10:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 10:
                    bIsClick_ChargingStop_User = false;
                    bIsCertificationSuccess = false;
                    bIsCertificationFailed = false;
                    //                if(!bIsAutoProcess)
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Visible = true;

                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(this);
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.CARD_CHECK);


                    //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(this);
                    //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_FOREVER;
                    bIsComplete_RFCard_Read = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 11);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 11:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 11:
                    if (processCondition_Charging())
                    {

                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, mDelay_Reset) || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                    }
                    else if (bIsComplete_RFCard_Read)
                    {
                        if (mCardNumber_Read_Temp.Equals(mCardNumber_Member))
                        {
                            mCardNumber_Member = mCardNumber_Read_Temp;
                            ocpp_setErrorOccured(
                                    Const_ErrorCode.CODE_0002_USER_COMPLETE,
                                    Reason.Local,
                                    ChargePointStatus.Finishing
                            );

                        }
                        else
                        {
                            if (mOCPP_ParentIdTag != null)
                            {
                                if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.LocalPreAuthorize))
                                {
                                    OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE state
                                            = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().getParentIdTag_DB_State(mOCPP_ParentIdTag);
                                    switch (state)
                                    {
                                        case EIDTAG_DB_STATE.NOT_EXIST:
                                        case EIDTAG_DB_STATE.EXIST_EXPIRY:
                                        case EIDTAG_DB_STATE.ERROR:
                                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                                            break;
                                        case EIDTAG_DB_STATE.EXIST:
                                            mCardNumber_Member = mCardNumber_Read_Temp;
                                            ocpp_setErrorOccured(
                                                    Const_ErrorCode.CODE_0002_USER_COMPLETE,
                                                    Reason.Local,
                                                    ChargePointStatus.Finishing
                                            );
                                            break;
                                    }
                                }
                                else
                                {
                                    //parentIdTag 비교해야되나
                                    getMyApplication().mPageManager_Main().mUC_Notify_1Tv[mChannelIndex - 1].setText(0, "회원 인증중입니다.\r\n잠시만 기다려주세요.");
                                    mOCPP_StopTransaction_Reason = Reason.Local;
                                    mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize(new OCPP_ConfAuthorize_Listener_Charging11_Case1(this));
                                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12);
                                }
                            }
                            else
                            {
                                getMyApplication().mPageManager_Main().mUC_Notify_1Tv[mChannelIndex - 1].setText(0, "회원 인증중입니다.\r\n잠시만 기다려주세요.");
                                //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                                mOCPP_StopTransaction_Reason = Reason.Local;

                                mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize(new OCPP_ConfAuthorize_Listener_Charging11_Case2(this));
                                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12);
                            }



                            //                    if (mCardNumber_Member.equals(mCardNumber_Read_Temp))
                            //                    {
                            //                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                            //                    }else
                            //                    {
                            //                        SC_1CH_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                            //                                .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");
                            //                        MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1 = true;
                            //                        sendOCPP_CP_Req_Authorize();
                            //                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12);
                            //                    }
                        }
                    }


                    if (mState != CONST_STATE_UIFLOWTEST.STATE_CHARGING + 11)
                    {
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                        //getMyApplication().getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
                        //getMyApplication().getCommPort_RFIDReader().setRFCardReader_Listener(null);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12:
                    if (processCondition_Charging())
                    {

                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, DELAY_OFFLINE) || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                    }

                    if (mState != CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12)
                    {
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                        //getMyApplication().getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
                        //getMyApplication().getCommPort_RFIDReader().setRFCardReader_Listener(null);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE:
                    initVariable_Pre_ChargingComplete();
                    bIsSelected_Confirm_ChargingComplete = false;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);

                    mApplication.getDataManager_CustomUC_Main().mUC_Charging_Charging_Complete[mChannelIndex - 1].updateView();
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Charging_Complete(mErrorReason);


                    if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                        initVariable_ChargingComplete();
                    }
                    else if (getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        initVariable_ChargingComplete();
                    }
                    else
                    {
                        switch (mPaymentType)
                        {
                            case EPaymentType.MEMBER_CARD:
                            case EPaymentType.MEMBER_NUMBER:
                            case EPaymentType.NONE:
                            case EPaymentType.QRCODE:
                                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1);
                                initVariable_ChargingComplete();
                                break;
                            case EPaymentType.NONMEMBER_CARDDEVICE:
                                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 20);
                                break;
                        }

                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1:
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (
                        !bIsCommand_UsingStart
                            ||
                        bIsSelected_Confirm_ChargingComplete
                            ||
                        (!mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                            &&
                            !bIsTest_Car)
                            ||
                        getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed()
                    )
                        {
                            switch (mPaymentType)
                            {
                                case EPaymentType.MEMBER_NUMBER:
                                case EPaymentType.MEMBER_CARD:
                                case EPaymentType.NONE:
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                                    break;
                                case EPaymentType.QRCODE:
                                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 10);
                                    break;
                                case EPaymentType.NONMEMBER_CARDDEVICE:
                                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 20);
                                    break;
                            }
                        }


                    }
                    break;
                //Nonmember QR결제
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 10:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn("문자 혹은 카카오톡을 통해 정산내역을 확인해 주세요.", false, null);
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 11);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 11:
                    if ((!bIsCommand_UsingStart) || bIsClick_Notify_1Button || getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 20:
                    int chargedValue = getMyApplication().getChannelTotalInfor(1).mChargingWattage.getChargingCharge();
                    int diff = mNonmember_Payment_Setting_First - chargedValue;
                    if (diff > 0)
                    {
                        //충전이 하나도 안되어 있으면
                        if (diff >= mNonmember_Payment_Setting_First)
                        {
                            mApplication.CardDevice_Manager.setCommand_Pay_Cancel(mCardPayment_DealResult_First);
                        }
                        //충전이 조금이라도 되어있으면
                        else
                        {
                            mApplication.CardDevice_Manager.setCommand_Pay_Cancel(diff, mCardPayment_DealResult_First);
                        }
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 21);
                    }
                    //선결제금액만큼 충전되어있으면(차액이 0이거나 0보다 작으면)
                    else
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 30);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 21:
                    if (isTimer_Sec(TIMER_WAITTIME, 30) || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_CARDDEVICE);
                    }
                    else if (mApplication.SerialPort_Smartro_CardReader.isCommand_Complete())
                    {
                        if (mApplication.CardDevice_Manager.isCommand_ErrorOccured())
                        {
                            mCardPayment_DealResult_Cancel = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_DealCancel_Receive();
                        }
                        else
                        {
                            mCardPayment_DealResult_Cancel = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_DealCancel_Receive();
                        }

                        Req_NPS2 nps2 = new Req_NPS2();
                        nps2.setRequiredValue(
                            mChannelIndex,
                            (Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request)mCardPayment_DealResult_First,
                            (Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request)mCardPayment_DealResult_Cancel
                        );
                        mSendManager_OCPP_Wev.sendOCPP_CP_Req_NPS2_Req(nps2);

                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 30);
                    }
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 30:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Calculate_Charge();
                    initVariable_ChargingComplete();
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 31);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 31:
                    if (isTimer_Sec(TIMER_WAITTIME, 1800) || bIsClick_Notify_1Button || getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                    }
                    break;

                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR:
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR:
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    getMyApplication().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Disconnect_Connector();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_SeperateConnector = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1:
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (
                                !bIsCommand_UsingStart ||
                                        (
                                        (mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                                        &&
                                        !mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                                                &&
                                                !bIsTest_Car)
                                        ||
                                        bIsTest_Car)
                                || bIsClick_BackButton || (getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE);
                        }
                    }
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().mUC_Charging_Disconnect_Connector[mChannelIndex - 1].updateView();
                    //MainActivity.bNeed_UpdateUI_Main_Update_ChargingInfor = true;                    
                    //if (mState == (CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800))
                    //{
                    //    MainActivity.bNeed_UpdateUI_Main_Update_ProgressBar_DisconnectConnector = true;
                    //}
                    //                else if(getMyApplication().getChannelInfor_ChargerTotal(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    //                {
                    //                    setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    //                }
                    break;
                #endregion


                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE:
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE:
                    getMyApplication().mPageManager_Main().setView_MainPanel_Include_ChargingMain_UseComplete();
                    //MainActivity.bNeed_UpdateUI_Main_Wait_UseComplete = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE + 1);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE + 1:
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE + 1:
                    if ((isTimer_Sec(TIMER_WAITTIME, 1)))
                    {
                        if (!bIsCommand_UsingStart || mSendManager_OCPP_ChargingReq.getCount_SendList() < 1)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                    }
                    else if ((isTimer_Sec(TIMER_90SEC, 30)))
                    {
                        mSendManager_OCPP_ChargingReq.clearAll();
                        mSendManager_OCPP_Wev.clearAll();
                        mSendManager_OCPP_Authorize.clearAll();
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY:
                case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY:
                    setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1);
                    getMyApplication().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Emergency();
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1:
                case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1:

                    if (!mApplication.DI_Manager.isEmergencyPushed())
                    {
                        moveToError(Const_ErrorCode.CODE_0116_EMERGENCY, Reason.EmergencyStop, ChargePointStatus.Faulted);
                        if (bIsCharging_MoreOnce)
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                        else
                        {
                            mErrorReason = "비상정지";
                            setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                        }

                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    //if (!getMyApplication().getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    //{

                    //    if (bIsCharging_MoreOnce)
                    //    {
                    //        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                    //    }
                    //    else
                    //    {
                    //        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    //    }
                    //}
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_DISABLE:
                case CONST_STATE_UIFLOWTEST.STATE_DISABLE:
                    getMyApplication().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "사용이 비활성화 되었습니다.\n\n관리자에게 문의해 주세요.", true);
                    //getMyApplication().mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("사용이 비활성화 되었습니다.\n\n관리자에게 문의해 주세요.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Unavailable);
                    setState(CONST_STATE_UIFLOWTEST.STATE_DISABLE + 1);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_DISABLE + 1:
                case CONST_STATE_UIFLOWTEST.STATE_DISABLE + 1:
                    if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    else if (bIsUseEnable_Channel && isTimer_Sec(TIMER_3SEC, 3))
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_RESET:
                case CONST_STATE_UIFLOWTEST.STATE_RESET:
                    getMyApplication().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "재부팅 대기중입니다..\n\n관리자에게 문의해 주세요.", true);
                    //getMyApplication().mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("재부팅 대기중입니다..\n\n관리자에게 문의해 주세요.");

                    setState(CONST_STATE_UIFLOWTEST.STATE_RESET + 1);
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_RESET + 1:
                case CONST_STATE_UIFLOWTEST.STATE_RESET + 1:
                    if ((isTimer_Sec(TIMER_WAITTIME, 2)))
                    {
                        if (mSendManager_OCPP_ChargingReq.getCount_SendList() < 1)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_RESET + 2);
                            bIsReady_Reboot = true;
                        }
                    }
                    break;
                #endregion

                #region >> case CONST_STATE_UIFLOWTEST.STATE_RESET + 2:
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
                    #endregion

            }


        }



        public void onClick_Confirm(object sender)
        {
            bIsClick_Notify_1Button = true;
        }

        public void onClick_Cancel(object sender)
        {
            throw new NotImplementedException();
        }

        public void onReceive(string rfCardNumber)
        {
            mCardNumber_Read_Temp = rfCardNumber;
            bIsComplete_RFCard_Read = true;
        }

        public void onReceiveFailed(string result)
        {
            throw new NotImplementedException();
        }


    }
}