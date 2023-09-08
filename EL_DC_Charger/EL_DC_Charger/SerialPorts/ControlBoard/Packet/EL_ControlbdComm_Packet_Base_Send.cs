using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet
{
    abstract public class EL_ControlbdComm_Packet_Base_Send : EL_ControlbdComm_Packet_Base
	{
	//Charger||->||BLE->Charger
		public EL_ControlbdComm_Packet_Base_Send(EL_DC_Charger_MyApplication application, int channelIndex, byte[] cmd)
			: base(application, channelIndex, cmd, false)
		{
			
		}

		//BLE->Charger||->||BLE
		public EL_ControlbdComm_Packet_Base_Send(EL_DC_Charger_MyApplication application, int channelIndex, EL_ControlbdComm_Packet_Base receivePacket)
			: base(application, channelIndex, receivePacket)
		{
			
		}

		protected byte[] send_mSendData_Data = null;

		override public void receive_applySystem()
		{
			// TODO Auto-generated method stub

		}

		override public bool receive_isNeedResponse()
		{
			// TODO Auto-generated method stub
			return false;
		}

		override public bool send_isNeedRequest()
		{
			return false;
		}




	}

}
