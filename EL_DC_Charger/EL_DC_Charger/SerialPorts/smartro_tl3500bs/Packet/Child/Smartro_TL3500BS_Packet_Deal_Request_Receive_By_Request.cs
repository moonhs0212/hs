using EL_DC_Charger.common.application;
using EL_DC_Charger.common.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request : Smartro_TL3500BS_Packet_Base_Receive_By_Request
    {
        public Smartro_TL3500BS_Packet_Deal_Request_Receive_By_Request(EL_MyApplication_Base application, int channelIndex, byte[] receiveData)
            : base(application, channelIndex, receiveData)
        {
        }





        protected byte mDeal_DivideCode = 0;
        public byte Deal_DivideCode
        {
            get { return mDeal_DivideCode; }
            set { mDeal_DivideCode = value; }
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

        protected byte[] mCompany_Issue_Code = new byte[4];
        protected String mCompany_Issue = "";
        public String Company_Issue
        {
            get { return mCompany_Issue; }
            set { mCompany_Issue = value; }
        }

        protected byte[] mCompany_Purchase_Code = new byte[4];
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
            Selling_Time = EL_Manager_Conversion.asciiByteArrayToString(temp);

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

            if (Deal_DivideCode == 'X')
            {
                for (int i = 0; i < mCompany_Issue_Code.Length; i++)
                {
                    mCompany_Issue_Code[i] = mReceiveData_Data[indexArray++];
                }

                temp = new byte[16];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = mReceiveData_Data[indexArray++];
                }
                Company_Issue = EL_Manager_Conversion.asciiByteArrayToString(temp);

                for (int i = 0; i < mCompany_Purchase_Code.Length; i++)
                {
                    mCompany_Purchase_Code[i] = mReceiveData_Data[indexArray++];
                }

                temp = new byte[16];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = mReceiveData_Data[indexArray++];
                }
                Company_Purchase = EL_Manager_Conversion.asciiByteArrayToString(temp);
            }
            else
            {
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

            



        }
    }
}
