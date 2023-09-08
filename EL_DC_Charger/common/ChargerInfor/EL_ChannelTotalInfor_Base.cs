
using EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.ChargerInfor;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.ocpp.ver16.interf;
using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.ocpp.ver16.infor;
using EL_DC_Charger.common.ChargerInfor;
using EL_DC_Charger.common;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;

namespace EL_DC_Charger.EL_DC_Charger.ChargerInfor
{
    abstract public class EL_ChannelTotalInfor_Base : EL_Object_Channel_Base
    {


        protected EL_Manager_Option_ChargingStop mManager_Option_ChargingStop = null;
        public EL_Manager_Option_ChargingStop getManager_Option_ChargingStop()
        {
            return mManager_Option_ChargingStop;
        }

        public int mChargingUnit_Member_Test = 100;
        public int mChargingUnit_Nonmember_Test = 100;

        override public void initVariable()
        {


            setControlbdComm_PacketManager();
            setAmi_PacketManager();
            setStateManager_Channel();
            setSmartroComm_PacketManager();
            mChargingTime = new EL_ChannelInfor_ChargingTime(this);
            mChargingWattage = new EL_Manager_ChargingWattage(mApplication, mChannelIndex);

            mChargingCharge = new EL_ChannelInfor_ChargingCharge(this);
            mChargingCharge.initVariable();

            mChargingUnit_Member_Test = mApplication.getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.CHARGINGUNIT_MEMBER_TEST);
            mChargingUnit_Nonmember_Test = mApplication.getManager_SQLite_Setting().getTable_Setting(0).getSettingData_Int(CONST_INDEX_MAINSETTING.CHARGINGUNIT_NONMEMBER_TEST);

            mOCPP_ChannelInfor = new OCPP_ChannelInfor(mApplication, mChannelIndex);
            mOCPP_ChannelInfor.initVariable();

            mManager_Option_ChargingStop = new EL_Manager_Option_ChargingStop(this);
        }
        protected EL_ControlbdComm_PacketManager mControlbdComm_PacketManager = null;
        public EL_ControlbdComm_PacketManager getControlbdComm_PacketManager()
        {
            return mControlbdComm_PacketManager;
        }
        protected Smartro_TL3500BS_PacketManager mSmatroPacketManager = null;
        public Smartro_TL3500BS_PacketManager getSmartro_PacketManager()
        {
            return mSmatroPacketManager;
        }
        protected OCPP_ChannelInfor mOCPP_ChannelInfor = null;
        public OCPP_ChannelInfor getOCPP_ChannelInfor()
        {
            return mOCPP_ChannelInfor;
        }

        abstract protected void setControlbdComm_PacketManager();
        abstract protected void setSmartroComm_PacketManager();

        protected EL_AMI_PacketManager_Base mAMI_PacketManager = null;
        public EL_AMI_PacketManager_Base getAMI_PacketManager()
        {
            return mAMI_PacketManager;
        }

        protected IEV_State mEV_State = null;
        public IEV_State getEV_State()
        {
            return mEV_State;
        }
        public void setEV_State(IEV_State value)
        {
            mEV_State = value;
        }
        abstract protected void setAmi_PacketManager();

        public EL_ChannelInfor_ChargingTime mChargingTime = null;
        public EL_ChannelInfor_ChargingCharge mChargingCharge = null;
        public EL_Manager_ChargingWattage mChargingWattage = null;

        //    public void setChargingStart()
        //    {
        //        mChargingTime.setChargingStart();
        //        mChargingWattage.setChargignStart();
        //    }

        public void setChargingStop()
        {
            mChargingTime.onChargingStop();
            mChargingWattage.setChargignStop();
        }

        public int mSoc_Start = 0;
        public int mSoc_Finish = 0;



        public EL_StateManager_Channel_Base mStateManager_Channel = null;
        public EL_StateManager_Channel_Base getStateManager_Channel()
        {
            return mStateManager_Channel;
        }
        //    public void setChargingStart()
        //    {
        //        mChargingTime.setChargingStart();
        //        mChargingWattage.setChargignStart();
        //    }

        abstract protected void setStateManager_Channel();

        public EL_ChannelTotalInfor_Base(EL_MyApplication_Base application, int channelIndex)
                        : base(application, channelIndex)
        {

        }
    }

}
