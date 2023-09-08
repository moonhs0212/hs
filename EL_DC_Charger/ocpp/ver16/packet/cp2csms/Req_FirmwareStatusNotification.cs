using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_FirmwareStatusNotification
    {
        public FirmwareStatus status;

        public void setRequiredValue(FirmwareStatus status)
        {
            this.status = status;
        }
    }
}
