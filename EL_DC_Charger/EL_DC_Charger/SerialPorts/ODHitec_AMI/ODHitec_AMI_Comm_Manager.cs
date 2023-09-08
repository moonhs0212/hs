using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.thread;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.Utils;
using Modbus.Device;
using Modbus.Extensions.Enron;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ODHitec_AMI
{

    public class ODHitec_AMI_Comm_Manager : EL_Thread_Base
    {
        string mPath_Commport = "";
        SerialPort mSerialPort = null;
        ModbusSerialMaster mModbusSerialMaster = null;
        public ODHitec_AMI_Comm_Manager(EL_MyApplication_Base application, bool isNeedAdd) : base(application, 450, isNeedAdd)
        {
            mPath_Commport = application.getManager_SQLite_Setting().getTable_Setting(0).getSettingData(CONST_INDEX_MAINSETTING.PATH_SERIAL_AMI);
            makePort();
        }




        public void makePort()
        {
            if (isExist_SerialPort(mPath_Commport))
            {
                mSerialPort = new SerialPort(mPath_Commport, 9600);
                mSerialPort.Parity = Parity.None;
                mSerialPort.StopBits = StopBits.One;
                mSerialPort.ReadTimeout = 100;
                mSerialPort.WriteTimeout = 100;

                try
                {
                    mSerialPort.Open();
                    mModbusSerialMaster = ModbusSerialMaster.CreateRtu(mSerialPort);
                }
                catch (Exception e)
                {
                    close();
                    mTime_Disconnect = new EL_Time();
                }
            }
        }

        public void close()
        {
            if (mSerialPort != null)
            {
                mSerialPort.Close();
                mSerialPort = null;
            }

            if (mModbusSerialMaster != null)
            {
                mModbusSerialMaster.Dispose();
                mModbusSerialMaster = null;
            }


        }


        EL_Time mTime_Disconnect = null;

        public override void initVariable()
        {

        }

        public bool isExist_SerialPort(string path)
        {
            if (path == null || path.Length < 4)
                return false;

            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();
            bool isExist_Port = false;
            for (int i = 0; i < comlist.Length; i++)
            {
                if (comlist[i].Equals(path))
                {
                    isExist_Port = true;
                    break;
                }
            }
            return isExist_Port;
        }

        private uint[] DcVoltage = new uint[4] { 0, 0, 0, 0 };
        private uint[] DcCurrent = new uint[4] { 0, 0, 0, 0 };
        private uint[] DcWattage = new uint[4] { 0, 0, 0, 0 };
        private long[] DcChargedWattage = new long[4] { 0, 0, 0, 0 };


        protected int mIndexID = 0;

        public override void intervalExcute()
        {
            if (mSerialPort != null && mSerialPort.IsOpen)
            {
                ushort[] response_data=null;
                try
                {
                    if (mApplication.EAmiCompany == common.EAmiCompany.Pilot)
                    {
                        response_data = mModbusSerialMaster.ReadHoldingRegisters((byte)(mIndexID + 1), 0, 23);

                        uint.TryParse(response_data[0].ToString(), out DcVoltage[0]);
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setVoltage(DcVoltage[0] / 10);
                        ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(mIndexID + 1).getControlbdComm_PacketManager())
                            .packet_z1.mAMI_Voltage = (int)(DcVoltage[0]);

                        uint.TryParse(response_data[1].ToString(), out DcCurrent[0]);
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setCurrent(DcCurrent[0] / 100);
                        ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(mIndexID + 1).getControlbdComm_PacketManager())
                            .packet_z1.mAMI_Current = (int)DcCurrent[0] * 10;

                        int value = 0;
                        //if(response_data[9]-3 > 0)
                        //{
                        //    for(int i = 0; i < response_data[9] - 3; i++)
                        //    {

                        //    }
                        //    DcWattage[0] = response_data[8];
                        //}
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setLastComm();
                        DcChargedWattage[0] = response_data[4] * 65536 + response_data[5];
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setPositive_Active_Energy_Pluswh(DcChargedWattage[0] * 10);
                    }
                    else
                    {
                        response_data = mModbusSerialMaster.ReadHoldingRegisters((byte)(mIndexID + 1), 0, 19);

                        uint.TryParse(response_data[0].ToString(), out DcVoltage[0]);
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setVoltage(DcVoltage[0]);
                        ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(mIndexID + 1).getControlbdComm_PacketManager())
                            .packet_z1.mAMI_Voltage = (int)(DcVoltage[0] * 10);
                        uint.TryParse(response_data[2].ToString(), out DcCurrent[0]);
                        ((EL_ControlbdComm_PacketManager)mApplication.getChannelTotalInfor(mIndexID + 1).getControlbdComm_PacketManager())
                            .packet_z1.mAMI_Current = (int)DcCurrent[0];
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setCurrent(DcCurrent[0] / 10.0f);
                        int value = 0;
                        //if(response_data[9]-3 > 0)
                        //{
                        //    for(int i = 0; i < response_data[9] - 3; i++)
                        //    {

                        //    }
                        //    DcWattage[0] = response_data[8];
                        //}
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setLastComm();
                        DcChargedWattage[0] = response_data[12] * 65536 + response_data[3];
                        mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setPositive_Active_Energy_Pluswh(DcChargedWattage[0] * 10);
                    }

                    mApplication.getChannelTotalInfor(mIndexID + 1).getAMI_PacketManager().setTime_Receive();

                    mIndexID++;
                    if (mIndexID >= mApplication.getChannelCount())
                        mIndexID = 0;
                }
                catch (Exception ex)
                {
                    close();
                    mTime_Disconnect = new EL_Time();
                    mTime_Disconnect.setTime();

                    CsUtil.WriteLog("전력량계 통신 에러 [현재 STATE : " + EL_DC_Charger_MyApplication.getInstance().mStateManager_Main.getState()
                        + " 에러내용 : " + ex.Message + "]"
                        + " 수신데이터 : " + EL_Manager_Conversion.ushortArrayToString(response_data));

                }
            }
            else if (mTime_Disconnect != null)
            {
                if (mTime_Disconnect.getSecond_WastedTime() > 60)
                {
                    mTime_Disconnect = null;
                    makePort();
                }
            }
            else
            {                
                close();
                mTime_Disconnect = new EL_Time();
                mTime_Disconnect.setTime();
            }
        }
    }
}
