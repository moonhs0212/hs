using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_Authorize
    {
        public String idTag;
        public MoreAuthorizeReq moreAuthorizeReq;
        public void setRequiredValue(String idTag)
        {
            this.idTag = idTag;
        }

        public void setRequiredValue_Wev_CardTag(int connectorId, String idTag)
        {
            this.idTag = idTag;
            //this.moreAuthorizeReq = new MoreAuthorizeReq();
            //moreAuthorizeReq.connectorId = connectorId;
            //moreAuthorizeReq.certifyType = 1;
            //        errorMessageOOPS = "kkkkkkkkkk";
        }

    }

}
