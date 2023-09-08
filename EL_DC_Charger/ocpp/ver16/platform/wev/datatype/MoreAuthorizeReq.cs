using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.datatype
{
    public class MoreAuthorizeReq
    {
        public Int32 connectorId;
        public Int32 certifyType;
        public String membershipCardPassword;
    }
}
