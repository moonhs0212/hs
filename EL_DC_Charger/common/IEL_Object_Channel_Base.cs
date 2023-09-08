using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common
{
    public interface IEL_Object_Channel_Base
    {
        int getChannelIndex();
        void setChannelIndex(int channelIndex);
    }
}
