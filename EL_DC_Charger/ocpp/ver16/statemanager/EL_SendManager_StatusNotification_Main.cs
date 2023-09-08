using EL_DC_Charger.ocpp.ver16.comm;
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
    public class EL_SendManager_StatusNotification_Main : EL_SendManager_StatusNotification_Base
    {
        public EL_SendManager_StatusNotification_Main(OCPP_Comm_Manager ocpp_comm_manager)
            : base(ocpp_comm_manager, 0)
        {
            
        }

        override public bool setOCPP_ChargePointStatus(ChargePointStatus status)
        {
            bool isChange = false;

            switch (status)
            {
                case ChargePointStatus.NONE:
                    isChange = true;
                    break;
                case ChargePointStatus.Available:
                case ChargePointStatus.Unavailable:
                case ChargePointStatus.Faulted:
                    if (mOCPP_ChargePointStatus != status)
                        isChange = true;
                    break;
            }

            if (isChange)
            {
                mOCPP_ChargePointStatus = status;
                sendOCPP_CP_Req_StatusNotification(mOCPP_ChargePointErrorCode, mOCPP_ChargePointStatus);
            }

            return isChange;
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

    }

}
