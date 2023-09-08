using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Req_StopTransaction
    {
        public String idTag;//: String?,
        public long? meterStop;//: Int,
        public String timestamp;//: LocalDateTime,
        public long? transactionId;//: Long,
        public Reason? reason;//: Reason?,
        public List<MeterValue> transactionData;//: List<MeterValue>?

        public void initVariable()
        {
            idTag = null;//: String?,
            meterStop = null;//: Int,
            timestamp = null;//: LocalDateTime,
            transactionId = null;//: Long,
            reason = null;//: Reason?,
            transactionData = null;//: List<MeterValue>?
        }

        public void setInfor_Required(long? meterStop, String timestamp, long? transactionId)
        {
            this.meterStop = meterStop;
            this.timestamp = timestamp;
            this.transactionId = transactionId;
        }
    }

}
