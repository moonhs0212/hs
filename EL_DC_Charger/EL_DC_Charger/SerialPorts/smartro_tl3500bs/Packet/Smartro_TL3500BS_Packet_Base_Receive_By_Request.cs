using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet
{
    abstract public class Smartro_TL3500BS_Packet_Base_Receive_By_Request : Smartro_TL3500BS_Packet_Base_Receive
	{

		//Charger->BLE||->||Charger
		//BLE||->||Charger->BLE
		public Smartro_TL3500BS_Packet_Base_Receive_By_Request(EL_MyApplication_Base application, int channelIndex, byte[] receiveData)
			: base(application, channelIndex, receiveData)
		{

		}

	}
}
