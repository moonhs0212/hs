using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ChargerInfor
{
    abstract public class EL_AMI_PacketManager_Base : EL_CommPort_PacketManager_Base
    {
        protected EL_AMI_PacketManager_Base(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {
        }

        protected long mPositive_Active_Energy_Pluswh = 0;
        public long getPositive_Active_Energy_Pluswh() { return mPositive_Active_Energy_Pluswh; }
        public void setPositive_Active_Energy_Pluswh(long plusWh) { mPositive_Active_Energy_Pluswh = plusWh; }

        protected float[] mVoltage_Phase = new float[] { 0, 0, 0 };
        protected float[] mCurrent_Phase = new float[] { 0, 0, 0 };
        public float getVoltage_Phase(int phase) { return mVoltage_Phase[phase]; }
        public float getCurrent_Phase(int phase) { return mCurrent_Phase[phase]; }

        public void setVoltage_Phase(int phase, float value) { mVoltage_Phase[phase] = value; }
        public void setCurrent_Phase(int phase, float value) { mCurrent_Phase[phase] = value; }

        protected float mVoltage = 0.0f;
        protected float mCurrent = 0.0f;
        public void setVoltage(float value) { mVoltage = value; }
        public void setCurrent(float value) { mCurrent = value; }

        public float getVoltage() { return mVoltage; }
        public float getCurrent() { return mCurrent; }

        protected EL_Time mTime_OverVoltage = new EL_Time();
        protected EL_Time mTime_LowVoltage = new EL_Time();
        protected EL_Time mTime_OverCurrent = new EL_Time();

        protected bool bIsOccured_OverVoltage = false;
        protected bool bIsOccured_LowVoltage = false;
        protected bool bIsOccured_OverCurrent = false;

        public bool isOccured_OverVoltage()
        {
            if (bIsOccured_OverVoltage)
            {
                if (mTime_OverVoltage.getSecond_WastedTime() >= 5)
                    return true;
            }
            return false;
        }

        public bool isOccured_LowVoltage()
        {
            if (bIsOccured_LowVoltage)
            {
                if (mTime_LowVoltage.getSecond_WastedTime() >= 10)
                    return true;
            }
            return false;
        }

        public bool isOccured_OverCurrent()
        {
            if (bIsOccured_OverCurrent)
            {
                if (mTime_OverCurrent.getSecond_WastedTime() >= 3)
                    return true;
            }
            return false;
        }

        private bool isOverVoltage()
        {
            if (mVoltage == 0) return false;

            if (mVoltage >= 220 * 1.1f) return true;

            return false;
        }

        private bool isLowVoltage()
        {
            if (mVoltage < 10) return false;

            if (mVoltage <= 220 * 0.9f) return true;

            return false;
        }

        private bool isOverCurrent()
        {
            if (mCurrent <= 1) return false;

            if (mCurrent >= 32 * 1.1f) return true;

            return false;
        }
    }
}
