using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Req_UpdateFirmware
    {
        public String location;//: String,
        public int? retries;//: Int?,
        public String retrieveDate;//: LocalDateTime,
        public int? retryInterval;//: Int?,
    }
}
