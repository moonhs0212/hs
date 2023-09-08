using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.ocpp.ver16.interf
{
    public interface IOCPP_ConfStartTransaction_Listener
    {
        void onReceive_ConfStartTransaction(Conf_StartTransaction conf);
    }
}
