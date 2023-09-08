
using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EL_DC_Charger.ChargerInfor
{
    public class EL_Manager_ChargingWattage : EL_Object_Channel_Base
    {

        public EL_Manager_ChargingWattage(EL_MyApplication_Base application, int channelIndex)
            : base(application, channelIndex)
        {

        }

        float mWattage_Start = 0;
        float mWattage_Stop = 0;

        public float mChargingWattage = -1;

        public bool bIsChargingStart_By_WattageMeter = false;



        public bool setChargingStart()
        {
            if (!mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().isConnected())
                return false;

            if ((mApplication).getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh() <= 0)
                return false;

            bIsChargingStart_By_WattageMeter = true;

            mWattage_Stop = -1;
            mChargingWattage = -1;
            return true;
        }

        public void setChargignStop()
        {
            if (bIsChargingStart_By_WattageMeter)
            {
                bIsChargingStart_By_WattageMeter = false;
                mWattage_Stop = mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh();
                mChargingWattage = (int)(mWattage_Stop - mWattage_Start);
            }
        }

        public string getChargingWattage_Start_String()
        {
            string result = String.Format("{0:0.00}", mWattage_Start / 10000.0f);
            //result += " kW";
            return result;
        }

        public string getChargingWattage_Stop_String()
        {
            string result = String.Format("{0:0.00}", mWattage_Stop / 10000.0f);
            //result += " kW";
            return result;
        }

        public string getCurrentWattage_String()
        {
            string result = String.Format("{0:0.00}", mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh() / 10000.0f);
            //result += " kW";
            return result;
        }

        public float mChagingUnit = 189.0f;
        public void setChargingUnit(float unit)
        {
            mChagingUnit = unit;
        }
        public int getChargingCharge()
        {
            float value = (float)Math.Round((getCharging_kWh() * EL_DC_Charger_MyApplication.getInstance().CurrentAmount), 2);
            return (int)value;
        }

        public float getCharging_kWh()
        {
            if (mChargingWattage > 0)
            {
                return mChargingWattage / 10000.0f; ;
            }

            if (!bIsChargingStart_By_WattageMeter)
                return 0;

            float chargingWatt = -1;

            if (mWattage_Stop < 0)
            {
                chargingWatt = (float)(mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh() - mWattage_Start);
            }
            else
            {
                chargingWatt = (float)(mWattage_Stop - mWattage_Stop);
            }
            return chargingWatt / 10000.0f;
        }

        public float getChargingWattage()
        {
            if (mChargingWattage > 0)
            {
                return mChargingWattage;
            }

            if (!bIsChargingStart_By_WattageMeter)
                return 0;

            float chargingWatt = -1;

            if (mWattage_Stop < 0)
            {
                chargingWatt = (float)(mApplication.getChannelTotalInfor(mChannelIndex).getAMI_PacketManager().getPositive_Active_Energy_Pluswh() - mWattage_Start);
            }
            else
            {
                chargingWatt = (float)(mWattage_Stop - mWattage_Stop);
            }
            return chargingWatt;
        }

        public string getChargingWattage_String()
        {
            float wattage = getChargingWattage();
            string result = String.Format("{0:0.00}", wattage / 10000.0f);
            //result += " kW";
            return result;
        }



        public override void initVariable()
        {
            mWattage_Start = 0;
            mWattage_Stop = 0;

            mChargingWattage = -1;

            bIsChargingStart_By_WattageMeter = false;
        }
    }
}
