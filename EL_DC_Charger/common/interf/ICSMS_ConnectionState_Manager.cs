using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface ICSMS_ConnectionState_Manager
    {
        bool isEnable_LocalMode();
        bool isLocalMode();
        bool isConnectedServer();
    }
}
