using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    public class SampledValue
    {
        public String value;
        public string context;
        public ValueFormat? format;
        public String measurand; // 충전 요금: Charging.Charge
        public Phase? phase;
        public Location? location;
        public UnitOfMeasure? unit; // 충전 요금: Won
        public void setRequiredValue(String value)
        {
            this.value = value;
        }

    }
}
