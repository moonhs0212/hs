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
    public class Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request : Smartro_TL3500BS_Packet_Base_Receive_By_Request
    {
        public Smartro_TL3500BS_Packet_Card_Check_Receive_By_Request(EL_MyApplication_Base application, int channelIndex, byte[] receiveData) : base(application, channelIndex, receiveData)
        {
        }

        protected byte mDeal_Media = 0;
        public byte Deal_Media
        {
            get { return mDeal_Media; }
            set { mDeal_Media = value; }
        }
        //CARD_VARIABLE
        protected byte mCard_Variable = 0;
        public byte Card_Variable
        {
            get { return mCard_Variable; }
            set { mCard_Variable = value; }
        }

        protected string mCard_Number = "";
        public string Card_Number
        {
            get { return mCard_Number; }
            set { mCard_Number = value; }
        }



        public override void receive_applySystem()
        {
            base.receive_applySystem();

            if (mReceiveData_Data == null) return;

            int indexArray = 0;
            Deal_Media = mReceiveData_Data[indexArray++];

            Card_Variable = mReceiveData_Data[indexArray++];

            byte[] temp = new byte[20];
            for(int i = 0; i < temp.Length; i++)
            {
                temp[i] = mReceiveData_Data[indexArray++];
            }
            Card_Number = EL_Manager_Conversion.asciiByteArrayToString(temp);

            if(Card_Number.Length > 16)
            {
                Card_Number = Card_Number.Substring(Card_Number.Length - 16);
            }
        }
    }
}
