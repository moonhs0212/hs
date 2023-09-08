using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet
{
	abstract public class EL_ControlbdComm_Packet_Base_Receive_By_Request : EL_ControlbdComm_Packet_Base_Receive
	{

		public EL_ControlbdComm_Packet_Base_Receive_By_Request(EL_DC_Charger_MyApplication application, int channelIndex, byte[] receiveData)
				: base(application, channelIndex, receiveData)
		{
		
		}

		
		override public bool receive_isNeedResponse()
		{
			// TODO Auto-generated method stub
			return false;
		}

	}
}
