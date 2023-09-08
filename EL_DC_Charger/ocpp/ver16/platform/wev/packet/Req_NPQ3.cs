using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.packet
{
    public class Req_NPQ3
    {
        public int connectorId;
        public int paymentResult;
        public String chargingLimitProfile;
        public String unitAmount;
        public string operatorType;
    }

}
