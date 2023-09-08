using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.chargingcontroller
{
    public class CState_ChargingControl
    {
        public const int STATE_INIT = 0;
        public const int STATE_WAIT = STATE_INIT + 1000;
        public const int STATE_WAIT_CONNECT_CONNECTOR = STATE_WAIT + 1000;
        public const int STATE_WAIT_CHARGING_START = STATE_WAIT_CONNECT_CONNECTOR + 1000;
        public const int STATE_WAIT_CHARGING = STATE_WAIT_CHARGING_START + 1000;
        public const int STATE_CHARGING = STATE_WAIT_CHARGING + 1000;
        public const int STATE_WAIT_RESTART = STATE_CHARGING + 1000;
        public const int STATE_WAIT_CHARGING_STOP = STATE_WAIT_RESTART + 1000;
        public const int STATE_CHARGINGSTOP_COMPLETE = STATE_WAIT_CHARGING_STOP + 1000;
    }
}
