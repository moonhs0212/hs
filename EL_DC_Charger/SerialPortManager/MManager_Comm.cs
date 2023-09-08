using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using ParkingControlCharger.baseClass;
using System;

namespace ParkingControlCharger.Object
{
    abstract public class MManager_Comm : EL_IntervalExcute_Item_Base
    {
        protected int mInterval_Send = 0;
        protected int mCount_Failed_Attempt_Comm = 0;

        protected int mSecond_CommFault_SW = 60;
        public int Second_Fault
        {
            get { return mSecond_CommFault_SW; }
            set { mSecond_CommFault_SW = value; }
        }
        
        protected EL_Time mDateTime_LastComm = new EL_Time();

        protected string mPath_Commport = "";
        protected bool bIsConnected_HW = false;
        protected bool bIsConnected_SW = false;

        protected void initVariable_OnDisconnect()
        {
            bIsConnected_HW = false;
            bIsConnected_SW = false;
            if (mTime_Disconnect == null)
                mTime_Disconnect = new EL_Time();
        }

        protected EL_Time mTime_Disconnect = null;

        public bool IsConnected_HW
        {
            get { return bIsConnected_HW; }
        }
        public bool IsConnected_SW
        {
            get { return bIsConnected_SW; }
        }

        public MManager_Comm(EL_MyApplication_Base application, int sendInterval)
            : base(application, 0, MODE_UNIT_MILISECOND, sendInterval) {
            mInterval_Send = sendInterval;
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


        protected void setLastComm() {
            mDateTime_LastComm.setTime();
            mCount_Failed_Attempt_Comm = 0;
            bIsConnected_HW = true;
            bIsConnected_SW = true;
        }

        virtual public bool isConnected_Comm() {
            if (!bIsConnected_HW || !bIsConnected_SW || mDateTime_LastComm.getMinute_WastedTime() > 0)
                return false;
            return true;
        }

        abstract public bool write(byte[] data);

        public bool isPossible_SerialPort() {
            string[] devices = System.IO.Ports.SerialPort.GetPortNames();

            if (devices == null && devices.Length < 1) return false;

            for (int i = 0; i < devices.Length; i++)
                if (devices[i].Equals(getPath_SerialPort()))
                    return true;
            return false;
        }

        public void destroy() {
            setStop();
            commClose();
        }


        abstract public void commMake();
        abstract public void commOpen();
        abstract public void commClose();
        

        protected int[] mTemp_Data = null;

        abstract public string getPath_SerialPort();

        protected bool bIsReceiveData = false;
        public bool isReceiveData() => bIsReceiveData;
    }
}
