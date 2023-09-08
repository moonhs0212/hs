using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class CONST_OCPP_UpdateFirmware
    {
        public const int READY = 0;
        public const int DOWNLOADING_START = READY + 1000;
        public const int DOWNLOADING = DOWNLOADING_START + 1000;
        public const int INSTALL_WAIT = DOWNLOADING + 1000;
        public const int INSTALLING = INSTALL_WAIT + 1000;
        public const int INSTALLED = INSTALLING + 1000;
    }
}
