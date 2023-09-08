using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs
{
    public class Smartro_TL3500BS_Constants
    {
        public class LENGTH
        {
            public const int DEFAULT_PACKET = STX + TERMINAL_ID + DATE_TIME + JOB_CODE + RESPONSE_CODE + LENGTH_DATA + ETX + BCC;

            public const int STX = 1;
            public const int TERMINAL_ID = 16;
            public const int DATE_TIME = 14;
            public const int JOB_CODE = 1;
            public const int RESPONSE_CODE = 1;
            public const int LENGTH_DATA = 2;
            public const int ETX = 1;
            public const int BCC = 1;
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
            public const int TERMINAL_ID = STX + LENGTH.STX;
            public const int DATE_TIME = TERMINAL_ID + LENGTH.TERMINAL_ID;
            public const int JOB_CODE = DATE_TIME + LENGTH.DATE_TIME;
            public const int RESPONSE_CODE = JOB_CODE + LENGTH.JOB_CODE;
            public const int LENGTH_DATA = RESPONSE_CODE + LENGTH.RESPONSE_CODE;
            public const int DATA = LENGTH_DATA + LENGTH.LENGTH_DATA;
        }

        public class Command
        {
            public const int NONE = 0;
            public const int CARD_CHECK = 1;
            public const int DEAL_APPROVAL = 2;
            public const int DEAL_CANCEL = 3;
            public const int RESET = 4;
            public const int DEVICE_CHECK = 5;
            public const int DEAL_WAIT = 6;
        }

        public class PacketSendState
        {
            public const int READY = 0;
            public const int CARD_CHECK = 1000;
            public const int DEAL_APPROVAL = 2000;
            public const int DEAL_CANCEL = 3000;
            public const int RESET = 4000;
            public const int DEVICE_CHECK = 5000;
            public const int NOT_SUPPORT = 6000;
            public const int DEAL_WAIT = 7000;
        }

        public class VALUE
        {
            public const byte STX = (byte)0x02;
            public const byte ETX = (byte)0x03;

            public const int STX_INT = STX & 0x000000ff;
            public const int ETX_INT = ETX & 0x000000ff;


            public const byte ACK = 0x06;
            public const byte NAK = 0x15;

            public static byte[] KIOSK_ID = new byte[] {(byte)'e', (byte)'l', (byte)'e', (byte)'l', (byte)'e',
                                                        (byte)'c', (byte)'t', (byte)'0', (byte)'1', (byte)'m',
                                                        (byte)0, (byte)0, (byte)0, (byte)0, (byte)0 , 0};
            //public static byte[] KIOSK_ID = new byte[] {(byte)'K', (byte)'I', (byte)'O', (byte)'S', (byte)'K',
            //                                            (byte)'1', (byte)'1', (byte)'1', (byte)'4', (byte)'9',
            //                                            (byte)'1', (byte)'5', (byte)'5', (byte)'4', (byte)'5' };

            public static byte[] KIOSK_ID_DEFAULT = new byte[] {(byte)'K', (byte)'I', (byte)'O', (byte)'S', (byte)'K',
                                                        (byte)'1', (byte)'1', (byte)'1', (byte)'4', (byte)'9',
                                                        (byte)'1', (byte)'5', (byte)'5', (byte)'4', (byte)'5' };

            public class JOB_CODE
            {
                public const byte A_DEVICE_CHECK_REQ = (byte)'A';
                public const byte A_DEVICE_CHECK_RES = (byte)'a';

                public const byte B_DEAL_APPROVAL_REQ = (byte)'B';
                public const byte B_DEAL_APPROVAL_RES = (byte)'b';

                public const byte C_DEAL_CANCEL_REQ = (byte)'C';
                public const byte C_DEAL_CANCEL_RES = (byte)'c';

                public const byte D_CARD_CHECK_REQ = (byte)'D';
                public const byte D_CARD_CHECK_RES = (byte)'d';

                public const byte E_DEALWAIT_REQ = (byte)'E';
                public const byte E_DEALWAIT_RES = (byte)'e';

                public const byte G_ADDINFOR_APPROVAL_REQ = (byte)'G';
                public const byte G_ADDINFOR_APPROVAL_RES = (byte)'g';

                public const byte EVENT_CODE = (byte)'@';
                public const byte R_RESET = (byte)'R';
                public const byte A_DEVICE_CHECK = (byte)'A';
            }

            public class DEAL_MEDIA
            {
                public const byte IC = (byte)'1';
                public const byte MS = (byte)'2';
                public const byte RF = (byte)'3';
            }

            public class CARD_VARIABLE
            {
                public const byte T_MONEY = (byte)'T';
                public const byte CASH_BEE = (byte)'E';
                public const byte MIBEE = (byte)'M';
                public const byte U_PAY = (byte)'U';
                public const byte H_PAY = (byte)'H';
                public const byte RAIL_PLUS = (byte)'K';
                public const byte FUTURE_PAYMENT = (byte)'P';
                public const byte T_MONEY_FUTURE_PAYMENT = (byte)'A';
                public const byte NOT_WORKING = (byte)'X';
            }

            public class JOB_C_DEAL_CANCEL
            {
                public class CANCEL_DIVIDE_CODE
                {
                    public const byte CODE_1_CANCEL_REQUEST = (byte)'1';
                    public const byte CODE_2_LAST_DEAL_CANCEL = (byte)'2';
                    public const byte CODE_3_VAN_CARD_CANCEL = (byte)'3';
                    public const byte CODE_4_PG_CARD_CANCEL = (byte)'4';
                    public const byte CODE_5_PG_PART_CANCEL = (byte)'5';
                }

                public class DEAL_DIVIDE_CODE
                {
                    public const byte CODE_1_IC = (byte)'1';
                    public const byte CODE_2_RF_MS = (byte)'2';
                    public const byte CODE_3_CASH_RECEIPT = (byte)'3';
                    public const byte CODE_4_ZERO_PAY = (byte)'4';
                    public const byte CODE_5_KAKAO_PAY_MONEY = (byte)'5';
                    public const byte CODE_6_KAKAO_PAY_CREDIT = (byte)'6';
                }
            }

        }
    }
}
