using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Device_Check_Send_Request : Smartro_TL3500BS_Packet_Base_Send_Request
    {
        const int LENGTH_DATA = 0;
        public Smartro_TL3500BS_Packet_Device_Check_Send_Request(EL_MyApplication_Base application, int channelIndex)
            : base(application, 0, LENGTH_DATA, Smartro_TL3500BS_Constants.VALUE.JOB_CODE.A_DEVICE_CHECK_REQ)
        {
            
        }
    }
}
