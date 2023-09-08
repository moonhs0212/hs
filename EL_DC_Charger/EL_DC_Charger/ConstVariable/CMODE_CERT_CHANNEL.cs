using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.EL_DC_Charger.ChargerVariable
{
    public class CMODE_CERT_CHANNEL
    {
        public const int MODE_BOOT_ON = 0;

        public const int MODE_PREPARE = MODE_BOOT_ON + 1000;

        public const int MODE_MAIN = MODE_PREPARE + 1000;

        public const int MODE_CHARGING = MODE_MAIN + 1000;

        public const int MODE_CHARGING_COMPLETE = MODE_CHARGING + 1000;

        public const int MODE_EMERGENCY = MODE_CHARGING_COMPLETE + 1000;

    }
}
