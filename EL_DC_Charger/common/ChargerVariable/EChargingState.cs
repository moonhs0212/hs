using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.constvariable
{
    public enum EChargingState
    {
        WAIT = 0,
        PREPARING_CHARGING = 1,
        CHARGING = 2,
        CHARGING_ERROR_BEFORE_CHARGING = 3,
        CHARGING_COMPLETE = 4,
        CHARGING_ERROR_AFTER_CHARGING = 5,
    }
}
