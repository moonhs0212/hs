using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.common.variable;
using EL_DC_Charger.EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.ocpp.ver16.platform.wev.packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.ChargerInfor
{
    public class EL_Manager_Option_ChargingStop : EL_Object_Channel_Base
    {

        public List<EL_Option_ChargingStop> mList_Option_ChargingStop = new List<EL_Option_ChargingStop>();

        


        protected EL_Option_ChargingStop mOption_ChargingStop_Full = null;
        protected EL_Option_ChargingStop mOption_ChargingStop_KW = null;
        protected EL_Option_ChargingStop mOption_ChargingStop_SOC = null;
        public EL_Option_ChargingStop mOption_ChargingStop_WON = null;
        protected EL_Option_ChargingStop mOption_ChargingStop_Time = null;

        protected IAddListener_Option_ChargingStop mAddListener_Option_ChargingStop = null;
        public void setAddListener_Option_ChargingStop(IAddListener_Option_ChargingStop listener)
        {
            mAddListener_Option_ChargingStop = listener;
        }
        public void addOption(EOption_ChargingStop option, float value)
        {
            EL_Option_ChargingStop option_ChargingStop = null;
            switch (option)
            {
                case EOption_ChargingStop.FULL:
                    option_ChargingStop = mOption_ChargingStop_Full;
                    break;
                case EOption_ChargingStop.KW:
                    option_ChargingStop = mOption_ChargingStop_KW;
                    break;
                case EOption_ChargingStop.SOC:
                    option_ChargingStop = mOption_ChargingStop_SOC;
                    break;
                case EOption_ChargingStop.WON:
                    option_ChargingStop = mOption_ChargingStop_WON;
                    break;
                case EOption_ChargingStop.TIME:
                    option_ChargingStop = mOption_ChargingStop_Time;
                    break;
            }
            option_ChargingStop.setInfor(value);
            mList_Option_ChargingStop.Add(option_ChargingStop);
        }



        public EL_Manager_Option_ChargingStop(EL_ChannelTotalInfor_Base channelTotalInfor)
            : base(channelTotalInfor.getApplication(), channelTotalInfor.getChannelIndex())
        {
            mChannelTotalInfor = channelTotalInfor;

            mOption_ChargingStop_Full = new EL_Option_ChargingStop(mApplication, channelTotalInfor.getChannelIndex(), EOption_ChargingStop.FULL);
            mOption_ChargingStop_Full.setInfor(1);
            mOption_ChargingStop_SOC = new EL_Option_ChargingStop(mApplication, channelTotalInfor.getChannelIndex(), EOption_ChargingStop.SOC);
            mOption_ChargingStop_WON = new EL_Option_ChargingStop(mApplication, channelTotalInfor.getChannelIndex(), EOption_ChargingStop.WON);
            mOption_ChargingStop_KW = new EL_Option_ChargingStop(mApplication, channelTotalInfor.getChannelIndex(), EOption_ChargingStop.KW);
            mOption_ChargingStop_Time = new EL_Option_ChargingStop(mApplication, channelTotalInfor.getChannelIndex(), EOption_ChargingStop.TIME);
        }

        public List<EL_Option_ChargingStop> getList_Option_ChargingStop()
        {
            return mList_Option_ChargingStop;
        }

        public bool isNeedStopCharging()
        {

            foreach (EL_Option_ChargingStop stopOption in mList_Option_ChargingStop)
            {
                if (stopOption.isNeedChargingComplete())
                {
                    mMessage_StopOption = stopOption.getErrorMessage_ChargingComplete();
                    return true;
                }
            }
            mMessage_StopOption = "";
            return false;
        }

        public void addOption(Req_NPQ3 req_NPQ3)
        {
            if (req_NPQ3 == null) return;
            if (req_NPQ3.paymentResult == 1)
            {
                if (string.Compare(req_NPQ3.chargingLimitProfile, "Full") == 0)
                {
                    mOption_ChargingStop_Full.setInfor(1);
                    mList_Option_ChargingStop.Add(mOption_ChargingStop_Full);
                }
                else if (string.Compare(req_NPQ3.chargingLimitProfile, "kW") == 0)
                {
                    mOption_ChargingStop_KW.setInfor(float.Parse(req_NPQ3.unitAmount));
                    mList_Option_ChargingStop.Add(mOption_ChargingStop_KW);
                }
                else if (string.Compare(req_NPQ3.chargingLimitProfile, "Soc") == 0)
                {
                    mOption_ChargingStop_SOC.setInfor(float.Parse(req_NPQ3.unitAmount));
                    mList_Option_ChargingStop.Add(mOption_ChargingStop_SOC);
                }
                else if (string.Compare(req_NPQ3.chargingLimitProfile, "Won") == 0)
                {
                    mOption_ChargingStop_WON.setInfor(float.Parse(req_NPQ3.unitAmount));
                    mList_Option_ChargingStop.Add(mOption_ChargingStop_WON);
                }

            }
        }

        public string getMessage_StopOption()
        {
            return mMessage_StopOption;
        }
        protected string mMessage_StopOption = "";

        public override void initVariable()
        {
            //base.initVariable();
            mList_Option_ChargingStop.Clear();
        }
    }
}
