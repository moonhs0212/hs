using EL_DC_Charger.ocpp.ver16.packet.csms2cp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.interf
{
    public interface IOCPP_ConfChangeConfiguration_Listener
    {
        void receive_ConfChangeConfiguration(Req_ChangeConfiguration data, Conf_ChangeConfiguration data_Result);
    }
}
