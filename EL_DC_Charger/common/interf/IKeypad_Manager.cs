using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IKeypad_Manager : IEL_Object_Channel_Base
    {
        void setVariable(object obj, int type, int lengthMax, int lengthMin, String title, String content, String contentDescript);
        void setKeypad_EventListener(IKeypad_EventListener eventListener);
    }
}
