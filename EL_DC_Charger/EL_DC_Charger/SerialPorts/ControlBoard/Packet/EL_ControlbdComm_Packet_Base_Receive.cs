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
    abstract public class EL_ControlbdComm_Packet_Base_Receive : EL_ControlbdComm_Packet_Base
    {

        //Charger->BLE||->||Charger
        //BLE||->||Charger->BLE
        public EL_ControlbdComm_Packet_Base_Receive(EL_DC_Charger_MyApplication application, int channelIndex, byte[] receiveData)
            : base(application, channelIndex, receiveData, true)
        {

        }
        protected int mCount_DisonnectConnector = 0;
        override public void receive_applySystem()
        {
            // TODO Auto-generated method stub
            if (mReceiveData == null)
                return;

            mApplication.getChannelTotalInfor(mChannelIndex).getControlbdComm_PacketManager().setLastComm();

            mChargerType = mReceiveData[CONST_DC_ControlBd.INDEX.CHARGER_TYPE];
            mOutlet_ID = EL_Manager_Conversion.getInt_2Byte(new byte[] { mReceiveData[CONST_DC_ControlBd.INDEX.OUTLET_ID], mReceiveData[CONST_DC_ControlBd.INDEX.OUTLET_ID + 1] });
            mCmd[1] = mReceiveData[CONST_DC_ControlBd.INDEX.INS + 1];
            mCmd[0] = mReceiveData[CONST_DC_ControlBd.INDEX.INS];
            mChargerType = mReceiveData[CONST_DC_ControlBd.INDEX.CHARGER_TYPE];

            if (mApplication.isTrd)
            {
                mLength_RData = (mReceiveData[CONST_DC_ControlBd.INDEX.LENGTH_RDATA] << 8) | mReceiveData[CONST_DC_ControlBd.INDEX.LENGTH_RDATA + 1];                
                if (mRData == null || mRData.Length != mLength_RData)
                {
                    mRData = new byte[mLength_RData];
                }
                for (int i = 0; i < mRData.Length; i++)
                    mRData[i] = mReceiveData[CONST_DC_ControlBd.INDEX.RDATA + 3 + i];
                
                bool tempConnector = EL_Manager_Conversion.getFlagByByteArray(mRData[2], 1);
                //충전건분리
                if (!tempConnector)
                {
                    mCount_DisonnectConnector++;

                    if (mCount_DisonnectConnector > 5)
                        getPacketManager().IsConnectedGun_Combo_Type1 = tempConnector;
                }
                else
                {
                    mCount_DisonnectConnector = 0;
                    getPacketManager().IsConnectedGun_Combo_Type1 = tempConnector;
                }
                if (mRData.Length > 1)
                    getPacketManager().IsPush_Emg = EL_Manager_Conversion.getFlagByByteArray(mRData[2], 7);
            }
            else
            {
                mLength_RData = (mReceiveData[CONST_DC_ControlBd.INDEX.LENGTH_RDATA] << 8) | mReceiveData[CONST_DC_ControlBd.INDEX.LENGTH_RDATA + 1];
                if (mRData == null || mRData.Length != mLength_RData)
                {
                    mRData = new byte[mLength_RData];
                }
                for (int i = 0; i < mRData.Length; i++)
                    mRData[i] = mReceiveData[CONST_DC_ControlBd.INDEX.RDATA + i];
                bool tempConnector = EL_Manager_Conversion.getFlagByByteArray(mRData[0], 1);
                //충전건분리
                if (!tempConnector)
                {
                    mCount_DisonnectConnector++;

                    if (mCount_DisonnectConnector > 5)
                        getPacketManager().IsConnectedGun_Combo_Type1 = tempConnector;
                }
                else
                {
                    mCount_DisonnectConnector = 0;
                    getPacketManager().IsConnectedGun_Combo_Type1 = tempConnector;
                }
                if (mRData.Length > 1)
                    getPacketManager().IsPush_Emg = EL_Manager_Conversion.getFlagByByteArray(mRData[1], 7);
            }


            //		mFlag_1 = mReceiveData[CONST_DC_ControlBd.INDEX.FLAG_1];
            //
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setSystemInit(EL_Manager_Conversion.getFlagByByteArray(mFlag_1, 0));
            //
            //		int length = (mReceiveData[CONST_DC_ControlBd.INDEX.LENGTH] << 8 | mReceiveData[CONST_DC_ControlBd.INDEX.LENGTH+1]) & 0x0000ffff;
            //		mData_Union_Flag = mReceiveData[CONST_DC_ControlBd.INDEX.DATA_UNION_FLAG_1];
            //		bool flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 0);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setConnectConnector_Chademo(flag);
            //		flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 1);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setConnectConnector_Combo(flag);
            //		flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 2);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setConnectConnector_AC3Phase(flag);
            //		flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 3);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setIsPush_Emg(flag);
            //
            //		flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 5);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setDoorOpen_Chademo(flag);
            //		flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 6);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setDoorOpen_Combo(flag);
            //		flag = EL_Manager_Conversion.getFlagByByteArray(mData_Union_Flag, 7);
            //		mManager_ChannelInfor.getDataManager_ControlBd_CommData_Channel().receive_setDoorOpen_AC3Phase(flag);
            //
            //		mReceiveData_Data = Arrays.copyOfRange(mReceiveData, CONST_DC_ControlBd.INDEX.DATA_UNION_FLAG_1+1, mReceiveData.length - 3);
        }

        override public bool send_isNeedRequest()
        {
            return false;
        }

        override public byte[] send_getSendData_Data()
        {
            // TODO Auto-generated method stub
            return null;
        }

    }
}
