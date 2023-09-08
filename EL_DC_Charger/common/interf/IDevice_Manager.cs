using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IDevice_Manager
    {
        void setCommand_Ready();
        void setCommand_Reset();

        void setCommand_DeviceCheck();
    }
}
