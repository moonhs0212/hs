using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IPT_Manager : IEL_Object_Channel_Base
    {
        float getVoltage();
        void setStandard_OverVoltage(float standard_Voltage);
        void setStandard_LowVoltage(float standard_Voltage);
        bool isError_PT();
        String isError_PT_String();
        bool isError_OverVoltage();
        bool isError_LowVoltage();
    }
}
