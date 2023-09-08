using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_StartTransaction
    {
        public int? connectorId;//: > 0
        public String idTag;//: String,
        public long? meterStart;//: Int,
        public long? reservationId;//: Long?,
        public String timestamp;//: LocalDateTime

        public void setRequiredValue(int connectorId, String idTag, long meterStart, String timestamp)
        {
            this.connectorId = connectorId;
            this.idTag = idTag;
            this.meterStart = meterStart;
            this.timestamp = timestamp;
        }
    }

}
