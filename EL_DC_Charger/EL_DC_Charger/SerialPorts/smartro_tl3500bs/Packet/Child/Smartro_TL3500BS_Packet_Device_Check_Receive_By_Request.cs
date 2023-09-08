using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Device_Check_Receive_By_Request : Smartro_TL3500BS_Packet_Base_Receive_By_Request
    {
        public Smartro_TL3500BS_Packet_Device_Check_Receive_By_Request(EL_MyApplication_Base application, int channelIndex, byte[] receiveData) : base(application, channelIndex, receiveData)
        {
        }

        protected byte mState_Comm_CardModule = 0;
        public byte State_Comm_CardModule
        {
            get { return mState_Comm_CardModule; }
            set { mState_Comm_CardModule = value; }
        }

        protected byte mState_RF_Module = 0;
        public byte State_RF_Module
        {
            get { return mState_RF_Module; }
            set { mState_RF_Module = value; }
        }

        protected byte mState_Comm_VAN_Server = 0;
        public byte State_Comm_VAN_Server
        {
            get { return mState_Comm_VAN_Server; }
            set { mState_Comm_VAN_Server = value; }
        }

        protected byte mState_Comm_Link_Server = 0;
        public byte State_Comm_Link_Server
        {
            get { return mState_Comm_Link_Server; }
            set { mState_Comm_Link_Server = value; }
        }
        
        //CARD_VARIABLE
        protected byte mCard_Variable = 0;
        public byte Card_Variable
        {
            get { return mCard_Variable; }
            set { mCard_Variable = value; }
        }

        protected string mCard_Number = "";
        public string Card_Number
        {
            get { return mCard_Number; }
            set { mCard_Number = value; }
        }



        public override void receive_applySystem()
        {
            base.receive_applySystem();

            if (mReceiveData_Data == null) return;

            int indexArray = 0;
            State_Comm_CardModule = mReceiveData_Data[indexArray++];
            State_RF_Module = mReceiveData_Data[indexArray++];
            State_Comm_VAN_Server = mReceiveData_Data[indexArray++];
            State_Comm_Link_Server = mReceiveData_Data[indexArray++];
        }
    }
}
