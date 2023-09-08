using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.Iksung_RFReader
{
    public class Iksung_RFReader_Constants
    {
        public class Command_Reading
        {
            public const int NONE = 0;
            public const int REQUEST_READ_ONCE = 1;
            public const int REQUEST_READ_FOREVER = 2;
        }


        public class PSTec_RFReader_PacketSendState
        {
            public const int READY = 0;
            public const int ALL_CARD_AUTO_READING = 1000;
            public const int ALL_CARD_AUTO_READING_FOREVER = 2000;

        }

        public class LENGTH
        {
            public const int SEND_DEFAULT_PACKET = 6;
            public const int RECEIVE_DEFAULT_PACKET = 7;

            public const int STX = 1;
            public const int CMD = 1;
        }

        public class INDEX
        {
            public const int STX = 0;
            public const int CMD = STX + Iksung_RFReader_Constants.LENGTH.STX;
            public const int RECEIVE_DATALENGTH = 4;

            public const int CARDNUMBER = CMD + 4;
        }


        public class VALUE
        {
            public const byte STX = (byte)0x02;
            public const byte ETX = (byte)0x03;

            public const int STX_INT = STX & 0x000000ff;
            public const int ETX_INT = ETX & 0x000000ff;


            public const byte CMD_ALL_CARD_AUTO_READING = 0x0e;
            public const byte CMD_READ_RF_TMONEY = 0x3d;
            public const byte CMD_READ_RF_CashBee_Railplus = 0x3e;

            public const byte CMD_DUMMY_Reader_Setting = 0x0f;

        }
    }
}
