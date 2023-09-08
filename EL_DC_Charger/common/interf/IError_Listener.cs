using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IError_Listener
    {
        void onError(int errorCode, String errorCode_String);
    }
}
