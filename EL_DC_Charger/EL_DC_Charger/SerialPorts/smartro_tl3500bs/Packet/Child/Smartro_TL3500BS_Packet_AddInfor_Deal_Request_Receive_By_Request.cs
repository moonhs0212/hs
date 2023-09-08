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
    public class Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request : Smartro_TL3500BS_Packet_Base_Receive_By_Request
    {
        public Smartro_TL3500BS_Packet_AddInfor_Deal_Request_Receive_By_Request(EL_MyApplication_Base application, int channelIndex, byte[] receiveData)
            : base(application, channelIndex, receiveData)
        {
        }

        protected byte mDeal_DivideCode = 0;
        public byte Deal_DivideCode
        {
            get { return mDeal_DivideCode; }
            set
            {
                mDeal_DivideCode = value;
                switch (Deal_DivideCode)
                {
                    case (byte)'1':
                        Deal_DivideCode_String = "신용승인";
                        break;
                    case (byte)'2':
                        Deal_DivideCode_String = "현금영수증";
                        break;
                    case (byte)'3':
                        Deal_DivideCode_String = "선불카드";
                        break;
                    case (byte)'4':
                        Deal_DivideCode_String = "제로페이";
                        break;
                    case (byte)'5':
                        Deal_DivideCode_String = "카카오페이(머니)";
                        break;
                    case (byte)'6':
                        Deal_DivideCode_String = "카카오페이(신용)";
                        break;
                    case (byte)'X':
                        Deal_DivideCode_String = "거래거절";
                        break;
                }
            }
        }

        protected string mDeal_DivideCode_String = "";
        public string Deal_DivideCode_String
        {
            get { return mDeal_DivideCode_String; }
            set { mDeal_DivideCode_String = value; }
        }

        protected byte mDealMedia = 0;
        public byte DealMedia
        {
            get { return mDealMedia; }
            set { mDealMedia = value; }
        }

        protected string mCard_Number = "";
        public string Card_Number
        {
            get { return mCard_Number; }
            set { mCard_Number = value; }
        }


        protected int mAmount_Approval = 0;
        public int Amount_Approval
        {
            get { return mAmount_Approval; }
            set { mAmount_Approval = value; }
        }


        protected int mAmount_Tax = 0;
        public int Amount_Tax
        {
            get { return mAmount_Tax; }
            set { mAmount_Tax = value; }
        }

        protected int mAmount_Service = 0;
        public int Amount_Service
        {
            get { return mAmount_Service; }
            set { mAmount_Service = value; }
        }

        protected int mMonthCount_Allotment = 0;
        public int MonthCount_Allotment
        {
            get { return mMonthCount_Allotment; }
            set { mMonthCount_Allotment = value; }
        }

        protected String mApproval_Number = "";
        public String Approval_Number
        {
            get { return mApproval_Number; }
            set { mApproval_Number = value; }
        }

        protected String mSelling_Date = "";
        public String Selling_Date
        {
            get { return mSelling_Date; }
            set { mSelling_Date = value; }
        }

        protected String mSelling_Time = "";
        public String Selling_Time
        {
            get { return mSelling_Time; }
            set { mSelling_Time = value; }
        }

        protected String mDealUniqNumber = "";
        public String DealUniqNumber
        {
            get { return mDealUniqNumber; }
            set { mDealUniqNumber = value; }
        }

        protected String mMemberPointNumber = "";
        public String MemberPointNumber
        {
            get { return mMemberPointNumber; }
            set { mMemberPointNumber = value; }
        }

        protected String mDeviceNumber = "";
        public String DeviceNumber
        {
            get { return mDeviceNumber; }
            set { mDeviceNumber = value; }
        }

        protected String mCompany_Issue = "";
        public String Company_Issue
        {
            get { return mCompany_Issue; }
            set { mCompany_Issue = value; }
        }

        protected String mCompany_Purchase = "";
        public String Company_Purchase
        {
            get { return mCompany_Purchase; }
            set { mCompany_Purchase = value; }
        }

        protected String mResponseCode = "";
        public String ResponseCode
        {
            get { return mResponseCode; }
            set { mResponseCode = value; }
        }

        protected String mResponseMessage_RejectionDeal = "";
        public String ResponseMessage_RejectionDeal
        {
            get { return mResponseMessage_RejectionDeal; }
            set { mResponseMessage_RejectionDeal = value; }
        }

        protected byte[] mPG_Deal_Approval_Request_ResponsePacket = new byte[30];
        public byte[] PG_Deal_Approval_Request_ResponsePacket
        {
            get { return mPG_Deal_Approval_Request_ResponsePacket; }
            set { mPG_Deal_Approval_Request_ResponsePacket = value; }
        }

        protected string mPG_Deal_Approval_Request_ResponsePacket_String;
        public string PG_Deal_Approval_Request_ResponsePacket_String
        {
            get { return mPG_Deal_Approval_Request_ResponsePacket_String; }
        }





        public override void receive_applySystem()
        {
            base.receive_applySystem();

            if (mReceiveData_Data == null) return;

            int indexArray = 0;
            Deal_DivideCode = mReceiveData_Data[indexArray++];



            mDealMedia = mReceiveData_Data[indexArray++];

            byte[] temp = new byte[20];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            Card_Number = EL_Manager_Conversion.asciiByteArrayToString(temp);


            temp = new byte[10];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            string tempString = EL_Manager_Conversion.asciiByteArrayToString(temp);
            Amount_Approval = Int32.Parse(tempString);


            temp = new byte[8];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            tempString = EL_Manager_Conversion.asciiByteArrayToString(temp);
            Amount_Tax = Int32.Parse(tempString);


            temp = new byte[8];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            tempString = EL_Manager_Conversion.asciiByteArrayToString(temp);
            Amount_Service = Int32.Parse(tempString);


            temp = new byte[2];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            tempString = EL_Manager_Conversion.asciiByteArrayToString(temp);
            MonthCount_Allotment = Int32.Parse(tempString);

            temp = new byte[12];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            Approval_Number = EL_Manager_Conversion.asciiByteArrayToString(temp);


            temp = new byte[8];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            Selling_Date = EL_Manager_Conversion.asciiByteArrayToString(temp);

            temp = new byte[6];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            Selling_Date = EL_Manager_Conversion.asciiByteArrayToString(temp);

            temp = new byte[12];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            DealUniqNumber = EL_Manager_Conversion.asciiByteArrayToString(temp);

            temp = new byte[15];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            MemberPointNumber = EL_Manager_Conversion.asciiByteArrayToString(temp);

            temp = new byte[14];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            DeviceNumber = EL_Manager_Conversion.asciiByteArrayToString(temp);

            
            if (Deal_DivideCode != 'X')
            {
                //거래정상
                temp = new byte[20];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = mReceiveData_Data[indexArray++];
                }
                Company_Issue = EL_Manager_Conversion.asciiByteArrayToString(temp);

                temp = new byte[20];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = mReceiveData_Data[indexArray++];
                }
                Company_Purchase = EL_Manager_Conversion.asciiByteArrayToString(temp);
            }
            else
            {
                //거래비정상
                indexArray++;

                temp = new byte[2];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = mReceiveData_Data[indexArray++];
                }
                ResponseCode = EL_Manager_Conversion.asciiByteArrayToString(temp);


                temp = new byte[37];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = mReceiveData_Data[indexArray++];
                }
                ResponseMessage_RejectionDeal = EL_Manager_Conversion.asciiByteArrayToString(temp);
            }


            for (int i = 0; i < 30; i++)
            {
                PG_Deal_Approval_Request_ResponsePacket[i] = mReceiveData_Data[indexArray++];
            }

            mPG_Deal_Approval_Request_ResponsePacket_String = EL_Manager_Conversion.asciiByteArrayToString(temp);
        }
    }
}
