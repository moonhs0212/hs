using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.item;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.ChargerInfor
{
    public class EL_Option_ChargingStop : EL_Object_Channel_Base
    {
        
        public EOption_ChargingStop mOption_ChargingStop = EOption_ChargingStop.NONE;
        protected float mValue_Target = 0;
        public float getValue_Target()
        {
            return mValue_Target;
        }
        public EOption_ChargingStop getChargingOption()
        {
            return mOption_ChargingStop;
        }
        public EOption_ChargingStop getOption_ChargingStop() { return mOption_ChargingStop; }

        

        public EL_Option_ChargingStop(EL_MyApplication_Base application, int channelIndex, EOption_ChargingStop option, float target)
            : this(application, channelIndex, option)
        {
            mValue_Target = target;
        }

        public EL_Option_ChargingStop(EL_MyApplication_Base application, int channelIndex, EOption_ChargingStop option)
            : base(application, channelIndex)
        {

            mChannelTotalInfor = mApplication.getChannelTotalInfor(mChannelIndex);
            mOption_ChargingStop = option;
        }
        public void setInfor(float target)
        {
            mValue_Target = target;
        }

        protected string mErrorMessage = "";
        public string getErrorMessage_ChargingComplete()
        {
            return mErrorMessage;
        }

        public bool isNeedChargingComplete()
        {
            switch (mOption_ChargingStop)
            {
                case EOption_ChargingStop.FULL:
                    if (mChannelTotalInfor.getEV_State().isCar_DataComm())
                    {
                        if (mChannelTotalInfor.getEV_State().getSOC() == 100 &&
                            mValue_Target <= mChannelTotalInfor.getControlbdComm_PacketManager().getWastedMinute_After_FullCharge())
                        {
                            mErrorMessage = "완충";
                            return true;
                        }
                        else
                        {
                            mErrorMessage = "";
                            return false;
                        }
                    }
                    else
                    {
                        mErrorMessage = "";
                        return false;
                    }
                case EOption_ChargingStop.KW:
                    if (float.Parse(mChannelTotalInfor.mChargingWattage.getChargingWattage_String()) >= mValue_Target)
                    {
                        mErrorMessage = "목표 충전량 도달";
                        return true;
                    }
                    else if (float.Parse(mChannelTotalInfor.mChargingWattage.getChargingWattage_String()) > mValue_Target - 0.02)  //출력 낮춤
                    {
                        EL_DC_Charger_MyApplication.getInstance().slowMode = true;
                        return false;
                    }
                    else
                    {
                        EL_DC_Charger_MyApplication.getInstance().slowMode = false;
                        mErrorMessage = "";
                        return false;
                    }
                case EOption_ChargingStop.SOC:
                    if (mChannelTotalInfor.getEV_State().isCar_DataComm() &&
                        mChannelTotalInfor.getEV_State().getSOC() >= mValue_Target)
                    {
                        mErrorMessage = "충전량(%) 도달";
                        return true;
                    }
                    else
                    {
                        mErrorMessage = "";
                        return false;
                    }
                case EOption_ChargingStop.WON:

                    if (mChannelTotalInfor.mChargingWattage.getChargingCharge() >= mValue_Target)
                    {
                        mErrorMessage = "결제금액 도달";
                        return true;
                    }
                    else if (mChannelTotalInfor.mChargingWattage.getChargingCharge() > mValue_Target - 3)
                    {
                        //출력 낮춤
                        EL_DC_Charger_MyApplication.getInstance().slowMode = true;
                        return false;
                    }

                    else
                    {
                        EL_DC_Charger_MyApplication.getInstance().slowMode = false;
                        mErrorMessage = "";
                        return false;
                    }
                case EOption_ChargingStop.TIME:
                    if (mChannelTotalInfor.mChargingTime.getMinute_WastedTime() >= mValue_Target)
                    {
                        mErrorMessage = "목표시간 도달";
                        return true;
                    }
                    else
                    {
                        mErrorMessage = "";
                        return false;
                    }
                default:
                    mErrorMessage = "";
                    return false;
            }
        }
    }
}
