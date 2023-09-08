using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.ChargerInfor
{
    public class EL_Manager_ChargeUnit : EL_Object_Channel_Base
    {
        public EL_Manager_ChargeUnit(EL_ChannelTotalInfor_Base infor, string memberType_String) : this(infor)
        {

            mMemberType_String = memberType_String;
        }
        private EL_Manager_ChargeUnit(EL_ChannelTotalInfor_Base infor)
     : base(infor.getApplication(), infor.getChannelIndex())
        {
            mChannelTotalInfor = infor;
        }
        public void setMemberType(String memberType)
        {
            mMemberType_String = memberType;
        }

        public void setStartDate(String startDate)
        {
            mStartDate = startDate;
        }

        public void setChargeUnit(List<Double> chargeUnits)
        {
            isSettingValue = true;
            for (int i = 0; i < chargeUnits.Count(); i++)
                mChargingUnit[i] = chargeUnits[i];
        }

        public void setChargeUnit(String operatorType)
        {
            String[] data = mApplication.getManager_SQLite_Setting().getManager_Table_ChargeUnit().getChargeUnit(mChannelIndex, operatorType);
            if (data != null)
            {
                for (int i = 4; i < data.Length; i++)
                {
                    setChargeUnit(i - 4, EL_Manager_Conversion.getDouble(data[i]));
                }
            }

        }

        public void setChargeUnit(int hour, double chargeUnit)
        {
            mChargingUnit[hour] = chargeUnit;
        }



        public String mMemberType_String = "";
        public String mStartDate = "";

        public double getExpectChargingWattage_By_PaymentCharge(int charge, int hour)
        {
            double unit = mChargingUnit[hour] + mChargingUnit[hour];
            if (unit <= 0)
                return 0;

            double result = (charge / (unit)) * 1000.0f;
            return result;
        }

        protected double[] mChargingUnit = new double[24];



        public byte[] getChargingUnit_4Byte_Multiply_100(int hour)
        {
            long watt = (long)(mChargingUnit[hour] * 100);

            byte[] convertBytes = new byte[4];
            convertBytes[0] = (byte)((watt >> 24) & 0x000000ff);
            convertBytes[1] = (byte)((watt >> 16) & 0x000000ff);
            convertBytes[2] = (byte)((watt >> 8) & 0x000000ff);
            convertBytes[3] = (byte)((watt) & 0x000000ff);
            return convertBytes;
        }

        protected long mChargeWattage1 = 0;
        protected long mChargeWattage2 = 0;
        protected long mChargeWattage3 = 0;
        BigInteger mChargeAmount1 = new BigInteger(0);
        BigInteger mChargeAmount2 = new BigInteger(0);
        BigInteger mChargeAmount3 = new BigInteger(0);
        BigInteger mChagerTotalAmount = new BigInteger(0);


        public void setChargeTotalAmount(BigInteger amount)
        {

            mChagerTotalAmount = amount;
        }

        public bool isIncludeZero_ChargeUnit()
        {
            for (int i = 0; i < 24; i++)
            {
                if (mChargingUnit[i] <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool isIncludeZero_ServiceUnit()
        {
            for (int i = 0; i < 24; i++)
            {
                if (mChargingUnit[i] <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void setChargingUnit(int member, double chargingUnit)
        {
            for (int i = 0; i < mChargingUnit.Length; i++)
            {
                setChargingUnit(member, i, chargingUnit);
            }
        }

        public byte[] getChargingUnit_4Byte_10Won(int hour)
        {
            long watt = (long)mChargingUnit[hour];

            byte[] convertBytes = new byte[4];
            convertBytes[0] = (byte)((watt >> 24) & 0x000000ff);
            convertBytes[1] = (byte)((watt >> 16) & 0x000000ff);
            convertBytes[2] = (byte)((watt >> 8) & 0x000000ff);
            convertBytes[3] = (byte)((watt) & 0x000000ff);
            return convertBytes;
        }


        public void setChargingUnit(int member, int indexArray, double chargingUnit)
        {
            mChargingUnit[indexArray] = chargingUnit;
        }



        public double getChargingUnit_Current()
        {

            double temp = mChargingUnit[EL_Time.GetCurrentHour()];
            return temp;
        }

        public double getChargingUnit(int indexArray)
        {
            double temp = mChargingUnit[indexArray];
            return temp;
        }


        //    public void setServiceUnit(int member, double chargingUnit)
        //    {
        //        for (int i = 0; i < mChargingUnit.length; i++)
        //        {
        //            setServiceUnit(member, i, chargingUnit);
        //        }
        //    }

        //    public void setServiceUnit(int member, int indexArray, double chargingUnit)
        //    {
        //        mChargingUnit[indexArray] = chargingUnit;
        //    }
        //
        //    public double getServiceUnit(int indexArray)
        //    {
        //        double temp = mChargingUnit[indexArray];
        //        return temp;
        //    }
        protected bool isSettingValue = false;
        public bool IsSettingValue
        {
            get { return isSettingValue; }
        }

        override public void initVariable()
        {
            isSettingValue = false;
        }
    }
}
