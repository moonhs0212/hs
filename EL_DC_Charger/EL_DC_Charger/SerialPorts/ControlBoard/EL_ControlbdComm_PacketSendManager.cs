using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard
{
    public class EL_ControlbdComm_PacketSendManager : EL_Object_Base
    {

        public EL_ControlbdComm_PacketManager[] mManager_Packet = null;
        EL_ControlbdComm_Comm_Manager mManager_SerialPort = null;
        public EL_ControlbdComm_PacketSendManager(EL_DC_Charger_MyApplication application, EL_ControlbdComm_Comm_Manager manager_SerialPort) : base(application)
        {
            mManager_SerialPort = manager_SerialPort;
            //mManager_Packet = new EL_ControlbdComm_PacketManager[application.getChannelCount()];
            mManager_Packet = new EL_ControlbdComm_PacketManager[2];

            mManager_Packet[0] = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager();
            mManager_Packet[1] = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager();

            //for (int i = 0; i < mManager_Packet.Length; i++)
            //{
            //    mManager_Packet[i] = (EL_ControlbdComm_PacketManager)EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(i + 1).getControlbdComm_PacketManager();
            //}

        }

        public override void initVariable()
        {

        }
        int mIndexSend = 0;


        public void sendData_Interval()
        {

            byte[] data = null;

            //mManager_Packet.mPacket_a1_Send.send_ApplyData();
            //data = mManager_Packet.mPacket_a1_Send.SendData;

            //if (!bIsInitComplete_a1_Receive || !bIsInitComplete_a1_Receive)
            //{
            //    mManager_Packet.mPacket_a1_Send.send_ApplyData();
            //    data = mManager_Packet.mPacket_a1_Send.SendData;
            //}
            //else
            //{

            //    data = mManager_Packet.mPacket_z1_Send.SendData;
            //}

            if (mManager_Packet[mIndexSend].mMode_SendState == 0 && EL_DC_Charger_MyApplication.getInstance().getChargerType() != EChargerType.CH1_CERT)
            {
                data = mManager_Packet[mIndexSend].packet_a0.send_makeSendData();
                mIndexSend++;
            }
            else
            {
                data = mManager_Packet[mIndexSend].packet_z1.send_makeSendData();
            }
            if (data != null)
                mManager_SerialPort.write(data);

            if (mManager_Packet.Length <= mIndexSend)
                mIndexSend = 0;




        }




        //protected bool bIs_InitComplete_a1_Send = false;
        //public bool Is_InitComplete_a1_Send
        //{
        //    get { return bIs_InitComplete_a1_Send; }
        //    set
        //    {
        //        if(!bIs_InitComplete_a1_Send)
        //        {
        //            mManager_Packet.mPacket_a1_Send.
        //        }
        //        bIs_InitComplete_a1_Send = value;
        //    }
        //}

        //protected bool bIs_InitComplete_a1_Receive = false;
        //public bool Is_InitComplete_a1_Receive
        //{
        //    get { return bIs_InitComplete_a1_Receive; }
        //    set
        //    {
        //        bIs_InitComplete_a1_Receive = value;
        //    }
        //}
    }
}

