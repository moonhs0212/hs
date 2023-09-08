using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface ICT_Manager : IEL_Object_Channel_Base
    {
        float getCurrent();
        void setStandard_OverCurrent(float standard_overcurrent);
        bool isError_CT();
        String isError_CT_String();
        bool isError_OverCurrent();
    }
}
