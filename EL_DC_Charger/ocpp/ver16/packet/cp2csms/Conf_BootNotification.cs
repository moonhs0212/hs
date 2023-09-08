using EL_DC_Charger.common.item;
using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.cp2csms
{
    public class Conf_BootNotification
    {

        public String currentTime;//: LocalDateTime,
        public int? interval;//: Int,
        public RegistrationStatus status;//: RegistrationStatus,

    }
}
