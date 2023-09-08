using EL_DC_Charger.common.application;
using EL_DC_Charger.common.statemanager;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.infor;
using EL_DC_Charger.ocpp.ver16.interf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    abstract public class EL_StateManager_OCPP_Base : EL_StateManager_Base
    {
        protected OCPP_Comm_Manager mOCPP_Comm_Manager = null;

        public EL_StateManager_OCPP_Base(EL_MyApplication_Base application)
            : base(application, 0)
        {
            
        }

        
        override public void setStart()
        {
            base.setStart();
            setState(0);
        }

        
        override public void initVariable()
        {

            setOCPP_initVariable();
        }

        protected OCPP_MainInfor mOCPP_MainInfor = null;

        protected OCPP_ChannelInfor mOCPP_ChannelInfor = null;

        protected OCPP_Comm_SendMgr mOCPP_Comm_SendMgr = null;
        protected void setOCPP_initVariable()
        {
            mOCPP_Comm_Manager = mApplication.getOCPP_Comm_Manager();
            mOCPP_Comm_SendMgr = mApplication.getOCPP_Comm_Manager().getSendMgr();
            mOCPP_MainInfor = mApplication.getOCPP_MainInfor();
            if (mChannelIndex > 0)
                mOCPP_ChannelInfor = mApplication.getChannelTotalInfor(mChannelIndex).getOCPP_ChannelInfor();
        }
    }

}
