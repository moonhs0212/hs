
using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.Manager;
using EL_DC_Charger.Queue;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Text;
using EL_DC_Charger.common;

namespace EL_DC_Charger.Controller
{
    abstract public class Controller_Base : EL_Object_Channel_Base
    {
        

        //protected bool bIsCommand_Certification = false;

        protected bool bIsNormalState = false;
        public bool isNormalState()
        {
            return bIsNormalState;
        }


        protected bool bController_Start = false;
        public void setController_Start(bool start)
        {
            bController_Start = start;
        }
        public bool getController_Start()
        {
            return bController_Start;
        }



        protected int mMode = 0;


        protected int mTemp_Event = 0;
        protected EventQueue mQueue = new EventQueue(0);


        public void addEvent(int eventNum)
        {
            mQueue.insert(eventNum);
        }

        public Controller_Base(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
            for (int i = 0; i < mTimes.Length; i++)
                mTimes[i] = new EL_Time();
        }

        protected EL_Time mTime_Mode = new EL_Time();

        virtual public void setMode(int mode)
        {
            mMode = mode;
            mTime_Mode.setTime();
            for (int i = 0; i < mTimes.Length; i++)
            {
                mTimes[i].setTime();
            }
        }

        public void setTime_NextTime(int sec)
        {
            mTime_NextStep_Sec = sec;
        }

        override public void initVariable()
        {
            setMode(0);
        }


        abstract public void process();


        protected bool isTimer(int timerIndex)
        {
            switch (timerIndex)
            {
                case TIMER_NEXTSTEP:
                    if (mTimes[timerIndex].getSecond_WastedTime() >= mTime_NextStep_Sec)
                    {
                        mTimes[timerIndex].setTime();
                        return true;
                    }
                    break;
                default:
                    if (mTimes[timerIndex].getMiliSecond_WastedTime() >= TIME_SEC[timerIndex])
                    {
                        mTimes[timerIndex].setTime();
                        return true;
                    }
                    break;
            }
            return false;
        }

        protected int mTime_NextStep_Sec = 0;

        protected static int[] TIME_SEC = {0, 100, 200, 300, 500,
            1000, 3000, 5000, 10000, 7000,
            15000
        };

        protected EL_Time[] mTimes = new EL_Time[TIME_SEC.Length];
        


        protected const int TIMER_NEXTSTEP = 0;
        protected const int TIMER_100MS = 1;
        protected const int TIMER_200MS = 2;
        protected const int TIMER_300MS = 3;
        protected const int TIMER_500MS = 4;
        protected const int TIMER_1S = 5;
        protected const int TIMER_3S = 6;
        protected const int TIMER_5S = 7;
        protected const int TIMER_10S = 8;
        protected const int TIMER_7S = 9;

        protected const int TIMER_15S = 10;


    }
}
