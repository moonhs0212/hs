using EL_DC_Charger.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet
{
    abstract public class Smartro_TL3500BS_Packet_Base_Send : Smartro_TL3500BS_Packet_Base
    {
        protected byte[] mSendData_Data = null;


        public Smartro_TL3500BS_Packet_Base_Send(EL_MyApplication_Base application, int channelIndex, int dataLength, byte jobCode)
            : base(application, channelIndex)
        {
            mJob_Code = jobCode;
            send_setSendData_Data_Length(dataLength);
        }

        //BLE->Charger||->||BLE
        public Smartro_TL3500BS_Packet_Base_Send(EL_MyApplication_Base application, int channelIndex, int dataLength, Smartro_TL3500BS_Packet_Base receivePacket)
            : this(application, channelIndex, dataLength, receivePacket.getResponseCode())
        {
            mReceivePacket_ForReturn = receivePacket;
        }

        public void send_setSendData_Data_Length(int dataLength)
        {
            if (mLength_Data == dataLength)
                return;

            mLength_Data = dataLength;
            if (mLength_Data > 0)
            {
                mSendData_Data = Enumerable.Repeat((byte)'0', mLength_Data).ToArray();

            }


            send_setDefaultPacket();
        }

        protected void send_setCheckSum(byte[] arrays)
        {
            byte crc = Smartro_TL3500BS_Checksum.getCheckSum(arrays);
            arrays[arrays.Length - 1] = crc;
        }
        abstract public bool send_isNeedReceive_Response();


        public byte[] send_makeSendData()
        {
            send_setDefaultPacket();

            byte[] temp = EL_Manager_Time.getTime_ASCii_14Byte();
            for (int i = 0; i < temp.Length; i++)
            {
                send_mSendData[Smartro_TL3500BS_Constants.INDEX.DATE_TIME + i] = temp[i];
            }


            if (mSendData_Data != null)
            {
                for (int i = 0; i < mSendData_Data.Length; i++)
                {
                    send_mSendData[Smartro_TL3500BS_Constants.INDEX.DATA + i] = mSendData_Data[i];
                }
            }

            send_setCheckSum(send_mSendData);

            Console.Out.WriteLine("보내는데이터 -> " + EL_Manager_Conversion.asciiByteArrayToString(send_mSendData));
            return send_mSendData;

            
        }


        virtual public void send_setDefaultPacket()
        {
            if (mSendData_Data == null || mSendData_Data.Length < 1)
            {
                mLength_Data = 0;
                mLength_Packet = mLength_Data + Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET;
            }
            else
            {
                mLength_Data = mSendData_Data.Length;
                mLength_Packet = mLength_Data + Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET;
            }
            byte[] temp = null;
            if (send_mSendData == null || send_mSendData.Length != mLength_Packet)
            {

                send_mSendData = new byte[mLength_Packet];
                send_mSendData[Smartro_TL3500BS_Constants.INDEX.STX] = Smartro_TL3500BS_Constants.VALUE.STX;

                for (int i = 0; i < Smartro_TL3500BS_Constants.VALUE.KIOSK_ID.Length; i++)
                {
                    send_mSendData[Smartro_TL3500BS_Constants.INDEX.TERMINAL_ID + i] = Smartro_TL3500BS_Constants.VALUE.KIOSK_ID[i];
                }

                send_mSendData[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] = mJob_Code;

                send_mSendData[Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA] = (byte)(mLength_Data & 0x000000ff);
                send_mSendData[Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA + 1] = (byte)((mLength_Data >> 8) & 0x000000ff);

                //send_mSendData[Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA] = (byte)((mLength_Data >> 8) & 0x000000ff); 
                //send_mSendData[Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA + 1] = (byte)(mLength_Data & 0x000000ff);

                send_mSendData[send_mSendData.Length - 2] = Smartro_TL3500BS_Constants.VALUE.ETX;
            }
        }

    }
}
