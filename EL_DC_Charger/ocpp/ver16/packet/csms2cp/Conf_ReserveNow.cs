using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.packet.csms2cp
{
    public class Conf_ReserveNow
    {
        public ReservationStatus status;//: String // 예약의 응답값에서는 문자열로만 확인함
    }

}
