using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Conf_GetConfiguration
    {
        public List<KeyValue> configurationKey;//: KeyValue?,
        public List<String> unknownKey;//: String?
    }
}
