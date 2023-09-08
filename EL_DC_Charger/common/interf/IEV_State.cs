using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IEV_State : IEL_Object_Channel_Base
    {
        int getRemainTime_FullCharging_Minute();
        bool isState_ConnectedCar();
        bool isState_ChargingCar();
        bool isRequesting_ChargingStart_Car();
        bool isState_ChargingCompleteCar();
        String isRequesting_ChargingStop_Car_String();
        bool isErrorState_Car();
        bool isErrorState_Comm_WithCar();
        int getErrorCode();
        String getErrorCode_String();


        bool isCar_DataComm();
        int getSOC();

        int getWastedMinute_After_FullCharge();
    }
}
