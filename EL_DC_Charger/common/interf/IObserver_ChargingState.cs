using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IObserver_ChargingState
    {
        void onChargingStart();
        void onChargingRestart();
        void onChargingPause();
        void onChargingStop();
    }
}
