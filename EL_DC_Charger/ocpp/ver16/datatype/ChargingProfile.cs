using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    public class ChargingProfile
    {
        public int? chargingProfileId;
        public int? transactionId;
        public int? stackLevel;
        public ChargingProfilePurposeType chargingProfilePurpose;
        public ChargingProfileKindType chargingProfileKind;
        public RecurrencyKindType recurrencyKind;
        public String validFrom;
        public String validTo;
        public ChargingSchedule chargingSchedule;
    }
}
