using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Req_ClearChargingProfile
    {
        public int id;
        public int connectorId;
        public ChargingProfilePurposeType chargingProfilePurpose;
        public int stackLevel;
    }

}
