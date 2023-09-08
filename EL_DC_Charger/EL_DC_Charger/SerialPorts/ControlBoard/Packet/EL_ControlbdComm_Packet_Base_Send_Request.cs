using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet
{
    abstract public class EL_ControlbdComm_Packet_Base_Send_Request : EL_ControlbdComm_Packet_Base_Send
	{
		//Charger||->||BLE->Charger
		public EL_ControlbdComm_Packet_Base_Send_Request(EL_DC_Charger_MyApplication application, int channelIndex, byte[] cmd)
			: base(application, channelIndex, cmd)
		{
			
		}

		override public bool send_isNeedRequest()
		{
			return true;
		}
	}

}
