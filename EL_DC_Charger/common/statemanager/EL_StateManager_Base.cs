using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using ParkingControlCharger.baseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.statemanager
{
    abstract public class EL_StateManager_Base : EL_IntervalExcute_Item_Base
    {
        public EL_StateManager_Base(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex, EL_IntervalExcute_Item_Base.MODE_UNIT_MILISECOND, 10)
        {


            //        mQueue = new EL_EventQueue();

            for (int i = 0; i < mTimers.Length; i++)
            {
                mTimers[i] = new EL_Time();
            }
        }

        protected int mState = 0;

        virtual protected bool isNeedExcute()
        {
            if (mApplication.getAdminMode() == EAdminMode.NONE)
                return true;

            return false;
        }

        public int getState() { return mState; }

        public bool isState_Same(int state)
        {
            if (mState / 100 == state / 100)
            {
                return true;
            }

            return false;
        }        

        virtual public void setState(int state)
        {
            //        Manager_Log.printLog("gntel_controller", "ChannelIndex="+mChannelIndex+", Controller Mode Change "+mMode+" to " + mode);
            if (mState == state)
                return;

            //        Logger.d(this.GetType().Name+"["+mChannelIndex+"] :: " + mState + ">>>" + state);

            mState = state;
            mTime_SetState.setTime();
            mCount_Timer = 0;
            initTimer();

            //        mApplication.setChangeMode();
        }

        public EL_Time getTime_SetState()
        {
            return mTime_SetState;
        }
        protected EL_Time mTime_SetState = new EL_Time();

        protected int mCount_Timer = 0;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////                                                                                  TIMER
        //////////////////////////////////////////////
        public const int TIMER_800_UIUPDATE = 0;
        public const int TIMER_1SEC = 1;
        public const int TIMER_2SEC = 2;
        public const int TIMER_3SEC = 3;
        public const int TIMER_10SEC = 4;
        public const int TIMER_60SEC = 5;
        public const int TIMER_90SEC = 6;
        public const int TIMER_WAITTIME = 7;
        public const int TIMER_120SEC = 8;
        public const int TIMER_SEND_TO_SERVER = 9;
        public const int TIMER_MeterValueSampleInterval = 10;


        public EL_Time[] mTimers = new EL_Time[20];
        public int[] mTimers_Count = new int[20];

        protected bool isTimer_MiliSec(int indexArray, int miliSecond)
        {
            if (mTimers[indexArray].getMiliSecond_WastedTime() > miliSecond)
            {
                mTimers[indexArray].setTime();
                mTimers_Count[indexArray]++;
                return true;
            }

            return false;
        }

        protected void initTimer(int indexArray)
        {
            mTimers[indexArray].setTime();
            mTimers_Count[indexArray] = 0;
        }

        protected void initTimer()
        {            
            for (int i = 0; i < mTimers.Length; i++)
            {
                mTimers[i].setTime();
            }
        }

        protected void setTimer_Sec(int indexArray)
        {
            mTimers[indexArray].setTime();
            mTimers_Count[indexArray]++;
        }

        protected bool isTimer_Sec(int indexArray, int second)
        {
            if (mTimers[indexArray].getSecond_WastedTime() > second)
            {
                setTimer_Sec(indexArray);
                return true;
            }


            return false;
        }

        //    protected int queueInfor = -1;
        //
        //
        //    protected EL_EventQueue mQueue = null;
        //    public EL_EventQueue getEventQueue(){	return mQueue;		}
        //    public void addEvent(int event) {
        //        // TODO Auto-generated method stub
        //
        //        try
        //        {
        //            synchronized (mQueue) {
        //                mQueue.insert(event);
        //            }
        //        }catch(Exception e)
        //        {
        //            e.printStackTrace();
        //        }
        //    }
    }
}
