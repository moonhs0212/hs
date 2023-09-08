using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Deal_Request_Send_Request : Smartro_TL3500BS_Packet_Base_Send_Request
    {
        const int LENGTH_DATA = 30;
        public Smartro_TL3500BS_Packet_Deal_Request_Send_Request(EL_MyApplication_Base application, int channelIndex) :
            base(application, channelIndex, LENGTH_DATA, Smartro_TL3500BS_Constants.VALUE.JOB_CODE.B_DEAL_APPROVAL_REQ)
        {
            mSendData_Data[0] = (byte)'1';

            mSendData_Data[1 + 10 + 8 + 8 + 2] = (byte)'1';
        }

        protected byte mDealDivideCode = (byte)'1';
        public byte DealDivideCode
        {
            get { return mDealDivideCode; }
            set
            {
                mDealDivideCode = value;
                mSendData_Data[0] = mDealDivideCode;
            }
        }

        

        public void setDeal_Request(int approvalAmount)
        {
            ApprovalAmount = approvalAmount;
        }


        protected int mApprovalAmount = 0;
        public int ApprovalAmount
        {
            get { return mApprovalAmount; }
            set
            {
                
                int indexArray_SendData = 1;
                mApprovalAmount = value;

                string temp = "" + mApprovalAmount;

                int length = 10;
                byte[] ddd = Encoding.ASCII.GetBytes(temp);
                int indexArray = length - ddd.Length;
                int indexArray_ddd = 0;
                for (int i = 0; i < length; i++)
                {
                    if(i < indexArray)                    
                        mSendData_Data[indexArray_SendData + i] = 0x30;
                    else
                        mSendData_Data[indexArray_SendData + i] = ddd[indexArray_ddd++];
                }
            }
        }

        protected int mTex = 0;
        public int Tex
        {
            get { return mTex; }
            set
            {
                int indexArray_SendData = 1 + 10;

                mTex = value;

                string temp = "" + mTex;

                int length = 8;
                byte[] ddd = Encoding.ASCII.GetBytes(temp);
                int indexArray = length - ddd.Length;
                int indexArray_ddd = 0;
                for (int i = 0; i < length; i++)
                {
                    if (i < indexArray)
                        mSendData_Data[indexArray_SendData + i] = 0x30;
                    else
                        mSendData_Data[indexArray_SendData + i] = ddd[indexArray_ddd++];
                }
            }
        }

        protected int mServiceTex = 0;
        public int ServiceTex
        {
            get { return mServiceTex; }
            set
            {
                int indexArray_SendData = 1 + 10 + 8;

                mServiceTex = value;

                string temp = "" + mServiceTex;
                int length = 8;
                byte[] ddd = Encoding.ASCII.GetBytes(temp);
                int indexArray = length - ddd.Length;
                int indexArray_ddd = 0;
                for (int i = 0; i < length; i++)
                {
                    if (i < indexArray)
                        mSendData_Data[indexArray_SendData + i] = 0x30;
                    else
                        mSendData_Data[indexArray_SendData + i] = ddd[indexArray_ddd++];
                }
            }
        }

        protected int mInstallmentMonth = 0;
        public int InstallmentMonth
        {
            get { return mInstallmentMonth; }
            set
            {
                int indexArray_SendData = 1 + 10 + 8 + 8;

                mInstallmentMonth = value;

                string temp = "" + mInstallmentMonth;
                int length = 2;
                byte[] ddd = Encoding.ASCII.GetBytes(temp);
                int indexArray = length - ddd.Length;
                int indexArray_ddd = 0;
                for (int i = 0; i < length; i++)
                {
                    if (i < indexArray)
                        mSendData_Data[indexArray_SendData + i] = 0x30;
                    else
                        mSendData_Data[indexArray_SendData + i] = ddd[indexArray_ddd++];
                }
            }
        }


    }
}