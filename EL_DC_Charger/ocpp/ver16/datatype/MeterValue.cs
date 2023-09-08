using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    public class MeterValue
    {
        public String timestamp;
        public List<SampledValue> sampledValue;

        public void setRequiredValue(String timestamp, List<SampledValue> sampledValue)
        {
            this.timestamp = timestamp;
            this.sampledValue = sampledValue;
        }

    }
}
