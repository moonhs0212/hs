using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class EL_SendManager_OCPP_ChargingReq : EL_SendManager_OCPP_Base
    {
        public bool bIsSendMeterValue_First = false;

        //전체 주석처리
        //public void sendOCPP_CP_Req_StopTransaction(String transactionInfor_DBId, String packetString)
        //{
        //    mTable_TransactionInfor.db_Req_StopTransaction(mOCPP_TransactionInfor_DBId, packetString);
        //    //try
        //    //{
        //    JArray jsonArray = JArray.Parse(packetString);
        //    Req_StopTransaction data_req = JsonConvert.DeserializeObject<Req_StopTransaction>(jsonArray[3].ToString());//mGson.fromJson(jsonArray[3], Req_StopTransaction.class);

        //    addReq_By_PayloadString(
        //            stopTransaction.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],//stopTransaction.GetType().Name.split("_")[1],
        //            JsonConvert.SerializeObject(stopTransaction, EL_MyApplication_Base.mJsonSerializerSettings));//mGson.toJson(stopTransaction, stopTransaction.getClass()));

        //    //} catch (JSONException e) {
        //    //    e.printStackTrace();
        //    //}
        //}

        public void sendOCPP_CP_Req_StartTransaction()
        {

            stopTransaction.reason = null;
            Req_StartTransaction startTransaction = new Req_StartTransaction();
            startTransaction.setRequiredValue(
                    mChannelIndex,
                    mStateManager_OCPP_Channel.mCardNumber_Member,
                    (long)((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                    mStateManager_OCPP_Channel.mTime_ChargingStart.toString_OCPP());
            if (mStateManager_OCPP_Channel.mOCPP_ReservationId != null)
                startTransaction.reservationId = mStateManager_OCPP_Channel.mOCPP_ReservationId.Value;
            JArray packetString = addReq_By_PayloadString(
                    startTransaction.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],//startTransaction.GetType().Name.split("_")[1],
                    JsonConvert.SerializeObject(startTransaction, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(startTransaction, startTransaction.getClass()));

            mTable_TransactionInfor.db_Req_StartTransaction(mStateManager_OCPP_Channel.mTransactionInfor_DBId,
                 packetString.ToString());
            saveOCPP_CP_Req_StopTransaction();
        }
        protected String mDivide_MeterValue = "";
        public void sendOCPP_CP_Req_MeterValues(string divide)
        {
            sendOCPP_CP_Req_MeterValues(divide, mOCPP_List_MeterValue_Charging);
            mOCPP_List_MeterValue_Charging.Clear();
        }

        public void sendOCPP_CP_Req_MeterValues(string divide, List<MeterValue> list_MeterValue)
        {
            Req_MeterValues meterValues = new Req_MeterValues();


            meterValues.setRequiredValue(mChannelIndex, list_MeterValue);
            if (mStateManager_OCPP_Channel.mOCPP_TransactionID != null)
                meterValues.transactionId = mStateManager_OCPP_Channel.mOCPP_TransactionID.Value;

            mDivide_MeterValue = divide;

            JArray packetString = addReq_By_PayloadString(
                    meterValues.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    JsonConvert.SerializeObject(meterValues, EL_MyApplication_Base.mJsonSerializerSettings));

            String packetString_Text = packetString.ToString();

            if (mDivide_MeterValue.Equals("first"))
            {
                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Req_MeterValue_Start(
                        "" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, packetString_Text);
            }
            else if (mDivide_MeterValue.Equals("last"))
            {
                mApplication.getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_Req_MeterValue_Complete(
                        "" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, packetString_Text);
            }

            try
            {
                mTable_Transaction_Metervalues.db_Req_MeterValues("" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, packetString[1].ToString(),
            mChannelIndex, "" + mStateManager_OCPP_Channel.mOCPP_TransactionID, packetString.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void sendOCPP_CP_Req_StopTransaction()
        {
            mStateManager_OCPP_Channel.mTime_ChargingStop.setTime();

            stopTransaction.initVariable();
            if (mStateManager_OCPP_Channel.mCardNumber_Member != null)
            {
                stopTransaction.idTag = mStateManager_OCPP_Channel.mCardNumber_Member;
            }
            if (mOCPP_CSMS_Conf_StartTransaction != null && mOCPP_CSMS_Conf_StartTransaction.transactionId != null)
            {
                stopTransaction.setInfor_Required(mStateManager_OCPP_Channel.mOCPP_MeterValue_ChargingWattage_Finish,
                        mStateManager_OCPP_Channel.mTime_ChargingStop.toString_OCPP(),
                        mOCPP_CSMS_Conf_StartTransaction.transactionId);
            }
            else
            {
                stopTransaction.setInfor_Required(mStateManager_OCPP_Channel.mOCPP_MeterValue_ChargingWattage_Finish,
                        mStateManager_OCPP_Channel.mTime_ChargingStop.toString_OCPP(),
                        null);
            }

            stopTransaction.reason = mStateManager_OCPP_Channel.mOCPP_StopTransaction_Reason;
            if (mOCPP_List_MeterValue_Charging.Count > 0)
            {
                stopTransaction.transactionData = mOCPP_List_MeterValue_StopTransaction;
            }
            JArray packetString = addReq_By_PayloadString(
                    stopTransaction.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    JsonConvert.SerializeObject(stopTransaction, EL_MyApplication_Base.mJsonSerializerSettings));

            mOCPP_List_MeterValue_Charging.Clear();
            mOCPP_List_MeterValue_StopTransaction.Clear();

            mTable_TransactionInfor.db_Req_StopTransaction("" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, packetString.ToString());
        }


        public void saveOCPP_CP_Req_StopTransaction()
        {

            if (mStateManager_OCPP_Channel.mCardNumber_Member != null)
            {
                stopTransaction.idTag = mStateManager_OCPP_Channel.mCardNumber_Member;
            }
            EL_Time time = new EL_Time();
            time.setTime();
            if (mOCPP_CSMS_Conf_StartTransaction != null)
            {
                stopTransaction.setInfor_Required((long)((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                        time.toString_OCPP(),
                        mOCPP_CSMS_Conf_StartTransaction.transactionId);
            }
            else
            {
                stopTransaction.setInfor_Required((long)((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                        time.toString_OCPP(),
                        null);
            }
            if (mStateManager_OCPP_Channel.mOCPP_StopTransaction_Reason == null)
                stopTransaction.reason = Reason.PowerLoss;
            else
                stopTransaction.reason = mStateManager_OCPP_Channel.mOCPP_StopTransaction_Reason;
            String payloadString = JsonConvert.SerializeObject(stopTransaction, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = stopTransaction.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));
                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }
        }

        public void saveOCPP_CP_Req_StopTransaction(Reason reason)
        {

            if (mStateManager_OCPP_Channel.mCardNumber_Member != null)
            {
                stopTransaction.idTag = mStateManager_OCPP_Channel.mCardNumber_Member;
            }
            EL_Time time = new EL_Time();
            time.setTime();
            if (mOCPP_CSMS_Conf_StartTransaction != null)
            {
                stopTransaction.setInfor_Required((long)((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                        time.toString_OCPP(),
                        mOCPP_CSMS_Conf_StartTransaction.transactionId);
            }
            else
            {
                stopTransaction.setInfor_Required((long)((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh(),
                        time.toString_OCPP(),
                        null);
            }
            mStateManager_OCPP_Channel.mOCPP_StopTransaction_Reason = reason;
            stopTransaction.reason = reason;

            String payloadString = JsonConvert.SerializeObject(stopTransaction, EL_MyApplication_Base.mJsonSerializerSettings);
            String actionName = stopTransaction.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(actionName);
            if (payloadString != null && payloadString.Length > 0)
            {
                //try
                //{
                call_Packet.Add(JObject.Parse(payloadString));
                //}
                //catch (JSONException e)
                //{
                //    e.printStackTrace();
                //}
            }

            mTable_TransactionInfor.db_Conf_StopTransaction_Temp("" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, call_Packet.ToString());
        }


        override protected String getLogTag()
        {
            return null;
        }

        //전체 주석처리
        override protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

            EL_Time time_Generate = new EL_Time();
            time_Generate.setTime();
            String time_String = time_Generate.getDateTime_DB();
            mStateManager_OCPP_Channel.setIsOffLine(false);


            if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.MeterValues.ToString()))
            {
                Conf_MeterValues data = JsonConvert.DeserializeObject<Conf_MeterValues>(((JObject)receivePacket[2]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_MeterValues.class);

                mTable_Transaction_Metervalues.db_Conf_MeterValues(sendPacket.mUniqueId);
                //처리할 필요 없을듯
            }
            else if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.StartTransaction.ToString()))
            {
                Conf_StartTransaction data = JsonConvert.DeserializeObject<Conf_StartTransaction>(((JObject)receivePacket[2]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_StartTransaction.class);
                Req_StartTransaction data_req = JsonConvert.DeserializeObject<Req_StartTransaction>(((JObject)sendPacket.mPacket[3]).ToString());
                //mGson.fromJson(((JSONObject)sendPacket.mPacket.get(3)).toString(), Req_StartTransaction.class);

                setOCPP_CSMS_Conf_StartTransaction(data, receivePacket);

                mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache()
                                .updateOrInsertIdTagInfo_Authorize(data_req.idTag, data.idTagInfo, time_String);

                mTable_TransactionInfor.db_idTag_Card(mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, data_req.idTag, EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mPaymentType.ToString());

                bOCPP_CSMS_Conf_StartTransaction = true;
            }
            else if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.StopTransaction.ToString()))
            {
                Conf_StopTransaction data = JsonConvert.DeserializeObject<Conf_StopTransaction>(((JObject)receivePacket[2]).ToString());
                //mGson.fromJson(((JSONObject)receivePacket.get(2)).toString(), Conf_StopTransaction.class);
                Req_StopTransaction data_req = JsonConvert.DeserializeObject<Req_StopTransaction>(((JObject)sendPacket.mPacket[3]).ToString());
                //mGson.fromJson(((JSONObject)sendPacket.mPacket.get(3)).toString(), Req_StopTransaction.class);
                mApplication.getManager_SQLite_Setting_OCPP().getTable_AuthCache()
                    .updateOrInsertIdTagInfo_Authorize(data_req.idTag, data.idTagInfo, time_String);
                setOCPP_Conf_StopTransaction(data, receivePacket);
                bOCPP_Conf_StopTransaction = true;
            }

        }

        override protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {
            EL_Time time_Generate = new EL_Time();
            time_Generate.setTime();
            String time_String = time_Generate.getDateTime_DB();
            mStateManager_OCPP_Channel.setIsOffLine(false);

            //        try {
            if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.MeterValues.ToString()))
            {

                //처리할 필요 없을듯
            }
            else if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.StartTransaction.ToString()))
            {
                mStateManager_OCPP_Channel.mErrorReason = "StartTransaction CallError";
                mStateManager_OCPP_Channel.bIsErrorOccured = true;
            }
            else if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.StopTransaction.ToString()))
            {
                mStateManager_OCPP_Channel.mErrorReason = "StopTransaction CallError";
                mStateManager_OCPP_Channel.bIsErrorOccured = true;
            }
            //        } catch (JSONException e) {
            //            e.printStackTrace();
            //        }
        }


        protected int getSecond_Delay()
        {
            return 10;
        }

        public Conf_StartTransaction mOCPP_CSMS_Conf_StartTransaction = null;
        public Conf_StartTransaction getOCPP_CSMS_Conf_StartTransaction()
        {
            return mOCPP_CSMS_Conf_StartTransaction;
        }

        public void setOCPP_CSMS_Conf_StartTransaction(Conf_StartTransaction conf, JArray receivePacket)
        {
            bIsSendMeterValue_First = false;
            mOCPP_CSMS_Conf_StartTransaction = conf;
            mStateManager_OCPP_Channel.mOCPP_TransactionID = conf.transactionId;
            lock (mPacket_SendList)
            {
                for (int i = 0; i < mPacket_SendList.Count; i++)
                {
                    if (mPacket_SendList[i].mActionName.Equals(EOCPP_Action_CP_Call.MeterValues.ToString()))
                    {
                        //        try {
                        Req_MeterValues data = JsonConvert.DeserializeObject<Req_MeterValues>(mPacket_SendList[i].mPacket[3].ToString());//mGson.fromJson(((JObject)mPacket_SendList.get(i).mPacket.get(3)).ToString(), Req_MeterValues.class);
                        data.transactionId = conf.transactionId.Value;
                        String jsonString = JsonConvert.SerializeObject(data, EL_MyApplication_Base.mJsonSerializerSettings);// mGson.toJson(data, Req_MeterValues.class);
                        JObject obj_Payload = JObject.Parse(jsonString);
                        mPacket_SendList[i].mPacket.RemoveAt(3);
                        mPacket_SendList[i].mPacket.Insert(3, obj_Payload);
                        //        } catch (JSONException e) {
                        //            e.printStackTrace();
                        //        }
                    }
                    else if (mPacket_SendList[i].mActionName.Equals(EOCPP_Action_CP_Call.StopTransaction.ToString()))
                    {
                        //try
                        //    {
                        Req_StopTransaction data = JsonConvert.DeserializeObject<Req_StopTransaction>(mPacket_SendList[i].mPacket[3].ToString());//   mGson.fromJson(((JObject)mPacket_SendList.get(i).mPacket.get(3)).ToString(), Req_StopTransaction.class);
                        data.transactionId = conf.transactionId;
                        String jsonString = JsonConvert.SerializeObject(data, EL_MyApplication_Base.mJsonSerializerSettings);
                        JObject obj_Payload = JObject.Parse(jsonString);
                        mPacket_SendList[i].mPacket.RemoveAt(3);
                        mPacket_SendList[i].mPacket.Insert(3, obj_Payload);
                        //} catch (JSONException e) {
                        //    e.printStackTrace();
                        //}
                    }
                }
            }


            if (conf.idTagInfo != null)
            {
                mStateManager_OCPP_Channel.mOCPP_ParentIdTag = conf.idTagInfo.parentIdTag;
                if (conf.idTagInfo.status != null)
                {
                    switch (conf.idTagInfo.status)
                    {

                        case AuthorizationStatus.Accepted:
                            mStateManager_OCPP_Channel.setIsOffLine(false);
                            bOCPP_CSMS_Conf_StartTransaction = true;
                            mTable_TransactionInfor.db_Conf_startTransaction("" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, "" + conf.transactionId, receivePacket.ToString());
                            mStateManager_OCPP_Channel.setIsOffLine(false);
                            saveOCPP_CP_Req_StopTransaction();
                            break;
                        case AuthorizationStatus.Blocked:
                        case AuthorizationStatus.Expired:
                        case AuthorizationStatus.Invalid:
                        case AuthorizationStatus.ConcurrentTx:

                            //스탑을 하지 않으면
                            if (!mSettingData_OCPP_Table.getSettingData_Boolean(((int)CONST_INDEX_OCPP_Setting.StopTransactionOnInvalidId)))
                            {
                                //mStateManager_OCPP_Channel.mOCPP_StopTransaction_Reason = Reason.DeAuthorized;//mStateManager_OCPP_Channel.mSendManager_StatusNotification.setOCPP_ChargePointStatus(ChargePointStatus.SuspendedEVSE);
                            }
                            else
                            {
                                mStateManager_OCPP_Channel.moveToError(
                                      Const_ErrorCode.CODE_0012_SERVER_CERTIFICATION_ERROR,
                                      Reason.DeAuthorized,
                                      ChargePointStatus.Finishing);
                                //mStateManager_OCPP_Channel.setErrorOccured("인증 오류");
                            }

                            break;
                    }
                }
            }



        }
        String divide = "";

        public void setOCPP_MeterValue_Sample_Charging()
        {
            MeterValue meterValue = new MeterValue();
            List<SampledValue> list_SampledValue = new List<SampledValue>();


            float voltage = ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getAMI_PacketManager().getVoltage();
            float current = ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getAMI_PacketManager().getCurrent();

            SampledValue sampledValue = new SampledValue();

            sampledValue.setRequiredValue("" + (int)(current));
            sampledValue.format = ValueFormat.Raw;
            sampledValue.context = "Sample.Periodic";
            sampledValue.measurand = "Current.Export";
            sampledValue.unit = UnitOfMeasure.A;
            sampledValue.location = Location.Cable;
            list_SampledValue.Add(sampledValue);

            sampledValue = new SampledValue();
            sampledValue.setRequiredValue("" + (int)(voltage));
            sampledValue.format = ValueFormat.Raw;
            sampledValue.context = "Sample.Periodic";
            sampledValue.measurand = "Voltage";
            sampledValue.location = Location.Cable;
            sampledValue.unit = UnitOfMeasure.V;
            list_SampledValue.Add(sampledValue);

            //!!NeedImplements
            switch (mApplication.getPlatform_Operator())
            {
                case EPlatformOperator.WEV:
                    if (!bIsSendMeterValue_First)
                    {

                        divide = "first";
                        bIsSendMeterValue_First = true;

                        sampledValue = new SampledValue();
                        sampledValue.setRequiredValue("EIM");
                        sampledValue.format = ValueFormat.Raw;
                        sampledValue.context = "Transaction.Begin";
                        sampledValue.measurand = "EV.Payment.Option";
                        sampledValue.unit = UnitOfMeasure.String;
                        sampledValue.location = Location.EV;
                        list_SampledValue.Add(sampledValue);

                        sampledValue = new SampledValue();
                        sampledValue.measurand = "EVSE.Accumulate.Watt";
                        sampledValue.format = ValueFormat.Raw;
                        sampledValue.context = "Transaction.Begin";
                        sampledValue.location = Location.Body;
                        sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargedWattage();
                        //sampledValue.value = "0";
                        sampledValue.unit = UnitOfMeasure.Wh;
                        list_SampledValue.Add(sampledValue);
                    }
                    else
                    {
                        sampledValue = new SampledValue();
                        sampledValue.measurand = "EVSE.Accumulate.Watt";
                        sampledValue.format = ValueFormat.Raw;
                        sampledValue.context = "Sample.Periodic";
                        sampledValue.location = Location.Body;
                        sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargedWattage();
                        sampledValue.unit = UnitOfMeasure.Wh;
                        list_SampledValue.Add(sampledValue);
                    }

                    sampledValue = new SampledValue();
                    sampledValue.measurand = "Charging.Charge";
                    sampledValue.format = ValueFormat.Raw;
                    sampledValue.context = "Sample.Periodic";
                    //sampledValue.value = mApplication.getChannelTotalInfor(mChannelIndex).getStateManager_Channel().getChargedCharge();
                    sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargedCharge();
                    sampledValue.location = Location.Body;
                    sampledValue.unit = UnitOfMeasure.Won;
                    list_SampledValue.Add(sampledValue);
                    break;
            }

            //ChargingGun Temp
            sampledValue = new SampledValue();
            sampledValue.measurand = "Temperature";
            sampledValue.format = ValueFormat.Raw;
            sampledValue.context = "Sample.Periodic";
            sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_1z.mChargingGun_Temp_1;
            sampledValue.location = Location.Cable;
            sampledValue.unit = UnitOfMeasure.Celsius;
            list_SampledValue.Add(sampledValue);

            sampledValue = new SampledValue();
            sampledValue.measurand = "Temperature";
            sampledValue.format = ValueFormat.Raw;
            sampledValue.context = "Sample.Periodic";
            sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_1z.mChargingGun_Temp_2;
            sampledValue.location = Location.Body;
            sampledValue.unit = UnitOfMeasure.Celsius;
            list_SampledValue.Add(sampledValue);

            EL_Time time = new EL_Time();
            time.setTime();
            meterValue.setRequiredValue(time.toString_OCPP(), list_SampledValue);
            mOCPP_List_MeterValue_Charging.Add(meterValue);
            sendOCPP_CP_Req_MeterValues(divide);


            //sampledValue.setRequiredValue("" + (int)(((voltage * current))));
            //sampledValue.format = ValueFormat.Raw;
            //sampledValue.measurand = "Power.Active.Export";
            //sampledValue.unit = UnitOfMeasure.W;
            //EL_Time time = new EL_Time();
            //time.setTime();
            //list_SampledValue.Add(sampledValue);

            ////!!NeedImplements
            ////        switch (mApplication.getPlatform_Operator())
            ////        {
            ////            case WEV:
            ////                sampledValue = new SampledValue();
            //////                sampledValue.value = mApplication.getChannelTotalInfor(mChannelIndex).
            //////                sampledValue.format = ValueFormat.Raw;
            ////                sampledValue.measurand = "Charging.Charge";
            ////                sampledValue.unit = UnitOfMeasure.Won;
            ////                list_SampledValue.add(sampledValue);
            ////                break;
            ////        }



            //meterValue.setRequiredValue(time.toString_OCPP(), list_SampledValue);
            //mOCPP_List_MeterValue_Charging.Add(meterValue);
            //sendOCPP_CP_Req_MeterValues();
            //int size = mSettingData_OCPP_Table.getSettingData_Int(((int)CONST_INDEX_OCPP_Setting.StopTxnSampledDataMaxLength)); ;
            ////        if(mOCPP_List_MeterValue_Charging.size() < size)
            ////        {
            ////            mOCPP_List_MeterValue_Charging.add(meterValue);
            ////        }
        }

        public void setOCPP_MeterValue_Sample_Before_ChargingComplete(int errorCode, String errorMessage)
        {
            MeterValue meterValue = new MeterValue();
            List<SampledValue> list_SampledValue = new List<SampledValue>();
            String divide = "";
            SampledValue sampledValue = null;
            //!!NeedImplements
            switch (mApplication.getPlatform_Operator())
            {
                case EPlatformOperator.WEV:
                    if (!bIsSendMeterValue_First)
                    {
                        bIsSendMeterValue_First = true;
                        divide = "total";
                        sampledValue = new SampledValue();
                        sampledValue.setRequiredValue("EIM");
                        sampledValue.format = ValueFormat.Raw;
                        sampledValue.context = "Transaction.Begin";
                        sampledValue.measurand = "EV.Payment.Option";
                        sampledValue.unit = UnitOfMeasure.String;
                        sampledValue.location = Location.EV;
                        list_SampledValue.Add(sampledValue);

                        sampledValue = new SampledValue();
                        sampledValue.measurand = "EVSE.Accumulate.Watt";
                        sampledValue.format = ValueFormat.Raw;
                        sampledValue.context = "Transaction.Begin";
                        sampledValue.location = Location.Body;
                        sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargedWattage();
                        sampledValue.unit = UnitOfMeasure.Wh;
                        list_SampledValue.Add(sampledValue);

                    }
                    else
                    {
                        divide = "last";
                    }
                    sampledValue = new SampledValue();
                    sampledValue.measurand = "EVSE.Accumulate.Watt";
                    sampledValue.format = ValueFormat.Raw;
                    sampledValue.context = "Transaction.End";
                    sampledValue.location = Location.Body;
                    sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargedWattage();
                    sampledValue.unit = UnitOfMeasure.Wh;
                    list_SampledValue.Add(sampledValue);

                    sampledValue = new SampledValue();
                    sampledValue.measurand = "Charging.Charge";
                    sampledValue.format = ValueFormat.Raw;
                    sampledValue.context = "Transaction.End";
                    sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).mChargingCharge.getChargedCharge();
                    sampledValue.unit = UnitOfMeasure.Won;
                    list_SampledValue.Add(sampledValue);



                    sampledValue = new SampledValue();
                    sampledValue.measurand = "ErrorCode";
                    sampledValue.format = ValueFormat.Raw;
                    sampledValue.context = "Transaction.End";
                    sampledValue.value = "" + errorCode;
                    sampledValue.location = Location.Body;
                    sampledValue.unit = UnitOfMeasure.String;
                    list_SampledValue.Add(sampledValue);

                    sampledValue = new SampledValue();
                    sampledValue.measurand = "ErrorMessage";
                    sampledValue.format = ValueFormat.Raw;
                    sampledValue.context = "Transaction.End";
                    sampledValue.value = errorMessage;
                    sampledValue.location = Location.Body;
                    sampledValue.unit = UnitOfMeasure.String;
                    list_SampledValue.Add(sampledValue);
                    break;
            }
            sampledValue = null;
            //ChargingGun Temp
            sampledValue = new SampledValue();
            sampledValue.measurand = "Temperature";
            sampledValue.format = ValueFormat.Raw;
            sampledValue.context = "Sample.Periodic";
            sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_1z.mChargingGun_Temp_2;
            sampledValue.location = Location.Cable;
            sampledValue.unit = UnitOfMeasure.Celsius;
            list_SampledValue.Add(sampledValue);

            sampledValue = new SampledValue();
            sampledValue.measurand = "Temperature";
            sampledValue.format = ValueFormat.Raw;
            sampledValue.context = "Sample.Periodic";
            sampledValue.value = "" + mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().packet_1z.mChargingGun_Temp_2;
            sampledValue.location = Location.Body;
            sampledValue.unit = UnitOfMeasure.Celsius;
            list_SampledValue.Add(sampledValue);


            EL_Time time = new EL_Time();
            time.setTime();
            meterValue.setRequiredValue(time.toString_OCPP(), list_SampledValue);
            mOCPP_List_MeterValue_Charging.Add(meterValue);
            sendOCPP_CP_Req_MeterValues(divide);
            int size = mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.StopTxnSampledDataMaxLength); ;
            //        if(mOCPP_List_MeterValue_Charging.size() < size)
            //        {
            //            mOCPP_List_MeterValue_Charging.add(meterValue);
            //        }
        }

        public void setOCPP_Conf_StopTransaction(Conf_StopTransaction conf_StopTransaction, JArray receivePacket)
        {
            mConf_StopTransaction = conf_StopTransaction;
            bOCPP_Conf_StopTransaction = true;
            mTable_TransactionInfor.db_Conf_StopTransaction("" + mChannelTotalInfor.getStateManager_Channel().mTransactionInfor_DBId, receivePacket.ToString());
        }

        public bool bOCPP_CSMS_Conf_StartTransaction = false;
        protected List<MeterValue> mOCPP_List_MeterValue_Charging = new List<MeterValue>();
        protected List<MeterValue> mOCPP_List_MeterValue_StopTransaction = new List<MeterValue>();
        public bool bOCPP_Conf_StopTransaction = false;
        public Conf_StopTransaction mConf_StopTransaction = null;
        Req_StopTransaction stopTransaction = new Req_StopTransaction();

        public EL_SendManager_OCPP_ChargingReq(OCPP_Comm_Manager ocpp_comm_manager, int channelIndex, EL_StateManager_OCPP_Channel stateManager_OCPP_Channel)
            : base(ocpp_comm_manager, channelIndex, true)
        {


            mStateManager_OCPP_Channel = stateManager_OCPP_Channel;
        }
    }
}
