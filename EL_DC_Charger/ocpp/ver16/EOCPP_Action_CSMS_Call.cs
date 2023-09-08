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
    public enum EOCPP_Action_CSMS_Call
    {


        CancelReservation,
        ChangeAvailability,
        ChangeConfiguration,
        ClearCache,
        ClearChargingProfile,
        DataTransfer,

        GetCompositeSchedule,
        GetConfiguration,
        GetDiagnostics,
        GetLocalListVersion,

        RemoteStartTransaction,
        RemoteStopTransaction,
        ReserveNow,
        Reset,
        SendLocalList,
        SetChargingProfile,

        TriggerMessage,
        UnlockConnector,
        UpdateFirmware,
    }
}
