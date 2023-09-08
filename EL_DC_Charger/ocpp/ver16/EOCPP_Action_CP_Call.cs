using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EOCPP_Action_CP_Call
    {
        Authorize,
        BootNotification,
        DiagnosticsStatusNotification,
        FirmwareStatusNotification,
        DataTransfer,
        Heartbeat,
        MeterValues,
        StartTransaction,
        StatusNotification,
        StopTransaction,
    }

}
