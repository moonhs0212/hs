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
    public enum UnitOfMeasure
    {
        Wh, // Watt-hours (energy)
        kWh, // Kilowatt-hours (energy)
        varh, // Var-hours (energy)
        kvarh, // Kilovar-hours (energy)
        W, // Watts (power)
        kW, // Kilowatts (power)
        VA, // Volt-Ampere (apparent power)
        kVA, // Kilovolt-Ampere (apparent power)
        vars, // Var-s (reactive power)
        kvars, // Kilovar-s (reactive power)
        A, // Amperes (current)
        V, // Voltage (r.m.s. AC)
        Celsius, // Degrees (temperature)
        Fahrenheit, // Degrees (temperature)
        K, // Degrees Kelvin (temperature)
        Percent, // Percentage
        Won,
        Dollar,
        Euro,
        String
            
    }
}
