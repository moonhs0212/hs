using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IEV_State_ChangeListener
    {
        void onConnected_By_Car();
        void onDisconnected_By_Car();
        void onRequestCharging_By_Car();
        void onRequestStop_Charging_By_Car(String result);
    }
}
