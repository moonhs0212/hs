using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class EL_SendManager_OCPP_CP_Req_Normal : EL_SendManager_OCPP_Base
    {
        public EL_SendManager_OCPP_CP_Req_Normal(OCPP_Comm_Manager ocpp_comm_manager)
            : base(ocpp_comm_manager, 0, false)
        {

        }

        public void sendOCPP_CP_Req_Heartbeat()
        {
            Req_Heartbeat heartbeat = new Req_Heartbeat();

            setSendPacket_Call_CP(
                    heartbeat.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //heartbeat.getClass().getSimpleName().split("_")[1],
                    JsonConvert.SerializeObject(heartbeat, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(heartbeat, heartbeat.getClass()));
        }



        override public void intervalExcuteAsync()
        {
            base.intervalExcuteAsync();
        }

        /*------------------------------------------------------------------------------------------
             CP->CSMS CALL
             ------------------------------------------------------------------------------------------*/
        public void sendOCPP_CP_Req_BootNotification()
        {
            Req_BootNotification bootNotification = new Req_BootNotification();
            bootNotification.setRequiredValue(mStateManager_OCPP_Main.getOCPP_MainInfor().getChargePointModel(), mStateManager_OCPP_Main.getOCPP_MainInfor().getChargePointVendor());
            setSendPacket_Call_CP(
                bootNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //bootNotification.getClass().getSimpleName().split("_")[1], 
                JsonConvert.SerializeObject(bootNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(bootNotification, bootNotification.getClass()));
        }

        public void sendOCPP_CP_Req_BootNotification_Directly()
        {
            Req_BootNotification bootNotification = new Req_BootNotification();
            bootNotification.setRequiredValue(mStateManager_OCPP_Main.getOCPP_MainInfor().getChargePointModel(), mStateManager_OCPP_Main.getOCPP_MainInfor().getChargePointVendor());

            setSendPacket_Call_CP(
                bootNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //bootNotification.getClass().getSimpleName().split("_")[1], 
                JsonConvert.SerializeObject(bootNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(bootNotification, bootNotification.getClass()));
            mDelay_First = 0;
        }

        protected List<MeterValue> mOCPP_List_MeterValue_Clock_Aligned = new List<MeterValue>();
        public void sendOCPP_CP_Req_MeterValues()
        {
            sendOCPP_CP_Req_MeterValues(mOCPP_List_MeterValue_Clock_Aligned);
        }

        protected void setOCPP_MeterValue_Sample_Clock_Aligned()
        {
            MeterValue meterValue = new MeterValue();
            List<SampledValue> list_SampledValue = new List<SampledValue>();
            SampledValue sampledValue = new SampledValue();
            sampledValue.setRequiredValue("");
            list_SampledValue.Add(sampledValue);
            meterValue.setRequiredValue(new EL_Time().toString_OCPP(), list_SampledValue);
            mOCPP_List_MeterValue_Clock_Aligned.Add(meterValue);
        }

        public void sendOCPP_CP_Req_MeterValues(List<MeterValue> list_MeterValue)
        {
            Req_MeterValues meterValues = new Req_MeterValues();
            //        ArrayList<MeterValue> list_MeterValue = new ArrayList<>();
            //        MeterValue meterValue = new MeterValue();
            //        ArrayList<SampledValue> list_SampledValue = new ArrayList<SampledValue>();
            //        SampledValue sampledValue = new SampledValue();
            //        sampledValue.setRequiredValue(""+(mOCPP_MeterValue_ChargingWattage_Current/1000.0f));
            //        list_SampledValue.add(sampledValue);
            //        meterValue.setRequiredValue(mTime_ChargingStart.toString_OCPP(), list_SampledValue);
            //        list_MeterValue.add(meterValue);

            meterValues.setRequiredValue(mChannelIndex, list_MeterValue);

            setSendPacket_Call_CP(
                    meterValues.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //meterValues.getClass().getSimpleName().split("_")[1],
                    JsonConvert.SerializeObject(meterValues, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(meterValues, meterValues.getClass()));
        }

        public void sendOCPP_CP_Req_Heartbeat_Directly()
        {

            if (mStateManager_OCPP_Main.mOCPP_CSMS_Conf_BootNotification != null)
            {
                Req_Heartbeat heartbeat = new Req_Heartbeat();
                setSendPacket_Call_CP(
                    heartbeat.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //heartbeat.getClass().getSimpleName().split("_")[1], 
                    JsonConvert.SerializeObject(heartbeat, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(heartbeat, heartbeat.getClass()));

                mDelay_First = /*0;*/ mStateManager_OCPP_Main.mOCPP_CSMS_Conf_BootNotification.interval.Value;
            }
            else
            {
                Logger.d("!!Error:Not Receive BootNotification");
            }

        }



        override protected String getLogTag()
        {
            return "EL_SendManager_OCPP_CP_Req_Normal";
        }


        override protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {
            if (sendPacket.mActionName.Equals(EOCPP_Action_CP_Call.Heartbeat.ToString()))
            {
                mStateManager_OCPP_Main.bOCPP_IsReceivePacket_CallResult_HeartBeat = true;
            }

        }


        override protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }



    }

}
