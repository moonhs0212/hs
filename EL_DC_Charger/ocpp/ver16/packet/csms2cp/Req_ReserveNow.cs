using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Req_ReserveNow
    {
        public int? connectorId;
        public String expiryDate; //LocalDateTime,
        public String idTag;
        public String parentIdTag;
        public long? reservationId;
    }
}
