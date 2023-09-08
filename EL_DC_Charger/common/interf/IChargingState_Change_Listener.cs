using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IChargingState_Change_Listener
    {
        void onChargingStart();
        void onChargingStop();
        void onChargingReStart();
        void onChargingStop_Briefly();
    }
}
