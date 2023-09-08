
using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.Manager;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.common;

namespace EL_DC_Charger.ChargerInfor
{
    abstract public class EL_CommPort_PacketManager_Base : EL_Object_Channel_Base
    {

        public EL_CommPort_PacketManager_Base(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {
            
        }

        protected bool bIsInitComplete_a1_Receive = false;

        public bool IsInitComplete_a1_Receive
        {
            get { return bIsInitComplete_a1_Receive; }
            set
            {


                bIsInitComplete_a1_Receive = value;
            }
        }

        protected bool bIsInitComplete_a1_Send = false;

        public bool IsInitComplete_a1_Send
        {
            get { return bIsInitComplete_a1_Send; }
            set
            { bIsInitComplete_a1_Send = value; }
        }


        protected bool bIsReceiveData = false;
        protected EL_Time mTime_Connected = new EL_Time();

        public void setDisconnect()
        {
            bIsReceiveData = false;
        }
        public void setTime_Receive()
        {
            bIsReceiveData = true;
            mTime_Connected.setTime();
        }
        public bool isConnected()
        {
            if (!bIsReceiveData)
                return false;

            if (mTime_Connected.getSecond_WastedTime() < 5)                                 
                return true;

            return false;
        }

        protected int mCount_Failed_Attempt_Comm = 0;
        protected DateTime mDateTime_LastComm = DateTime.Now;

        protected bool bIsConnected = false;
        public bool IsConnected
        {
            get { return bIsConnected; }
        }
        public void setLastComm()
        {
            mDateTime_LastComm = DateTime.Now;
            mCount_Failed_Attempt_Comm = 0;
            bIsConnected = true;
        }

        abstract public bool isConnected_Comm();

        public override void initVariable()
        {
            
        }
    }
}
