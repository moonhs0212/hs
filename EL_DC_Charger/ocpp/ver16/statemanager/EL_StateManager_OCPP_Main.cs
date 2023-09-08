using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.infor;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    abstract public class EL_StateManager_OCPP_Main : EL_StateManager_Main_Base
    {

        
        
        override public void initVariable()
        {

            setOCPP_initVariable();

            //        mSendManager_StatusNotification_Main.setOCPP_ChargePointStatus(ChargePointStatus.Unavailable);
        }

        public EL_Time mTime_Send_HeartBeat = new EL_Time();

        protected Conf_Heartbeat mOCPP_CSMS_Conf_Heartbeat = null;

        public void setOCPP_COMS_Conf_HeartBeat(Conf_Heartbeat conf)
        {
            mOCPP_CSMS_Conf_Heartbeat = conf;

            mTime_Send_HeartBeat.setTime();
        }
        public bool bOCPP_IsReceivePacket_CallResult_HeartBeat = false;
        public bool bOCPP_IsReceivePacket_CallResult_BootNotification = false;
        public Conf_BootNotification mOCPP_CSMS_Conf_BootNotification = null;
        public void setOCPP_CSMS_Conf_BootNotification(Conf_BootNotification conf)
        {
            mOCPP_CSMS_Conf_BootNotification = conf;
            bOCPP_IsReceivePacket_CallResult_BootNotification = true;
        }

        protected int mOCPP_Count_Send_BootNotification = 1;
        public int getOCPP_Interval_Send_BootNotification()
        {
            int interval = 0;
            if (EL_MyApplication_Base.TEST_OCPP)
            {
                interval = 60;
            }
            else
            {
                if (mOCPP_CSMS_Conf_BootNotification == null)
                {
                    interval = 60;
                }
                else
                {
                    interval = mOCPP_CSMS_Conf_BootNotification.interval.Value;
                }

            }
            return interval;
        }

        protected void initVariable_After_Receive_BootNotification()
        {
            switch (mApplication.getPlatform_Operator())
            {
                case common.variable.EPlatformOperator.WEV:
                    mSendManager_StatusNotification_Main.sendOCPP_CP_Req_StatusNotification_Wev_Booting();
                    break;
                default:
                    break;
            }
        }


        protected OCPP_MainInfor mOCPP_MainInfor = null;
        public OCPP_MainInfor getOCPP_MainInfor()
        {
            return mOCPP_MainInfor;
        }



        protected OCPP_Comm_SendMgr mOCPP_Comm_SendMgr = null;
        public OCPP_Comm_SendMgr getOCPP_Comm_SendMgr()
        {
            return mOCPP_Comm_SendMgr;
        }



        protected bool bOCPP_IsError_ChargingStop = false;
        public void setOCPP_IsError_ChargingStop(bool setting) { bOCPP_IsError_ChargingStop = setting; }
        public bool getOCPP_IsError_ChargingStop() { return bOCPP_IsError_ChargingStop; }


        protected String bOCPPError_Message = "";
        public void setOCPPError_Message(String setting) { bOCPPError_Message = setting; }
        public String getOCPPError_Message() { return bOCPPError_Message; }
        
        protected void setOCPP_initVariable()
        {
            mOCPP_Comm_Manager = mApplication.getOCPP_Comm_Manager();
            mOCPP_Comm_SendMgr = mApplication.getOCPP_Comm_Manager().getSendMgr();

            
            mOCPP_MainInfor = mApplication.getOCPP_MainInfor();
            

            mSendManager_StatusNotification_Main = new EL_SendManager_StatusNotification_Main(mApplication.getOCPP_Comm_Manager());
            mSendManager_StatusNotification_Main.initVariable();

            mSendManager_OCPP_CP_Req_Normal = new EL_SendManager_OCPP_CP_Req_Normal(mOCPP_Comm_Manager);
            mSendManager_OCPP_CP_Req_Normal.initVariable();
            mSendManager_StatusNotification_Main = new EL_SendManager_StatusNotification_Main(mOCPP_Comm_Manager);
            mSendManager_StatusNotification_Main.initVariable();

            mStateManager_OCPP_DiagnosticsManager = new EL_StateManager_OCPP_DiagnosticsManager(mOCPP_Comm_Manager);
            mStateManager_OCPP_DiagnosticsManager.initVariable();
            mStateManager_OCPP_UpdateFirmware = new EL_StateManager_OCPP_UpdateFirmware(mOCPP_Comm_Manager);
            mStateManager_OCPP_UpdateFirmware.initVariable();
        }












        protected void rebootByCheckReconnectSocket()
        {
            if (mOCPP_Comm_SendMgr.getComm_Manager().mCount_Reconnect > 10)
                EL_Manager_Application.restartSystem();
        }



        
        protected OCPP_Comm_Manager mOCPP_Comm_Manager = null;


        public EL_SendManager_OCPP_CP_Req_Normal mSendManager_OCPP_CP_Req_Normal = null;
        public EL_SendManager_StatusNotification_Main mSendManager_StatusNotification_Main = null;

        protected EL_StateManager_OCPP_DiagnosticsManager mStateManager_OCPP_DiagnosticsManager = null;
        protected EL_StateManager_OCPP_UpdateFirmware mStateManager_OCPP_UpdateFirmware = null;

        public EL_StateManager_OCPP_DiagnosticsManager getStateManager_OCPP_DiagnosticsManager()
        {
            return mStateManager_OCPP_DiagnosticsManager;
        }
        public EL_StateManager_OCPP_UpdateFirmware getStateManager_OCPP_UpdateFirmware()
        {
            return mStateManager_OCPP_UpdateFirmware;
        }
        public EL_StateManager_OCPP_Main(EL_MyApplication_Base application)
            : base(application)
        {
            
            setIsUseEnable();
        }





    }

}
