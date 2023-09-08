using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EL_DC_Charger.common.item;

namespace EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard
{
    public class EL_ControlbdComm_PacketManager : EL_CommPort_PacketManager_Base, IEV_State, IDI_Manager
    {

        //정승현
        public int mMode_SendState = 0;

        public EL_ControlbdComm_Packet_z1_FuncTest_Send packet_z1 = null;
        public EL_ControlbdComm_Packet_z1_FuncTest_Receive packet_1z = null;

        //정승현
        public EL_ControlbdComm_Packet_a0_RequestResponse_Send packet_a0 = null;

        public EL_ControlbdComm_PacketManager(EL_DC_Charger_MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            packet_z1 = new EL_ControlbdComm_Packet_z1_FuncTest_Send(application, channelIndex);
            packet_1z = new EL_ControlbdComm_Packet_z1_FuncTest_Receive(application, channelIndex);
            packet_a0 = new EL_ControlbdComm_Packet_a0_RequestResponse_Send(application, channelIndex);

            mApplication.getChannelTotalInfor(mChannelIndex).setEV_State(this);
            //mChannelTotalInfor.setEV_State(this);
        }

        public void command_openDoorLock(int index)
        {

        }



        public int getErrorCode()
        {
            int errorCode = packet_1z.mErrorCode;
            //if (!packet_1z.bFlag1_EMG_Push)
            //    errorCode = 0;
            //else if (!packet_1z.bFlag1_EMG_Push)
            //    errorCode = 0;
            //else if (!packet_1z.bFlag1_Fuse)
            //    errorCode = 0;
            //else if (!packet_1z.bFlag1_GroundFault)
            //    errorCode = 0;
            //else if (!packet_1z.bFlag1_PowerRelay_Minus)
            //    errorCode = 0;
            //else if (!packet_1z.bFlag1_PowerRelay_Plus)
            //    errorCode = 0;

            return errorCode;
        }

        public String getErrorCode_String()
        {
            int errorCode = getErrorCode();
            string errorCode_String = "";
            switch (errorCode)
            {
                case 0:
                    errorCode_String = "정상";
                    break;
            }

            return "" + errorCode;
        }

        public override void initVariable()
        {

        }
        override public bool isConnected_Comm()
        {
            if (bIsConnected)
            {
                TimeSpan span = DateTime.Now - mDateTime_LastComm;
                if (span.TotalMinutes >= 1) return false;
                return true;
            }

            return false;
        }

        public bool isEmergencyPushed()
        {
            return IsPush_Emg;
        }

        #region  IEVState
        public bool isErrorState_Car()
        {
            int errorState = 0;
            return false;
        }

        public bool isErrorState_Comm_WithCar()
        {
            return false;
        }

        public bool isOpenDoorLock(int index)
        {
            bool result = false;
            switch (index)
            {
                case 1:
                    result = packet_1z.bFlag2_Door_Open_1;
                    break;
                case 2:
                    result = packet_1z.bFlag2_Door_Open_2;
                    break;
                case 3:
                    result = packet_1z.bFlag2_Door_Open_3;
                    break;
            }
            return result;
        }

        public bool isRequesting_ChargingStart_Car()
        {
            if (packet_1z.getState_CommunicationCar() > 100)
                return false;


            return true;
        }

        public string isRequesting_ChargingStop_Car_String()
        {
            string result = "";
            return result;
        }

        public bool isState_ChargingCar()
        {
            if (packet_1z.mChargingProcessState == 100 || EL_DC_Charger_MyApplication.getInstance().debug)
                return true;
            else
                return false;
        }

        public bool isState_ConnectedCar()
        {
            return IsConnectedGun_Combo_Type1;
        }
        #endregion IEVState

        public bool isUse_DoorLock()
        {
            return false;
        }

        public int getRemainTime_FullCharging_Minute()
        {
            return packet_1z.mRemainTime_Minute;
        }

        protected IEV_State_ChangeListener mEV_State_ChangeListener = null;
        public void setEV_State_ChangeListener(IEV_State_ChangeListener listener)
        {
            mEV_State_ChangeListener = listener;
        }

        public bool isCar_DataComm()
        {
            return true;
        }

        public int getSOC()
        {
            return packet_1z.mSOC;
        }

        public int getWastedMinute_After_FullCharge()
        {
            return (int)packet_1z.getWastedMinute_FullCharge();
        }
        protected EL_Time mTime = null;
        public bool isState_ChargingCompleteCar()
        {
            if (isState_ConnectedCar() && !isState_ChargingCar())
            {
                if (mTime == null) mTime = new EL_Time();

                if (mTime.getSecond_WastedTime() > 60) return true;
            }

            return false;
        }

        //public bool  = false;
        protected bool bIsPush_Emg = false;
        public bool IsPush_Emg
        {
            get { return bIsPush_Emg; }
            set { bIsPush_Emg = value; }
        }

        protected bool bIsConnectedGun_Chademo = false;
        public bool IsConnectedGun_Chademo
        {
            get { return bIsConnectedGun_Chademo; }
            set { bIsConnectedGun_Chademo = value; }
        }
        protected bool bIsConnectedGun_Combo_Type1 = false;
        public bool IsConnectedGun_Combo_Type1
        {
            get { return bIsConnectedGun_Combo_Type1; }
            set
            {
                if (bIsConnectedGun_Combo_Type1 != value)
                {
                    if (mEV_State_ChangeListener != null)
                    {
                        if (value)
                        {
                            mEV_State_ChangeListener.onConnected_By_Car();
                        }
                        else
                        {
                            mEV_State_ChangeListener.onDisconnected_By_Car();
                        }
                    }
                }
                bIsConnectedGun_Combo_Type1 = value;

            }
        }
        protected bool bIsConnectedGun_Combo_Type2 = false;
        public bool IsConnectedGun_Combo_Type2
        {
            get { return bIsConnectedGun_Combo_Type2; }
            set { bIsConnectedGun_Combo_Type2 = value; }
        }
        protected bool bIsConnectedGun_AC3Phase = false;
        public bool IsConnectedGun_AC3Phase
        {
            get { return bIsConnectedGun_AC3Phase; }
            set { bIsConnectedGun_AC3Phase = value; }
        }
        protected bool bIsConnectedGun_GBT = false;
        public bool IsConnectedGun_GBT
        {
            get { return bIsConnectedGun_GBT; }
            set { bIsConnectedGun_GBT = value; }
        }
        protected bool bIsConnectedGun_ACSlow = false;
        public bool IsConnectedGun_ACSlow
        {
            get { return bIsConnectedGun_ACSlow; }
            set { bIsConnectedGun_ACSlow = value; }
        }
    }
}
