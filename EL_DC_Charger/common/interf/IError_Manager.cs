using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IError_Manager : IEL_Object_Channel_Base
    {
        int getErrorCode();
        String getErrorCode_String();

        bool isErrorOccured_Need_Reboot();
        bool isErrorOccured_Need_ChargingStop();

        void setError_Listener(IError_Listener listener);
        IError_Listener getError_Listener();
    }
}
