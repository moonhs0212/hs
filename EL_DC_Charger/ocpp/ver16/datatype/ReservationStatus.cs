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
    public enum ReservationStatus
    {
        Waiting, // Waiting for charge point to respond to a reservation request
        Accepted, // Reservation has been made, The only status for active, usable reservations (if expiryDatetime is in future)
        Used, // Reservation used by the user for a transaction

        CancelWaiting,
        Cancelled, // Reservation cancelled by the user
        CancelRejected, // Charge point refuses to cancel reservation

        Faulted, // Reservation has not been made, because connectors or specified connector are in a faulted state.
        Rejected, // Reservation has not been made. Charge Point is not configured to accept reservations.
        Occupied, // Reservation has not been made. All connectors or the specified connector are occupied
        Unavailable, // Reservation has not been made, because connectors or specified connector are in an unavailable state.
    }

}
