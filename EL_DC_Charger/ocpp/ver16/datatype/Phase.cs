using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.datatype
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Phase
    {
        L1,
        L2,
        L3,
        N,
        L1_N,
        L2_N,
        L3_N,
        L1_L2,
        L2_L3,
        L3_L1,
    }
}
