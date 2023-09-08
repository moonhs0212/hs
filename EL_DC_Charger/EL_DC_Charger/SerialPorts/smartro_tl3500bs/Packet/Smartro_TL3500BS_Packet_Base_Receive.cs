using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet
{
    abstract public class Smartro_TL3500BS_Packet_Base_Receive : Smartro_TL3500BS_Packet_Base
    {
        public Smartro_TL3500BS_Packet_Base_Receive(EL_MyApplication_Base application, int channelIndex, byte[] receiveData)
            : base(application, channelIndex)
        {
            if (receiveData != null)
            {
                mReceiveData = receiveData;
                receive_applySystem();
            }
        }



        public bool receive_isNeedResponse()
        {
            // TODO Auto-generated method stub
            return false;
        }



        /*
		 * ----------------------------------------------------------------------------------- ���� ������ ó��(�����Լ�)
		 */
        virtual public void receive_applySystem()
        {
            if (mReceiveData == null)
                return;

            for (int i = 0; i < Smartro_TL3500BS_Constants.LENGTH.TERMINAL_ID; i++)
            {
                mTerminal_ID[i] = mReceiveData[Smartro_TL3500BS_Constants.INDEX.TERMINAL_ID + i];
            }

            for (int i = 0; i < Smartro_TL3500BS_Constants.LENGTH.DATE_TIME; i++)
            {
                mDate_Time[i] = mReceiveData[Smartro_TL3500BS_Constants.INDEX.DATE_TIME + i];
            }

            mJob_Code = mReceiveData[Smartro_TL3500BS_Constants.INDEX.JOB_CODE];

            mLength_Data = EL_Manager_Conversion.getInt_2Byte(mReceiveData[Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA + 1], mReceiveData[Smartro_TL3500BS_Constants.INDEX.LENGTH_DATA]);
            mLength_Packet = mLength_Data + Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET;

            mReceiveData_Data = new byte[mLength_Data];

            if (!EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsRebootChargeYN && mReceiveData_Data.Length == 187)
                EL_DC_Charger_MyApplication.getInstance().lastDealInfo = mReceiveData_Data;


            Array.Copy(mReceiveData, Smartro_TL3500BS_Constants.INDEX.DATA, mReceiveData_Data, 0, mLength_Data);

            Console.Out.WriteLine("Smartro [TL3500BP->Charger ASCII]" + EL_Manager_Conversion.asciiByteArrayToString(mReceiveData_Data));
            Console.Out.WriteLine("Smartro [TL3500BP->Charger HEX]" + EL_Manager_Conversion.ByteArrayToHexString(mReceiveData_Data));

            //카드 승인
            if (mReceiveData.Length == 224)
            {
                EL_DC_Charger_MyApplication.getInstance().getManager_SQLite_Setting_OCPP().getTable_TransactionInfor().db_CardDevicePacket(EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.mTransactionInfor_DBId.ToString(),
                    EL_Manager_Conversion.ByteHexToHexString(mReceiveData));
            }

        }
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

        public byte[] getReceiveData_Data()
        {
            return mReceiveData_Data;
        }



        protected byte[] mReceiveData_Data = null;

        protected byte[] mReceiveData = null;
        public void setReceiveData(byte[] packet)
        {
            mReceiveData = packet;
            mReceiveData_Data = null;
            receive_applySystem();
        }



    }
}
