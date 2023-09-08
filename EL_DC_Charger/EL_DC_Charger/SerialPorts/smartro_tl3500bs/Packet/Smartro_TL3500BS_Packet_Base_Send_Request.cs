using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet
{
    abstract public class Smartro_TL3500BS_Packet_Base_Send_Request : Smartro_TL3500BS_Packet_Base_Send
	{
		public Smartro_TL3500BS_Packet_Base_Send_Request(EL_MyApplication_Base application, int channelIndex, int lengthData, byte cmd)
			: base(application, channelIndex, lengthData, cmd)
		{

		}

        public override bool send_isNeedReceive_Response()
        {
            return true;
        }
    }
}
