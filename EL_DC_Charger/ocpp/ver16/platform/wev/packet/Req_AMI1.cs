using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.packet
{
    public class Req_AMI1
    {
        public int connectorId;
        public String membershipCardPassword;

        public void setRequiredValue(int connectorId)
        {
            this.connectorId = connectorId;
        }
    }

}
