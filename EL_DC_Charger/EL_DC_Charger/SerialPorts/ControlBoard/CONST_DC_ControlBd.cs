using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard
{
    public class CONST_DC_ControlBd
    {
        public class LENGTH
        {
            public const int DEFAULT_PACKET_NONETRD = 15;
            //public const int DEFAULT_PACKET_TRD = 15 + 2 + 2;
            public const int DEFAULT_PACKET_TRD = 15;

            public const int STX = 1;
            public const int SEQ = 2;
            public const int CHARGER_TYPE = 1;
            public const int CHARGER_ID = 1;
            public const int OUTLET_ID = 1;
            public const int PROTOCOL_DIVIDE = 2;
            public const int INS = 2;
            public const int LENGTH_DATA = 2;
            public const int LENGTH_TRDATA = 2;
            public const int LENGTH_RDATA = 2;
            public const int RDATA_HMI2CONTROL = 1;
            public const int RDATA_CONTROL2HMI = 3;
            public const int CHECKSUM = 2;
            public const int ETX = 1;
        }
        //    public const int STX = 1;
        //    public const int SEQ = 2;
        //    public const int CHARGER_TYPE = 1;
        //    public const int CHARGER_ID = 1;
        //    public const int OUTLET_ID = 1;
        //    public const int PROTOCOL_DIVIDE = 2;
        //    public const int INS = 2;
        //    public const int LENGTH_DATA = 2;
        //    public const int LENGTH_RDATA = 2;
        //    public const int CHECKSUM = 2;
        //    public const int ETX = 1;
        public class INDEX
        {
            public const int STX = 0;
            public const int SEQ = STX + LENGTH.STX; //1
            public const int CHARGER_TYPE = SEQ + LENGTH.SEQ; //3
            public const int CHARGER_ID = CHARGER_TYPE + LENGTH.CHARGER_TYPE; //4
            public const int OUTLET_ID = CHARGER_ID + LENGTH.CHARGER_ID; //5
            public const int PROTOCOL_DIVIDE = OUTLET_ID + LENGTH.CHARGER_ID; //6
            public const int INS = PROTOCOL_DIVIDE + LENGTH.PROTOCOL_DIVIDE; //8
            public const int LENGTH_DATA = INS + LENGTH.INS; //10
            public const int LENGTH_RDATA = LENGTH_DATA + LENGTH.LENGTH_DATA; //12
            public const int RDATA = LENGTH_RDATA + LENGTH.LENGTH_RDATA; // 14

            public const int LENGTH_TRDATA = LENGTH_DATA + LENGTH.LENGTH_DATA; //16
        }


        public class VALUE
        {
            public const byte STX = (byte)0xfe;
            public const byte ETX = (byte)0xff;

            public const int STX_INT = STX & 0x000000ff;
            public const int ETX_INT = ETX & 0x000000ff;


            public const byte ACK = 0x06;
            public const byte NAK = 0x15;

        }
    }
}
