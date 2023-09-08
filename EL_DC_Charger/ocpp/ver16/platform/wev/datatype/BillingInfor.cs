using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.platform.wev.datatype
{
    public class BillingInfor
    {
        public int? payCharge;
        public String payTransactionDT;
        public String payApprovalNumber;
        public String payUniqueNumber;
        public String payResponseCode;
        public String payResponseMessage;
        public String cardResponseCode;

    }

}
