using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using EL_DC_Charger.ocpp.ver16.platform.wev.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.packet
{
    public class WevReq_Authorize : Req_Authorize
    {
        public MoreAuthorizeReq moreAuthorizeReq;
        //    public String errorMessageOOPS = "";
        public void setRequiredValue_Wev_CardTag(int connectorId, String idTag)
        {
            this.idTag = idTag;
            this.moreAuthorizeReq = new MoreAuthorizeReq();
            moreAuthorizeReq.connectorId = connectorId;
            moreAuthorizeReq.certifyType = 1;
            //        errorMessageOOPS = "kkkkkkkkkk";
        }

        public void setRequiredValue_Wev_CardNumber(int connectorId, String idTag, String password)
        {
            this.idTag = idTag;
            this.moreAuthorizeReq = new MoreAuthorizeReq();
            moreAuthorizeReq.connectorId = connectorId;
            moreAuthorizeReq.certifyType = 2;
            moreAuthorizeReq.membershipCardPassword = password;
        }
    }
}
