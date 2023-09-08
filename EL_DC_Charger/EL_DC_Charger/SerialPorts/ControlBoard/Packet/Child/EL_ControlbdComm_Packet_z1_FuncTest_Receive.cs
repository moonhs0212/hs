using EL_DC_Charger.Manager;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using AxWMPLib;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child
{
    public class EL_ControlbdComm_Packet_z1_FuncTest_Receive : EL_ControlbdComm_Packet_Base_Receive_By_Request
    {
        //public static List<z1_RecvDataStruct> z1_RecvDataStructs = new List<z1_RecvDataStruct>();

        public EL_ControlbdComm_Packet_z1_FuncTest_Receive(
                EL_DC_Charger_MyApplication application, int channelIndex,
                byte[] receiveData)
            : base(application, channelIndex, receiveData)
        {

            // TODO Auto-generated constructor stub
        }

        public EL_ControlbdComm_Packet_z1_FuncTest_Receive(
                EL_DC_Charger_MyApplication application, int channelIndex)
            : base(application, channelIndex, null)
        {
            // TODO Auto-generated constructor stub
        }


        //	byte[] max_output_voltage = Manager_Conversion.getByteArrayByInt(mApplication.getSettingData_System().getSettingData_Int(INDEX_SETTINGDATA_SYSTEM.INTEGER_MAXIMUM_OUTPUT_VOLTAGE), 2);
        //	byte[] max_output_current = Manager_Conversion.getByteArrayByInt(mApplication.getSettingData_System().getSettingData_Int(INDEX_SETTINGDATA_SYSTEM.INTEGER_MAXIMUM_OUTPUT_CURRENT), 2);
        //	byte[] count_output_port = Manager_Conversion.getByteArrayByInt(mApplication.getSettingData_System().getSettingData_Int(INDEX_SETTINGDATA_SYSTEM.INTEGER_COUNT_OUTPUT_PORT), 2);
        //	byte[] sametime_variable = Manager_Conversion.getByteArrayByInt(mApplication.getSettingData_System().getSettingData_Int(INDEX_SETTINGDATA_SYSTEM.INTEGER_SAMETIME_CHARGING_VARIABLE), 1);
        //	byte[] sametime_maximum_charging_count = Manager_Conversion.getByteArrayByInt(mApplication.getSettingData_System().getSettingData_Int(INDEX_SETTINGDATA_SYSTEM.INTEGER_SAMETIME_CHARGING_MAX_COUNT), 2);
        //	byte[] setting_Flag = new byte[1];
        //	bool setting_ControlBd_Doorlock_Control


        public static bool SUM_Relay_Plus;
        public static bool SUM_Relay_Minus;
        public static bool EmgStatus;


        public bool bFlag1_PowerRelay_Plus = false;
        public bool bFlag1_PowerRelay_Minus = false;
        //public bool bFlag1_EMG_Push = false;
        public bool bFlag1_Fuse = false;
        public bool bFlag1_GroundFault = false;
        public bool bFlag1_AMI_Comm_Normal = false;
        public bool bFlag1_Commstate_Powerpack_01 = false;
        public bool bFlag1_GunDetect = false;
        public bool bFlag1_PlcStatus = false;


        public bool bFlag2_Door_Open_1 = false;
        public bool bFlag2_Door_Open_2 = false;
        public bool bFlag2_Door_Open_3 = false;
        public bool bFlag2_GunLock = false;
        public bool bFlag2_Powerbank_MC_Relay = false;
        public bool bFlag2_Powerbank_PowerRelay_Plus = false;
        public bool bFlag2_Powerbank_PowerRelay_Minus = false;


        public int m_Charge_SEQ_NUM = 0;
        public int mDipSwitch = 0;

        public int mOutput_Voltage = 0;
        public int mOutput_Current = 0;

        public int mPWM1_High = 0;
        public int mPWM1_Low = 0;

        public int mPWM2_High = 0;
        public int mPWM2_Low = 0;

        public int mChargingGun_Temp_1 = 0;
        public int mChargingGun_Temp_2 = 0;

        public long mAMI_Wattage = 0;

        public int mChargingProcessState = 3;
        public int mErrorCode = 0;
        public int mSOC = 0;
        public int mRemainTime_Minute = 0;

        private byte[] mByteArrayEvcc_Id = new byte[8];
        private byte[] mByteArrayStartWattage = new byte[4];
        private byte[] mByteArrayEndWattage = new byte[4];
        public string mEvcc_id;
        public string board_StartWattage;
        public string board_EndWattage;

        public int getState_CommunicationCar()
        {
            //커넥터 분리
            if (mPWM1_High < 135
                    && mPWM1_High > 105)
            {
                return 1;
            }

            //커넥터 연결
            else if (mPWM1_High < 105
                   && mPWM1_High > 75)
            {
                return 2;
            }
            //충전 중
            else if (mPWM1_High < 75
                    && mPWM1_High > 5)
            {
                return 3;
            }
            else
            {
                return 0;
            }

        }

        public double getWastedMinute_FullCharge()
        {
            if (mTime_FullCharge == null)
                return 0;
            double minute = mTime_FullCharge.getMinute_WastedTime();
            return minute;
        }
        protected EL_Time mTime_FullCharge = null;
        override public void receive_applySystem()
        {
            // TODO Auto-generated method stub
            base.receive_applySystem();

            if (mApplication.isTrd)
            {
                ///////공통////////
                int index = 14;
                SUM_Relay_Plus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                index = 15;
                SUM_Relay_Minus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                index = 16;
                EmgStatus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 7);

                index = 19;
                bFlag1_PowerRelay_Plus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                bFlag1_PowerRelay_Minus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 1);
                //bFlag1_EMG_Push = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 2);
                bFlag1_Fuse = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 3);
                bFlag1_GroundFault = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 4);
                bFlag1_AMI_Comm_Normal = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 5);
                bFlag1_GunDetect = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 6);
                index = 20;
                bFlag2_Door_Open_1 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                bFlag2_Door_Open_2 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 1);
                bFlag2_Door_Open_3 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 2);
                bFlag2_GunLock = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 3);

                index = 22;
                m_Charge_SEQ_NUM = mReceiveData[index];
                ///////////////////
                ///

                index = 23;

                bFlag1_PowerRelay_Plus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                bFlag1_PowerRelay_Minus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 1);
                bFlag1_PlcStatus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 2);
                bFlag1_Fuse = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 3);
                bFlag1_GroundFault = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 4);
                bFlag1_Commstate_Powerpack_01 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 7);


                index++;
                bFlag2_GunLock = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 3);
                bFlag2_Powerbank_MC_Relay = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 4);
                bFlag2_Powerbank_PowerRelay_Plus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 5);
                bFlag2_Powerbank_PowerRelay_Minus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 6);


                //25
                index++;
                mDipSwitch = mReceiveData[index];


                mOutput_Voltage = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);
                mOutput_Current = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);

                mPWM1_High = mReceiveData[index++];
                mPWM1_Low = mReceiveData[index++];

                mPWM2_High = mReceiveData[index++];
                mPWM2_Low = mReceiveData[index++];

                mChargingGun_Temp_1 = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);
                mChargingGun_Temp_2 = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);

                if (bFlag1_AMI_Comm_Normal)
                {
                    mAMI_Wattage = (
                            ((mReceiveData[index++] << 24) & 0x0000000ff000000)
                                    | ((mReceiveData[index++] << 16) & 0x0000000000ff0000)
                                    | ((mReceiveData[index++] << 8) & 0x000000000000ff00)
                                    | ((mReceiveData[index++]) & 0x00000000000000ff)
                    );

                    (mApplication).getChannelTotalInfor(1).getAMI_PacketManager().setVoltage(mOutput_Voltage / 10.0f);
                    (mApplication).getChannelTotalInfor(1).getAMI_PacketManager().setCurrent(mOutput_Current / 10.0f);

                    (mApplication).getChannelTotalInfor(1).getAMI_PacketManager().setPositive_Active_Energy_Pluswh((long)(mAMI_Wattage / 100.0f));
                }

                index = 42;
                mChargingProcessState = EL_Manager_Conversion.getInt(mReceiveData[index++]);
                mErrorCode = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);
                mSOC = EL_Manager_Conversion.getInt(mReceiveData[index++]);

                if (mChargingProcessState == 100 && mSOC == 100)
                {
                    if (mTime_FullCharge == null) mTime_FullCharge = new EL_Time();
                }
                else
                {
                    mTime_FullCharge = null;
                }
                //46,47
                mRemainTime_Minute = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);

                index = 54;
                Buffer.BlockCopy(mReceiveData, index, mByteArrayEvcc_Id, 0, mByteArrayEvcc_Id.Length);
                mEvcc_id = EL_Manager_Conversion.ByteArrayToHexString(mByteArrayEvcc_Id);
                //mEvcc_id = EL_Manager_Conversion.HextoString(mEvcc_id);                
                index = 62;

                Buffer.BlockCopy(mReceiveData, index, mByteArrayStartWattage, 0, mByteArrayStartWattage.Length);
                board_StartWattage = EL_Manager_Conversion.byte4arrayToLong(mByteArrayStartWattage).ToString();

                index = 66;
                Buffer.BlockCopy(mReceiveData, index, mByteArrayEndWattage, 0, mByteArrayEndWattage.Length);
                board_EndWattage = EL_Manager_Conversion.byte4arrayToLong(mByteArrayEndWattage).ToString();
            }
            else
            {
                int index = 17;
                bFlag1_PowerRelay_Plus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                bFlag1_PowerRelay_Minus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 1);
                //bFlag1_EMG_Push = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 2);
                bFlag1_Fuse = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 3);
                bFlag1_GroundFault = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 4);
                bFlag1_AMI_Comm_Normal = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 5);

                bFlag1_Commstate_Powerpack_01 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 7);
                index++;
                bFlag2_Door_Open_1 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 0);
                bFlag2_Door_Open_2 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 1);
                bFlag2_Door_Open_3 = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 2);
                bFlag2_GunLock = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 3);
                bFlag2_Powerbank_MC_Relay = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 4);
                bFlag2_Powerbank_PowerRelay_Plus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 5);
                bFlag2_Powerbank_PowerRelay_Minus = EL_Manager_Conversion.getFlagByByteArray(mReceiveData[index], 6);
                index++;
                mDipSwitch = mReceiveData[index];
                index++;

                mOutput_Voltage = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);
                mOutput_Current = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);

                mPWM1_High = mReceiveData[index++];
                mPWM1_Low = mReceiveData[index++];

                mPWM2_High = mReceiveData[index++];
                mPWM2_Low = mReceiveData[index++];

                mChargingGun_Temp_1 = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);
                mChargingGun_Temp_2 = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);

                if (bFlag1_AMI_Comm_Normal)
                {
                    mAMI_Wattage = (
                            ((mReceiveData[index++] << 24) & 0x0000000ff000000)
                                    | ((mReceiveData[index++] << 16) & 0x0000000000ff0000)
                                    | ((mReceiveData[index++] << 8) & 0x000000000000ff00)
                                    | ((mReceiveData[index++]) & 0x00000000000000ff)
                    );

                    (mApplication).getChannelTotalInfor(1).getAMI_PacketManager().setVoltage(mOutput_Voltage / 10.0f);
                    (mApplication).getChannelTotalInfor(1).getAMI_PacketManager().setCurrent(mOutput_Current / 10.0f);

                    (mApplication).getChannelTotalInfor(1).getAMI_PacketManager().setPositive_Active_Energy_Pluswh((long)(mAMI_Wattage / 100.0f));
                }
                else
                {
                    index += 4;
                }




                mChargingProcessState = EL_Manager_Conversion.getInt(mReceiveData[index++]);
                mErrorCode = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]); ;
                mSOC = EL_Manager_Conversion.getInt(mReceiveData[index++]); ;

                if (mChargingProcessState == 100 && mSOC == 100)
                {
                    if (mTime_FullCharge == null) mTime_FullCharge = new EL_Time();
                }
                else
                {
                    mTime_FullCharge = null;
                }
                mRemainTime_Minute = EL_Manager_Conversion.getInt_2Byte(mReceiveData[index++], mReceiveData[index++]);


                //        if(mReceiveData_Data == null)
                //            return;
                //
                //        int indexArray = 0;
                //
                //        for(int i = 0; i < responseCode_2Byte_.length; i++)
                //        {
                //            responseCode_2Byte_[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //
                //        for(int i = 0; i < max_output_voltage_2Byte.length; i++)
                //        {
                //            max_output_voltage_2Byte[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //
                //        for(int i = 0; i < max_output_current_2Byte.length; i++)
                //        {
                //            max_output_current_2Byte[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //
                //        for(int i = 0; i < count_output_port_2Byte.length; i++)
                //        {
                //            count_output_port_2Byte[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //
                //        for(int i = 0; i < sametime_variable_1Byte.length; i++)
                //        {
                //            sametime_variable_1Byte[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //
                //        for(int i = 0; i < sametime_maximum_charging_count_2Byte.length; i++)
                //        {
                //            sametime_maximum_charging_count_2Byte[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //        for(int i = 0; i < controlbd_version_3Byte.length; i++)
                //        {
                //            controlbd_version_3Byte[i] = mReceiveData_Data[indexArray++];
                //        }
                //
                //        if (mReceiveData_Data.length >= 17)
                //        {
                //            for (int i = 0; i < pcu_version_3Byte.length; i++)
                //            {
                //                pcu_version_3Byte[i] = mReceiveData_Data[indexArray++];
                //            }
                //        }

                //		((TranportationCS_Manager_UIPage_Main)mApplication.getManager_UIPage_Main()).getManager_VG_Main().setVersion_Fw(mChannelIndex-1);
            }

        }

    }
}
