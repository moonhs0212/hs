using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs.Packet.Child
{
    public class Smartro_TL3500BS_Packet_Event_Receive_Only : Smartro_TL3500BS_Packet_Base_Receive
    {
        public Smartro_TL3500BS_Packet_Event_Receive_Only(EL_MyApplication_Base application, int channelIndex, byte[] receiveData) 
            : base(application, channelIndex, receiveData)
        {
        }

        protected byte mEvent;
        public byte Event
        {
            get { return mEvent; }
            set {
                mEvent = value; 
                switch(mEvent)
                {
                    case (byte)'M':
                        EventString = "MS 카드 인식";
                        break;
                    case (byte)'R':
                        EventString = "RF 카드 인식";
                        break;
                    case (byte)'I':
                        EventString = "IC 카드 인식";
                        break;
                    case (byte)'O':
                        EventString = "IC 카드 제거";
                        break;
                    case (byte)'F':
                        EventString = "IC 카드 FallBack 시";
                        break;
                    case (byte)'Q':
                        EventString = "QR 인식";
                        break;
                }
            }
        }

        protected string mEventString = "";
        public string EventString
        {
            get { return mEventString; }
            set { mEventString = value; }
        }

        public override void receive_applySystem()
        {
            base.receive_applySystem();

            if (mReceiveData_Data == null) return;

            Event = mReceiveData_Data[0];
        }
    }
}
