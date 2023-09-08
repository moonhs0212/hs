using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IAMI_Manager : IEL_Object_Channel_Base
    {
        bool isError_By_Overcurrent();

        bool isError_AMIComm();

        bool isIncreaseStop_ChargingWattage();
        void setAmiType(int type);


        float getPositive_Active_Energy_Pluswh();

        float getVoltage_Phase(int phase);
        float getCurrent_Phase(int phase);

        void setVoltage_Phase(int phase, float value);
        void setCurrent_Phase(int phase, float value);

        float getVoltage();
        float getCurrent();
    }
}
