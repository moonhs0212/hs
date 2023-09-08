using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IKeypad_EventListener
    {

        void onEnterComplete(object obj, int type, String title, String content, String afterContent);
        void onCancel(object obj, int type, String title, String content);
    }
}
