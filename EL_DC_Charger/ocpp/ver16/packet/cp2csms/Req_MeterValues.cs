using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_MeterValues
    {
        public int connectorId;
        public long transactionId;
        public List<MeterValue> meterValue;

        public void setRequiredValue(int connectorId, List<MeterValue> meterValue)
        {
            this.connectorId = connectorId;
            this.meterValue = meterValue;
        }
    }
}
