using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    public class ChargingSchedule
    {
        public int? duration;
        public String startSchedule;//dateTime
        public ChargingRateUnitType chargingRateUnit;
        public ChargingSchedulePeriod chargingSchedulePeriod;
        public int? minChargingRate;
    }
}
