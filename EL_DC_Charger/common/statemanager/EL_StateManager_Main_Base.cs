using EL_DC_Charger.common.application;
using EL_DC_Charger.EL_DC_Charger.ChargerVariable;
using EL_DC_Charger.EL_DC_Charger.ConstVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.statemanager
{
    abstract public class EL_StateManager_Main_Base : EL_StateManager_Base
    {

        protected bool bIsClicked_Back = false;
        public void setClick_Back() { bIsClicked_Back = true; }
        protected bool bIsClicked_Start = false;
        public void setClick_Start() { bIsClicked_Start = true; }

        public bool bIsUseEnable = false;

        public bool bIsNeedReset_Charger_Hard = false;
        public bool bIsNeedReset_Charger_Soft = false;

        public bool bIsNormalState = false;
        public bool bIsPendingState = false;

        public bool isNeedReset()
        {
            if (bIsNeedReset_Charger_Hard || bIsNeedReset_Charger_Soft)
                return true;

            return false;

        }

        
        override public void setState(int state)
        {

            base.setState(state);
        }

        protected void excuteResetNormal()
        {
            for (int i = 0; i < mApplication.getChannelTotalInfor().Length; i++)
            {
                mApplication.getChannelTotalInfor(i + 1).
                        getStateManager_Channel().bIsNeedReset_Charger_Hard = true;
            }
            setState(CONST_MAINSTATE.REBOOT);
        }

        public EL_StateManager_Main_Base(EL_MyApplication_Base application)
            : base(application, 0)
        {
            
        }
        public void setIsUseEnable()
        {
            bIsUseEnable = mApplication.getManager_SQLite_Setting().getTable_Setting(0)
                    .getSettingData_Boolean(CONST_INDEX_MAINSETTING.IS_USE_ENABLE);
        }
        public void setIsUseEnable(bool setting)
        {
            bIsUseEnable = setting;
            mApplication.getManager_SQLite_Setting().getTable_Setting(0)
                    .setSettingData(CONST_INDEX_MAINSETTING.IS_USE_ENABLE, setting);
        }
        protected void initVariable_By_ChangeMode()
        {
            bIsClicked_Back = false;
            bIsClicked_Start = false;
        }
    }

}
