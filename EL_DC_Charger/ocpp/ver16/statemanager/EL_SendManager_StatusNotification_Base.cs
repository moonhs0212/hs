using EL_DC_Charger.common.application;
using EL_DC_Charger.common.ChargerVariable;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
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
    abstract public class EL_SendManager_StatusNotification_Base : EL_SendManager_OCPP_Base
    {





        public EL_SendManager_StatusNotification_Base(OCPP_Comm_Manager ocpp_comm_manager, int channelIndex)
            : base(ocpp_comm_manager, channelIndex, true)
        {
            setDelay_First_Default();
        }

        public ChargePointStatus mOCPP_ChargePointStatus = ChargePointStatus.NONE;
        protected ChargePointErrorCode mOCPP_ChargePointErrorCode = ChargePointErrorCode.NoError;

        bool bIsSend_First = false;


        virtual public void sendOCPP_CP_Req_StatusNotification_Wev_Booting()
        {
            Req_StatusNotification statusNotification = new Req_StatusNotification();
            statusNotification.setRequiredValue_Wev(mChannelIndex, ChargePointErrorCode.NoError, ChargePointStatus.Available,
                    "MaxChargingCapacityKw", CCharger_Variable.MaxChargingCapacityKw);

            addReq_By_PayloadString(
                statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                //statusNotification.getClass().getSimpleName().split("_")[1],
                JsonConvert.SerializeObject(statusNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(statusNotification, statusNotification.getClass()));

            if (mChannelIndex > 0)
            {
                statusNotification = new Req_StatusNotification();
                statusNotification.setRequiredValue_Wev(mChannelIndex, ChargePointErrorCode.NoError, ChargePointStatus.Available,
                        "ConnectorType", ConnectorType.DCCCSType1.ToString());

                addReq_By_PayloadString(
                    statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //statusNotification.getClass().getSimpleName().split("_")[1],
                    JsonConvert.SerializeObject(statusNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(statusNotification, statusNotification.getClass()));
            }
        }


        public void sendOCPP_CP_Req_StatusNotification_Dreictly_Once()
        {
            Req_StatusNotification statusNotification = new Req_StatusNotification();
            statusNotification.setRequiredValue(mChannelIndex, mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus);
            JArray call_Packet = new JArray();
            call_Packet.Add(2);
            call_Packet.Add(Guid.NewGuid().ToString());
            call_Packet.Add(statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1]);
            call_Packet.Add(JObject.Parse(JsonConvert.SerializeObject(statusNotification, EL_MyApplication_Base.mJsonSerializerSettings)));
            
            mComm_Manager.sendPacket(getLogTag(), call_Packet.ToString());
        }

        //
        virtual public void sendOCPP_CP_Req_StatusNotification()
        {
            Req_StatusNotification statusNotification = new Req_StatusNotification();
            statusNotification.setRequiredValue(mChannelIndex, mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus);

            addReq_By_PayloadString(
                statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //statusNotification.getClass().getSimpleName().split("_")[1],
                JsonConvert.SerializeObject(statusNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(statusNotification, statusNotification.getClass()));
        }
        //    public void sendOCPP_CP_Req_StatusNotification(ChargePointErrorCode errorCode, ChargePointStatus status)
        //    {
        //        Req_StatusNotification statusNotification = new Req_StatusNotification();
        //        statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
        //
        //        setSendPacket_Call_CP(statusNotification.getClass().getSimpleName().split("_")[1],
        //                mGson.toJson(statusNotification, statusNotification.getClass()));
        //    }


        virtual public void sendOCPP_CP_Req_StatusNotification(ChargePointErrorCode errorCode, ChargePointStatus status)
        {
            setDelay_First_Default();

            Req_StatusNotification statusNotification = new Req_StatusNotification();
            statusNotification.setRequiredValue(mChannelIndex, errorCode, status);

            switch (mApplication.getPlatform_Operator())
            {
                case EPlatformOperator.WEV:
                    switch (mOCPP_ChargePointStatus)
                    {
                        case ChargePointStatus.Available:
                            if (!bIsSend_First)
                            {
                                statusNotification.setRequiredValue_Wev(mChannelIndex, errorCode, status,
                                        "MaxChargingCapacityKw", CCharger_Variable.MaxChargingCapacityKw);
                                bIsSend_First = true;
                            }
                            else
                            {
                                statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
                            }
                            break;
                        default:
                            statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
                            break;
                    }
                    break;
                default:
                    statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
                    break;
            }


            addReq_By_PayloadString(
                statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //statusNotification.getClass().getSimpleName().split("_")[1],
                JsonConvert.SerializeObject(statusNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(statusNotification, statusNotification.getClass()));
        }


        virtual public void sendOCPP_CP_Req_StatusNotification(ChargePointErrorCode errorCode, ChargePointStatus status, string info)
        {
            setDelay_First_Default();

            Req_StatusNotification statusNotification = new Req_StatusNotification();
            statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
            statusNotification.info = info;

            switch (mApplication.getPlatform_Operator())
            {
                case EPlatformOperator.WEV:
                    switch (mOCPP_ChargePointStatus)
                    {
                        case ChargePointStatus.Available:
                            if (!bIsSend_First)
                            {
                                statusNotification.setRequiredValue_Wev(mChannelIndex, errorCode, status,
                                        "MaxChargingCapacityKw", CCharger_Variable.MaxChargingCapacityKw);
                                bIsSend_First = true;
                            }
                            else
                            {
                                statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
                            }
                            break;
                        default:
                            statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
                            break;
                    }
                    break;
                default:
                    statusNotification.setRequiredValue(mChannelIndex, errorCode, status);
                    break;
            }


            addReq_By_PayloadString(
                statusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //statusNotification.getClass().getSimpleName().split("_")[1],
                JsonConvert.SerializeObject(statusNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(statusNotification, statusNotification.getClass()));
        }


        virtual public void setOCPP_ChargePoint_Status_ErrorCode(ChargePointStatus status, ChargePointErrorCode errorCode)
        {

            bool isChange_errorCode = false;
            if (errorCode != mOCPP_ChargePointErrorCode)
            {
                mOCPP_ChargePointErrorCode = errorCode;
                isChange_errorCode = true;
            }
            bool isChange = false;
            isChange = setOCPP_ChargePointStatus_For_Check(status);

            if (isChange)
            {
                mOCPP_ChargePointStatus = status;
            }
            if (isChange || isChange_errorCode)
                sendOCPP_CP_Req_StatusNotification(mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus);
        }

        virtual public void setOCPP_ChargePoint_Status_ErrorCode(ChargePointStatus status, ChargePointErrorCode errorCode, string info)
        {

            bool isChange_errorCode = false;
            if (errorCode != mOCPP_ChargePointErrorCode)
            {
                mOCPP_ChargePointErrorCode = errorCode;
                isChange_errorCode = true;
            }
            bool isChange = false;
            isChange = setOCPP_ChargePointStatus_For_Check(status);

            if (isChange)
            {
                mOCPP_ChargePointStatus = status;
            }
            if (isChange || isChange_errorCode)
                sendOCPP_CP_Req_StatusNotification(mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus, info);
        }


        virtual protected void setOCPP_ChargePointErrorCode(ChargePointErrorCode errorCode)
        {
            if (errorCode != mOCPP_ChargePointErrorCode)
            {
                bool isChangeState = false;
                mOCPP_ChargePointErrorCode = errorCode;
                switch (errorCode)
                {
                    case ChargePointErrorCode.ConnectorLockFailure:
                    case ChargePointErrorCode.EVCommunicationError:
                    case ChargePointErrorCode.GroundFailure:
                    case ChargePointErrorCode.HighTemperature:
                    case ChargePointErrorCode.InternalError:
                    case ChargePointErrorCode.LocalListConflict:
                    case ChargePointErrorCode.OverCurrentFailure:
                    case ChargePointErrorCode.OverVoltage:
                    case ChargePointErrorCode.PowerMeterFailure:
                    case ChargePointErrorCode.PowerSwitchFailure:
                    case ChargePointErrorCode.ReaderFailure:
                    case ChargePointErrorCode.ResetFailure:
                    case ChargePointErrorCode.UnderVoltage:
                    case ChargePointErrorCode.WeakSignal:
                    case ChargePointErrorCode.OtherError:
                        isChangeState = setOCPP_ChargePointStatus(ChargePointStatus.Faulted);
                        break;

                    case ChargePointErrorCode.NoError:
                        break;
                }
                if (!isChangeState)
                    sendOCPP_CP_Req_StatusNotification(mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus);
            }
        }


        virtual public bool setOCPP_ChargePointStatus(ChargePointStatus status)
        {
            bool isChange = false;
            isChange = setOCPP_ChargePointStatus_For_Check(status);

            if (isChange)
            {
                mOCPP_ChargePointStatus = status;
                sendOCPP_CP_Req_StatusNotification(mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus);
            }

            return isChange;
        }

        virtual public bool setOCPP_ChargePointStatus(ChargePointStatus status, String info)
        {
            bool isChange = false;
            isChange = setOCPP_ChargePointStatus_For_Check(status);

            if (isChange)
            {
                mOCPP_ChargePointStatus = status;
                sendOCPP_CP_Req_StatusNotification(mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus, info);
            }

            return isChange;
        }



        virtual public bool setOCPP_ChargePointStatus_For_Check(ChargePointStatus status)
        {
            bool isChange = false;
            switch (mOCPP_ChargePointStatus)
            {
                case ChargePointStatus.NONE:
                    isChange = true;
                    break;
                case ChargePointStatus.Available:
                    switch (status)
                    {
                        case ChargePointStatus.Preparing:
                        case ChargePointStatus.Charging:
                        case ChargePointStatus.SuspendedEVSE:
                        case ChargePointStatus.SuspendedEV:
                        case ChargePointStatus.Reserved:
                        case ChargePointStatus.Unavailable:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.Preparing:
                    switch (status)
                    {
                        case ChargePointStatus.Available:
                        case ChargePointStatus.Charging:
                        case ChargePointStatus.SuspendedEVSE:
                        case ChargePointStatus.SuspendedEV:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.Charging:
                    switch (status)
                    {
                        case ChargePointStatus.Available:
                        case ChargePointStatus.SuspendedEVSE:
                        case ChargePointStatus.SuspendedEV:
                        case ChargePointStatus.Finishing:
                        case ChargePointStatus.Unavailable:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.SuspendedEVSE:
                    switch (status)
                    {
                        case ChargePointStatus.Available:
                        case ChargePointStatus.Charging:
                        case ChargePointStatus.SuspendedEV:
                        case ChargePointStatus.Finishing:
                        case ChargePointStatus.Unavailable:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.SuspendedEV:
                    switch (status)
                    {
                        case ChargePointStatus.Available:
                        case ChargePointStatus.Charging:
                        case ChargePointStatus.SuspendedEVSE:
                        case ChargePointStatus.Finishing:
                        case ChargePointStatus.Unavailable:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.Finishing:
                case ChargePointStatus.Reserved:
                    switch (status)
                    {
                        case ChargePointStatus.Available:
                        case ChargePointStatus.Preparing:
                        case ChargePointStatus.Unavailable:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.Unavailable:
                    switch (status)
                    {
                        case ChargePointStatus.Available:
                        case ChargePointStatus.Preparing:
                        case ChargePointStatus.Charging:
                        case ChargePointStatus.SuspendedEVSE:
                        case ChargePointStatus.SuspendedEV:
                        case ChargePointStatus.Faulted:
                            isChange = true;
                            break;
                    }
                    break;
                case ChargePointStatus.Faulted:
                    switch (status)
                    {
                        case ChargePointStatus.Faulted:
                            break;
                        default:
                            isChange = true;
                            break;
                    }
                    break;
            }

            return isChange;
        }



        override public void initVariable()
        {

        }

        
        public void setDelay_First_Default()
        {
            int duration = 0;
            //try
            //{
            duration = mSettingData_OCPP_Table.getSettingData_Int((int)CONST_INDEX_OCPP_Setting.MinimumStatusDuration);
            //if (duration == 0)
            //    duration = 10;
            //}
            //catch (Exception e)
            //{
            //    mSettingData_OCPP_Table.setSettingData((int)CONST_INDEX_OCPP_Setting.MinimumStatusDuration, 0);
            //    e.printStackTrace();
            //}

            mDelay_First = duration;
        }
        

        
        override protected int getDelay_Second()
        {
            
            return 10;
        }



        //    @Override
        //    public void onError(int errorCode, String errorCode_String) {
        //
        //    }
    }

}
