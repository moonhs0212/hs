using EL_DC_Charger.Manager;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.Interface_Common;
using ParkingControlCharger.Object;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common.interf;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.Iksung_RFReader
{
    public class Iksung_RFReader_Comm_Manager : MManager_Comm, IRFCardReader_Manager
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

        public Iksung_RFReader_Comm_Manager(EL_DC_Charger_MyApplication application) : base(application, 500)
        {
            mPath_Commport = "COM14"; //application.getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_RFREADER);
            //mPath_Commport = "COM8";//PATH;
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

        public override void initVariable()
        {
            mManager_Send = new Iksung_RFReader_PacketSendManager(this);
        }

        public override void commClose()
        { // 닫힘
            if (mSerialPort == null)
            {
                
                return;
            }

            bIsConnected_HW = false;
            bIsConnected_SW = false;
            

            mSerialPort.Close();
            mSerialPort.Dispose();
            
        }

        public override void commOpen()
        {

            if ((!isPossible_SerialPort() && bIsConnected_HW) || mSerialPort == null)
            {
                bIsConnected_HW = false;
                bIsConnected_SW = false;
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
            }
        }

        public override string getPath_SerialPort() => mPath_Commport;
        public override void intervalExcuteAsync() => mManager_Send.process(); //타이머
        protected Iksung_RFReader_PacketSendManager mManager_Send = null;
        public Iksung_RFReader_PacketSendManager getManager_Send() => mManager_Send;

        public override bool write(byte[] data)
        {
            if (mSerialPort == null)
                return false;

            try
            {
                mSerialPort.Write(data, 0, data.Length);
                bIs_FaultSerial = false;
            }
            catch (Exception ex)
            {
                if (!bIs_FaultSerial)
                {
                    bIs_FaultSerial = true;
                    mTime_FaultSerial.setTime();
                    commClose();
                }
            }
            finally
            {
                if (bIs_FaultSerial && mTime_FaultSerial.getSecond_WastedTime() > 15)
                {
                    try
                    {
                        commOpen();
                    }
                    catch (Exception ex)
                    {
                        bIs_FaultSerial = true;
                        mTime_FaultSerial.setTime();
                        commClose();
                    }

                }
            }
            return true;
        }

        protected EL_Time mTime_FaultSerial = new EL_Time();
        bool bIs_FaultSerial = false;

        protected byte[] mData_Temp = new byte[4096];

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int readSize = mSerialPort.Read(mData_Temp, 0, mData_Temp.Length);

            if (readSize < 1)
                return;

            for (int i = 0; i < readSize; i++)
                processData(mData_Temp[i]);
        }

        protected int mCountData = -1;
        protected byte[] mReceive_Data = new byte[2048];

        protected IRFCardReader_EventListener mRFCardReader_Listener = null;
        public void setRFCardReader_Listener(IRFCardReader_EventListener listener)
        {
            mRFCardReader_Listener = listener;
        }


        protected void processData(byte data)
        {
            if (mCountData >= (mReceive_Data.Length - 1))
            {
                mCountData = -1;
            }

            mCountData++;
            mReceive_Data[mCountData] = data;

            if (mReceive_Data[mCountData] == Iksung_RFReader_Constants.VALUE.ETX)
            {
                if (mCountData >= Iksung_RFReader_Constants.LENGTH.RECEIVE_DEFAULT_PACKET - 1)
                {
                    for (int i = mCountData - Iksung_RFReader_Constants.LENGTH.RECEIVE_DEFAULT_PACKET + 1; i > -1; i--)
                    {
                        if (Iksung_RFReader_Constants.VALUE.STX == mReceive_Data[i])
                        {
                            byte[] data_copyed = compare_Data(mReceive_Data, i, mCountData + 1);

                            if (data_copyed == null)
                            {
                                continue;
                            }

                            if (data_copyed[Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_ALL_CARD_AUTO_READING)
                            {
                                mManager_Send.bIsReceive_Response_All_Card_Auto_Reading = true;
                            }
                            else if (data_copyed[Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_READ_RF_TMONEY)
                            {
                                StringBuilder cardNumber = new StringBuilder();
                                for (int k = 0; k < 8; k++)
                                {
                                    String conversion = EL_Manager_Conversion.getInt_BCDCode_String(data_copyed[Iksung_RFReader_Constants.INDEX.CARDNUMBER + k]);
                                    cardNumber.Append(conversion);
                                }
                                string cardNumber_String = cardNumber.ToString();
                                if (mRFCardReader_Listener != null)
                                    mRFCardReader_Listener.onReceive(cardNumber_String);

                                mManager_Send.setCardNumber(cardNumber_String);

                                //                            ((EL_DC_Charger_MyApplication)mApplication).getActivity_Main().mHandler.post(new Runnable() {
                                //                                @Override
                                //                                public void run() {
                                //                                    ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main.getVGManager_CertMode().setText_RFCardNumber(cardNumber.toString());
                                //                                }
                                //                            });
                            }
                            else if (data_copyed[Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_READ_RF_CashBee_Railplus)
                            {
                                StringBuilder cardNumber = new StringBuilder();
                                for (int k = 0; k < 8; k++)
                                {
                                    int conversion = EL_Manager_Conversion.getInt_BCDCode(data_copyed[Iksung_RFReader_Constants.INDEX.CARDNUMBER + k]);
                                    cardNumber.Append(conversion);
                                }

                                string cardNumber_String = cardNumber.ToString();
                                if (mRFCardReader_Listener != null)
                                    mRFCardReader_Listener.onReceive(cardNumber_String);

                                mManager_Send.setCardNumber(cardNumber_String);

                                //                            ((EL_DC_Charger_MyApplication)mApplication).getActivity_Main().mHandler.post(new Runnable() {
                                //                                @Override
                                //                                public void run() {
                                //                                    ((EL_DC_Charger_MyApplication)mApplication).mPageManager_Main.getVGManager_CertMode().setText_RFCardNumber(cardNumber.toString());
                                //                                }
                                //                            });
                            }
                            else if (data_copyed[Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_DUMMY_Reader_Setting)
                            {
                                mManager_Send.bIsReceive_Response_Dummy_Reader_Setting = true;
                            }

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

        protected byte[] compare_Data(byte[] receive_Data, int startIndexArray, int finishIndex)
        {
            if (receive_Data == null)
                return null;

            if (finishIndex - startIndexArray < Iksung_RFReader_Constants.LENGTH.RECEIVE_DEFAULT_PACKET)
                return null;


            if (receive_Data[startIndexArray] != Iksung_RFReader_Constants.VALUE.STX)
                return null; ;

            if (receive_Data[finishIndex - 1] != Iksung_RFReader_Constants.VALUE.ETX)
                return null;

            int length = (receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX.RECEIVE_DATALENGTH]) & 0x000000ff;

            if ((finishIndex - startIndexArray) != length + Iksung_RFReader_Constants.LENGTH.RECEIVE_DEFAULT_PACKET)
                return null;

            if (
                !(
                        receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_ALL_CARD_AUTO_READING
                        || receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_DUMMY_Reader_Setting
                        || receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_READ_RF_CashBee_Railplus
                        || receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX.CMD] == Iksung_RFReader_Constants.VALUE.CMD_READ_RF_TMONEY
                )
            )
                return null;

            byte checksum = 0;

            for (int i = startIndexArray + 1; i < finishIndex - 2; i++)
            {
                checksum = (byte)((checksum + receive_Data[i]) & 0x000000ff);
            }

            if (checksum != receive_Data[finishIndex - 2])
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

        //    if (finishIndex - startIndexArray < Iksung_RFReader_Constants.LENGTH_DEFUALT)
        //        return null;

        //    if (receive_Data[startIndexArray] != Iksung_RFReader_Constants.VALUE_STX)
        //        return null; ;

        //    if (receive_Data[finishIndex - 1] != Iksung_RFReader_Constants.VALUE_ETX)
        //        return null;

        //    int Length = (0x0000ff00 & (receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX_LENGTH_DATA] << 8))
        //            | (0x000000ff & receive_Data[startIndexArray + Iksung_RFReader_Constants.INDEX_LENGTH_DATA + 1]);

        //    if ((finishIndex - startIndexArray) != Length + Iksung_RFReader_Constants.LENGTH_DEFUALT)
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

        public void setCommand_Ready()
        {
            throw new NotImplementedException();
        }

        public void setCommand_Search_RFCard()
        {
            throw new NotImplementedException();
        }

        public void setCommand_Reset()
        {
            throw new NotImplementedException();
        }

        public void setCommand_DeviceCheck()
        {
            throw new NotImplementedException();
        }

        public override void commMake()
        {
            throw new NotImplementedException();
        }
    }
}
