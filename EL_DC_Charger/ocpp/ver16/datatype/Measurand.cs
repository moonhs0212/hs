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
    public enum Measurand
    {
        Current_Export,
        Current_Import,
        Current_Offered,
        Energy_Active_Export_Register,
        Energy_Active_Import_Register,
        Energy_Reactive_Export_Register,
        Energy_Reactive_Import_Register,
        Energy_Active_Export_Interval,
        Energy_Active_Import_Interval,
        Energy_Reactive_Export_Interval,
        Energy_Reactive_Import_Interval,
        Frequency,
        Power_Active_Export,
        Power_Active_Import,
        Power_Factor,
        Power_Offered,
        Power_Reactive_Export,
        Power_Reactive_Import,
        RPM,
        SoC,
        Temperature,
        Voltage,
        Charging_Charge

        //override public String ToString()
        //{
        //    String result = base.toString();
        //    result = result.replace("_", ".");
        //    return result;
        //}
    }

}
