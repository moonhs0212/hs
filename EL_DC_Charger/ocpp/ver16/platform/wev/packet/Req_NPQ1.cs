using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.packet
{
    public class Req_NPQ1
    {
        public int connectorId;
        public Resolution resolution;
        public Req_NPQ1()
        {
            resolution = new Resolution();
            resolution.width = 350;
            resolution.height = 350;
        }
        public void setRequiredValue(int connectorId)
        {
            this.connectorId = connectorId;
        }
    }

}
