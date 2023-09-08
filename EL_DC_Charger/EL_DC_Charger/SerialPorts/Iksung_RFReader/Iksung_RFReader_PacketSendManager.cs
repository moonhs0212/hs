using EL_DC_Charger.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.Iksung_RFReader
{
    public class Iksung_RFReader_PacketSendManager : Controller_Base
    {
        protected Iksung_RFReader_Comm_Manager mCommPort_Manager = null;
        public Iksung_RFReader_PacketSendManager(Iksung_RFReader_Comm_Manager commPort_Manager)
            : base(commPort_Manager.getApplication(), 0)
        {
            mCommPort_Manager = commPort_Manager;
            setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY);
        }
        //02 0E 00 00 0E 03
        static byte[] mPacket_ALL_Card_Auto_Reading = new byte[] { 0x02, 0x0e, 0x00, 0x00, 0x0e, 0x03 };
        //    static byte[] mPacket_ALL_Card_Auto_Reading = new byte[]{0x02, (byte)0x8e, 0x00, 0x00, (byte)0x8e, 0x03};
        //    static byte[] mPacket_ALL_Card_Auto_Reading = new byte[]{0x02, (byte) 0x96, 0x00, 0x00, (byte) 0x96, 0x03};
        static byte[] mPacket_Dummy_Reader_Setting = new byte[] { 0x02, (byte)0x0f, 0x00, 0x00, 0x0f, 0x03 };

        public bool bIsReceive_Response_All_Card_Auto_Reading = false;
        public bool bIsReceive_Response_Dummy_Reader_Setting = false;


        public int mCommand_Reading = Iksung_RFReader_Constants.Command_Reading.NONE;
        public void setCommand_Reading(int command)
        {
            mCommand_Reading = command;
        }


        public bool bIsReceive_CardNumber = false;
        protected String mCardNumber = "";
        public String getCardNumber()
        {
            return mCardNumber;
        }
        public void setCardNumber(String number)
        {
            if (number == null || number.Length < 16)
            {
                bIsReceive_CardNumber = false;
                mCardNumber = "";
                return;
            }

            mCardNumber = number;
            bIsReceive_CardNumber = true;
        }

        public override void process()
        {
            switch (mMode)
            {
                case Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY:
                    bIsReceive_Response_Dummy_Reader_Setting = false;
                    mCommPort_Manager.write(mPacket_Dummy_Reader_Setting);
                    //                mCommPort_Manager.writeData(mPacket_ALL_Card_Auto_Reading);
                    setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY + 1);
                    break;
                case Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY + 1:
                    if (isTimer(TIMER_10S))
                    {
                        mCommPort_Manager.write(mPacket_Dummy_Reader_Setting);
                    }
                    if (isTimer(TIMER_1S))
                    {
                        if (!bIsReceive_Response_Dummy_Reader_Setting)
                            setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY);
                    }
                    else
                    {
                        switch (mCommand_Reading)
                        {
                            case Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_ONCE:
                                setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING);
                                break;
                            case Iksung_RFReader_Constants.Command_Reading.REQUEST_READ_FOREVER:
                                setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING_FOREVER);
                                break;
                        }
                    }
                    break;

                case Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING:
                    bIsReceive_CardNumber = false;
                    bIsReceive_Response_All_Card_Auto_Reading = false;
                    mCommPort_Manager.write(mPacket_ALL_Card_Auto_Reading);
                    setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING + 1);
                    break;

                case Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING + 1:
                    if (mCommand_Reading < 1 || bIsReceive_CardNumber)
                    {
                        mCommand_Reading = 0;
                        setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY);
                    }

                    if (isTimer(TIMER_5S))
                    {
                        mCommPort_Manager.write(mPacket_ALL_Card_Auto_Reading);
                    }
                    break;

                case Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING_FOREVER:
                    bIsReceive_Response_All_Card_Auto_Reading = false;
                    mCommPort_Manager.write(mPacket_ALL_Card_Auto_Reading);
                    setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING_FOREVER + 1);
                    break;

                case Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.ALL_CARD_AUTO_READING_FOREVER + 1:
                    if (mCommand_Reading < 1)
                    {
                        setMode(Iksung_RFReader_Constants.PSTec_RFReader_PacketSendState.READY);
                    }

                    if (isTimer(TIMER_5S))
                    {
                        mCommPort_Manager.write(mPacket_ALL_Card_Auto_Reading);
                    }
                    break;
            }
        }

        

        //override public void initVariable()
        //{

        //}
    }
}
