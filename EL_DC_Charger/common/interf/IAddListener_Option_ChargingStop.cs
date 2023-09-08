using EL_DC_Charger.common.ChargerInfor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IAddListener_Option_ChargingStop
    {
        void onAddOption_ChargingStop(EL_Option_ChargingStop option_ChargingStop);
    }
}
