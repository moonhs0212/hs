using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Req_GetDiagnostics
    {
        public String location;
        public int? retries;
        public int? retryInterval;
        public String startTime; //LocalDateTime?,
        public String stopTime; //LocalDateTime?
    }
}
