using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Deal_Cancel_Send_Request : Smartro_TL3500BS_Packet_Base_Send_Request
    {
        const int LENGTH_DATA = 57;
        public Smartro_TL3500BS_Packet_Deal_Cancel_Send_Request(EL_MyApplication_Base application, int channelIndex) :
            base(application, channelIndex, LENGTH_DATA, Smartro_TL3500BS_Constants.VALUE.JOB_CODE.C_DEAL_CANCEL_REQ)
        {

        }

        //public void setDealCancel_Before()
        //{
        //    send_setSendData_Data_Length(57);
        //    mSendData_Data[0] = (byte)'2';
        //}

        public void setDealCancel(Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request packet_Deal_Response)
        {
            send_setSendData_Data_Length(57 + 32);
            int indexArray = 0;
            byte[] receiveData_Data = packet_Deal_Response.getReceiveData_Data();

            mSendData_Data[indexArray++] = (byte)'4';

            int indexArray_Receive = 0;
            mSendData_Data[indexArray++] = (byte)'2';


            indexArray_Receive = 1 + 1 + 20;
            int length = 10 + 8 + 8 + 2;
            for (int i = 0; i < length; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_Receive++];

            //서명여부
            mSendData_Data[indexArray++] = (byte)'1';

            //승인번호
            indexArray_Receive = 1 + 1 + 20 + 10 + 8 + 8 + 2;
            length = 12 + 8 + 6;
            for (int i = 0; i < length; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_Receive++];

            //부가정보 길이
            mSendData_Data[indexArray++] = (byte)'3';
            mSendData_Data[indexArray++] = (byte)'0';

            indexArray_Receive = 1 + 1 + 20 + 10 + 8 + 8 + 2 + 12 + 8 + 6 + 12 + 15 + 14 + 20 + 20;

            for (int i = 0; i < 30; i++)
            {
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_Receive++];
            }
            mSendData_Data_String = EL_Manager_Conversion.asciiByteArrayToString(mSendData_Data);
            Console.Out.WriteLine("Deal VAN Cancel Data = " + mSendData_Data_String);

        }


        public void setDealCancel(int amount_Approval_Cancel, Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request packet_Deal_Response)
        {
            send_setSendData_Data_Length(57 + 32);
            int indexArray = 0;


            if (packet_Deal_Response == null && EL_DC_Charger_MyApplication.getInstance().getChannelTotalInfor(1).mStateManager_Channel.bIsRebootChargeYN)
                packet_Deal_Response = mApplication.SerialPort_Smartro_CardReader.getManager_Send().PacketManager.regenPacket_AddInfor_Deal_Request_Receive();

            byte[] receiveData_Data;
            if (EL_DC_Charger_MyApplication.getInstance().lastDealInfo != null)
            {
                receiveData_Data = EL_DC_Charger_MyApplication.getInstance().lastDealInfo;
                EL_DC_Charger_MyApplication.getInstance().lastDealInfo = null;
            }
            else
                receiveData_Data = packet_Deal_Response.getReceiveData_Data();

            mSendData_Data[indexArray++] = (byte)'5';

            mSendData_Data[indexArray++] = (byte)'2';


            Console.Out.WriteLine("Deal Part Cancel 금액 = " + amount_Approval_Cancel);

            //승인금액
            string temp = "" + amount_Approval_Cancel;

            int length = 10;
            byte[] ddd = Encoding.ASCII.GetBytes(temp);
            int indexArray_Temp = length - ddd.Length;
            int indexArray_ddd = 0;
            for (int i = 0; i < length; i++)
            {
                if (i < indexArray_Temp)
                    mSendData_Data[indexArray++] = 0x30;
                else
                    mSendData_Data[indexArray++] = ddd[indexArray_ddd++];
            }

            int indexArray_ReceiveData = 1 + 1 + 20 + 10;

            //세금
            for (int i = 0; i < 8; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_ReceiveData++];

            //봉사료
            for (int i = 0; i < 8; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_ReceiveData++];

            //할부개월
            for (int i = 0; i < 2; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_ReceiveData++];

            //서명여부
            mSendData_Data[indexArray++] = (byte)'1';

            //승인번호
            for (int i = 0; i < 12; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_ReceiveData++];

            //원거래일자
            for (int i = 0; i < 8; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_ReceiveData++];

            //원거래시간
            for (int i = 0; i < 6; i++)
                mSendData_Data[indexArray++] = receiveData_Data[indexArray_ReceiveData++];

            //부가정보 길이
            mSendData_Data[indexArray++] = (byte)'3';
            mSendData_Data[indexArray++] = (byte)'0';

            int countTemp = indexArray_ReceiveData + 2;

            for (int i = 0; i < 30; i++)
            {
                mSendData_Data[indexArray++] = receiveData_Data[157 + i];
            }

            mSendData_Data_String = EL_Manager_Conversion.asciiByteArrayToString(mSendData_Data);

        }

        string mSendData_Data_String = "";

    }
}
