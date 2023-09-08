
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
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;

namespace EL_DC_Charger.ChargerInfor
{
    public class EL_ChannelInfor_ChargingTime : EL_Object_Channel_Base, IObserver_ChargingState
    {

        protected long mWastedSecond = 0;

        protected EL_Time mTime_Start = new EL_Time();
        protected EL_Time mTime_Stop = new EL_Time();

        public String mTimeText = "";


        public void setWastedSecond(long second)
        {
            mWastedSecond = second;
        }
        public String getChargingTime()
        {
            String timeText = "";
            if (mTimeText != null && mTimeText.Length > 6)
            {
                return mTimeText;
            }

            timeText = EL_Time.getTime_HMS_6String_includeDivider((int)getSecond_WastedTime(), ":");

            return timeText;
        }

        public int getMinute_WastedTime()
        {
            int minute = (int)(getSecond_WastedTime() / 60);
            return minute;
        }

        public long getSecond_WastedTime()
        {
            if (mTimeText != null && mTimeText.Length > 6)
            {
                return mWastedSecond;
            }

            long second = mWastedSecond;
            if (mTime_Start != null)
                second += (long)mTime_Start.getSecond_WastedTime();
            return second;
        }

        public EL_ChannelInfor_ChargingTime(EL_ChannelTotalInfor_Base channelTotalInfor)
            : base(channelTotalInfor)
        {

        }


        override public void initVariable()
        {

        }


        public void onChargingStart()
        {
            if (mTime_Start == null)
            {
                mTime_Start = new EL_Time();
            }
            mWastedSecond = 0;
            mTime_Start.setTime();
            mTimeText = "";
        }

        public void onChargingRestart()
        {
            if (mTime_Start == null)
            {
                mTime_Start = new EL_Time();
            }
            mTime_Stop = null;
            mTime_Start.setTime();
            mTimeText = "";
        }


        public void onChargingPause()
        {
            if (mTime_Start == null)
                return;
            mWastedSecond = (long)(mWastedSecond + mTime_Start.getSecond_WastedTime());
            mTime_Start = null;
            mTimeText = EL_Time.getTime_HMS_6String_includeDivider(mWastedSecond, ":");
        }

        public void onChargingStop()
        {
            if (mTime_Start == null)
                return;
            mWastedSecond = (long)(mWastedSecond + mTime_Start.getSecond_WastedTime());
            mTime_Start = null;
            mTimeText = EL_Time.getTime_HMS_6String_includeDivider(mWastedSecond, ":");
        }
    }

}
