using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Reset_Send_Only
        : Smartro_TL3500BS_Packet_Base_Send
    {
        public Smartro_TL3500BS_Packet_Reset_Send_Only(EL_MyApplication_Base application)
            : base(application, 0, 0, Smartro_TL3500BS_Constants.VALUE.JOB_CODE.R_RESET)
        {
        }

        public override bool send_isNeedReceive_Response()
        {
            return false;
        }
    }
}
