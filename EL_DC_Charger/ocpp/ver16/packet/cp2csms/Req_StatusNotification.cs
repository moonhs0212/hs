using EL_DC_Charger.common.item;
using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_StatusNotification
    {
        public int? connectorId;//: Int,
        public ChargePointErrorCode errorCode;//: ChargePointErrorCode,
        public String info;//: String?,
        public ChargePointStatus status;//: ChargePointStatus,
        public String timestamp;//: LocalDateTime?,
        public String vendorId;//: String?,
        public String vendorErrorCode;//: String?

        public void setRequiredValue_Wev(int connectorId, ChargePointErrorCode errorCode, ChargePointStatus status, String info, String vendorId)
        {
            if (info != null)
                this.info = info;

            if (vendorId != null)
                this.vendorId = vendorId;

            this.connectorId = connectorId;
            this.errorCode = errorCode;
            this.status = status;
        }

        public void setRequiredValue(int connectorId, ChargePointErrorCode errorCode, ChargePointStatus status)
        {
            this.connectorId = connectorId;
            this.errorCode = errorCode;
            this.status = status;
            this.timestamp = new EL_Time().toString_OCPP();
        }
    }

}
