using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.packet
{
    public class Req_CTP1
    {
        public int connectorId;
        public String operatorType;
        public void setRequiredValue(int connectorId, String operatorType)
        {
            this.connectorId = connectorId;
            this.operatorType = operatorType;
        }
    }
}
