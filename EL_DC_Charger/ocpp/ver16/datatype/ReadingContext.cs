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
    public enum ReadingContext
    {
        Interruption_Begin,
        Interruption_End,
        Other,
        Sample_Clock,
        Sample_Periodic,
        Transaction_Begin,
        Transaction_End,
        Trigger

    //    @Override
    //public String toString()
    //{
    //    String result = super.toString();
    //    result = result.replace("_", ".");
    //    return result;
    //}
    }
}
