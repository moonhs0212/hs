using common.Database;
using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
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
    public class EL_StateManager_OCPP_UpdateFirmware : EL_SendManager_OCPP_Base
    {

        
        override public void setStop()
        {
            base.setStop();
            bFirmwareUpdate_Complete = false;
        }
        
        override public void setStart()
        {
            base.setStart();
            bFirmwareUpdate_Complete = true;
        }


        public void sendOCPP_CP_Req_FirmwareStatusNotification(FirmwareStatus status)
        {
            Req_FirmwareStatusNotification firmwareStatusNotification = new Req_FirmwareStatusNotification();
            firmwareStatusNotification.setRequiredValue(status);

            addReq_By_PayloadString(
                firmwareStatusNotification.GetType().Name.Split(new String[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1], //firmwareStatusNotification.getClass().getSimpleName().split("_")[1],
                JsonConvert.SerializeObject(firmwareStatusNotification, EL_MyApplication_Base.mJsonSerializerSettings)); //mGson.toJson(firmwareStatusNotification, firmwareStatusNotification.getClass()));
        }


        
        override public void initVariable()
        {
            bFirmwareUpdate_Complete = ((EL_Manager_Table_Setting)mApplication.getManager_SQLite_Setting().getList_Manager_Table()[0])
                    .getSettingData_Boolean(CONST_INDEX_MAINSETTING.IS_SW_UPDATE_COMPLETE);
        }


        
        override public void intervalExcuteAsync()
        {
            switch (mState)
            {
                case CONST_OCPP_UpdateFirmware.READY:
                    if (bFirmwareUpdate_Complete)
                    {
                        setState(CONST_OCPP_UpdateFirmware.INSTALLED);
                        ((EL_Manager_Table_Setting)mApplication.getManager_SQLite_Setting().getList_Manager_Table()[0])
                                .setSettingData(CONST_INDEX_MAINSETTING.IS_SW_UPDATE_COMPLETE, false);
                    }
                    else
                        setState(CONST_OCPP_UpdateFirmware.READY + 1);
                    break;
                case CONST_OCPP_UpdateFirmware.READY + 1:
                    if (bCommand_UpdateFW)
                        setState(CONST_OCPP_UpdateFirmware.DOWNLOADING_START);
                    break;
                ///////////////////////////////////////////
                case CONST_OCPP_UpdateFirmware.DOWNLOADING_START:
                    setState(CONST_OCPP_UpdateFirmware.DOWNLOADING_START + 1);
                    break;
                case CONST_OCPP_UpdateFirmware.DOWNLOADING_START + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        setState(CONST_OCPP_UpdateFirmware.DOWNLOADING);
                    }
                    break;
                ///////////////////////////////////////////
                case CONST_OCPP_UpdateFirmware.DOWNLOADING:
                    setState(CONST_OCPP_UpdateFirmware.DOWNLOADING + 1);
                    break;
                case CONST_OCPP_UpdateFirmware.DOWNLOADING + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        setState(CONST_OCPP_UpdateFirmware.INSTALL_WAIT);
                    }
                    break;
                ///////////////////////////////////////////
                case CONST_OCPP_UpdateFirmware.INSTALL_WAIT:
                    sendOCPP_CP_Req_FirmwareStatusNotification(FirmwareStatus.Downloaded);
                    setState(CONST_OCPP_UpdateFirmware.INSTALL_WAIT + 1);
                    break;
                case CONST_OCPP_UpdateFirmware.INSTALL_WAIT + 1:
                    if (isTimer_Sec(TIMER_2SEC, 2))
                    {
                        setState(CONST_OCPP_UpdateFirmware.INSTALLING);
                    }
                    break;
                ///////////////////////////////////////////
                case CONST_OCPP_UpdateFirmware.INSTALLING:
                    setState(CONST_OCPP_UpdateFirmware.INSTALLING + 1);
                    break;
                case CONST_OCPP_UpdateFirmware.INSTALLING + 1:
                    if (isTimer_Sec(TIMER_WAITTIME, 2))
                    {
                        ((EL_Manager_Table_Setting)mApplication.getManager_SQLite_Setting().getList_Manager_Table()[0])
                                .setSettingData(CONST_INDEX_MAINSETTING.IS_SW_UPDATE_COMPLETE, true);
                        setStop();
                        mApplication.mStateManager_Main.bIsNeedReset_Charger_Soft = true;
                        for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
                        {
                            mApplication.getChannelTotalInfor(i + 1).getStateManager_Channel().bIsNeedReset_Charger_Soft = true;
                        }


                    }
                    break;
                ///////////////////////////////////////////
                case CONST_OCPP_UpdateFirmware.INSTALLED:
                    sendOCPP_CP_Req_FirmwareStatusNotification(FirmwareStatus.Installed);
                    setState(CONST_OCPP_UpdateFirmware.INSTALLED + 1);
                    break;
                case CONST_OCPP_UpdateFirmware.INSTALLED + 1:
                    setStop();
                    break;
                    ///////////////////////////////////////////
            }
        }

        
        override protected String getLogTag()
        {
            return "EL_StateManager_OCPP_UpdateFirmware";
        }

        
        override protected void processReceivePacket_CallResult(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }


        override protected void processReceivePacket_CallError(EL_OCPP_Packet_Wrapper sendPacket, JArray receivePacket)
        {

        }

        public bool bFirmwareUpdate_Complete = false;
        public bool bCommand_UpdateFW = false;
        public String mURI = "";


        public EL_StateManager_OCPP_UpdateFirmware(OCPP_Comm_Manager comm_Manager)
            : base(comm_Manager, 0, false)
        {
            


        }
    }

}
