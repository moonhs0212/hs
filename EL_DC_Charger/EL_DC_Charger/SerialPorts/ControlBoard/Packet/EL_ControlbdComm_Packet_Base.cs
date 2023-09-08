using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.Manager;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet
{
    abstract public class EL_ControlbdComm_Packet_Base
    {

        //BLE->Charger->BLE
        //Charger->BLE->Charger

        protected EL_DC_Charger_MyApplication mApplication = null;


        public int getOutlet_Id()
        {
            return mOutlet_ID;
        }
        protected byte mChargerType = 0;
        protected int mOutlet_ID = 0;
        protected byte[] mCmd = new byte[2];
        protected byte mFlag_1 = 0;
        protected byte mData_Union_Flag = 0;


        protected int mChannelIndex = 0;
        protected int mLength_Packet = 0;
        protected int mLength_Data = 0;
        protected int mLength_TRData = 0;
        protected byte[] mTRData = null;
        protected int mLength_RData = 0;
        protected byte[] mRData = null;


        protected byte[] send_mSendData = null;

        public String getString_Data() { return mString_Data; }

        protected String mString_Data = "";


        //protected EL_SC_1CH_Public_ChannelInfor_ChargerTotal mManager_ChannelInfor = null;
        //public EL_SC_1CH_Public_ChannelInfor_ChargerTotal getManager_ChannelInfor() { return mManager_ChannelInfor; }
        private EL_ControlbdComm_Packet_Base(EL_DC_Charger_MyApplication application, int channelIndex)
        {
            mApplication = application;
            mChannelIndex = channelIndex;


            //		mManager_ChannelInfor = mApplication.getManager_ChannelInforTotal(mChannelIndex-1);
            //		mOutlet_ID = mChannelIndex;
            //		mManager_ChannelInfor = application.getManager_ChannelInforTotal(mChannelIndex-1);

        }

        /*
		 * -----------------------------------------------------------------------------------������
		 */
        //Charger->BLE||->||Charger
        //BLE||->||Charger->BLE
        public EL_ControlbdComm_Packet_Base(EL_DC_Charger_MyApplication application, int channelIndex, byte[] receiveData_or_cmd, bool isReceive)
            : this(application, channelIndex)
        {
            if (isReceive)
                mReceiveData = receiveData_or_cmd;
            else
                mCmd = receiveData_or_cmd;
            //		receive_applySystem();
        }

        //BLE->Charger||->||BLE
        public EL_ControlbdComm_Packet_Base(EL_DC_Charger_MyApplication application, int channelIndex, EL_ControlbdComm_Packet_Base receivePacket)
            : this(application, channelIndex)
        {
            mReceivePacket_ForReturn = receivePacket;

        }

        /*
		 * ----------------------------------------------------------------------------------- ���� ������ ó��(�����Լ�)
		 */
        abstract public void receive_applySystem();
        abstract public bool receive_isNeedResponse();

        public void setReceiveData_NotCreate(byte[] receiveData)
        {
            mReceiveData = receiveData;
            if (mReceiveData == null) return;
            receive_applySystem();
        }

        /*
		 * ----------------------------------------------------------------------------------- ���� ������ ó��(�����Լ�)
		 */

        public byte[] getReceiveData()
        {
            return mReceiveData;
        }

        protected void send_setCheckSum(byte[] arrays)
        {
            byte[] crc = EL_ControlbdComm_Crc16.getCRC16_CCITT(arrays);
            arrays[arrays.Length - 3] = crc[0];
            arrays[arrays.Length - 2] = crc[1];
        }
        abstract public byte[] send_getSendData_Data();
        abstract public bool send_isNeedRequest();

        protected void send_applyData()
        {
            byte[] sendData = send_getSendData_Data();

            if (sendData != null)
            {
                for (int i = 0; i < sendData.Length; i++)
                {
                    send_mSendData[CONST_DC_ControlBd.INDEX.LENGTH_DATA + i] = sendData[i];
                }
            }

            byte[] temp = EL_ControlbdComm_Crc16.getCRC16_CCITT(send_mSendData, 0, mLength_Packet);
            send_mSendData[mLength_Packet - 3] = temp[0];
            send_mSendData[mLength_Packet - 2] = temp[1];

            send_mSendData[mLength_Packet - 1] = CONST_DC_ControlBd.VALUE.ETX;
        }

        protected EL_ControlbdComm_PacketManager getPacketManager()
        {
            return (EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager();
        }

        public byte[] send_makeSendData()
        {

            byte[] sendData_Data = send_getSendData_Data();

            if (sendData_Data == null || sendData_Data.Length < 1)
            {
                mLength_Data = 0;
                mLength_Packet = mLength_Data + getLength_DefaultPacket();
            }
            else
            {
                mLength_Data = sendData_Data.Length;
                mLength_Packet = mLength_Data + getLength_DefaultPacket();
            }

            if (send_mSendData == null)
                send_mSendData = new byte[mLength_Packet];

            send_mSendData[CONST_DC_ControlBd.INDEX.STX] = CONST_DC_ControlBd.VALUE.STX;
            //		send_mSendData[CONST_DC_ControlBd.INDEX.CHARGER_TYPE] = mApplication.getSettingData_System().getSettingData_Byte(EINDEX_SETTINGDATA_SYSTEM.CHARGER_TYPE);
            byte[] temp;
            if (EL_DC_Charger_MyApplication.getInstance().isTrd)
                temp = EL_Manager_Conversion.getByteArrayByInt(mChannelIndex, 1);
            else
                temp = new byte[] { 0 };
            for (int i = 0; i < temp.Length; i++)
            {
                send_mSendData[CONST_DC_ControlBd.INDEX.OUTLET_ID + i] = temp[i];
            }

            send_mSendData[CONST_DC_ControlBd.INDEX.INS] = mCmd[0];
            send_mSendData[CONST_DC_ControlBd.INDEX.INS + 1] = mCmd[1];

            send_mSendData[CONST_DC_ControlBd.INDEX.PROTOCOL_DIVIDE] = (byte)'E';
            send_mSendData[CONST_DC_ControlBd.INDEX.PROTOCOL_DIVIDE + 1] = (byte)'V';



            send_mSendData[CONST_DC_ControlBd.INDEX.LENGTH_DATA] = (byte)(0x000000ff & (mLength_Data >> 8));
            send_mSendData[CONST_DC_ControlBd.INDEX.LENGTH_DATA + 1] = (byte)(0x000000ff & (mLength_Data));

            if (sendData_Data != null)
            {
                for (int i = 0; i < sendData_Data.Length; i++)
                {
                    send_mSendData[CONST_DC_ControlBd.INDEX.LENGTH_RDATA + i] = sendData_Data[i];
                }
            }
            temp = EL_ControlbdComm_Crc16.getCRC16_CCITT(send_mSendData, 0, mLength_Packet);
            send_mSendData[mLength_Packet - 3] = temp[0];
            send_mSendData[mLength_Packet - 2] = temp[1];

            send_mSendData[mLength_Packet - 1] = CONST_DC_ControlBd.VALUE.ETX;
            return send_mSendData;
        }

        /*
		 * ----------------------------------------------------------------------------------- ����(�����Լ�)
		 */

        /*
		 * ----------------------------------------------------------------------------------- �������� ����� ���� �Լ�
		 */
        protected int getLength_DefaultPacket()
        {
            return mApplication.getSerialPort_ControlBd().getLength_DefaultPacket();
        }

        public byte[] getCmd()
        {
            return mCmd;
        }
        /*
		 * ----------------------------------------------------------------------------------- ���� ����
		 */


        protected byte[] mReceiveData_Data = null;

        protected byte[] mReceiveData = null;
        public void setReceiveData(byte[] packet)
        {
            mReceiveData = packet;
            mReceiveData_Data = null;
        }
        protected EL_ControlbdComm_Packet_Base mReceivePacket_ForReturn = null;
        protected int mIndexArray_Channel = 0;


        public bool IsCorrectData_LongByteArray(byte[] data, int startIndexArray, int length)
        {
            if (data[startIndexArray + CONST_DC_ControlBd.INDEX.STX] != CONST_DC_ControlBd.VALUE.STX)
                return false;

            if (data[startIndexArray + length - 1] != CONST_DC_ControlBd.VALUE.ETX)
                return false;

            int packetLength = EL_Manager_Conversion.getInt_2Byte(
                    new byte[]{
                            data[startIndexArray + CONST_DC_ControlBd.INDEX.LENGTH_DATA],
                            data[startIndexArray + CONST_DC_ControlBd.INDEX.LENGTH_DATA+1]
                            }) + mApplication.getSerialPort_ControlBd().getLength_DefaultPacket();

            if (length != packetLength + mApplication.getSerialPort_ControlBd().getLength_DefaultPacket())
                return false;

            byte[] crc = EL_ControlbdComm_Crc16.getCRC16_CCITT(data, startIndexArray, startIndexArray + packetLength);

            if (crc[0] != data[startIndexArray + length - 3])
                return false;

            if (crc[1] != data[startIndexArray + length - 2])
                return false;

            return true;
        }

        public static byte[] GetIns(byte[] packet)
        {
            byte[] ins = new byte[2];

            ins[0] = packet[CONST_DC_ControlBd.INDEX.INS];
            ins[1] = packet[CONST_DC_ControlBd.INDEX.INS + 1];
            return ins;
        }


        //	public static byte SetDefaultSendData_Charger_To_BLE(
        //		Klinelex_Bluetooth_DataManager manager, 
        //		byte[] data
        //	)
        //	{
        //		if(data[INDEX.STX] != VALUE.STX)
        //			return false;
        //		
        //		if(data[data.length-1] != VALUE.ETX)
        //			return false;
        //		
        //		int frameLength = data[INDEX.FRAME_LENGTH];
        //		int dataLength = data[INDEX.DATA_LENGTH];
        //		
        //		if(data.length != dataLength + 7)
        //			return false;
        //		
        //		byte checkSum = Klinelex_Bluetooth_CheckSum.CheckSum(data, INDEX.MESSAGE_TYPE, INDEX.MESSAGE_TYPE+dataLength);
        //		
        //		if(checkSum != data[data.length-2])
        //			return false;
        //		
        //		return true;
        //	}
    }
}
