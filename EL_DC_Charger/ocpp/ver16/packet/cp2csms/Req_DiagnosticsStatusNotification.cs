using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_DiagnosticsStatusNotification
    {
        public DiagnosticsStatus status;

        public void setRequiredValue(DiagnosticsStatus status)
        {
            this.status = status;
        }
    }

}
