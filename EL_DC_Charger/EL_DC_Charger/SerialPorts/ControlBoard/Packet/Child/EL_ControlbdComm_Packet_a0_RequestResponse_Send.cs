using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child
{
    public class EL_ControlbdComm_Packet_a0_RequestResponse_Send : EL_ControlbdComm_Packet_Base_Send_Request
    {
        protected static byte[] INS = new byte[] { (byte)'a', (byte)'0' };
        public EL_ControlbdComm_Packet_a0_RequestResponse_Send(EL_DC_Charger_MyApplication application, int channelIndex) : base(application, channelIndex, INS)
        {
        }

        public override byte[] send_getSendData_Data()
        {
            byte[] data = null;
            if (mApplication.isTrd)
            {
                data = new byte[2 + 3 + 2 + 1 + 7 + 2 + 2 + 2];
                //TRD Length [index 12]
            }
            else
            {

            }
            return data;
        }
    }
}
