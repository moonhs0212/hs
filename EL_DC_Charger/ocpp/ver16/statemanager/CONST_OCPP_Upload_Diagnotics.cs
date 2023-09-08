using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.statemanager
{
    public class CONST_OCPP_Upload_Diagnotics
    {
        public const int READY = 0;
        public const int CONNECTION = 1000;
        public const int UPLOADING = 2000;
        public const int COMPLETE = 3000;
    }
}
