
using EL_DC_Charger.BatteryChange_Charger.SerialPorts.IOBoard;
using EL_DC_Charger.common.application;
using EL_DC_Charger.common.interf;
using EL_DC_Charger.EL_DC_Charger.Applications;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.AMI;
using EL_DC_Charger.EL_DC_Charger.SerialPorts.smartro_tl3500bs;
using EL_DC_Charger.EL_DC_Charger.statemanager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ChargerInfor
{
    public class EL_DC_Charger_ChannelTotalInfor : EL_ChannelTotalInfor_Base
    {
        public EL_DC_Charger_ChannelTotalInfor(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
        }

        protected override void setAmi_PacketManager()
        {

            mAMI_PacketManager = new ODHitec_AMI_PacketManager((EL_DC_Charger_MyApplication)mApplication, mChannelIndex);
            mAMI_PacketManager.initVariable();
        }

        protected override void setControlbdComm_PacketManager()
        {
            mControlbdComm_PacketManager = new EL_ControlbdComm_PacketManager((EL_DC_Charger_MyApplication)mApplication, mChannelIndex);
            mControlbdComm_PacketManager.initVariable();

            mApplication.DI_Manager = (EL_ControlbdComm_PacketManager)mControlbdComm_PacketManager;

        }
        protected override void setSmartroComm_PacketManager()
        {
            mSmatroPacketManager = new Smartro_TL3500BS_PacketManager((EL_DC_Charger_MyApplication)mApplication, mChannelIndex);
            mSmatroPacketManager.initVariable();
        }
        protected override void setStateManager_Channel()
        {

            switch (mApplication.getChargerType())
            {
                case common.variable.EChargerType.NONE:
                    mStateManager_Channel = new SC_1CH_None_StateManager_Channel(mApplication, mChannelIndex);
                    break;
                case common.variable.EChargerType.CH1_CERT:
                    mStateManager_Channel = new SC_1CH_OCPP_StateManager_Channel(mApplication, mChannelIndex);
                    break;
                case common.variable.EChargerType.CH1_NOT_PUBLIC:
                    mStateManager_Channel = new EL_Charger_1CH_Private_StateManager_Channel((EL_DC_Charger_MyApplication)mApplication, mChannelIndex);
                    break;
                case common.variable.EChargerType.CH1_PUBLIC:
                    switch (mApplication.getPlatform())
                    {
                        case common.variable.EPlatform.NONE:
                            mStateManager_Channel = new SC_1CH_None_StateManager_Channel(mApplication, mChannelIndex);
                            break;
                        case common.variable.EPlatform.WEV:
                            mStateManager_Channel = new EL_Charger_1CH_OCPP_StateManager_Channel_Wev(mApplication, mChannelIndex);
                            break;
                        default:
                            mStateManager_Channel = new SC_1CH_None_StateManager_Channel(mApplication, mChannelIndex);
                            break;
                            //case common.variable.EPlatform.OCTT_CERTIFICATION:
                            //case common.variable.EPlatform.SOFTBERRY:
                            //case common.variable.EPlatform.NICE_CHARGER:
                            //    break;
                    }
                    break;
                case common.variable.EChargerType.CH2_CERT:
                    mStateManager_Channel = new SC_1CH_OCPP_StateManager_Channel(mApplication, mChannelIndex);
                    break;
            }


        }
    }
}
