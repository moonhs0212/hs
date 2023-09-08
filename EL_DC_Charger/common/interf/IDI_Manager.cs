using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IDI_Manager : IEL_Object_Channel_Base
    {
        bool isEmergencyPushed();
        bool isUse_DoorLock();
        void command_openDoorLock(int index);
        bool isOpenDoorLock(int index);
    }
}
