using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.Iksung_RFReader;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.statemanager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.statemanager
{
    public class SC_1CH_OCPP_StateManager_Channel : EL_StateManager_OCPP_Channel
        , IRFCardReader_EventListener, IOnClickListener_Button
    {

        public const int DELAY_OFFLINE = 30;
        public SC_1CH_OCPP_StateManager_Channel(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {
            //mChanneldx = channelIndex - 1;
            setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING);


            //mPageManager_Main.mContentLayout_Notify_1Tv_1Btn.setOnClickListener_NotifyButton(this);

        }

        public override void initVariable()
        {
            ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main().mUC_Notify_1Tv_1Btn[mChannelIndex - 1].setOnClickListener(this);
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

        String[] result = null;
        protected bool bTemp_Skip_NotCompletePacketSend = false;
        protected bool bTemp_Skip_NotCompletePacketSend_Processing = false;
        EL_Time mTime_Manual = null;
        bool bIsCompleteMCOn = false;

        override public async void intervalExcuteAsync()
        {
            //        ((EL_DC_Charger_MyApplication) mApplication).getChannelInfor_ChargerTotal(1).getControlbdComm_PacketManager().packet_z1.bMC_ON_Manual = true;
            //process_GetAmiValue_Booting();


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

                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 2:

                    result = mTable_TransactionInfor.db_NotCompleteTransaction(mChannelIndex);
                    if (result != null)
                        Logger.d("NotSendData= " + result.ToString());
                    else
                        Logger.d("NotSendData=Nothing");
                    if (result != null)
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_System(
                        "미전송 데이터를 전송중입니다.\r\n잠시만 기다려 주세요.", false
                        );
                        if (result[20] != null)
                        {


                            JArray jsonArray = null;
                            Req_StopTransaction req_stopTransaction = new Req_StopTransaction();
                            //try
                            //{
                            jsonArray = JArray.Parse(result[20]);
                            //req_stopTransaction = JsonConvert.DeserializeObject<Req_StopTransaction>(jsonArray[3].ToString()); //mGson.fromJson(jsonArray.getString(3), Req_StopTransaction.class);
                            //} catch (JSONException e)
                            //{
                            //    e.printStackTrace();
                            //}

                            EL_Time time = new EL_Time();
                            time.setTime();
                            //if (result[CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason].ToString() == Reason.HardReset.ToString()
                            //    || result[CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason].ToString() == Reason.SoftReset.ToString())
                            //{

                            req_stopTransaction.setInfor_Required((mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh()),
                                time.toString_OCPP(), long.Parse(result[CONST_TRANSACTIONINFOR.INDEX.TransactionID].ToString()));
                            req_stopTransaction.idTag = result[CONST_TRANSACTIONINFOR.INDEX.IdTag].ToString();
                            if (result[CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason].ToString().Equals(""))
                                result[CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason] = Reason.PowerLoss.ToString();

                            req_stopTransaction.reason = ((Reason)Enum.Parse(typeof(Reason), result[CONST_TRANSACTIONINFOR.INDEX.ChargingStop_Reason].ToString()));




                            //mSendManager_OCPP_ChargingReq.addReq_By_PayloadString("StopTransaction", JsonConvert.SerializeObject(req_stopTransaction));
                            mSendManager_OCPP_ChargingReq.addReq_By_PayloadString(req_stopTransaction.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                            JsonConvert.SerializeObject(req_stopTransaction, EL_MyApplication_Base.mJsonSerializerSettings));
                            //}

                            setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 5);

                        }
                        else
                        {
                            mTable_TransactionInfor.db_ProcessCompleteTransaction(result[0]);
                            setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                        }
                    }
                    else
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                    }
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 5:
                    if (mSendManager_OCPP_ChargingReq.bOCPP_Conf_StopTransaction)
                    {
                        mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                        mTable_TransactionInfor.db_ProcessCompleteTransaction(result[0]);
                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, 5))
                    {
                        mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Finishing);
                        mTable_TransactionInfor.db_ProcessCompleteTransaction(result[0]);
                        setState(CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;

                //////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_PREFARING + 100:
                    if (mTime_SetState.getSecond_WastedTime() > 3 && bIsPrepareComplete_StateManager_Main)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    break;

                ////////////////////////////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_READY:
                    bIsComplete_PrepareChannel = true;
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                    initVariable_UsingComplete();
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    bIsTest_Car = false;

                    bIsCertificationFailed = false;
                    bIsCertificationSuccess = false;
                    setState(CONST_STATE_UIFLOWTEST.STATE_READY);

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_Select_Touch_Screen(mChannelIndex - 1);

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
                    else if (!bIsUseEnable_Channel && mSendManager_StatusNotification.mOCPP_ChargePointStatus == ChargePointStatus.Available)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_DISABLE);
                    }
                    else if (process_EmergencyButton())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
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
                    else if (bIsCommand_UsingStart
                        || mMemberType != EMemberType.NONE)
                    {
                        initVariable_UsingStart();
                        //setState(CONST_STATE_UIFLOWTEST.STATE_SELECT_CONNECTORTYPE);
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG);
                    }
                    //if (mState != (CONST_STATE_UIFLOWTEST.STATE_READY + 1))
                    //{
                    //    MainActivity.bNeed_UpdateUI_Main_VersionDisplay_Invisible = true;
                    //}
                    break;
                ////////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_REMOTE_START_TRANSACTION:
                    //                MainActivity.bNeed_UpdateUI_Main_MenuButton_Invisible = true;
                    bIsAutoProcess = true;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Preparing);


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
                            "서버에서 요청한 사용자 인증중입니다.\r\n잠시만 기다려주세요.", false, mChannelIndex - 1);
                        //.mContentLayout_Notify_1Tv
                        //    .setTv_Content_1("서버에서 요청한 사용자 인증중입니다.\r\n잠시만 기다려주세요.");
                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;

                        mSendManager_StatusNotification.setDelay_First(0);
                        mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                        mSendManager_OCPP_Authorize.setDelay_First(5);
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2);
                    }
                    else//즉시 충전
                    {
                        mCardNumber_Member = mOCPP_CSMS_Req_RemoteStartTransaction.idTag;
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                    }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_RESERVATION:
                    bIsReservationProcessing = true;
                    bIsReservation = false;
                    bIsClick_Notify_1Button = false;
                    bIsComplete_RFCard_Read = false;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Reserved);

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                            "예약중입니다.\r\n확인 버튼을 눌러 인증을 시작해 주세요.", false, this, mChannelIndex - 1);

                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                    //        .setTv_Content_1("예약중입니다.\r\n확인 버튼을 눌러 인증을 시작해 주세요.", this);
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
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
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag(mChannelIndex - 1);
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
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag(mChannelIndex - 1);
                        //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(this);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.CARD_CHECK);
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(this);
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_FOREVER;
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
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv("사용자 인증중입니다.\r\n잠시만 기다려주세요.", true, mChannelIndex - 1);

                        //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                        //        .setTv_Content_1("사용자 인증중입니다.\r\n잠시만 기다려주세요.");
                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                        mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 3);
                    }
                    break;
                //Reservation
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
                                "잘못된 RemoteStartTransaction입니다..", true, this, mChannelIndex - 1);

                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                            //        .setTv_Content_1("잘못된 RemoteStartTransaction입니다..", this);
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                            setState(CONST_STATE_UIFLOWTEST.STATE_RESERVATION + 4);
                        }
                        else
                        {
                            bIsClick_Notify_1Button = false;
                            EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n예약된 사용자가 아닙니다.", true, this, mChannelIndex - 1);
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
                //////////////////////////////////////////////////////////////
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
                        setState(CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE);
                    }
                    else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }
                    break;
                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE:
                    //                MainActivity.bNeed_UpdateUI_BackButton_Visible = true;


                    //MainActivity.bNeed_UpdateUI_Main_Select_MemberType = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_SELECT_MEMBERTYPE + 1:

                    if (!bIsCommand_UsingStart
                            || isTimer_Sec(TIMER_WAITTIME, mDelay_Reset) || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsCommand_ChargingStop)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (mMemberType == EMemberType.MEMBER)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG);
                    }
                    else if (mMemberType == EMemberType.NONMEMBER)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_NONMEMBER_SELECT_PAYMENTTYPE);
                    }
                    else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }
                    break;


                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG:
                    bIsCertificationSuccess = false;
                    bIsCertificationFailed = false;
                    //                if (!bIsAutoProcess)
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Visible = true;
                    //                else
                    //                    MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;

                    //mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Preparing);

                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(this);
                    ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.CARD_CHECK);

                    //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(this);
                    //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_FOREVER;
                    bIsComplete_RFCard_Read = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag(mChannelIndex - 1);
                    //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 1:
                    if (!bIsCommand_UsingStart
                            || isTimer_Sec(TIMER_WAITTIME, mDelay_Reset) || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsCommand_ChargingStop)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsComplete_RFCard_Read)
                    {

                        if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.LocalPreAuthorize))
                        {

                            mOCPP_AuthCache_State
                                    = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().getIdTag_DB_State(mCardNumber_Read_Temp);
                            switch (mOCPP_AuthCache_State)
                            {
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.NOT_EXIST:
                                    //if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.AllowOfflineTxForUnknownId))
                                    //{
                                    //    //캐쉬에 인증 내역이 없을 경우 그냥 충전진행
                                    //    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                                    //}
                                    //else
                                    //{
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "회원 인증중입니다.\r\n잠시만 기다려주세요.", true, mChannelIndex - 1);
                                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                                    //        .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");

                                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                                    mSendManager_OCPP_Authorize.setOCPP_Authorize_Listener(null);
                                    mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2);
                                    //}
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST:
                                    //061                                    
                                    mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                                    await Task.Delay(1000);
                                    //////
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST_EXPIRY:
                                    mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();

                                    bIsClick_Notify_1Button = false;
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", false, this, mChannelIndex - 1);
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.ERROR:
                                    bIsClick_Notify_1Button = false;
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", false, this, mChannelIndex - 1);
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
                                        "회원 인증중입니다.\r\n잠시만 기다려주세요.", true, mChannelIndex - 1);
                            //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                            //        .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");
                            //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                            mCardNumber_Member = mCardNumber_Read_Temp;
                            mSendManager_OCPP_Authorize.setOCPP_Authorize_Listener(null);
                            mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize();
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2);
                        }


                    }
                    else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }

                    if (mState != CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 1)
                    {

                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(null);
                    }
                    break;
                //

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 2:
                    if (!bIsCommand_UsingStart || bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    //                else if(isTimer_Sec(TIMER_WAITTIME, mDelay_Reset)
                    //                && !mSettingData_OCPP_Table.getSettingData_Boolean(CONST_OCPP_Setting.LocalAuthorizeOffline.getValue()))
                    //                {
                    //                    setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    //                }
                    else if (bIsCommand_ChargingStop)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (bIsCertificationSuccess)
                    {
                        if (mCardNumber_Member == null)
                            mCardNumber_Member = mCardNumber_Read_Temp;

                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                    }
                    else if (bIsCertificationFailed)
                    {
                        bIsClick_Notify_1Button = false;
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아닙니다.", false, this, mChannelIndex - 1);
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
                            mSendManager_OCPP_Authorize.initVariable();
                            mOCPP_AuthCache_State
                                    = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().getIdTag_DB_State(mCardNumber_Read_Temp);
                            switch (mOCPP_AuthCache_State)
                            {
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.NOT_EXIST:
                                    if (mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.AllowOfflineTxForUnknownId))
                                    {
                                        //캐쉬에 인증 내역이 없을 경우 그냥 충전진행
                                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                                    }
                                    else
                                    {
                                        bIsClick_Notify_1Button = false;
                                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "인증 시간이 지났습니다.\r\n다시 시도해 주세요.", false, this, mChannelIndex - 1);
                                        //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                                        //        .setTv_Content_1("인증 시간이 지났습니다.\r\n다시 시도해 주세요.", this);
                                        //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_Btn1_ProcessMain_ChangeLayout = true;
                                        setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CARDTAG + 3);
                                    }
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST:
                                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR);
                                    break;
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST_EXPIRY:
                                case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.ERROR:
                                    bIsClick_Notify_1Button = false;
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv_1Btn(
                                "카드를 확인 해 주세요.\r\n회원카드가 아니거나 만료되었습니다..", false, this, mChannelIndex - 1);
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
                                "인증 시간이 지났습니다.\r\n다시 시도해 주세요.", false, this, mChannelIndex - 1);
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
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    break;


                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR:
                    mSendManager_OCPP_Authorize.setSendPacket_Call_CP(null);
                    if (mCardNumber_Member != null)
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

                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Connect_Connector(mChannelIndex - 1);

                    //MainActivity.bNeed_UpdateUI_Main_Wait_ConnectConnector = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_CONNECTCONNECTOR + 1);
                    mDelay_ConnectionTimeOut = mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.ConnectionTimeOut);
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
                            mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Available);
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);

                        }
                        else if (bIsCommand_ChargingStop)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                        else if (
                            mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                        )
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION);
                        //else if (
                        //    (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_1z.getState_CommunicationCar() >= 2
                        //      && isTimer_Sec(TIMER_3SEC, 3))
                        //      || bIsTest_Car)
                        //{
                        //    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION);
                        //}
                        else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        }
                    }
                    break;
                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "거래 진행중입니다.\r\n잠시만 기다려주세요.", true, mChannelIndex - 1);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("거래 진행중입니다.\r\n잠시만 기다려주세요.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                    mSendManager_OCPP_ChargingReq.sendOCPP_CP_Req_StartTransaction();
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION + 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_STARTTRANSACTION + 1:
                    if (bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (!bIsCommand_UsingStart)
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                        }
                        else if (bIsErrorOccured)
                        {
                            if (mErrorReason == null)
                                mErrorReason = "오류발생 (" + mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().getErrorCode() + ")";
                            setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                        }
                        else if (bIsCommand_ChargingStop)
                        {

                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                        }
                        else if (mSendManager_OCPP_ChargingReq.bOCPP_CSMS_Conf_StartTransaction)
                        {
                            switch (mSendManager_OCPP_ChargingReq.getOCPP_CSMS_Conf_StartTransaction().idTagInfo.status)
                            {

                                case AuthorizationStatus.Accepted:
                                    setIsOffLine(false);
                                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE);
                                    break;
                                case AuthorizationStatus.Blocked:
                                case AuthorizationStatus.Expired:
                                case AuthorizationStatus.Invalid:
                                case AuthorizationStatus.ConcurrentTx:
                                    mOCPP_StopTransaction_Reason = Reason.DeAuthorized;
                                    ocpp_setErrorOccured(Const_ErrorCode.CODE_0012_SERVER_CERTIFICATION_ERROR, Reason.DeAuthorized, ChargePointStatus.Faulted);
                                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
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
                                    mErrorReason = "대기시간 초과";
                                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                                }
                            }

                        }
                    }


                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE:
                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 1;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Charging);
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Preparing_Charging(mChannelIndex - 1);
                    //MainActivity.bNeed_UpdateUI_Main_Charging_Prepare = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_PREPARE + 1:
                    if (bIsClick_BackButton)
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    else if (isTimer_Sec(TIMER_3SEC, 2))
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
                        else if (bIsCommand_ChargingStop && isTimer_Sec(TIMER_90SEC, 3))
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
                        //else if (
                        //      (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_1z.getState_CommunicationCar() == 3
                        //              && !bIsTest_Car
                        //      )
                        //              ||
                        //              bIsTest_Car)
                        //{
                        //    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                        //}
                        else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        }
                        else if (
                            (
                                (mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mChargingProcessState == 102
                                ||
                                (mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager()).packet_1z.mErrorCode > 0
                                )
                        )
                        {
                            mErrorReason = "오류발생 (" + mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().getErrorCode() + ")";
                            mOCPP_StopTransaction_Reason = Reason.Local;
                            setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                        }
                    }

                    break;
                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING:
                    setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING + 1);
                    bIsClick_Notify_1Button = false;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Error_Before_Charging(mErrorReason, mChannelIndex - 1);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv_1Btn
                    //        .setTv_Content_1("충전 준비 중 오류가 발생했습니다..\r\n오류 내용을 확인 후 확인버튼을 눌러 주세요.", this);
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
                    if (bIsErrorOccured)
                        ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 0;
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    //MainActivity.bNeed_UpdateUI_Main_Chargking = true;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Charging(mChannelIndex - 1);

                    initVariable_ChargingStart();
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1);
                    mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction();
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1:

                    if (!bIsChargingStart_By_ChargingWattage)
                    {
                        bIsChargingStart_By_ChargingWattage = mChannelTotalInfor.getControlbdComm_PacketManager().packet_1z.bFlag1_AMI_Comm_Normal;
                        mSendManager_OCPP_ChargingReq.saveOCPP_CP_Req_StopTransaction();
                    }

                    if (processCondition_Charging())
                    {

                    }
                    else if (bIsClick_ChargingStop_User)
                    {
                        mErrorReason = "충전정지 버튼 누름";
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 10);
                    }

                    if (mState == (CONST_STATE_UIFLOWTEST.STATE_CHARGING + 1) && isTimer_MiliSec(TIMER_800_UIUPDATE, 800))
                    {
                        EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().mUC_Charging_Charging[mChannelIndex - 1].updateView();

                        //MainActivity.bNeed_UpdateUI_Main_Update_ChargingInfor = true;
                    }
                    break;

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
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_CardTag(mChannelIndex - 1);
                    //MainActivity.bNeed_UpdateUI_Main_Wait_CardTag = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 11);
                    break;
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
                            if (mOCPP_StopTransaction_Reason == null)
                                mOCPP_StopTransaction_Reason = Reason.Local;
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
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
                                        case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.NOT_EXIST:
                                        case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST_EXPIRY:
                                        case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.ERROR:
                                            mErrorReason = null;
                                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                                            break;
                                        case OCPP_EL_Manager_Table_AuthCache.EIDTAG_DB_STATE.EXIST:
                                            mCardNumber_Member = mCardNumber_Read_Temp;
                                            if (mOCPP_StopTransaction_Reason == null)
                                                mOCPP_StopTransaction_Reason = Reason.Local;
                                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                                            break;
                                    }
                                }
                                else
                                {
                                    //parentIdTag 비교해야되나
                                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "회원 인증중입니다.\r\n잠시만 기다려주세요.", true, mChannelIndex - 1);
                                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                                    //        .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");
                                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                                    if (mOCPP_StopTransaction_Reason == null)
                                        mOCPP_StopTransaction_Reason = Reason.Local;
                                    mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize(new OCPP_ConfAuthorize_Listener_Charging11_Case1(this));
                                    setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12);

                                }

                            }
                            else
                            {
                                EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "회원 인증중입니다.\r\n잠시만 기다려주세요.", true, mChannelIndex - 1);

                                //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                                //        .setTv_Content_1("회원 인증중입니다.\r\n잠시만 기다려주세요.");
                                //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                                if (mOCPP_StopTransaction_Reason == null)
                                    mOCPP_StopTransaction_Reason = Reason.Local;
                                mSendManager_OCPP_Authorize.sendOCPP_CP_Req_Authorize(new OCPP_ConfAuthorize_Listener_Charging11_Case2(this));

                                setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12);
                            }
                        }
                    }


                    if (mState != CONST_STATE_UIFLOWTEST.STATE_CHARGING + 11)
                    {

                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(null);
                    }
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12:
                    if (processCondition_Charging())
                    {

                    }
                    else if (isTimer_Sec(TIMER_WAITTIME, DELAY_OFFLINE) || bIsClick_BackButton)
                    {
                        mErrorReason = null;
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING);
                    }

                    if (mState != CONST_STATE_UIFLOWTEST.STATE_CHARGING + 12)
                    {
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.setRFCardReader_Listener(null);
                        ((EL_DC_Charger_MyApplication)mApplication).SerialPort_Smartro_CardReader.getManager_Send().setCommand(Smartro_TL3500BS_Constants.Command.NONE);
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().getCommport_SendManager().mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
                        //((EL_DC_Charger_MyApplication)mApplication).getCommPort_RFIDReader().setRFCardReader_Listener(null);
                    }
                    break;



                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE:
                    bIsSelected_Confirm_ChargingComplete = false;


                    initVariable_ChargingComplete();


                    mApplication.getDataManager_CustomUC_Main().mUC_Charging_Charging_Complete[mChannelIndex - 1].updateView();
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Charging_Complete(mErrorReason, mChannelIndex - 1);
                    //MainActivity.bNeed_UpdateUI_Main_Update_ChargingInfor = true;
                    //MainActivity.bNeed_UpdateUI_Main_Charging_Complete = true;
                    if (isNeedReset())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_RESET);
                    }
                    if (mApplication.DI_Manager.isEmergencyPushed())
                    {
                        if (bIsCharging_MoreOnce)
                            setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE);
                        else
                        {
                            mErrorReason = "비상정지";
                            setState(CONST_STATE_UIFLOWTEST.STATE_ERROR_BEFORE_CHARGING);
                        }

                    }
                    else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    }
                    else
                    {
                        setState(CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1);
                    }

                    break;

                case CONST_STATE_UIFLOWTEST.STATE_CHARGING_COMPLETE + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (
                                (!bIsCommand_UsingStart) || bIsSelected_Confirm_ChargingComplete
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                        }
                        else if (((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                        }
                        else if (isTimer_Sec(TIMER_60SEC, 10) &&
                                mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            !mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                        }
                        else if (isTimer_Sec(TIMER_WAITTIME, 120) &&
                                mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                            &&
                            !mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar())
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR);
                        }

                    }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR:
                    //                MainActivity.bNeed_UpdateUI_BackButton_Invisible = true;
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Disconnect_Connector(mChannelIndex - 1);
                    //MainActivity.bNeed_UpdateUI_Main_Wait_SeperateConnector = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1);
                    break;

                case CONST_STATE_UIFLOWTEST.STATE_WAIT_SEPERATE_CONNECTOR + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        if (
                                !bIsCommand_UsingStart ||
                                        (mApplication.getChannelTotalInfor(mChannelIndex).getEV_State() != null
                                        &&
                                        !mApplication.getChannelTotalInfor(mChannelIndex).getEV_State().isState_ConnectedCar()
                                                &&
                                                !bIsTest_Car)
                        )
                        {
                            setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE);
                        }
                    }

                    //                else if(((EL_DC_Charger_MyApplication)mApplication).getChannelInfor_ChargerTotal(1).getControlbdComm_PacketManager().isEmergencyPushed())
                    //                {
                    //                    setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY);
                    //                }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_UseComplete(mChannelIndex - 1);
                    //MainActivity.bNeed_UpdateUI_Main_Wait_UseComplete = true;
                    setState(CONST_STATE_UIFLOWTEST.STATE_WAIT_USECOMPLETE + 1);
                    break;

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
                        setState(CONST_STATE_UIFLOWTEST.STATE_READY);
                    }
                    break;

                //////////////////////////////////////////////////////////////
                case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY:
                    setState(CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1);
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_ChargingMain_Emergency(mChannelIndex - 1);
                    break;
                case CONST_STATE_UIFLOWTEST.STATE_EMERGENCY + 1:

                    if (!mApplication.DI_Manager.isEmergencyPushed())
                    {
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
                    //if (!((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().isEmergencyPushed())
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

                case CONST_STATE_UIFLOWTEST.STATE_DISABLE:
                    EL_DC_Charger_MyApplication.getInstance().mPageManager_Main().setView_MainPanel_Include_1Tv(
                                        "사용이 비활성화 되었습니다.\n\n관리자에게 문의해 주세요.", true);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("사용이 비활성화 되었습니다.\n\n관리자에게 문의해 주세요.");
                    //MainActivity.bOCPP_Need_UpdateUI_Main_Notify_Tv1_ProcessMain_ChangeLayout = true;
                    mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.Unavailable);
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
                                        "재부팅 대기중입니다..\n\n관리자에게 문의해 주세요.", true);
                    //EL_DC_Charger_MyApplication.getInstance().mPageManager_Main.mContentLayout_Notify_1Tv
                    //        .setTv_Content_1("재부팅 대기중입니다..\n\n관리자에게 문의해 주세요.");

                    setState(CONST_STATE_UIFLOWTEST.STATE_RESET + 1);
                    break;
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















        public void onReceive(String rfCardNumber)
        {
            mCardNumber_Read_Temp = rfCardNumber;
            bIsComplete_RFCard_Read = true;
        }


        public void onReceiveFailed(String result)
        {

        }





        public void onClick_Confirm(object sender)
        {
            bIsClick_Notify_1Button = true;
        }


        public void onClick_Cancel(object sender)
        {

        }
    }
}
