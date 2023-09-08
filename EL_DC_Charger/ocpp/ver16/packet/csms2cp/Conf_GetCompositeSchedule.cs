using EL_DC_Charger.common.item;
using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Conf_GetCompositeSchedule
    {
        public GetCompositeScheduleStatus status;//: GetCompositeScheduleStatus,
        public int? connectorId;//: Int?,
        public String scheduleStart;//: LocalDateTime?,
        public ChargingSchedule chargingSchedule;//: ChargingSchedule?
    }

}
