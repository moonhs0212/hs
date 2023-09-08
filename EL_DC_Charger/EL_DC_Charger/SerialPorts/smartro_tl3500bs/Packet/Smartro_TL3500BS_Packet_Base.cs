using EL_DC_Charger.Manager;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common.application;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet
{
    abstract public class Smartro_TL3500BS_Packet_Base
    {
		//BLE->Charger->BLE
		//Charger->BLE->Charger

		protected EL_MyApplication_Base mApplication = null;


		protected byte[] mTerminal_ID = new byte[16];
		protected byte[] mDate_Time = new byte[14];

		protected byte mJob_Code = 0;


		protected int mChannelIndex = 0;
		protected int mLength_Packet = 0;
		protected int mLength_Data = 0;


		protected byte[] send_mSendData = null;

		public String getString_Data() { return mString_Data; }

		protected String mString_Data = "";

		//protected EL_SC_1CH_Public_ChannelInfor_ChargerTotal mManager_ChannelInfor = null;
		//public EL_SC_1CH_Public_ChannelInfor_ChargerTotal getManager_ChannelInfor() { return mManager_ChannelInfor; }
		protected Smartro_TL3500BS_Packet_Base(EL_MyApplication_Base application, int channelIndex)
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
		

		

		

		
		protected Smartro_TL3500BS_Packet_Base mReceivePacket_ForReturn = null;

		/*
		 * ----------------------------------------------------------------------------------- ����(�����Լ�)
		 */

		/*
		 * ----------------------------------------------------------------------------------- �������� ����� ���� �Լ�
		 */
		protected int getLength_DefaultPacket()
		{
			return Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET;
		}

		public byte getJobCode()
		{
			return mJob_Code;
		}

		public byte getResponseCode()
		{
			int jobCode = mJob_Code & 0x000000ff;
			if (mJob_Code >= 0x41 && mJob_Code <= 0x5a)
            {
				return (byte)((mJob_Code + 0x20) & 0x000000ff);
			}else
            {
				return (byte)((mJob_Code - 0x20) & 0x000000ff);
			}

			
		}
		/*
		 * ----------------------------------------------------------------------------------- ���� ����
		 */


		


		public static bool IsCorrectData(byte[] data, int startIndexArray, int finishIndex)
		{
			if (data[startIndexArray + Smartro_TL3500BS_Constants.INDEX.STX] != Smartro_TL3500BS_Constants.VALUE.STX)
				return false;

			if (data[finishIndex - 2] != Smartro_TL3500BS_Constants.VALUE.ETX)
				return false;

			int packetLength = EL_Manager_Conversion.getInt_2Byte(
					new byte[]{
							data[startIndexArray + Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA+1],
							data[startIndexArray + Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA]
							}) + Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET;

			if (finishIndex - startIndexArray != packetLength)
				return false;

			bool result = Smartro_TL3500BS_Checksum.isCheckSum(data, startIndexArray, startIndexArray + packetLength);

			return result;
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
