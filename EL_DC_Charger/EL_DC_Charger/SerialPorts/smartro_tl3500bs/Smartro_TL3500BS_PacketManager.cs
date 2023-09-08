using EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs
{
    public class Smartro_TL3500BS_PacketManager : EL_CommPort_PacketManager_Base
    {

        protected Smartro_TL3500BS_Packet_Deal_Cancel_Send_Request mPacket_DealCancel_Send = null;
        protected Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request mPacket_DealCancel_Receive = null;

        protected Smartro_TL3500BS_Packet_Card_Check_Send_Request mPacket_CardCheck_Send = null;
        protected Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request mPacket_CardCheck_Receive = null;


        protected Smartro_TL3500BS_Packet_Deal_Request_Send_Request mPacket_Deal_Request_Send = null;
        protected Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request mPacket_Deal_Request_Receive = null;


        protected Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Send_Request mPacket_AddInfor_Deal_Request_Send = null;
        protected Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request mPacket_AddInfor_Deal_Request_Receive = null;

        protected Smartro_TL3500BS_Packet_Event_Receive_Only mPacket_Event_Receive = null;
        protected Smartro_TL3500BS_Packet_Reset_Send_Only mPacket_Reset_Send = null;
        protected Smartro_TL3500BS_Packet_Deal_Wait_Send_Only mPacket_DealWait_Send = null;

        public Smartro_TL3500BS_Packet_Deal_Request_Send_Request Packet_Deal_Request_Send
        {
            get { return mPacket_Deal_Request_Send; }
            set { mPacket_Deal_Request_Send = value; }
        }

        public Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request Packet_Deal_Request_Receive
        {
            get { return mPacket_Deal_Request_Receive; }
            set { mPacket_Deal_Request_Receive = value; }
        }

        public Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request Packet_AddInfor_Deal_Request_Receive
        {
            get { return mPacket_AddInfor_Deal_Request_Receive; }
            set { mPacket_AddInfor_Deal_Request_Receive = value; }
        }

        public Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Send_Request Packet_AddInfor_Deal_Request_Send
        {
            get { return mPacket_AddInfor_Deal_Request_Send; }
            set { mPacket_AddInfor_Deal_Request_Send = value; }
        }


        public Smartro_TL3500BS_Packet_Card_Check_Send_Request Packet_CardCheck_Send
        {
            get { return mPacket_CardCheck_Send; }
            set { mPacket_CardCheck_Send = value; }
        }

        public Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request Packet_CardCheck_Receive
        {
            get { return mPacket_CardCheck_Receive; }
            set { mPacket_CardCheck_Receive = value; }
        }

        public Smartro_TL3500BS_Packet_Deal_Cancel_Send_Request Packet_DealCancel_Send
        {
            get { return mPacket_DealCancel_Send; }
            set { mPacket_DealCancel_Send = value; }
        }

        public Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request Packet_DealCancel_Receive
        {
            get { return mPacket_DealCancel_Receive; }
            set { mPacket_DealCancel_Receive = value; }
        }


        public Smartro_TL3500BS_Packet_Event_Receive_Only Packet_Event_Receive
        {
            get { return mPacket_Event_Receive; }
            set { mPacket_Event_Receive = value; }
        }

        public Smartro_TL3500BS_Packet_Reset_Send_Only Packet_Reset_Send
        {
            get { return mPacket_Reset_Send; }
            set { mPacket_Reset_Send = value; }
        }

        public Smartro_TL3500BS_Packet_Deal_Wait_Send_Only Packet_Deal_Wait_Send_Only
        {
            get { return mPacket_DealWait_Send; }
            set { mPacket_DealWait_Send = value; }
        }

        public Smartro_TL3500BS_PacketManager(EL_DC_Charger_MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            mPacket_CardCheck_Send = new Smartro_TL3500BS_Packet_Card_Check_Send_Request(application, channelIndex);
            mPacket_CardCheck_Receive = new Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request(application, channelIndex, null);

            mPacket_DealCancel_Send = new Smartro_TL3500BS_Packet_Deal_Cancel_Send_Request(application, channelIndex);
            mPacket_DealCancel_Receive = new Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request(application, channelIndex, null);

            mPacket_Deal_Request_Send = new Smartro_TL3500BS_Packet_Deal_Request_Send_Request(application, channelIndex);
            mPacket_Deal_Request_Receive = new Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request(application, channelIndex, null);

            mPacket_Event_Receive = new Smartro_TL3500BS_Packet_Event_Receive_Only(application, channelIndex, null);
            mPacket_Reset_Send = new Smartro_TL3500BS_Packet_Reset_Send_Only(application);
            mPacket_DealWait_Send = new Smartro_TL3500BS_Packet_Deal_Wait_Send_Only(application);


            mPacket_AddInfor_Deal_Request_Receive = new Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request(application, channelIndex, null);
            mPacket_AddInfor_Deal_Request_Send = new Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Send_Request(application, channelIndex);
        }

        public Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request regenPacket_CardCheck_Receive()
        {
            Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request packet_return = mPacket_CardCheck_Receive;
            mPacket_CardCheck_Receive = new Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request(mApplication, mChannelIndex, null);
            return packet_return;
        }
        public Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request regenPacket_DealCancel_Receive()
        {
            Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request packet_return = mPacket_DealCancel_Receive;
            mPacket_DealCancel_Receive = new Smartro_TL3500BS_Packet_Deal_Cancel_Receive_By_Request(mApplication, mChannelIndex, null);
            return packet_return;
        }

        public Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request regenPacket_Deal_Request_Receive()
        {
            Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request packet_return = mPacket_Deal_Request_Receive;
            mPacket_Deal_Request_Receive = new Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request(mApplication, mChannelIndex, null);
            return packet_return;
        }

        public Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request regenPacket_AddInfor_Deal_Request_Receive()
        {
            Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request packet_return = mPacket_AddInfor_Deal_Request_Receive;
            mPacket_AddInfor_Deal_Request_Receive = new Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request(mApplication, mChannelIndex, null);
            return packet_return;
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

    }
}
