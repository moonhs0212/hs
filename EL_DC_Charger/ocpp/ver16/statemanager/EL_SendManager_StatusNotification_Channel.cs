using EL_DC_Charger.common.interf;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.datatype;
using EL_DC_Charger.ocpp.ver16.packet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class EL_SendManager_StatusNotification_Channel : EL_SendManager_StatusNotification_Base, IEV_State_ChangeListener
    {

        protected EL_StateManager_OCPP_Channel mStateManager_Channel = null;

        public EL_SendManager_StatusNotification_Channel(OCPP_Comm_Manager ocpp_comm_manager, int channelIndex, EL_StateManager_OCPP_Channel stateManager)
            : base(ocpp_comm_manager, channelIndex)
        {
            
            mStateManager_Channel = stateManager;
        }


        
        public void onConnected_By_Car()
        {
            switch (mOCPP_ChargePointStatus)
            {
                case ChargePointStatus.Available:
                case ChargePointStatus.Reserved:
                    if (!mStateManager_Channel.bIsCommand_UsingStart)
                        setOCPP_ChargePointStatus(ChargePointStatus.Preparing);
                    break;
                case ChargePointStatus.SuspendedEVSE:
                case ChargePointStatus.SuspendedEV:
                case ChargePointStatus.Finishing:
                case ChargePointStatus.Preparing:
                case ChargePointStatus.Unavailable:
                case ChargePointStatus.Faulted:
                    break;
            }
        }

        
        public void onDisconnected_By_Car()
        {
            switch (mOCPP_ChargePointStatus)
            {
                case ChargePointStatus.Preparing:
                    if (!mStateManager_Channel.bIsCommand_UsingStart)
                    {
                        if (mStateManager_Channel.bIsReservationProcessing)
                            setOCPP_ChargePointStatus(ChargePointStatus.Reserved);
                        else
                            setOCPP_ChargePointStatus(ChargePointStatus.Available);
                    }
                    break;
                case ChargePointStatus.Charging:
                    if (!mSettingData_OCPP_Table.getSettingData_Boolean((int)CONST_INDEX_OCPP_Setting.StopTransactionOnEVSideDisconnect))
                        setOCPP_ChargePointStatus(ChargePointStatus.SuspendedEV);
                    break;
                case ChargePointStatus.Finishing:
                case ChargePointStatus.SuspendedEVSE:
                case ChargePointStatus.SuspendedEV:
                case ChargePointStatus.Faulted:
                case ChargePointStatus.Available:
                case ChargePointStatus.Reserved:
                case ChargePointStatus.Unavailable:
                    break;
            }
        }

        
        public void onRequestCharging_By_Car()
        {

        }

        
        public void onRequestStop_Charging_By_Car(String result)
        {

        }



        
        override protected String getLogTag()
        {
            return null;
        }

        
        override protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }


        override protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }


        public void onError(int errorCode, String errorCode_String)
        {

        }
    }

}
