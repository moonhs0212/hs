using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IChargingControl_Manager : IEL_Object_Channel_Base
    {
        int getChargingState();
        void setChargingStart();
        void setChargingPause();
        void setChargingRestart();
        void setChargingStop();
        bool isCommand_ChargingStart();
        bool isCommand_ChargingPause();

        bool isState_ChargingStart();
        bool isState_ChargingStart_Real();
        bool isState_ChargingStartByOutputPower();

        void setChargingType(int chargingType);
        int getChargingType();
    }
}
