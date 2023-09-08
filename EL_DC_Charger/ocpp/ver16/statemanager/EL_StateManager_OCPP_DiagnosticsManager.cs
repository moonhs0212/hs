using EL_DC_Charger.common.application;
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
    public class EL_StateManager_OCPP_DiagnosticsManager : EL_SendManager_OCPP_Base
    {


        public EL_StateManager_OCPP_DiagnosticsManager(OCPP_Comm_Manager ocpp_comm_manager)
            : base(ocpp_comm_manager, 0, false)
        {
            
        }

        
        override public void setStop()
        {

            bCommand_Upload_Diagnostics = false;
        }
        
        override public void setStart()
        {

            bCommand_Upload_Diagnostics = true;
        }

        public void sendOCPP_CP_Req_DiagnosticsStatusNotification(DiagnosticsStatus status)
        {
            Req_DiagnosticsStatusNotification diagnosticsStatusNotification = new Req_DiagnosticsStatusNotification();
            diagnosticsStatusNotification.setRequiredValue(status);

            addReq_By_PayloadString(
                   diagnosticsStatusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1],
                   //diagnosticsStatusNotification.getClass().getSimpleName().split("_")[1],
                   JsonConvert.SerializeObject(diagnosticsStatusNotification, EL_MyApplication_Base.mJsonSerializerSettings));
                //mGson.toJson(diagnosticsStatusNotification, diagnosticsStatusNotification.getClass()));
        }


        public bool bCommand_Upload_Diagnostics = false;



        
        override public void intervalExcuteAsync()
        {
            switch (mState)
            {
                case CONST_OCPP_Upload_Diagnotics.READY:
                    setState(CONST_OCPP_Upload_Diagnotics.READY + 1);
                    break;
                case CONST_OCPP_Upload_Diagnotics.READY + 1:
                    if (bCommand_Upload_Diagnostics)
                        setState(CONST_OCPP_Upload_Diagnotics.CONNECTION);
                    break;
                //////////////////////////////////
                case CONST_OCPP_Upload_Diagnotics.CONNECTION:
                    sendOCPP_CP_Req_DiagnosticsStatusNotification(DiagnosticsStatus.Uploading);
                    setState(CONST_OCPP_Upload_Diagnotics.CONNECTION + 1);
                    break;
                case CONST_OCPP_Upload_Diagnotics.CONNECTION + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                        setState(CONST_OCPP_Upload_Diagnotics.UPLOADING);
                    break;
                //////////////////////////////////
                case CONST_OCPP_Upload_Diagnotics.UPLOADING:
                    setState(CONST_OCPP_Upload_Diagnotics.UPLOADING + 1);
                    break;
                case CONST_OCPP_Upload_Diagnotics.UPLOADING + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                        setState(CONST_OCPP_Upload_Diagnotics.COMPLETE);
                    break;
                //////////////////////////////////
                case CONST_OCPP_Upload_Diagnotics.COMPLETE:
                    sendOCPP_CP_Req_DiagnosticsStatusNotification(DiagnosticsStatus.Uploaded);
                    setState(CONST_OCPP_Upload_Diagnotics.COMPLETE + 1);
                    break;
                case CONST_OCPP_Upload_Diagnotics.COMPLETE + 1:
                    setStop();

                    break;
            }
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
