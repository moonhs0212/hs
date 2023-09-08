using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_DC_Charger.common.interf.uiux
{
    public interface IUC_Channel_Button : IUC_Channel
    {
        void setOnClickListener(IOnClickListener_Button listener);
    }
}
