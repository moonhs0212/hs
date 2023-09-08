using EL_DC_Charger.common.interf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.Interface_Common
{
    public interface IRFCardReader_Manager : IDevice_Manager
    {
        void setCommand_Search_RFCard();
        

        void setRFCardReader_Listener(IRFCardReader_EventListener listener);
    }
}
