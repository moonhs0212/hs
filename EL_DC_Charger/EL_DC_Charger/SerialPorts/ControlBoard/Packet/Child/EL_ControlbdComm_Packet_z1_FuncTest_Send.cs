using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.ControlBoard.Packet.Child
{
    public class EL_ControlbdComm_Packet_z1_FuncTest_Send : EL_ControlbdComm_Packet_Base_Send_Request
    {

        protected static byte[] INS = new byte[] { (byte)'z', (byte)'1' };
        public EL_ControlbdComm_Packet_z1_FuncTest_Send(
                EL_DC_Charger_MyApplication application, int channelIndex)
            : base(application, channelIndex, INS)
        {

            // TODO Auto-generated constructor stub
        }

        //    case R.id.cb_mc_on:
        //            ((EL_ChannelInfor_ChargerTotal)mApplication.getChannelTotalInfor()).getControlbdComm_PacketManager().packet_z1.
        //                    break;
        //                case R.id.cb_charging_on:
        //            break;
        //                case R.id.cb_rf_on:
        //            break;

        public bool bMC_On = false;
        public bool bPWM_On = false;
        public bool bFAN_On = false;
        public bool bGunLock = false;
        //추가 z1
        public bool bOn_D1 = false;
        public bool bOn_D2 = false;

        public bool bPowerRelay_Plus = false;
        public bool bPowerRelay_Minus = false;

        public bool bSumRelay_Plus = false;
        public bool bSumRelay_Minus = false;

        public bool bHMI_Manual_Control = false;
        
        public void setHMI_Manual_Control(bool setting)
        {
            bHMI_Manual_Control = setting;
        }
        public bool bLED1_Red = false;
        public bool bLED1_Green = false;
        public bool bLED1_Blue = false;

        public int mCommand_Output_Channel1 = 0;
        public int mCommand_Output_Channel2 = 0;

        public int mCommand_Output_Voltage = 0;
        public int mCommand_Output_Current = 0;

        public int mAMI_Voltage = 0;
        public int mAMI_Current = 0;

        //추가
        public bool bReset_ControlBD = false;
        public bool bOn_IRLED = false;
        public bool bOn_RFID_Power = false;

        public bool[] bON_Sum_Plus = new bool[8];
        public bool[] bON_Sum_Minus = new bool[8];



        public bool bOn_PowerBank_MCRelay = false;
        public bool bOn_PowerBank_PowerRelay_Plus = false;
        public bool bOn_PowerBank_PowerRelay_Minus = false;
        public bool bOn_PowerBank_FAN_Relay = false;

        public bool bOn_DoorOpen_Chademo = false;
        public bool bOn_DoorOpen_Combo1 = false;
        public bool bOn_DoorOpen_Combo2 = false;
        public bool bOn_DoorOpen_AC3 = false;
        public bool bOn_DoorOpen_GBT = false;
        public bool bOn_DoorOpen_Slow = false;

        public int mCommand_Output_Power = 0;

        public int mRequestWatage = 100;

        byte[] tempByteWattage;
        public void initVariable()
        {
            bMC_On = false;
            bPWM_On = false;
            bFAN_On = false;

            bPowerRelay_Plus = false;
            bPowerRelay_Minus = false;

            bLED1_Red = false;
            bLED1_Green = false;
            bLED1_Blue = false;
            mCommand_Output_Channel1 = 0;

            for (int i = 0; i < bON_Sum_Plus.Length; i++) bON_Sum_Plus[i] = false;

            for (int i = 0; i < bON_Sum_Minus.Length; i++) bON_Sum_Minus[i] = false;
            bOn_D1 = false; bOn_D2 = false;
            mCommand_Output_Channel1 = 0;
            mCommand_Output_Channel2 = 0;

            bOn_PowerBank_MCRelay = false;
            bOn_PowerBank_PowerRelay_Plus = false;
            bOn_PowerBank_PowerRelay_Minus = false;
            bOn_PowerBank_FAN_Relay = false;


        }

        override public byte[] send_getSendData_Data()
        {

            //공통 데이터

            // TODO Auto-generated method stub
            byte[] data = null;
            if (mApplication.isTrd)
            {
                data = new byte[2 + 3 + 2 + 1 + 7 + 2 + 2 + 2 + 4];
                //TRD Length [index 12]
                data[0] = 0; data[1] = 3;

                //TRD Data 14
                data[2] = 0;
                if (bReset_ControlBD) data[2] |= 0x80;
                if (bOn_IRLED) data[2] |= 0x40;
                if (bOn_RFID_Power) data[2] |= 0x20;
                if (bMC_On) data[2] |= 0x10;
                if (bFAN_On) data[2] |= 0x08;
                if (bHMI_Manual_Control)
                    data[2] |= 0x04;

                //15 
                data[3] = 0;
                if (bSumRelay_Plus) data[3] |= 0x80;
                //for (int i = 0; i < 8; i++)
                //{
                //    if (bON_Sum_Plus[i])
                //    {
                //        int temp = 0x80;
                //        temp = (temp >> (8 - (8 - i))) & 0x000000ff;
                //        data[3] |= (byte)temp;
                //    }
                //}
                //16
                data[4] = 0;
                if (bSumRelay_Minus) data[4] |= 0x80;
                //for (int i = 0; i < 8; i++)
                //{
                //    if (bON_Sum_Minus[i])
                //    {
                //        int temp = 0x80;
                //        temp = (temp >> (8 - (8 - i))) & 0x000000ff;
                //        data[4] |= (byte)temp;
                //    }
                //}

                //RD 17,18
                data[5] = 0; data[6] = 1;


                //19
                int tempIndex = 7;
                if (bOn_DoorOpen_Chademo) data[tempIndex] |= 0x80;
                if (bOn_DoorOpen_Combo1) data[tempIndex] |= 0x40;
                if (bOn_DoorOpen_Combo2) data[tempIndex] |= 0x20;
                if (bOn_DoorOpen_AC3) data[tempIndex] |= 0x10;
                if (bOn_DoorOpen_GBT) data[tempIndex] |= 0x08;
                if (bOn_DoorOpen_Slow) data[tempIndex] |= 0x04;
                //////////////////여기까지 공통////////////////////

                //20
                tempIndex = 8;
                if (bPowerRelay_Plus) data[tempIndex] |= 0x40;
                if (bPowerRelay_Minus) data[tempIndex] |= 0x20;
                if (bGunLock) data[tempIndex] |= 0x10;
                if (bOn_D1) data[tempIndex] |= 0x08;
                if (bOn_D2) data[tempIndex] |= 0x04;

                //21
                tempIndex = 9;
                if (bLED1_Red) data[tempIndex] |= 0x10;
                if (bLED1_Green) data[tempIndex] |= 0x08;
                if (bLED1_Blue) data[tempIndex] |= 0x04;

                //22
                tempIndex = 10;
                data[tempIndex] = 0;
                if (bOn_PowerBank_MCRelay) data[tempIndex] |= 0x80;
                if (bOn_PowerBank_PowerRelay_Plus) data[tempIndex] |= 0x40;
                if (bOn_PowerBank_PowerRelay_Minus) data[tempIndex] |= 0x20;
                if (bOn_PowerBank_FAN_Relay) data[tempIndex] |= 0x10;
                data[tempIndex] |= (byte)((mCommand_Output_Channel1 << 2) & 0b00001100);
                data[tempIndex] |= (byte)((mCommand_Output_Channel2) & 0b00000011);

                tempIndex = 11;
                data[tempIndex++] = (byte)((mCommand_Output_Voltage >> 8) & 0x000000ff);
                data[tempIndex++] = (byte)((mCommand_Output_Voltage) & 0x000000ff);

                //Console.Out.WriteLine("ChannelIndex = " + mChannelIndex);

                if (EL_DC_Charger_MyApplication.getInstance().slowMode)
                {
                    data[tempIndex++] = (byte)(((mCommand_Output_Current / 5) >> 8) & 0x000000ff);
                    data[tempIndex++] = (byte)(((mCommand_Output_Current / 5)) & 0x000000ff);
                }
                else
                {
                    data[tempIndex++] = (byte)((mCommand_Output_Current >> 8) & 0x000000ff);
                    data[tempIndex++] = (byte)((mCommand_Output_Current) & 0x000000ff);
                }


                data[tempIndex++] = (byte)((mAMI_Voltage >> 8) & 0x000000ff);
                data[tempIndex++] = (byte)((mAMI_Voltage) & 0x000000ff);

                data[tempIndex++] = (byte)((mAMI_Current >> 8) & 0x000000ff);
                data[tempIndex++] = (byte)((mAMI_Current) & 0x000000ff);

                data[tempIndex++] = (byte)((mCommand_Output_Power >> 8) & 0x000000ff);
                data[tempIndex++] = (byte)((mCommand_Output_Power) & 0x000000ff);


                //9409840 0x008f9fd0
                uint mAMI_Wattage = (uint)mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh();

                tempByteWattage = new[] {
                    (byte)((mAMI_Wattage>>24) & 0xFF)
                ,   (byte)((mAMI_Wattage>>16) & 0xFF)
                ,   (byte)((mAMI_Wattage>>8) & 0xFF)
                ,   (byte)((mAMI_Wattage>>0) & 0xFF)
                };

                Buffer.BlockCopy(tempByteWattage, 0, data, tempIndex, tempByteWattage.Length);
            }
            else
            {                
                data = new byte[2 + 1 + 7 + 2 + 2];
                data[0] = 0; data[1] = 1;

                if (bPowerRelay_Plus)
                    data[3] |= 0x40;

                if (bHMI_Manual_Control)
                    data[3] |= 0x01;

                if (bPowerRelay_Minus)
                    data[3] |= 0x20;

                if (bLED1_Red)
                    data[4] |= 0x10;

                if (bLED1_Green)
                    data[4] |= 0x08;

                if (bLED1_Blue)
                    data[4] |= 0x04;

                if (bMC_On)
                    data[4] |= 0x02;

                if (bFAN_On)
                    data[4] |= 0x01;

                data[5] = 0;
                data[5] |= (byte)((mCommand_Output_Channel1 << 2) & 0b00001100);

                //data[5] = 0;
                //data[5] |= (byte)((mCommand_Output_Channel1) & 0b00000011);

                data[6] = (byte)((mCommand_Output_Voltage >> 8) & 0x000000ff);
                data[7] = (byte)((mCommand_Output_Voltage) & 0x000000ff);

                data[8] = (byte)((mCommand_Output_Current >> 8) & 0x000000ff);
                data[9] = (byte)((mCommand_Output_Current) & 0x000000ff);

                data[10] = (byte)((mAMI_Voltage >> 8) & 0x000000ff);
                data[11] = (byte)((mAMI_Voltage) & 0x000000ff);

                data[12] = (byte)((mAMI_Current >> 8) & 0x000000ff);
                data[13] = (byte)((mAMI_Current) & 0x000000ff);
            }
            return data;
        }
    }
}
