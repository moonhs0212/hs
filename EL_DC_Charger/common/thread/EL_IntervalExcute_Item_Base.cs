using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.thread;
using ParkingControlCharger.Object;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ParkingControlCharger.baseClass
{
    abstract public class EL_IntervalExcute_Item_Base : EL_Object_Channel_Base
    {
        public const int MODE_UNIT_SECOND = 0;
        public const int MODE_UNIT_MILISECOND = 1;

        protected int mInterval = 0;
        protected int mTimeMode = 0;
        public EL_IntervalExcute_Item_Base(EL_MyApplication_Base application, int channelIndex, int mode_Unit, int interval) : base(application, channelIndex) {
            mTimeMode = mode_Unit;
            mInterval = interval;
            if (mInterval > 0) {
                bCommand_Stop = false;
            }
        }



        protected EL_IntervalExcute_Thread mThread_IntervalExcute = null;        
        public void setThread_IntervalExcute(EL_IntervalExcute_Thread thread)
        {
            mThread_IntervalExcute = thread;
        }

        protected EL_Time mTime_Excute = new EL_Time();

        public void run()
        {
            if (bCommand_Stop)
                return;

            
            if(mTimeMode == MODE_UNIT_MILISECOND)
            {                
                if (mTime_Excute.getMiliSecond_WastedTime() >= mInterval)
                {
                     intervalExcuteAsync();
                    mTime_Excute.setTime();
                }
            }
            else
            {
                if (mTime_Excute.getSecond_WastedTime() >= mInterval)
                {
                    intervalExcuteAsync();
                    mTime_Excute.setTime();
                }
            }
            
        }

        abstract public void intervalExcuteAsync();

        virtual public void setStart() {
            if (mInterval > 0)
            {
                bCommand_Stop = false;
                mTime_Excute.setTime();
            }
            
        }
        virtual public void setStop()
        {
            bCommand_Stop = true;
        }

        protected bool bCommand_Stop = false;
    }
}
