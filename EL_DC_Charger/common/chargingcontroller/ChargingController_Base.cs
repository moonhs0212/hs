using EL_DC_Charger.Controller;
using EL_DC_Charger.common.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.chargingcontroller
{
    abstract public class ChargingController_Base : Controller_Base
    {

        abstract protected bool process_EmergencyButton();

        public void setErrorReason(string reason)
        {
            mErrorReson = reason;
        }
        protected bool bIsErrorOccured = false;
        public string mErrorReson = "";
        

        public bool bIsChargingStart_Current = false;
        public bool bIsCharging_MoreOnce = false;
        public bool bIsChargingStart_By_ChargingWattage = false;

        protected bool bIsCommand_UsingStart = false;
        protected bool bIsCommand_Certification = false;

        public bool bIsClick_UsingStart = false;
        public bool bIsClick_Back = false;
        public bool bIsClick_Home = false;

        public bool bIsClick_Confirm_ErrorReson_BeforeCharging = false;

        public bool bIsClick_ChargingStop = false;
        public bool bIsClick_ChargingComplete = false;

        public bool bIsReceive_CardNumber = false;
        public bool bIsReceive_RFCard_Failed = false;

        protected ChargingController_Base(EL_MyApplication_Base application, int channelIndex) : base(application, channelIndex)
        {
        }

        virtual protected void initChargingVariable()
        {
            mErrorReson = "";

            bIsChargingStart_Current = false;
            bIsCharging_MoreOnce = false;
            bIsChargingStart_By_ChargingWattage = false;

            bIsCommand_UsingStart = false;
            bIsCommand_Certification = false;

            bIsClick_UsingStart = false;
            bIsClick_Back = false;

            bIsClick_Confirm_ErrorReson_BeforeCharging = false;

            bIsClick_ChargingStop = false;
            bIsClick_ChargingComplete = false;

            bIsReceive_CardNumber = false;
            bIsReceive_RFCard_Failed = false;
        }

        virtual protected void initButtonClick()
        {
            bIsClick_UsingStart = false;
            bIsClick_Back = false;
        }

        override public void setMode(int mode)
        {
            base.setMode(mode);
            initButtonClick();
        }
    }
}
