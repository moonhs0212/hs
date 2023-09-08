using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf
{
    public interface IOnClickListener_Button
    {
        void onClick_Confirm(object sender);
        void onClick_Cancel(object sender);
    }
}
