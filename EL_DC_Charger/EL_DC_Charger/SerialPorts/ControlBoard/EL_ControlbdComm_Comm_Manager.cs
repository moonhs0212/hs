using EL_DC_Charger.CRC;
using EL_DC_Charger.Manager;
using EL_DC_Charger.common.item;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet;
using ParkingControlCharger.Object;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.Utils;
using EL_DC_Charger.common.statemanager;

namespace EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard
{
    public class EL_ControlbdComm_Comm_Manager : MManager_Comm
    {
        public string PATH = "COM6"; //path
        public static string sPATH = "COM5";
        protected const int BAUDRATE = 38400; //보레이트

        protected SerialPort mSerialPort = null;

        //protected static HS_ControlBd_BoardComm_Manager_SerialPort mPort = null;
        //public static HS_ControlBd_BoardComm_Manager_SerialPort getInstance()
        //{
        //    if (mPort != null)
        //        return mPort;

        //    sPATH = "COM" + MyApplication.getInstance().getSettingData_System().getSettingData(EINDEX_SETTING_MAIN.path);

        //    if (!Manager_SerialPort.isConnected_SerialPort(sPATH)) //sPath에 연결됐는지
        //        return null;

        //    mPort = new HS_ControlBd_BoardComm_Manager_SerialPort(MyApplication.getInstance());
        //    return mPort;
        //}

        public EL_ControlbdComm_Comm_Manager(EL_DC_Charger_MyApplication application) : base(application, 50)
        {
            mPath_Commport = application.getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_CONTROLBD);
            //mPath_Commport = "COM8";//PATH;
            commMake();
        }

        public override void initVariable()
        {
            mManager_Send = new EL_ControlbdComm_PacketSendManager((EL_DC_Charger_MyApplication)mApplication, this);

        }

        public override void commClose()
        { // 닫힘
            if (mSerialPort == null)
            {
                bIsConnected_HW = false;
                bIsConnected_SW = false;
                commMake();
            }
            else
            {
                mSerialPort.Close();
                mSerialPort.Dispose();
                mSerialPort = null;
                bIsConnected_HW = false;
                bIsConnected_SW = false;
                commMake();
            }
        }

        public override void commOpen()
        {

            if ((!isPossible_SerialPort()) || mSerialPort == null)
            {
                commClose();
                return;
            }

            try
            {
                mSerialPort.Open();
                bIsConnected_HW = true;
            }
            catch (Exception ex)
            {
                bIsConnected_HW = false;
                bIsConnected_SW = false;
            }
        }

        public int getLength_DefaultPacket()
        {
            if (mApplication.isTrd)
                return CONST_DC_ControlBd.LENGTH.DEFAULT_PACKET_TRD;
            else
                return CONST_DC_ControlBd.LENGTH.DEFAULT_PACKET_NONETRD;
        }


        public override string getPath_SerialPort() => mPath_Commport;
        public override void intervalExcuteAsync() => mManager_Send.sendData_Interval(); //타이머
        protected EL_ControlbdComm_PacketSendManager mManager_Send = null;
        public EL_ControlbdComm_PacketSendManager getManager_Send() => mManager_Send;

        public override bool write(byte[] data)
        {
            if (mDateTime_LastComm.getMinute_WastedTime() > 0)
            {
                mDateTime_LastComm.setTime();
                commClose();
                commOpen();
                return false;
            }

            if (mSerialPort != null)
            {
                try
                {
                    mSerialPort.Write(data, 0, data.Length);
                    return true;
                }
                catch (Exception e)
                {

                    return false;
                }


            }
            else
            {
                return false;
            }
        }

        public override void commMake()
        {
            mDateTime_LastComm.setTime();
            if (isExist_SerialPort(mPath_Commport) && isPossible_SerialPort())
            {
                mSerialPort = new SerialPort(mPath_Commport, BAUDRATE);
                try
                {
                    //if (!mSerialPort.IsOpen)
                    //    mSerialPort.Open();
                    mSerialPort.DataReceived += serialPort1_DataReceived; //시리얼포트 데이터를 받음
                    bIsConnected_HW = true;
                }
                catch (Exception e)
                {
                }
            }
        }


        protected byte[] mData_Temp = new byte[4096];

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            int readSize = mSerialPort.Read(mData_Temp, 0, mData_Temp.Length);
            try
            {
                if (readSize < 1)
                    return;

                for (int i = 0; i < readSize; i++)
                    processData(mData_Temp[i]);
            }
            catch (Exception ex)
            {
                CsUtil.WriteLog("보드 통신 에러 [현재 STATE : " + EL_DC_Charger_MyApplication.getInstance().mStateManager_Main.getState()
                    + " 에러내용 : " + ex.Message + "]"
                    + " 수신데이터(readSize : " + readSize
                    + " 수신데이터 : " + EL_Manager_Conversion.ByteArrayToHexString(mData_Temp));
            }
        }

        protected int mCountData = -1;
        protected byte[] mReceive_Data = new byte[2048];

        protected void processData(byte data)
        {
            if (mCountData >= (mReceive_Data.Length - 1))
            {
                mCountData = -1;
            }

            mCountData++;
            mReceive_Data[mCountData] = data;

            if (mReceive_Data[mCountData] == CONST_DC_ControlBd.VALUE.ETX)
            {
                if (mCountData >= getLength_DefaultPacket() - 1)
                {
                    for (int i = mCountData - getLength_DefaultPacket() + 1; i > -1; i--)
                    {
                        if (CONST_DC_ControlBd.VALUE.STX == mReceive_Data[i])
                        {
                            byte[] data_copyed = compare_Data(mReceive_Data, i, mCountData + 1);

                            if (data_copyed == null)
                            {
                                continue;
                            }
                            if (EL_DC_Charger_MyApplication.getInstance().isTrd)
                            {
                                int outletId = data_copyed[5];
                                
                                ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(outletId).getControlbdComm_PacketManager()).packet_1z.setReceiveData_NotCreate(data_copyed);
                                ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(outletId).getControlbdComm_PacketManager().setTime_Receive();
                            }
                            else
                            {
                                ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager()).packet_1z.setReceiveData_NotCreate(data_copyed);
                                ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().setTime_Receive();
                            }

                            if (EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).getControlbdComm_PacketManager().mMode_SendState == 0)
                            {
                                if(mApplication.getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_1z.mChargingProcessState == 100)
                                {
                                    ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().packet_z1.mCommand_Output_Channel1 = 1;
                                }
                                ((EL_DC_Charger_MyApplication)mApplication).getChannelTotalInfor(1).getControlbdComm_PacketManager().mMode_SendState = 1; 
                            }
                            
                            //                        ((EL_DC_Charger_MyApplication)mApplication).getCommPort_ControlBd().mReceiveThread.mHandler.post(new Runnable() {
                            //                            @Override
                            //                            public void run() {
                            //                                ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main.getVGManager_CertMode().updateUI_By_ControlbdComm();
                            //                            }
                            //                        });  //mPageManager_Main.getVGManager_CertMode().mHandler.sendEmptyMessage(1);//
                            //                        ((EL_DC_Charger_MyApplication)mApplication).getActivity_Main().mHandler.post(new Runnable() {
                            //                                 @Override
                            //                                 public void run() {
                            //
                            //                                         ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main.getVGManager_CertMode().updateUI_By_ControlbdComm();
                            //
                            //                                 }
                            //                             });


                            //                        if(mListener_DataReceive != null)
                            //                        {
                            //                            mListener_DataReceive.onReceiveCorrectPacket(data_copyed);
                            //                        }
                            mCountData = -1;
                            return;
                        }

                    }
                }
            }
        }

        //public void onReceiveCorrectPacket(byte[] packet)
        //{
        //    if (packet == null)
        //        return;

        //    if (packet[CONST_DC_ControlBd.INDEX.PROTOCOL_ID] != CONST_DC_ControlBd.VALUE.PROTOCOL_ID[0]
        //       && packet[CONST_DC_ControlBd.INDEX_PROTOCOL_ID + 1] != CONST_DC_ControlBd.VALUE_PROTOCOL_ID[1])
        //        return;

        //    int index_Cmd = CONST_DC_ControlBd.INDEX_CMD;
        //    int channelIndex = (packet[CONST_DC_ControlBd.INDEX_CHARGER_ID]) & 0x000000ff;
        //    byte[] ins = new byte[] { packet[index_Cmd], packet[index_Cmd + 1] };

        //    setLastComm();

        //    bIsReceiveData = true;
        //    switch (ins[1])
        //    {
        //        case (byte)'a':
        //            switch (ins[0])
        //            {
        //                case (byte)'1':
        //                    mManager_Send.mManager_Packet.mPacket_a1_Receive.receive_ApplyData(packet);
        //                    break;
        //            }
        //            break;
        //        case (byte)'z':
        //            switch (ins[0])
        //            {
        //                case (byte)'1':
        //                    mManager_Send.mManager_Packet.mPacket_z1_Receive.receive_ApplyData(packet);
        //                    break;
        //            }
        //            break;
        //    }
        //}


        protected byte[] compare_Data(byte[] receive_Data, int startIndexArray, int finishIndex)
        {
            if (receive_Data == null)
                return null;

            if (finishIndex - startIndexArray < getLength_DefaultPacket())
                return null;


            if (receive_Data[startIndexArray] != CONST_DC_ControlBd.VALUE.STX)
                return null; ;

            if (receive_Data[finishIndex - 1] != CONST_DC_ControlBd.VALUE.ETX)
                return null;

            int length = (0x0000ff00 & (receive_Data[startIndexArray + CONST_DC_ControlBd.INDEX.LENGTH_DATA] << 8))
                    | (0x000000ff & receive_Data[startIndexArray + CONST_DC_ControlBd.INDEX.LENGTH_DATA + 1]);

            if ((finishIndex - startIndexArray) != length + getLength_DefaultPacket())
                return null;

            byte[] crc = EL_ControlbdComm_Crc16.getCRC16_CCITT(receive_Data, startIndexArray, finishIndex);

            if (crc[0] != receive_Data[finishIndex - 3]
                    || crc[1] != receive_Data[finishIndex - 2])
            {
                return null;
            }

            byte[] returnData = new byte[finishIndex - startIndexArray];
            Array.Copy(receive_Data, startIndexArray, returnData, 0, returnData.Length);
            return returnData;
        }


        //protected byte[] compare_Data(byte[] receive_Data, int startIndexArray, int finishIndex)
        //{
        //    if (receive_Data == null)
        //        return null;

        //    if (finishIndex - startIndexArray < CONST_DC_ControlBd.LENGTH_DEFUALT)
        //        return null;

        //    if (receive_Data[startIndexArray] != CONST_DC_ControlBd.VALUE_STX)
        //        return null; ;

        //    if (receive_Data[finishIndex - 1] != CONST_DC_ControlBd.VALUE_ETX)
        //        return null;

        //    int Length = (0x0000ff00 & (receive_Data[startIndexArray + CONST_DC_ControlBd.INDEX_LENGTH_DATA] << 8))
        //            | (0x000000ff & receive_Data[startIndexArray + CONST_DC_ControlBd.INDEX_LENGTH_DATA + 1]);

        //    if ((finishIndex - startIndexArray) != Length + CONST_DC_ControlBd.LENGTH_DEFUALT)
        //        return null;

        //    byte[] crc = CRC16.getCRC16_CCITT(receive_Data, startIndexArray, finishIndex);

        //    if (crc[0] != receive_Data[finishIndex - 3]
        //            || crc[1] != receive_Data[finishIndex - 2])
        //    {
        //        return null;
        //    }

        //    byte[] returnData = new byte[finishIndex - startIndexArray];
        //    Array.Copy(receive_Data, startIndexArray, returnData, 0, returnData.Length);
        //    return returnData;
        //}



        public override bool isConnected_Comm()
        {


            return base.isConnected_Comm();
        }
    }

}