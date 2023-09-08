using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet
{
    public class Smartro_TL3500BS_Checksum
    {
        public static byte getCheckSum(byte[] packet)
        {
            byte checksum = packet[0];
            for (int i = 1; i < packet.Length - 1; i++)
            {
                checksum ^= packet[i];
            }
            return checksum;
        }        
        public static void setCheckSum(byte[] packet)
        {
            byte checksum = getCheckSum(packet);
            packet[packet.Length - 1] = checksum;
        }

        public static bool isCheckSum(byte[] packet, int startIndexArray, int finishIndex)
        {
            byte checksum = packet[startIndexArray];
            for (int i = startIndexArray + 1; i < finishIndex - 1; i++)
            {
                checksum ^= packet[i];
            }

            if (packet[finishIndex - 1] == checksum)
                return true;

            return false;
        }
    }
}
