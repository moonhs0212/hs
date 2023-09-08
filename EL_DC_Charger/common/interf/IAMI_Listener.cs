using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IAMI_Listener : IEL_Object_Channel_Base
    {
        void onStart_Increase_Wattage(float wattage);
        void onIncrease_Wattage(float wattage);
    }
}
