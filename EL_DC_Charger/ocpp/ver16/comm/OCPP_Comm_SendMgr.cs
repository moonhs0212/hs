using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.common_action;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.packet.csms2cp;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using EL_DC_Charger.ocpp.ver16.platform.wev.packet;
using EL_DC_Charger.ocpp.ver16.platform.wev.statemanager;
using EL_DC_Charger.ocpp.ver16.statemanager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParkingControlCharger.baseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.comm
{
    public class OCPP_Comm_SendMgr : EL_IntervalExcute_Item_Base
    {
        public OCPP_Comm_SendMgr(EL_MyApplication_Base application, OCPP_Comm_Manager comm_Manager)
            : base(application, 0, MODE_UNIT_MILISECOND, 200)
        {
            mComm_Manager = comm_Manager;

            mSettingData_OCPP_Table = ((OCPP_Manager_Table_Setting)mApplication.getManager_SQLite_Setting_OCPP().getList_Manager_Table()[0]);
            mSettingData_OCPP_AuthCache = mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache();
        }

        public const int INTERVAL_SEC_SOCKET_RECONNECTING = 5;
        protected OCPP_Comm_Manager mComm_Manager = null;

        public OCPP_Comm_Manager getComm_Manager()
        {
            return mComm_Manager;
        }
        protected OCPP_Manager_Table_Setting mSettingData_OCPP_Table = null;
        public OCPP_EL_Manager_Table_AuthCache mSettingData_OCPP_AuthCache = null;


        IOCPP_ConfChangeConfiguration_Listener mListener = null;
        public void setOCPP_ConfChangeConfiguration_Listener(IOCPP_ConfChangeConfiguration_Listener listener)
        {
            mListener = listener;
        }


        protected EL_StateManager_OCPP_Channel[] mStateManager_OCPP_Channel = null;
        public EL_StateManager_OCPP_Main getStateManager_OCPP_Main()
        {
            return mStateManager_OCPP_Main;
        }

        protected EL_StateManager_OCPP_Main mStateManager_OCPP_Main = null;


        protected int mMode_Process = 0;




        public int mCount_Send = 0;
        public int mCount_SendFault = 0;

        protected EL_SendManager_OCPP_Base[] mSendManager_OCPP = new EL_SendManager_OCPP_Base[8];

        protected bool bInit_StateManager = false;

        public void setStateManager()
        {
            mStateManager_OCPP_Main = (EL_StateManager_OCPP_Main)mApplication.mStateManager_Main;
            mStateManager_OCPP_Channel = new EL_StateManager_OCPP_Channel[mApplication.getChannelTotalInfor().Length];
            for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
            {
                mStateManager_OCPP_Channel[i] = (EL_StateManager_OCPP_Channel)mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel();
            }
            bInit_StateManager = true;

            int count = 0;

            switch (mApplication.getPlatform_Operator())
            {
                case common.variable.EPlatformOperator.WEV:
                    mSendManager_OCPP = new EL_SendManager_OCPP_Base[2 + (mStateManager_OCPP_Channel.Length * 4)];
                    break;
                default:
                    mSendManager_OCPP = new EL_SendManager_OCPP_Base[2 + (mStateManager_OCPP_Channel.Length * 3)];
                    break;
            }


            for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
            {
                mSendManager_OCPP[count++] = mStateManager_OCPP_Channel[i].mSendManager_OCPP_ChargingReq;
            }
            mSendManager_OCPP[count++] = mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal;
            mSendManager_OCPP[count++] = mStateManager_OCPP_Main.mSendManager_StatusNotification_Main;

            for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
            {
                mSendManager_OCPP[count++] = mStateManager_OCPP_Channel[i].mSendManager_OCPP_Authorize;
                mSendManager_OCPP[count++] = mStateManager_OCPP_Channel[i].mSendManager_StatusNotification;
                switch (mApplication.getPlatform_Operator())
                {
                    case common.variable.EPlatformOperator.WEV:
                        mSendManager_OCPP[count++] = ((EL_Charger_1CH_OCPP_StateManager_Channel_Wev)mStateManager_OCPP_Channel[i]).mSendManager_OCPP_Wev;
                        break;                    
                    default:
                        break;
                }

            }


            mComm_Manager.onDisconnect_Socket();
        }

        EL_Time mTime_Send = new EL_Time();

        protected void process_ReconnectSocket()
        {
            if (mComm_Manager.mTime_Disconnect_Socket != null
                    && mComm_Manager.mTime_Disconnect_Socket.getSecond_WastedTime() > INTERVAL_SEC_SOCKET_RECONNECTING)
            {
                mComm_Manager.openComm();
            }
        }

        protected void process_sendCall_HeartBeat()
        {
            if (mStateManager_OCPP_Main.bOCPP_IsReceivePacket_CallResult_BootNotification == false)
                return;

            if (!mComm_Manager.bIsConnected_HW)
                return;

            if (mStateManager_OCPP_Main.bOCPP_IsReceivePacket_CallResult_BootNotification
                &&
                mStateManager_OCPP_Main.bOCPP_IsReceivePacket_CallResult_HeartBeat
                    &&
                    mStateManager_OCPP_Main.getOCPP_Interval_Send_BootNotification() - 1 <= mStateManager_OCPP_Main.mTime_Send_HeartBeat.getSecond_WastedTime())
            {
                mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_Heartbeat();
                mStateManager_OCPP_Main.mTime_Send_HeartBeat.setTime();
            }
        }

        public override void initVariable()
        {

        }

        public void process_ReceivePacket_CallResult(JArray receivePacket)
        {
            Logger.d("☆Receive☆ OCPP CSMS->CP CallResult => " + receivePacket.ToString());

            EL_OCPP_Packet_Wrapper packet_Wrapper = null;
            for (int i = 0; i < mSendManager_OCPP.Length; i++)
            {
                packet_Wrapper = mSendManager_OCPP[i].setReceivePacket_CallResult(receivePacket);
                if (packet_Wrapper != null)
                    break;
            }
            if (packet_Wrapper == null)
                return;

            if (packet_Wrapper != null && packet_Wrapper.mPacket[1].ToString().Equals(receivePacket[1].ToString()))
            {
                EL_Time time_Generate = new EL_Time();
                time_Generate.setTime();
                String time_String = time_Generate.getDateTime_DB();

                String sendPacket_ActionName = packet_Wrapper.mPacket[2].ToString();
                if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.BootNotification.ToString()))
                {
                    Conf_BootNotification data = JsonConvert.DeserializeObject<Conf_BootNotification>(((JObject)receivePacket[2]).ToString());//mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_BootNotification.class);
                    mStateManager_OCPP_Main.setOCPP_CSMS_Conf_BootNotification(data);
                    if (!mComm_Manager.bOCPP_Receive_BootNotification_First)
                    {
                        mComm_Manager.bOCPP_Receive_BootNotification_First = true;
                    }
                    else
                    {
                        mComm_Manager.bOCPP_Receive_BootNotification_Socket_Disconnect = true;
                    }
                }
                else if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.DiagnosticsStatusNotification.ToString()))
                {
                    Conf_DiagnosticsStatusNotification data = JsonConvert.DeserializeObject<Conf_DiagnosticsStatusNotification>(((JObject)receivePacket[2]).ToString());
                    //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_DiagnosticsStatusNotification.class);
                    //처리할 필요 없을듯
                }
                else if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.FirmwareStatusNotification.ToString()))
                {
                    Conf_FirmwareStatusNotification data = JsonConvert.DeserializeObject<Conf_FirmwareStatusNotification>(((JObject)receivePacket[2]).ToString());
                    //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_FirmwareStatusNotification.class);
                    //처리할 필요 없을듯
                }
                else if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.Heartbeat.ToString()))
                {
                    Conf_Heartbeat data = JsonConvert.DeserializeObject<Conf_Heartbeat>(((JObject)receivePacket[2]).ToString());
                    //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_Heartbeat.class);
                    mStateManager_OCPP_Main.setOCPP_COMS_Conf_HeartBeat(data);

                }
                else if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.StatusNotification.ToString()))
                {
                    Conf_StatusNotification data = JsonConvert.DeserializeObject<Conf_StatusNotification>(((JObject)receivePacket[2]).ToString());
                    //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_StatusNotification.class);

                }
                else if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.StopTransaction.ToString()))
                {
                    mComm_Manager.bOCPP_Receive_StopTransaction = true;
                }
                else if (sendPacket_ActionName.Equals(EOCPP_Action_CP_Call.MeterValues.ToString()))
                {
                    mComm_Manager.bOCPP_Receive_MeterValue = true;
                }
                else
                {
                    Logger.d("☆Receive☆ OCPP CSMS->CP CallResult 처리되지 않음 => " + sendPacket_ActionName);
                }
                mTime_Send.setTime();

            }
        }



        public void process_ReceivePacket_Call(JArray receivePacket)
        {

            Logger.d("☆Receive☆ OCPP CSMS->CP Call => " + receivePacket.ToString());


            Req_TriggerMessage req_TriggerMessage = null;
            Conf_TriggerMessage conf_TriggerMessage = null;
            String callResult_message = "";
            JArray callResult_Packet = new JArray();
            callResult_Packet.Add(3);
            callResult_Packet.Add(receivePacket[1].ToString());
            if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.CancelReservation.ToString()))
            {
                Req_CancelReservation data =
                    JsonConvert.DeserializeObject<Req_CancelReservation>(((JObject)receivePacket[3]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_CancelReservation.class);
                Conf_CancelReservation data_Result = new Conf_CancelReservation();
                data_Result.status = CancelReservationStatus.Rejected;
                //
                bool result = false;
                for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                {
                    bool resultTemp = mStateManager_OCPP_Channel[i].setOCPP_Cancel_Reservation(data);
                    if (resultTemp)
                    {
                        result = true;
                        data_Result.status = CancelReservationStatus.Accepted;
                        break;
                    }
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings);
                //mGson.toJson(data_Result, Conf_CancelReservation.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.ChangeAvailability.ToString()))
            {
                Req_ChangeAvailability data = JsonConvert.DeserializeObject<Req_ChangeAvailability>(((JObject)receivePacket[3]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_ChangeAvailability.class);

                Conf_ChangeAvailability data_Result = new Conf_ChangeAvailability();

                if (data.connectorId > 0 && mStateManager_OCPP_Channel.Length >= data.connectorId)
                {
                    if (data.type == AvailabilityType.Operative)
                    {
                        if (mStateManager_OCPP_Channel[data.connectorId - 1].bIsProcessing_Using)
                        {
                            data_Result.status = AvailabilityStatus.Scheduled;
                        }
                        else
                        {
                            data_Result.status = AvailabilityStatus.Accepted;
                            mStateManager_OCPP_Channel[data.connectorId - 1].bIsCommand_UsingStart = false;
                        }
                        mStateManager_OCPP_Channel[data.connectorId - 1].setIsUseEnable_Channel(true);
                    }
                    else
                    {
                        if (mStateManager_OCPP_Channel[data.connectorId - 1].bIsProcessing_Using)
                        {
                            data_Result.status = AvailabilityStatus.Scheduled;
                        }
                        else
                        {
                            data_Result.status = AvailabilityStatus.Accepted;
                            mStateManager_OCPP_Channel[data.connectorId - 1].bIsCommand_UsingStart = false;
                        }
                        mStateManager_OCPP_Channel[data.connectorId - 1].setIsUseEnable_Channel(false);
                    }
                }
                else
                {
                    int count_Scheduled = 0;
                    data_Result.status = AvailabilityStatus.Accepted;

                    for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                    {
                        if (data.type == AvailabilityType.Operative)
                        {
                            mStateManager_OCPP_Channel[i].setIsUseEnable_Channel(true);
                            if (mStateManager_OCPP_Channel[i].bIsProcessing_Using)
                            {
                                count_Scheduled++;
                            }
                            else
                            {
                                mStateManager_OCPP_Channel[i].bIsCommand_UsingStart = false;
                            }
                        }
                        else
                        {
                            mStateManager_OCPP_Channel[i].setIsUseEnable_Channel(false);
                            if (mStateManager_OCPP_Channel[i].bIsProcessing_Using)
                            {
                                count_Scheduled++;
                            }
                            else
                            {
                                mStateManager_OCPP_Channel[i].bIsCommand_UsingStart = false;
                            }
                        }
                    }
                    if (data.type == AvailabilityType.Operative)
                    {
                        mStateManager_OCPP_Main.setIsUseEnable(true);
                    }
                    else
                    {
                        mStateManager_OCPP_Main.setIsUseEnable(false);
                    }
                    if (count_Scheduled > 0)
                    {

                        data_Result.status = AvailabilityStatus.Scheduled;
                    }
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings);
                //mGson.toJson(data_Result, Conf_ChangeAvailability.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.ChangeConfiguration.ToString()))
            {
                String replaceText = ((JObject)receivePacket[3]).ToString().Replace("Readonly", "readonly");
                Req_ChangeConfiguration data = JsonConvert.DeserializeObject<Req_ChangeConfiguration>(replaceText);
                //mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_ChangeConfiguration.class);
                Conf_ChangeConfiguration data_Result = new Conf_ChangeConfiguration();
                int dataIndex = -1;
                try
                {
                    dataIndex = (int)((CONST_INDEX_OCPP_Setting)Enum.Parse(typeof(CONST_INDEX_OCPP_Setting), data.key));     //CONST_INDEX_OCPP_Setting.valueOf(data.key).ordinal();
                }
                catch (ArgumentException e)
                {
                    Logger.d("ChangeConfiguration Error => " + e.Message);
                    dataIndex = -1;
                }


                if (dataIndex >= 0 && dataIndex < 43)
                {
                    String explain = mSettingData_OCPP_Table.getSettingData_Explain(dataIndex);
                    int value = 0;
                    try
                    {
                        value = Int32.Parse(data.value);

                        if (value < 0)
                        {
                            data_Result.status = ConfigurationStatus.Rejected;
                        }
                        else
                        {

                            if (explain.Equals("RW") || explain.Equals("rw") || explain.Equals("W") || explain.Equals("w"))
                            {
                                mSettingData_OCPP_Table.setSettingData(dataIndex, data.value);
                                //설정 변경에 대한 추가 필요
                                data_Result.status = ConfigurationStatus.Accepted;
                            }
                            else
                            {
                                mSettingData_OCPP_Table.setSettingData(dataIndex, data.value);
                                //설정 변경에 대한 추가 필요
                                data_Result.status = ConfigurationStatus.Rejected;
                            }
                        }
                    }
                    catch (FormatException e)
                    {
                        Logger.d("Exception Occured => " + e.Message);


                        if (explain.Equals("RW") || explain.Equals("rw") || explain.Equals("W") || explain.Equals("w"))
                        {
                            switch (dataIndex)
                            {
                                case (int)CONST_INDEX_OCPP_Setting.MinimumStatusDuration:
                                case (int)CONST_INDEX_OCPP_Setting.BlinkRepeat:
                                case (int)CONST_INDEX_OCPP_Setting.ClockAlignedDataInterval:
                                case (int)CONST_INDEX_OCPP_Setting.ConnectionTimeOut:
                                case (int)CONST_INDEX_OCPP_Setting.ConnectorPhaseRotationMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.GetConfigurationMaxKeys:
                                case (int)CONST_INDEX_OCPP_Setting.HeartbeatInterval:
                                case (int)CONST_INDEX_OCPP_Setting.LightIntensity:
                                case (int)CONST_INDEX_OCPP_Setting.MaxEnergyOnInvalidId:
                                case (int)CONST_INDEX_OCPP_Setting.MeterValuesAlignedDataMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.MeterValuesSampledDataMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.NumberOfConnectors:
                                case (int)CONST_INDEX_OCPP_Setting.ResetRetries:
                                case (int)CONST_INDEX_OCPP_Setting.StopTxnAlignedDataMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.StopTxnSampledDataMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.SupportedFeatureProfilesMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.TransactionMessageAttempts:
                                case (int)CONST_INDEX_OCPP_Setting.TransactionMessageRetryInterval:
                                case (int)CONST_INDEX_OCPP_Setting.WebSocketPingInterval:
                                case (int)CONST_INDEX_OCPP_Setting.LocalAuthListMaxLength:
                                case (int)CONST_INDEX_OCPP_Setting.SendLocalListMaxLength:
                                    data_Result.status = ConfigurationStatus.Rejected;

                                    break;
                                default:
                                    mSettingData_OCPP_Table.setSettingData(dataIndex, data.value);
                                    //설정 변경에 대한 추가 필요
                                    data_Result.status = ConfigurationStatus.Accepted;
                                    break;
                            }

                        }
                        else
                        {
                            //                        mSettingData_OCPP_Table.setSettingData(dataIndex, data.value);
                            //설정 변경에 대한 추가 필요
                            data_Result.status = ConfigurationStatus.Rejected;
                        }
                    }
                }
                else
                {
                    data_Result.status = ConfigurationStatus.NotSupported;
                }

                if (data_Result.status == ConfigurationStatus.Accepted)
                {
                    if (mListener != null)
                    {
                        mListener.receive_ConfChangeConfiguration(data, data_Result);
                    }
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_ChangeConfiguration.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.ClearCache.ToString()))
            {
                Req_ClearCache data = JsonConvert.DeserializeObject<Req_ClearCache>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.ClearCache.name()))
                //{
                //    Req_ClearCache data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_ClearCache.class);
                Conf_ClearCache data_Result = new Conf_ClearCache();
                //캐쉬 삭제에 대한 로직 추가 필요

                //try {
                Thread.Sleep(500);
                mSettingData_OCPP_AuthCache.deleteRow("");
                Thread.Sleep(500);
                //} catch (InterruptedException e) {
                //    e.printStackTrace();
                //}

                data_Result.status = ClearCacheStatus.Accepted;
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_ClearCache.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.ClearChargingProfile.ToString()))
            {
                Req_ClearChargingProfile data = JsonConvert.DeserializeObject<Req_ClearChargingProfile>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.ClearChargingProfile.name()))
                //{
                //    Req_ClearChargingProfile data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_ClearChargingProfile.class);
                Conf_ClearChargingProfile data_Result = new Conf_ClearChargingProfile();
                //
                data_Result.status = ClearChargingProfileStatus.Accepted;
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_ClearChargingProfile.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.DataTransfer.ToString()))
            {
                Req_DataTransfer reqDataTranfer = JsonConvert.DeserializeObject<Req_DataTransfer>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.DataTransfer.name()))
                //{
                //    Req_DataTransfer reqDataTranfer = mGson.fromJson(receivePacket.getString(3), Req_DataTransfer.class);
                String messageId = reqDataTranfer.messageId;

                if (messageId.Equals("NPQ2"))
                {
                    Req_NPQ2 reqNPQ2 = JsonConvert.DeserializeObject<Req_NPQ2>(reqDataTranfer.data); //mGson.fromJson(reqDataTranfer.data, Req_NPQ2.class);

                    Conf_DataTransfer confNPQ2 = new Conf_DataTransfer();
                    if (mStateManager_OCPP_Channel[0].GetType() == typeof(EL_Charger_1CH_OCPP_StateManager_Channel_Wev))
                    {
                        ((EL_Charger_1CH_OCPP_StateManager_Channel_Wev)mStateManager_OCPP_Channel[0]).wev_bNonmember_Receive_NPQ2_Req = true;
                        confNPQ2.status = DataTransferStatus.Accepted;
                    }
                    else
                    {
                        confNPQ2.status = DataTransferStatus.Rejected;
                    }
                    callResult_message = JsonConvert.SerializeObject(confNPQ2, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(confNPQ2, confNPQ2.getClass());
                }
                else if (messageId.Equals("NPQ3"))
                {



                    Req_NPQ3 reqNPQ3 = JsonConvert.DeserializeObject<Req_NPQ3>(reqDataTranfer.data); //mGson.fromJson(reqDataTranfer.data, Req_NPQ3.class);

                    switch (reqNPQ3.operatorType)
                    {
                        case "ER":
                            EL_DC_Charger_MyApplication.getInstance().CurrentAmount = EL_DC_Charger_MyApplication.getInstance().MemberAmount;
                            break;
                        case "NM":
                            EL_DC_Charger_MyApplication.getInstance().CurrentAmount = EL_DC_Charger_MyApplication.getInstance().NonmemberAmount;
                            break;
                    }


                    Conf_DataTransfer confNPQ3 = new Conf_DataTransfer();
                    ((Wev_StateManager_OCPP_Channel)mStateManager_OCPP_Channel[0]).wev_setNonmember_Receive_NPQ3_Req(reqNPQ3);
                    confNPQ3.status = DataTransferStatus.Accepted;
                    //if (mStateManager_OCPP_Channel[0].GetType() == typeof(Wev_StateManager_OCPP_Channel))
                    //{
                    //    ((Wev_StateManager_OCPP_Channel)mStateManager_OCPP_Channel[0]).wev_setNonmember_Receive_NPQ3_Req(reqNPQ3);
                    //    confNPQ3.status = DataTransferStatus.Accepted;
                    //}
                    //else
                    //{
                    //    confNPQ3.status = DataTransferStatus.Rejected;
                    //}
                    callResult_message = JsonConvert.SerializeObject(confNPQ3, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(confNPQ3, confNPQ3.getClass());
                }
                else
                {
                    Conf_DataTransfer conf = new Conf_DataTransfer();
                    conf.status = DataTransferStatus.UnknownVendorId;
                    callResult_message = JsonConvert.SerializeObject(conf, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(confNPQ3, confNPQ3.getClass());
                }
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.GetCompositeSchedule.ToString()))
            {
                Req_GetCompositeSchedule data = JsonConvert.DeserializeObject<Req_GetCompositeSchedule>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.GetCompositeSchedule.name()))
                //{
                //    Req_GetCompositeSchedule data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_GetCompositeSchedule.class);
                Conf_GetCompositeSchedule data_Result = new Conf_GetCompositeSchedule();
                //


                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_GetCompositeSchedule.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.GetConfiguration.ToString()))
            {

                Req_GetConfiguration data = JsonConvert.DeserializeObject<Req_GetConfiguration>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.GetConfiguration.name()))
                //{
                //    Req_GetConfiguration data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_GetConfiguration.class);
                Conf_GetConfiguration data_Result = new Conf_GetConfiguration();
                data_Result.configurationKey = new List<KeyValue>();

                if (data.key != null && data.key.Count == 1 && data.key[0].Equals("SupportedFeatureProfiles"))
                {
                    //                    String[] profiles = new String[]{"Core","FirmwareManagement","LocalAuthListManagement","Reservation","SmartCharging","RemoteTrigger"};
                    String[] profiles = new String[] { "Core" };
                    for (int i = 0; i < profiles.Length; i++)
                    {
                        KeyValue key = new KeyValue();
                        key.key = "SupportedFeatureProfiles";
                        key.Readonly = true;
                        key.value = profiles[i];

                        data_Result.configurationKey.Add(key);
                    }
                }
                else if (data.key != null && data.key.Count > 0)
                {
                    for (int i = 0; i < data.key.Count; i++)
                    {
                        KeyValue key = new KeyValue();
                        key.key = data.key[i];
                        key.value = mSettingData_OCPP_Table.getSettingData((int)EL_Manager_Enum<CONST_INDEX_OCPP_Setting>.Parse(key.key));// CONST_INDEX_OCPP_Setting.valueOf(key.key).ordinal());
                        if (mSettingData_OCPP_Table.getSettingData_Explain((int)EL_Manager_Enum<CONST_INDEX_OCPP_Setting>.Parse(key.key)).Equals("RW"))
                        {
                            key.Readonly = false;
                        }
                        else
                            key.Readonly = true;

                        data_Result.configurationKey.Add(key);
                    }
                }
                else
                {
                    for (int i = 0; i < 34; i++)
                    {
                        KeyValue key = new KeyValue();
                        key.key = mSettingData_OCPP_Table.getSettingData_Name(i);
                        key.value = mSettingData_OCPP_Table.getSettingData(i);
                        if (mSettingData_OCPP_Table.getSettingData_Explain(i).Equals("RW"))
                        {
                            key.Readonly = false;
                        }
                        else
                            key.Readonly = true;

                        data_Result.configurationKey.Add(key);
                    }
                }
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_GetConfiguration.class);
                callResult_message = callResult_message.Replace("Readonly", "readonly");
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.GetDiagnostics.ToString()))
            {
                Req_GetDiagnostics data = JsonConvert.DeserializeObject<Req_GetDiagnostics>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.GetDiagnostics.name()))
                //{
                //    Req_GetDiagnostics data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_GetDiagnostics.class);
                Conf_GetDiagnostics data_Result = new Conf_GetDiagnostics();
                //
                data_Result.fileName = "testDiagnostics";
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_GetDiagnostics.class);

                //                mStateManager_OCPP_Main.getStateManager_OCPP_DiagnosticsManager().setStart();
            }

            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.GetLocalListVersion.ToString()))
            {
                Req_GetLocalListVersion data = JsonConvert.DeserializeObject<Req_GetLocalListVersion>(((JObject)receivePacket[3]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_GetLocalListVersion.class);
                Conf_GetLocalListVersion data_Result = new Conf_GetLocalListVersion();
                //
                data_Result.listVersion = mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.LocalListVersion);
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings);
                //mGson.toJson(data_Result, Conf_GetLocalListVersion.class);
            }

            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.RemoteStartTransaction.ToString()))
            {
                Req_RemoteStartTransaction data = JsonConvert.DeserializeObject<Req_RemoteStartTransaction>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.RemoteStartTransaction.name()))
                //{
                //    Req_RemoteStartTransaction data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_RemoteStartTransaction.class);
                Conf_RemoteStartTransaction data_Result = new Conf_RemoteStartTransaction();
                //
                if (
                        //                        mStateManager_OCPP_Main.bIsPendingState == true ||
                        data.connectorId == null ||
                        data.connectorId < 1 ||
                        data.connectorId > mStateManager_OCPP_Channel.Length ||
                        data.idTag == null
                )
                {
                    data_Result.status = RemoteStartStopStatus.Rejected;
                }
                else
                {
                    data_Result.status = mStateManager_OCPP_Channel[data.connectorId.Value - 1].setOCPP_CSMS_Req_RemoteStartTransaction(data);
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_RemoteStartTransaction.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.RemoteStopTransaction.ToString()))
            {
                Req_RemoteStopTransaction data = JsonConvert.DeserializeObject<Req_RemoteStopTransaction>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.RemoteStopTransaction.name()))
                //{
                //    Req_RemoteStopTransaction data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_RemoteStopTransaction.class);
                Conf_RemoteStopTransaction data_Result = new Conf_RemoteStopTransaction();
                //
                if (mStateManager_OCPP_Main.bIsPendingState == true
                || !mStateManager_OCPP_Main.bIsNormalState)
                {
                    data_Result.status = RemoteStartStopStatus.Rejected;
                }
                else
                {
                    for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                    {
                        data_Result.status = mStateManager_OCPP_Channel[i].setOCPP_CSMS_Req_RemoteStopTransaction(data);
                        if (data_Result.status == RemoteStartStopStatus.Accepted)
                            break;
                    }
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_RemoteStopTransaction.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.ReserveNow.ToString()))
            {
                Req_ReserveNow data = JsonConvert.DeserializeObject<Req_ReserveNow>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.ReserveNow.name()))
                //{
                //    Req_ReserveNow data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_ReserveNow.class);
                Conf_ReserveNow data_Result = new Conf_ReserveNow();
                //

                if (data.connectorId == null
                    || data.connectorId > mStateManager_OCPP_Channel.Length
                    || data.reservationId == null)
                {
                    data_Result.status = ReservationStatus.Faulted;
                }
                else if (
                        data.connectorId != null
                        && data.connectorId == 0
                        && mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.ReserveConnectorZeroSupported))
                {
                    bool isReservation = false;
                    bool isUsingStart = false;
                    bool isDisable = false;
                    for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                    {
                        if (mStateManager_OCPP_Channel[i].bIsReservationProcessing)
                        {
                            if (data.reservationId != mStateManager_OCPP_Channel[i].getOCPP_CSMS_Req_ReserveNow().reservationId)
                                isReservation = true;
                        }
                        if (mStateManager_OCPP_Channel[i].bIsProcessing_Using)
                            isUsingStart = true;

                        if (!mStateManager_OCPP_Channel[i].bIsUseEnable_Channel
                            || mStateManager_OCPP_Channel[i].isNeedReset())
                            isDisable = true;
                    }
                    if (isReservation)
                        data_Result.status = ReservationStatus.Faulted;
                    else if (isUsingStart)
                        data_Result.status = ReservationStatus.Occupied;
                    else if (isDisable)
                        data_Result.status = ReservationStatus.Unavailable;
                    else
                        data_Result.status = ReservationStatus.Accepted;

                    if (data_Result.status == ReservationStatus.Accepted)
                    {
                        for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                        {
                            mStateManager_OCPP_Channel[i].setOCPP_CSMS_Req_ReserveNow(data);
                        }
                    }
                }
                else if (data.connectorId != null && data.connectorId > 0 && data.connectorId <= mStateManager_OCPP_Channel.Length)
                {
                    bool isReservation = false;
                    bool isUsingStart = false;
                    bool isDisable = false;

                    if (mStateManager_OCPP_Channel[data.connectorId.Value - 1].bIsReservation)
                    {
                        if (data.reservationId != mStateManager_OCPP_Channel[data.connectorId.Value - 1].getOCPP_CSMS_Req_ReserveNow().reservationId)
                            isReservation = true;
                    }
                    if (mStateManager_OCPP_Channel[data.connectorId.Value - 1].bIsProcessing_Using)
                        isUsingStart = true;

                    if (!mStateManager_OCPP_Channel[data.connectorId.Value - 1].bIsUseEnable_Channel)
                        isDisable = true;



                    if (isReservation)
                        data_Result.status = ReservationStatus.Faulted;
                    else if (isUsingStart)
                        data_Result.status = ReservationStatus.Occupied;
                    else if (isDisable)
                        data_Result.status = ReservationStatus.Unavailable;
                    else
                        data_Result.status = ReservationStatus.Accepted;

                    if (data_Result.status == ReservationStatus.Accepted)
                        mStateManager_OCPP_Channel[data.connectorId.Value - 1].setOCPP_CSMS_Req_ReserveNow(data);
                }
                else
                {
                    data_Result.status = ReservationStatus.Faulted;
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_ReserveNow.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.Reset.ToString()))
            {
                Req_Reset data = JsonConvert.DeserializeObject<Req_Reset>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.Reset.name()))
                //{
                //    Req_Reset data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_Reset.class);
                Conf_Reset data_Result = new Conf_Reset();

                //
                if (data.type == ResetType.Hard)
                {
                    mStateManager_OCPP_Main.bIsNeedReset_Charger_Hard = true;
                    for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                    {
                        mStateManager_OCPP_Channel[i].bIsNeedReset_Charger_Hard = true;
                        mStateManager_OCPP_Channel[i].bIsCommand_UsingStart = false;

                 
                    }
                }
                else
                {
                    mStateManager_OCPP_Main.bIsNeedReset_Charger_Soft = true;
                    for (int i = 0; i < mStateManager_OCPP_Channel.Length; i++)
                    {
                        
                        mStateManager_OCPP_Channel[i].bIsNeedReset_Charger_Soft = true;
                        mStateManager_OCPP_Channel[i].bIsCommand_UsingStart = false;
                    }
                }
                data_Result.status = ResetStatus.Accepted;
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_Reset.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.SendLocalList.ToString()))
            {
                Req_SendLocalList data = JsonConvert.DeserializeObject<Req_SendLocalList>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.SendLocalList.name()))
                //{
                //    Req_SendLocalList data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_SendLocalList.class);
                Conf_SendLocalList data_Result = new Conf_SendLocalList();
                //
                EL_Time time_Generate = new EL_Time();
                time_Generate.setTime();
                String time_String = time_Generate.getDateTime_DB();
                if (!mApplication.bOCPP_Support_CORE_LOCALAUTHLISTMANAGEMENT)
                    data_Result.status = UpdateStatus.NotSupported;
                else
                    switch (data.updateType)
                    {
                        case UpdateType.Differential:
                            if (mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.LocalListVersion) > data.listVersion)
                            {
                                data_Result.status = UpdateStatus.VersionMismatch;
                            }
                            else
                            {
                                if (mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.LocalListVersion) == data.listVersion)
                                {
                                    mSettingData_OCPP_Table.setSettingData((int)CONST_INDEX_OCPP_Setting.LocalListVersion, data.listVersion.Value);
                                }
                                data_Result.status = UpdateStatus.Accepted;
                                if (data.localAuthorizationList != null)
                                {
                                    for (int i = 0; i < data.localAuthorizationList.Count; i++)
                                    {
                                        AuthorizationData aData = data.localAuthorizationList[i];
                                        mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache()
                                                .updateOrInsertIdTagInfo_LocalList(aData.idTag, aData.idTagInfo, time_String);
                                    }
                                }
                            }

                            break;
                        case UpdateType.Full:
                            mSettingData_OCPP_Table.setSettingData((int)CONST_INDEX_OCPP_Setting.LocalListVersion, data.listVersion.Value);
                            mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache().deleteRow_LocalList();
                            if (data.localAuthorizationList != null)
                            {
                                for (int i = 0; i < data.localAuthorizationList.Count; i++)
                                {
                                    AuthorizationData aData = data.localAuthorizationList[i];
                                    mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache()
                                            .insertIdTagInfo_Authorize(aData.idTag, aData.idTagInfo, time_String);
                                }
                            }
                            data_Result.status = UpdateStatus.Accepted;
                            break;
                        default:
                            data_Result.status = UpdateStatus.Failed;
                            break;
                    }


                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_SendLocalList.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.SetChargingProfile.ToString()))
            {
                Req_SetChargingProfile data = JsonConvert.DeserializeObject<Req_SetChargingProfile>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.SetChargingProfile.name()))
                //{
                //    Req_SetChargingProfile data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_SetChargingProfile.class);
                Conf_SetChargingProfile data_Result = new Conf_SetChargingProfile();
                //
                data_Result.status = ChargingProfileStatus.Accepted;

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_SetChargingProfile.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.TriggerMessage.ToString()))
            {
                Req_TriggerMessage data = JsonConvert.DeserializeObject<Req_TriggerMessage>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.TriggerMessage.name()))
                //{

                //    Req_TriggerMessage data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_TriggerMessage.class);
                Conf_TriggerMessage data_Result = new Conf_TriggerMessage();
                if (data.connectorId != null && data.connectorId > 0 && data.connectorId <= mStateManager_OCPP_Channel.Length)
                {
                    switch (data.requestedMessage)
                    {
                        case MessageTrigger.BootNotification:
                        case MessageTrigger.DiagnosticsStatusNotification:
                        case MessageTrigger.Heartbeat:
                        case MessageTrigger.MeterValues:
                        case MessageTrigger.StatusNotification:
                        case MessageTrigger.FirmwareStatusNotification:
                            data_Result.status = TriggerMessageStatus.Accepted;
                            break;
                        default:
                            data_Result.status = TriggerMessageStatus.Rejected;
                            break;
                    }
                }
                else
                {
                    data_Result.status = TriggerMessageStatus.Rejected;
                }
                //
                switch (data_Result.status)
                {
                    case TriggerMessageStatus.Accepted:
                        req_TriggerMessage = data;
                        break;
                    case TriggerMessageStatus.NotImplemented:
                    case TriggerMessageStatus.Rejected:
                    default:
                        req_TriggerMessage = null;
                        break;
                }

                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_TriggerMessage.class);
            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.UnlockConnector.ToString()))
            {
                Req_UnlockConnector data = JsonConvert.DeserializeObject<Req_UnlockConnector>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.UnlockConnector.name()))
                //{
                //    Req_UnlockConnector data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_UnlockConnector.class);
                Conf_UnlockConnector data_Result = new Conf_UnlockConnector();
                //

                data_Result.status = UnlockStatus.NotSupported;
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings); //mGson.toJson(data_Result, Conf_UnlockConnector.class);

            }
            else if (receivePacket[2].ToString().Equals(EOCPP_Action_CSMS_Call.UpdateFirmware.ToString()))
            {
                Req_UpdateFirmware data = JsonConvert.DeserializeObject<Req_UpdateFirmware>(((JObject)receivePacket[3]).ToString());
                //else if (receivePacket.getString(2).equals(EOCPP_Action_CSMS_Call.UpdateFirmware.name()))
                //{
                //    Req_UpdateFirmware data = mGson.fromJson(((JSONObject)receivePacket.get(3)).toString(), Req_UpdateFirmware.class);
                Conf_UpdateFirmware data_Result = new Conf_UpdateFirmware();
                //
                //                mStateManager_OCPP_Main.getStateManager_OCPP_UpdateFirmware().setStart();
                callResult_message = JsonConvert.SerializeObject(data_Result, EL_MyApplication_Base.mJsonSerializerSettings);//mGson.toJson(data_Result, Conf_UpdateFirmware.class);

            }


            if (callResult_message.Length > 0)
            {
                JObject obj_Payload = JObject.Parse(callResult_message);
                callResult_Packet.Add(obj_Payload);
            }

            String callResult_Packet_String = callResult_Packet.ToString();
            mComm_Manager.sendPacket("OCPP CP->CSMS CallResult => ", callResult_Packet_String);            
            
            if (req_TriggerMessage != null)
            {
                switch (req_TriggerMessage.requestedMessage)
                {
                    case MessageTrigger.BootNotification:
                        mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_BootNotification_Directly();
                        break;
                    case MessageTrigger.DiagnosticsStatusNotification:
                        //                        mStateManager_OCPP_Main.sendOCPP_CP_Req_DiagnosticsStatusNotification(DiagnosticsStatus.);
                        break;
                    case MessageTrigger.FirmwareStatusNotification:
                        break;
                    case MessageTrigger.Heartbeat:
                        mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_Heartbeat();
                        break;
                    case MessageTrigger.MeterValues:
                        if (req_TriggerMessage.connectorId > 0)
                            mStateManager_OCPP_Channel[req_TriggerMessage.connectorId.Value - 1].mSendManager_OCPP_ChargingReq.sendOCPP_CP_Req_MeterValues("");
                        else
                            mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.sendOCPP_CP_Req_MeterValues();
                        break;
                    case MessageTrigger.StatusNotification:
                        if (req_TriggerMessage.connectorId > 0)
                            mStateManager_OCPP_Channel[req_TriggerMessage.connectorId.Value - 1].mSendManager_StatusNotification.sendOCPP_CP_Req_StatusNotification();
                        break;
                }
            }
        }//public void process_ReceivePacket_Call(JArray receivePacket)


        public void process_ReceivePacket_CallError(JArray receivePacket)
        {
            Logger.d("☆Receive☆ OCPP CSMS->CP CallResult => " + receivePacket.ToString());

            EL_OCPP_Packet_Wrapper packet_Wrapper = null;
            for (int i = 0; i < mSendManager_OCPP.Length; i++)
            {
                packet_Wrapper = mSendManager_OCPP[i].setReceivePacket_CallError(receivePacket);
                if (packet_Wrapper != null)
                {
                    if (mSendManager_OCPP[i].mStateManager_OCPP_Channel != null)
                        if (mSendManager_OCPP[i].mStateManager_OCPP_Channel.bIsProcessing_Using)
                        {
                            mSendManager_OCPP[i].mStateManager_OCPP_Channel.mErrorReason = "서버 비정상 응답 (" + receivePacket.ToString() + ")";
                            mSendManager_OCPP[i].mStateManager_OCPP_Channel.bIsErrorOccured = true;
                        }

                    break;
                }

            }
            if (packet_Wrapper == null)
                return;


            //try
            //{
            if (packet_Wrapper != null && packet_Wrapper.mPacket[1].ToString().Equals(receivePacket[1].ToString()))
            {
                EL_Time time_Generate = new EL_Time();
                time_Generate.setTime();
                String time_String = time_Generate.getDateTime_DB();

                String sendPacket_ActionName = packet_Wrapper.mPacket[2].ToString();
            }




            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}


            //        try {
            //            Logger.d("☆Receive☆ OCPP CSMS->CP CallError => " + receivePacket.toString());
            //            if(mPacket_SendPacket_Call_CP != null && mPacket_SendPacket_Call_CP.getString(1).equals(receivePacket.getString(1))) {
            //                mStateManager_OCPP_Main.setOCPPError_Message("OCPP CallError ("+receivePacket.getString(1)+")");
            //
            //            }
            //        } catch (JSONException e) {
            //            e.printStackTrace();
            //        }
        }

        public override void intervalExcuteAsync()
        {
            if (!bInit_StateManager)
                return;
            process_ReconnectSocket();

            if (mSendManager_OCPP != null)
            {
                for (int i = 0; i < mSendManager_OCPP.Length; i++)
                {
                    if (mSendManager_OCPP[i] != null)
                    {
                        //try
                        //{
                        //    Thread.sleep(200);
                        //}
                        //catch (InterruptedException e)
                        //{
                        //    e.printStackTrace();
                        //}
                        mSendManager_OCPP[i].intervalExcuteAsync();
                        Thread.Sleep(200);

                    }
                }

            }
            process_sendCall_HeartBeat();
        }



        public void setReceiveData(JArray receivePacket)
        {


            int value = 0;

            mStateManager_OCPP_Main.mSendManager_OCPP_CP_Req_Normal.initTime_Send();
            //try
            //{
            value = (int)receivePacket[0];

            //}
            //catch (JSONException e)
            //{
            //    e.printStackTrace();
            //}

            switch (value)
            {
                
                //CALL
                case 2:
                    process_ReceivePacket_Call(receivePacket);
                    mComm_Manager.onConnect_Software();
                    mStateManager_OCPP_Main.mTime_Send_HeartBeat.setTime();
                    break;
                //CALL Result
                case 3:
                    process_ReceivePacket_CallResult(receivePacket);
                    mComm_Manager.onConnect_Software();
                    mStateManager_OCPP_Main.mTime_Send_HeartBeat.setTime();
                     break;
                //Error
                case 4:
                    process_ReceivePacket_CallError(receivePacket);
                    break;
                default:
                    break;
            }
            if (EL_DC_Charger_MyApplication.getInstance().offlineTest_isuse)
            {
                mComm_Manager.ws.Abort();
            }

        }
    }
}
