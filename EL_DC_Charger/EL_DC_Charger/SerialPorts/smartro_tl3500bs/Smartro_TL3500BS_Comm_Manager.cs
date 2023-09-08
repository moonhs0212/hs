using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child;
using EL_DC_Charger.Interface_Common;
using Newtonsoft.Json.Bson;
using ParkingControlCharger.Object;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs
{

    public class Smartro_TL3500BS_Comm_Manager : MManager_Comm, ICardDevice_Manager
    {

        public string PATH = "COM6"; //path
        public static string sPATH = "COM5";
        protected const int BAUDRATE = 115200; //보레이트

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

        public Smartro_TL3500BS_Comm_Manager(EL_DC_Charger_MyApplication application) : base(application, 100)
        {
            mPath_Commport = application.getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_RFREADER);
            //mPath_Commport = "COM8";//PATH;
            commMake();
            
        }

        public override void initVariable()
        {
            mManager_Send = new Smartro_TL3500BS_PacketSendManager(this);
        }

        public override void commClose()
        { // 닫힘
            if (mSerialPort == null)
            {
                
            }else
            {
                mSerialPort.Close();
                mSerialPort.Dispose();
                mSerialPort = null;
            }
            initVariable_OnDisconnect();
            
            
        }

        public override void commOpen()
        {
            mTime_Disconnect = null;
            if ((!isPossible_SerialPort()))
            {
                commClose();
                return;
            }

            try
            {
                if (mSerialPort == null)
                    commMake();

                mSerialPort.Close();
                mSerialPort.Open();
                bIsConnected_HW = true;
            }
            catch (Exception ex)
            {
                initVariable_OnDisconnect();
            }
        }

        public override string getPath_SerialPort() => mPath_Commport;
        public override void intervalExcuteAsync() => mManager_Send.process(); //타이머
        public Smartro_TL3500BS_PacketSendManager mManager_Send = null;
        public Smartro_TL3500BS_PacketSendManager getManager_Send() => mManager_Send;


        public override bool write(byte[] data)
        {
            Console.Out.WriteLine("Smartro [Charger->TL3500BP ASCII]" +  EL_Manager_Conversion.asciiByteArrayToString(data));
            Console.Out.WriteLine("Smartro [Charger->TL3500BP HEX]" + EL_Manager_Conversion.ByteArrayToHexString(data));

            if (mSerialPort == null)
            {
                initVariable_OnDisconnect();
                if (mTime_Disconnect.getSecond_WastedTime() >= 10)
                    commOpen();
                return false;
            }
                

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
        int readSize = 0;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            readSize = mSerialPort.Read(mData_Temp, 0, mData_Temp.Length);

            if (readSize < 1)
                return;

            if (readSize == 1
                && mData_Temp[0] == 0x06)
            {
                mManager_Send.bIsReceive_ResponseAck = true;
                return;
            }
                
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

            if (mCountData >= Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET - 1 &&
                mReceive_Data[mCountData-1] == Smartro_TL3500BS_Constants.VALUE.ETX)
            {
                
                for (int i = mCountData - Smartro_TL3500BS_Constants.LENGTH.DEFAULT_PACKET + 1; i > -1; i--)
                {
                    if (Smartro_TL3500BS_Constants.VALUE.STX == mReceive_Data[i])
                    {
                        byte[] data_copyed = compare_Data(mReceive_Data, i, mCountData + 1);

                        

                        if (data_copyed == null)
                        {
                            continue;
                        }
                        


                        mManager_Send.PacketManager.setLastComm();

                        if (data_copyed[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] == Smartro_TL3500BS_Constants.VALUE.JOB_CODE.E_DEALWAIT_RES)
                        {
                            mManager_Send.responseAck();
                            mManager_Send.bIsCommandComplete = false;
                        }
                        else if (data_copyed[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] == Smartro_TL3500BS_Constants.VALUE.JOB_CODE.B_DEAL_APPROVAL_RES)
                        {
                            mManager_Send.responseAck();
                            mManager_Send.PacketManager.Packet_Deal_Request_Receive.setReceiveData(data_copyed);
                            mManager_Send.bIsReceive_Deal_Approval = true;

                            if (mManager_Send.PacketManager.Packet_Deal_Request_Receive.Deal_DivideCode == (byte)'X')
                                mManager_Send.bIsCommandComplete_OccuredError = true;

                            mManager_Send.bIsCommandComplete = true;
                        }
                        else if (data_copyed[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] == Smartro_TL3500BS_Constants.VALUE.JOB_CODE.G_ADDINFOR_APPROVAL_RES)
                        {
                            mManager_Send.responseAck();
                            mManager_Send.PacketManager.Packet_AddInfor_Deal_Request_Receive.setReceiveData(data_copyed);
                            mManager_Send.bIsReceive_Deal_Approval = true;

                            if (mManager_Send.PacketManager.Packet_AddInfor_Deal_Request_Receive.Deal_DivideCode == (byte)'X')
                                mManager_Send.bIsCommandComplete_OccuredError = true;

                            mManager_Send.bIsCommandComplete = true;
                        }

                        else if (data_copyed[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] == Smartro_TL3500BS_Constants.VALUE.JOB_CODE.C_DEAL_CANCEL_RES)
                        {
                            mManager_Send.responseAck();
                            mManager_Send.PacketManager.Packet_DealCancel_Receive.setReceiveData(data_copyed);
                            Console.WriteLine("Smartro_TL3500BS C Deal Cancel Receive =>" + mManager_Send.PacketManager.Packet_DealCancel_Receive.Deal_DivideCode_String);

                            mManager_Send.bIsReceive_Deal_Cancel = true;

                            if (mManager_Send.PacketManager.Packet_DealCancel_Receive.Deal_DivideCode == (byte)'X')
                                mManager_Send.bIsCommandComplete_OccuredError = true;

                            mManager_Send.bIsCommandComplete = true;
                        }
                        else if (data_copyed[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] == Smartro_TL3500BS_Constants.VALUE.JOB_CODE.D_CARD_CHECK_RES)
                        {
                            Console.WriteLine("Smartro_TL3500BS D Card Check Receive =>" + mManager_Send.PacketManager.Packet_CardCheck_Receive.Card_Number);

                            mManager_Send.PacketManager.Packet_CardCheck_Receive.setReceiveData(data_copyed);
                            if (mManager_Send.PacketManager.Packet_CardCheck_Receive.Card_Number.Length == 16)
                            {
                                mManager_Send.responseAck();
                                mManager_Send.bIsReceive_CardNumber = true;
                                if (mRFCardReader_Listener != null)
                                    mRFCardReader_Listener.onReceive(mManager_Send.PacketManager.Packet_CardCheck_Receive.Card_Number);
                            }
                            else
                            {
                                mManager_Send.bIsCommandComplete_OccuredError = true;
                            }

                            mManager_Send.bIsCommandComplete = true;

                        }



                        else if (data_copyed[Smartro_TL3500BS_Constants.INDEX.JOB_CODE] == Smartro_TL3500BS_Constants.VALUE.JOB_CODE.EVENT_CODE)
                        {
                            Console.WriteLine("Smartro_TL3500BS EVENTCODE Receive");
                        }
                        mCountData = -1;
                        return;
                    }

                }
                
            }
        }

        protected byte[] compare_Data(byte[] receive_Data, int startIndexArray, int finishIndex)
        {
            if(Smartro_TL3500BS_Packet_Base.IsCorrectData(receive_Data, startIndexArray, finishIndex))
            {
                byte[] returnData = new byte[finishIndex - startIndexArray];
                Array.Copy(receive_Data, startIndexArray, returnData, 0, returnData.Length);
                return returnData;
            }
            return null;
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
            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.NONE);
        }

        public void setCommand_Search_RFCard()
        {
            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.CARD_CHECK);
        }

        public bool isInclude_RFCardReader()
        {
            return true;
        }

        public bool isCanUse_PartCancel()
        {
            return true;
        }

        public void setCommand_Pay_First(int deal_Approval)
        {
            mManager_Send.PacketManager.Packet_AddInfor_Deal_Request_Send.setDeal_Request(deal_Approval);

            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.DEAL_APPROVAL);
        }


        public void setCommand_Pay_Cancel(int amount_Cancel, object obj)
        {
            mManager_Send.PacketManager.Packet_DealCancel_Send.setDealCancel(
               amount_Cancel, (Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request)obj);

            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.DEAL_CANCEL);
        }

        public void setCommand_Pay_Cancel(object obj)
        {
            mManager_Send.PacketManager.Packet_DealCancel_Send.setDealCancel(
                (Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request)obj);

            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.DEAL_CANCEL);
        }

        public void setCommand_Reset()
        {
            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.RESET);
        }

        public void setCommand_Deal_Wait()
        {
            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.DEAL_WAIT);
        }

        public void setCommand_DeviceCheck()
        {
            mManager_Send.setCommand(Smartro_TL3500BS_Constants.Command.DEVICE_CHECK);
        }

        public void setCardDevice_Listener(IRFCardReader_EventListener listener)
        {
            mRFCardReader_Listener = listener;
        }

        public bool isCommand_Complete()
        {
            return mManager_Send.bIsCommandComplete;
        }

        public bool isCommand_ErrorOccured()
        {
            return mManager_Send.bIsCommandComplete_OccuredError;
        }

        public string getErrorCode_String()
        {
            string errorCode = "";
            switch(mManager_Send.Command_Processing)
            {
                default:
                    errorCode = "명령없음";
                    break;
                case Smartro_TL3500BS_Constants.Command.CARD_CHECK:
                    if (mManager_Send.PacketManager.Packet_CardCheck_Receive.Card_Number.Length == 16)
                        errorCode = "에러없음";
                    else
                        errorCode = "카드번호 길이 에러 (" + mManager_Send.PacketManager.Packet_CardCheck_Receive.Card_Number.Length + ")";
                    break;
                case Smartro_TL3500BS_Constants.Command.DEAL_APPROVAL:
                    if (mManager_Send.PacketManager.Packet_AddInfor_Deal_Request_Receive.Deal_DivideCode_String.Equals("X"))
                    {
                        errorCode = "거래오류 (" + mManager_Send.PacketManager.Packet_AddInfor_Deal_Request_Receive.ResponseMessage_RejectionDeal + ")";
                    }
                    else
                    {
                        errorCode = "에러없음";
                    }
                    break;
                case Smartro_TL3500BS_Constants.Command.DEAL_CANCEL:
                    if (mManager_Send.PacketManager.Packet_DealCancel_Receive.Deal_DivideCode_String.Equals("X"))
                    {
                        errorCode = "거래오류 (" + mManager_Send.PacketManager.Packet_DealCancel_Receive.ResponseMessage_RejectionDeal + ")";
                    }
                    else
                    {
                        errorCode = "에러없음";
                    }
                    break;
                case Smartro_TL3500BS_Constants.Command.RESET:
                    errorCode = "에러없음";
                    break;
                case Smartro_TL3500BS_Constants.Command.DEVICE_CHECK:
                    errorCode = "에러없음";
                    break;
            }
            return errorCode;
        }

        public int getErrorCode()
        {
            throw new NotImplementedException();
        }

        public override void commMake()
        {
            if (isExist_SerialPort(mPath_Commport) && isPossible_SerialPort())
            {
                if (mSerialPort != null)
                    commClose();
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
    }
}
