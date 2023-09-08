using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.Manager;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace EL_DC_Charger.common.ChargerInfor
{
    public class EL_ChannelInfor_ChargingCharge : EL_ChannelInfor_Base, IChargeUnit_ChangeListener
    {
        protected EL_Manager_ChargeUnit mChargeUnit_Member = null;
        public EL_Manager_ChargeUnit getChargeUnit_Member()
        {
            return mChargeUnit_Member;
        }
        protected EL_Manager_ChargeUnit mChargeUnit_Nonmember = null;
        public EL_Manager_ChargeUnit getChargeUnit_Nonmember()
        {
            return mChargeUnit_Nonmember;
        }

        protected EL_Manager_ChargeUnit mChargeUnit_Process = null;
        public EL_Manager_ChargeUnit getChargeUnit_Process()
        {
            return mChargeUnit_Process;
        }
        public EL_ChannelInfor_ChargingCharge(EL_ChannelTotalInfor_Base channelTotalInfor) : base(channelTotalInfor)
        {
        }


        protected String mOperatorType = "";
        public String getOperatorType()
        {
            return mOperatorType;;
        }


        override public void initVariable()
        {
            mChannelTotalInfor = mApplication.getChannelTotalInfor(mChannelIndex);
            mChargeUnit_Member = new EL_Manager_ChargeUnit(mChannelTotalInfor, OperatorType.ER.ToString());
            mChargeUnit_Nonmember = new EL_Manager_ChargeUnit(mChannelTotalInfor, OperatorType.NM.ToString());

            mChargeUnit_Process = new EL_Manager_ChargeUnit(mChannelTotalInfor, "");
        }
        public void onChange_ChargeUnit(int connectorId, string operatorType, List<UnitPriceTable> unitPriceTable)
        {
            setCurrentChargeUnit(operatorType, unitPriceTable);
        }
        protected long mChargedWattage = 0;
        public void setChargedWattage(long wattage)
        {
            mChargedWattage = wattage;
        }
        protected int mChargedCharge = 0;
        public void setChargedCharge(int charge)
        {
            mChargedCharge = charge;
        }
        protected int mCurrentHour = -1;
        protected double mCurrentWattage = 0;
        protected List<double[]> mArray = new List<double[]>();
        public void setCurrentWattage(double wattage)
        {
            // TODO Auto-generated method stub
            if (wattage == 0)
                return;

            lock (mArray)
            {
                int hour = EL_Time.GetCurrentHour();
                if (hour != mCurrentHour)
                {
                    mCurrentHour = hour;
                    mArray.Add(new double[] { mCurrentHour, wattage, mChargeUnit_Process.getChargingUnit(mCurrentHour) });
                }

                mCurrentWattage = wattage;
            }
        }
        public String getChargedKWattage_String()
        {
            double wattage = getChargedWattage() / 10000.0;
            string formattedValue = wattage.ToString("#,###.##");
            return formattedValue;
        }

        protected long chargedWattage = 0;
        protected long getChargedWattage_Current()
        {
            long temp = 0;
            lock (mArray)
            {
                if (mArray.Count < 1)
                    return 0;
                temp = (long)(mCurrentWattage - mArray[0][1]);
            }
            return temp;
        }

        public void init()
        {
            // TODO Auto-generated method stub
            lock (mArray)
            {
                mArray.Clear();
            }
            mCurrentHour = -1;
            mCurrentWattage = 0;
            mChargedWattage = 0;
            mChargedCharge = 0;
        }

        public int getChargedCharge()
        {
            int value = mChargedCharge + getChargedCharge_Current();

            if (mChannelTotalInfor.getManager_Option_ChargingStop().getList_Option_ChargingStop().Count > 1)
            {
                EOption_ChargingStop option = mChannelTotalInfor.getManager_Option_ChargingStop().getList_Option_ChargingStop()[1].mOption_ChargingStop;
                float target = mChannelTotalInfor.getManager_Option_ChargingStop().getList_Option_ChargingStop()[1].getValue_Target();
                switch (option)
                {
                    case EOption_ChargingStop.NONE:
                        break;
                    case EOption_ChargingStop.FULL:
                        break;
                    case EOption_ChargingStop.KW:
                        break;
                    case EOption_ChargingStop.SOC:
                        break;
                    case EOption_ChargingStop.WON:
                        if (target < value)
                            value = (int)target;
                        break;
                    case EOption_ChargingStop.TIME:
                        break;
                }
            }
            return value;
        }
        protected int getChargedCharge_Current()
        {
            double chargeValue = 0.0d;
            lock (mArray)
            {
                if (mArray.Count < 1) return 0;

                for (int i = 0; i < mArray.Count; i++)
                {
                    double chargeUnit = mChargeUnit_Process.getChargingUnit((int)mArray[i][0]);
                    double diff = 0.0d;
                    if (i == mArray.Count - 1)
                        diff = mCurrentWattage - mArray[i][1];
                    else
                        diff = mArray[i + 1][1] - mArray[i][1];

                    double value = (chargeUnit * diff) / 10000.0;
                    chargeValue += value;
                }
            }
            int chargedCharge = (int)Math.Floor(chargeValue);
            return chargedCharge;
        }

        public bool setCurrentChargeUnitByDatabase(String operatorType)
        {
            String[] tempList = mApplication.getManager_SQLite_Setting().getManager_Table_ChargeUnit().getChargeUnit(mChannelIndex, operatorType);
            if (tempList != null)
            {
                mChargeUnit_Process.setStartDate(tempList[3]);
                mChargeUnit_Process.setMemberType(tempList[2]);
                for (int i = 0; i < 24; i++)
                {
                    String temp = tempList[4 + i];
                    mChargeUnit_Process.setChargingUnit(i, EL_Manager_Conversion.getDouble(temp));
                }
                return true;
            }
            return false;
        }
        public void setCurrentChargeUnit(string operatorType, List<UnitPriceTable> unitPriceTable)
        {
            mChargeUnit_Process.setMemberType(operatorType);

            int index = -1;
            DateTime toDay = DateTime.Now;
            List<DateTime> dateArray = new List<DateTime>();
            for (int i = 0; i < unitPriceTable.Count; i++)
            {
                string startDate = unitPriceTable[i].startDate;
                dateArray.Add(DateTime.Parse(startDate));
            }

            DateTime closestDate = DateTime.MinValue;
            long minDifference = long.MaxValue;
            int countIndex = -1;
            for (int i = 0; i < dateArray.Count; i++)
            {
                DateTime date = dateArray[i];
                long difference = Math.Abs((long)(toDay.Date - date.Date).TotalDays);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestDate = date;
                    countIndex = i;
                    break;
                }
            }
            if (countIndex >= 0)
            {
                mChargeUnit_Process.setStartDate(unitPriceTable[countIndex].startDate);
                List<string> unitList = unitPriceTable[countIndex].price;
                for (int i = 0; i < unitList.Count; i++)
                {
                    mChargeUnit_Process.setChargeUnit(i, EL_Manager_Conversion.getDouble(unitList[i]));
                }

                mChannelTotalInfor.getStateManager_Channel().
                        setProcess_OperatorChargeUnit_Current(mChargeUnit_Process.getChargingUnit_Current());
            }
        }

        public long getChargedWattage()
        {
            long wattage = getChargedWattage_Current();

            return wattage + mChargedWattage;
        }        
    }
}
