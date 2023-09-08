using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Card_Check_Send_Request : Smartro_TL3500BS_Packet_Base_Send_Request
    {
        const int LENGTH_DATA = 0;
        public Smartro_TL3500BS_Packet_Card_Check_Send_Request(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex, LENGTH_DATA, Smartro_TL3500BS_Constants.VALUE.JOB_CODE.D_CARD_CHECK_REQ)
        {
            
        }

        public override void send_setDefaultPacket()
        {
            base.send_setDefaultPacket();

            for (int i = 0; i < Smartro_TL3500BS_Constants.VALUE.KIOSK_ID_DEFAULT.Length; i++)
            {
                send_mSendData[Smartro_TL3500BS_Constants.INDEX.TERMINAL_ID + i] = Smartro_TL3500BS_Constants.VALUE.KIOSK_ID_DEFAULT[i];
            }

        }
    }
}
